﻿using OpenAI_API;
using OpenAI_API.Audio;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static OpenAI_API.Audio.TextToSpeechRequest;

namespace AIdeaGPTWritiingPrompter
{
    public partial class MainWindow : Window
    {
        private List<string> genreSuggestionList;
        private List<string> tagSuggestionList;
        private ObservableCollection<string> addedGenres = new ObservableCollection<string>();
        private ObservableCollection<string> addedTags = new ObservableCollection<string>();

        private static string apiKey = "KEY_GOES_HERE";
        private OpenAIAPI api = new OpenAIAPI(apiKey);

        private string genresFilePath = "genres.csv";
        private string tagsFilePath = "tags.csv";

        public MainWindow()
        {
            InitializeComponent();
            LoadSuggestions();

            genreInput.TextChanged += GenreInput_TextChanged;
            genreSuggestions.SelectionChanged += GenreSuggestions_SelectionChanged;

            tagInput.TextChanged += TagInput_TextChanged;
            tagSuggestions.SelectionChanged += TagSuggestions_SelectionChanged;

            genreInput.KeyDown += GenreInput_KeyDown;
            tagInput.KeyDown += TagInput_KeyDown;

            addedGenresDisplay.ItemsSource = addedGenres;
            addedTagsDisplay.ItemsSource = addedTags;
        }

        private void LoadSuggestions()
        {
            genreSuggestionList = LoadFromFile(genresFilePath);
            tagSuggestionList = LoadFromFile(tagsFilePath);
        }

        private List<string> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {

                MessageBox.Show("Before you begin generating prompts, please select the CSV files that contain Genre and Tag options. If you cancel, you will have to manually enter them in and create your lists from scratch.", "Error: CSV files were not found in the program root directory.", MessageBoxButton.OK, MessageBoxImage.Information);

                filePath = SelectFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", $"Select {Path.GetFileNameWithoutExtension(filePath)} CSV File");
            }

            return !string.IsNullOrEmpty(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();
        }

        private string SelectFile(string filter, string title)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = filter,
                Title = title
            };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        private void GenreInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSuggestions(genreInput.Text, genreSuggestionList, genreSuggestions);
        }

        private void TagInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSuggestions(tagInput.Text, tagSuggestionList, tagSuggestions);
        }

        private void UpdateSuggestions(string query, List<string> suggestions, ListBox suggestionsListBox)
        {
            query = query.Trim().ToLower();
            if (!string.IsNullOrEmpty(query))
            {
                var filteredSuggestions = suggestions
                    .Where(s => s.ToLower().Contains(query))
                    .OrderBy(s => s)
                    .ToList();

                suggestionsListBox.ItemsSource = filteredSuggestions;
                suggestionsListBox.Visibility = filteredSuggestions.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                suggestionsListBox.ItemsSource = null;
                suggestionsListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void GenreSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (genreSuggestions.SelectedItem != null)
            {
                var selectedGenre = genreSuggestions.SelectedItem.ToString();
                AddGenre(selectedGenre);
                genreInput.Clear();
                genreSuggestions.ItemsSource = null;
            }
        }

        private void TagSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tagSuggestions.SelectedItem != null)
            {
                var selectedTag = tagSuggestions.SelectedItem.ToString();
                AddTag(selectedTag);
                tagInput.Clear();
                tagSuggestions.ItemsSource = null;
            }
        }

        private void AddGenre(string genre)
        {
            if (!addedGenres.Contains(genre))
            {
                addedGenres.Add(genre);
                if (!genreSuggestionList.Contains(genre))
                {
                    genreSuggestionList.Add(genre);
                    File.AppendAllText(genresFilePath, Environment.NewLine + genre);
                }
            }
        }

        private void AddTag(string tag)
        {
            if (!addedTags.Contains(tag))
            {
                addedTags.Add(tag);
                if (!tagSuggestionList.Contains(tag))
                {
                    tagSuggestionList.Add(tag);
                    File.AppendAllText(tagsFilePath, Environment.NewLine + tag);
                }
            }
        }

        private void RemoveGenre(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var genre = (string)button.DataContext;
            addedGenres.Remove(genre);
        }

        private void RemoveTag(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tag = (string)button.DataContext;
            addedTags.Remove(tag);
        }

        private async void GeneratePrompt(object sender, RoutedEventArgs e)
        {
            if (!addedGenres.Any()) return;

            string complexity = ((ComboBoxItem)complexitySelector.SelectedItem).Content.ToString();
            string type = oneShotOption.IsChecked == true ? "One Shot Story" : "Ongoing Series";
            string demographic = ((ComboBoxItem)demographicSelector.SelectedItem).Content.ToString();
            string contentRating = ((ComboBoxItem)contentRatingSelector.SelectedItem).Content.ToString();
            string storyLength = ((ComboBoxItem)storyLengthSelector.SelectedItem).Content.ToString();
            string writingStyle = ((ComboBoxItem)writingStyleSelector.SelectedItem).Content.ToString();
            string pov = ((ComboBoxItem)povSelector.SelectedItem).Content.ToString();
            string timePeriod = ((ComboBoxItem)timePeriodSelector.SelectedItem).Content.ToString();
            string setting = settingInput.Text;
            string characterTraits = characterTraitsInput.Text;

            string prompt = $@"You are a prompt generation robot. You do not express your own thoughts and opinions. You exist to help authors come up with creative works. You do not generate stories yourself. You only generate writing prompts for the author to be inspired by. 
Please create a writing prompt with the following specifications:

- Type: {type}
- Genres: {string.Join(", ", addedGenres)}
- Complexity: {complexity}
- Target Demographic: {demographic}
- Content Rating: {contentRating}
- Story Length: {storyLength}
- Writing Style: {writingStyle}
- Point of View: {pov}
- Time Period: {timePeriod}
- Setting: {setting}
- Main Character Traits: {characterTraits}

The following fiction tags must be considered for the story: {string.Join(", ", addedTags)}. You need to mention how each tag will affect or should be implemented in the story.

The prompt should be engaging, creative, and thought-provoking. Provide a brief synopsis of the story idea, including key plot points, character motivations, and potential conflicts. Ensure that all specified elements are incorporated into the prompt in a cohesive and engaging manner.";

            Storyboard loadingAnimation = (Storyboard)FindResource("LoadingAnimation");
            loadingAnimation.Begin(GeneratePromptButton, true);

            try
            {
                GeneratePromptButton.IsEnabled = false;

                string generatedPrompt = await GetGptPrompt(prompt);

                outputDisplay.Text = generatedPrompt;
                ReadPromptButton.IsEnabled = true;
            }
            finally
            {
                loadingAnimation.Stop(GeneratePromptButton);
                GeneratePromptButton.IsEnabled = true;
            }
        }

        private async Task<string> GetGptPrompt(string prompt)
        {
            try
            {
                var chat = api.Chat.CreateConversation();
                chat.Model = "gpt-4o-mini";
                chat.AppendUserInput(prompt);
                var response = await chat.GetResponseFromChatbotAsync();
                return response.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddCustomItem(TextBox inputBox, ObservableCollection<string> collection, List<string> suggestionList, string filePath)
        {
            string item = inputBox.Text.Trim();
            if (!string.IsNullOrEmpty(item))
            {
                if (!collection.Contains(item))
                {
                    collection.Add(item);
                    if (!suggestionList.Contains(item))
                    {
                        suggestionList.Add(item);
                        File.AppendAllText(filePath, Environment.NewLine + item);
                    }
                }
                inputBox.Clear();
            }
        }

        private void GenreInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                AddCustomItem(genreInput, addedGenres, genreSuggestionList, genresFilePath);
            }
        }

        private void TagInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                AddCustomItem(tagInput, addedTags, tagSuggestionList, tagsFilePath);
            }
        }

        private async void ReadPrompt(object sender, RoutedEventArgs e)
        {
            string promptText = outputDisplay.Text;
            if (string.IsNullOrWhiteSpace(promptText))
            {
                MessageBox.Show("No prompt to read.");
                return;
            }

            Storyboard loadingAnimation = (Storyboard)FindResource("LoadingAnimation");
            loadingAnimation.Begin(ReadPromptButton, true);

            try
            {
                ReadPromptButton.IsEnabled = false;

                var request = new TextToSpeechRequest()
                {
                    Input = promptText,
                    Model = Model.TTS_HD,
                    Voice = Voices.Onyx,
                    ResponseFormat = ResponseFormats.MP3,
                    Speed = 1.0
                };

                using (var result = await api.TextToSpeech.GetSpeechAsStreamAsync(request))
                using (var fileStream = new FileStream("prompt_audio.mp3", FileMode.Create, FileAccess.Write))
                {
                    await result.CopyToAsync(fileStream);
                }

                audioPlayer.Source = new Uri(Path.GetFullPath("prompt_audio.mp3"), UriKind.Absolute);
                audioPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating audio: {ex.Message}");
            }
            finally
            {
                loadingAnimation.Stop(ReadPromptButton);
                ReadPromptButton.IsEnabled = true;
            }
        }
    }
}