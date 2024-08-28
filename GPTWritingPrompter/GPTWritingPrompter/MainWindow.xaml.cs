using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using OpenAI_API;
using OpenAI_API.Chat;

namespace GPTWritingPrompter
{
    public partial class MainWindow : Window
    {
        private List<string> tagSuggestions;
        private List<string> genreSuggestions;
        private List<string> addedTags = new List<string>();
        private List<string> addedGenres = new List<string>();

        private string genresFilePath = "genres.csv";
        private string tagsFilePath = "tags.csv";

        public MainWindow()
        {
            InitializeComponent();
            LoadGenres();
            LoadTags();

            tagSearchTextBox.TextChanged += TagSearchTextBox_TextChanged;
            tagSuggestionsListBox.SelectionChanged += TagSuggestionsListBox_SelectionChanged;

            genreSearchTextBox.TextChanged += GenreSearchTextBox_TextChanged;
            genreSuggestionsListBox.SelectionChanged += GenreSuggestionsListBox_SelectionChanged;
        }

        private void LoadGenres()
        {
            if (!File.Exists(genresFilePath))
            {
                genresFilePath = SelectFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Select Genre CSV File");
            }

            if (!string.IsNullOrEmpty(genresFilePath))
            {
                genreSuggestions = File.ReadAllLines(genresFilePath).ToList();
            }
        }

        private void LoadTags()
        {
            if (!File.Exists(tagsFilePath))
            {
                tagsFilePath = SelectFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Select Tag CSV File");
            }

            if (!string.IsNullOrEmpty(tagsFilePath))
            {
                tagSuggestions = File.ReadAllLines(tagsFilePath).ToList();
            }
        }

        private string SelectFile(string filter, string title)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Title = title
            };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void TagSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = tagSearchTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(query))
            {
                var filteredTags = tagSuggestions
                    .Where(tag => tag.Contains(query))
                    .ToList();
                tagSuggestionsListBox.ItemsSource = filteredTags;
                tagSuggestionsListBox.Visibility = filteredTags.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                tagSuggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void TagSuggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tagSuggestionsListBox.SelectedItem != null)
            {
                var selectedTag = tagSuggestionsListBox.SelectedItem.ToString();
                AddTag(selectedTag);
                tagSearchTextBox.Clear();
                tagSuggestionsListBox.ItemsSource = null;
            }
        }

        private void AddTag(string tag)
        {
            if (!addedTags.Contains(tag))
            {
                addedTags.Add(tag);
                var border = new Border
                {
                    Background = new SolidColorBrush(Colors.LightGray),
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(5),
                    Margin = new Thickness(5)
                };
                var tagBlock = new TextBlock
                {
                    Text = $"#{tag}"
                };
                border.Child = tagBlock;
                tagsWrapPanel.Children.Add(border);
            }
        }

        private void GenreSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = genreSearchTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(query))
            {
                var filteredGenres = genreSuggestions
                    .Where(genre => genre.Contains(query))
                    .ToList();
                genreSuggestionsListBox.ItemsSource = filteredGenres;
                genreSuggestionsListBox.Visibility = filteredGenres.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                genreSuggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void GenreSuggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (genreSuggestionsListBox.SelectedItem != null)
            {
                var selectedGenre = genreSuggestionsListBox.SelectedItem.ToString();
                AddGenre(selectedGenre);
                genreSearchTextBox.Clear();
                genreSuggestionsListBox.ItemsSource = null;
            }
        }

        private void AddGenre(string genre)
        {
            if (!addedGenres.Contains(genre))
            {
                addedGenres.Add(genre);
                var border = new Border
                {
                    Background = new SolidColorBrush(Colors.LightGray),
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(5),
                    Margin = new Thickness(5)
                };
                var genreBlock = new TextBlock
                {
                    Text = $"#{genre}"
                };
                border.Child = genreBlock;
                genresWrapPanel.Children.Add(border);
            }
        }

        private async void GeneratePrompt_Click(object sender, RoutedEventArgs e)
        {
            if (!addedGenres.Any()) return;
            string complexity = ((ComboBoxItem)complexityComboBox.SelectedItem).Content.ToString();
            string type = oneShotRadioButton.IsChecked == true ? "One Shot Story" : "Ongoing Series";

            // Create the super prompt
            string superPrompt = $"You are a prompt generation robot. You do not express you own thoughts and opinions. You exist to help authors come up with creative works. You do not generate stories yourself. You only generate writing prompts for the author to be ispired by. Please create a writing prompt that is a {type}. It should include the genres: {string.Join(", ", addedGenres)} and have a complexity of {complexity}. The following fiction tags must be considered for the story: {string.Join(", ", addedTags)}. You need to mention how each tag will affect or should be implemented in the story.The prompt should be engaging, creative, and thought-provoking.";

            // Call OpenAI GPT-3.5/4 API
            string prompt = await GetGpt4oMiniPromptAsync(superPrompt);

            outputTextBox.Text = prompt;
        }

        private async Task<string> GetGpt4oMiniPromptAsync(string superPrompt)
        {


            var apiKey = ("KEY GOES HERE");
            OpenAIAPI _client = new OpenAIAPI(apiKey);

            try
            {
                var chat = _client.Chat.CreateConversation();
                chat.Model = "gpt-4o-mini";
                chat.AppendUserInput(superPrompt);
                var response = await chat.GetResponseFromChatbotAsync();
                return response.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}