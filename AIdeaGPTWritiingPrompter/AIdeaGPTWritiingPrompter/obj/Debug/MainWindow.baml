   M S B A M L   `   `   `  ����  AIdeaGPTWritiingPrompter)    #AIdeaGPTWritiingPrompter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$'  !Ai-dea : Writing Prompt Generator��5   	   $	��800��5   	   $
��1200��5   	   ���#F0F0F0 � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ 	CanResize=�5	   	    � WindowStyle$ SingleBorderWindow=�5
   	    � 	Resources 5         LoadingAnimation �� TargetProperty W� From	 W� To g� Duration	 g� RepeatBehavior  ModernTextBox
 �� 
TargetType �� Property �� Value  ModernComboBox  ModernButton  ModernRadioButton  RemoveButtonStyle �� 
TargetType��%�  &	        &	 �     &	 �    &	 �    &	 �    &	 %    �� 5   
   .��5      W� $9 3(UIElement.RenderTransform).(RotateTransform.Angle)�6   $ 0X�5      $	 360X�5      $ 0:0:1D�5      $	 Forever��5      5      �� 5   
   "
 ��5      .�5      ��  � ��$
 10,5q�5      �� 5       � ��$ 2q�5      �� 5       � �� �#CCCCCC5      �� 5       � �� �#FFFFFF5      �� 5       � ��$ 14(�5      5      �� 5!   
   "
 ��5"      .�5#      ��  � ��$
 10,5q�5$      �� 5%       � ��$ 0q�5&      �� 5'       � �� �#FFFFFF5(      �� 5)       � ��$ 14(�5*      5+      �� 5-   
   "
 ��5.      .�5/      ��  � ��$ 15,10q�50      �� 51       � �� �#4CAF5052      �� 53       � ��
 �����54      �� 55       � ��$ 0q�56      �� 57       � ��$ 14(�58      �� 59       � ��$
 Bold"�5:      5;      �� 5=   
   "
 	�5>      .�5?      ��  � ��$ 0,5,10,5q�5@      �� 5A       � ��$ 14(�5B      5C      �� 5E   
   "
 ��5F      .�5G      ��  � ��
 ���� 5H      �� 5I       � ��
 ���� 5J      �� 5K       � �� �#FF41365L      �� 5M       � ��$
 Bold"�5N      �� 5O       � ��$ 5,0,0,0q�5P      �� 5Q       � ��$ 3q�5R      �� 5S       � ��$ Center=�5T      �� 5U       � �� 5V      �� 5W      " ��6&   .R�5X      �� #��z��6!   #��z��5Y   !   #��z��5Z   !   ._�5[      �� $��Center=�6/   $��Center=�5\   /   5]      5^      5`      5a      5b      .��5c      �� ���#FFFFFF �� CornerRadius$ 10��5d      $��10q�5e      ._�5f   
   � $��20q�6    � ColumnDefinitions 5g      �� 5h      $��1* �6#   �� 5i      $��2* �6#   .+�5l      �� $��0��6   $��0,0,20,0q�5m      .+�5n      �� $��Genre��6   $
��Bold"�5o      $��0,0,0,5q�5p      �� 5q      -   5         ��Name$ 
genreInput��5q      #��[ 5r      $��0,0,0,10q�5s      �� 5t      -   5         ��Name$ genreSuggestions��5t      $	��100��5u      $y�	Collapsed=�5v      $��0,0,0,10q�5w      �� 5x      $��Added Genres��6   $
��Bold"�5y      $��0,0,0,5q�5z      �� 5{      $��80��6   $
��Auto=�5|      $��0,0,0,20q�5}      .��5~      �� -   5         ��Name$ addedGenresDisplay��5~   #   ��5      �� 5�      .R�5�   "   � 5�      ��5�      �� 5�      .R�5�   "   �� ���#E0E0E0$ 15��5�   )    �� Padding$
 10,5q�5�   )   $��0,5,5,0q�5�   )   ._�5�   &   ��  �� Orientation$ 
Horizontal=�61   .+�5�   *   �� $��Center=�5�   4   ��5�   4   �� 5�   4   �� 5�   *   -   5        $��×��5�   1   #��[ 5�   1   5�   1   5�   '   5�   #   5�      5�      5�      �� 5�      $
��Tags��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      -   5        $ tagInput��5�      #��[ 5�      $��0,0,0,10q�5�      �� 5�      -   5        $ tagSuggestions��5�      $	��100��5�      $y�	Collapsed=�5�      $��0,0,0,10q�5�      �� 5�      $��
Added Tags��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      $��80��6   $
��Auto=�5�      .��5�      �� -   5        $ addedTagsDisplay��5�   #   ��5�      �� 5�      .R�5�   "   � 5�      ��5�      �� 5�      .R�5�   "   �� ���#E0E0E0$ 15��5�   )   $
 10,5q�5�   )   $��0,5,5,0q�5�   )   ._�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��Center=�5�   4   ��5�   4   �� 5�   4   �� 5�   *   -   5        $��×��5�   1   #��[ 5�   1   5�   1   5�   '   5�   #   5�      5�      5�      �� 5�      $��Target Demographic��6   $
��Bold"�5�      $��0,20,0,5q�5�      �� 5�      -	   5         ��Name$ demographicSelector��5�      #��[ 5�      $��0,0,0,20q�5�      .@�5�      �� $��Children��6#   �� 5�      $��Young Adult��6#   �� 5�      $��Adult��6#   �� 5�      $��Senior��6#   �� 5�      $��All Ages��6#   5�      �� 5�      $��Content Rating��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      -
   5        $ contentRatingSelector��5�      #��[ 5�      $��0,0,0,20q�5�      .@�5�      �� $��G (General Audience)��6#   �� 5�      $��PG (Parental Guidance)��6#   �� 5�      $��PG-13 (Teens)��6#   �� 5�      $��R (Restricted)��6#   �� 5�      $��NC-17 (Adults Only)��6#   5�      �� 5�      $��Story Length��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      -   5        $ storyLengthSelector��5�      #��[ 5�      $��0,0,0,20q�5�      .@�5�      �� $!��Flash Fiction (<1000 words)��6#   �� 5�      $#��Short Story (1000-7500 words)��6#   �� 5�      $"��Novelette (7500-17500 words)��6#   �� 5�      $!��Novella (17500-40000 words)��6#   �� 5�      $��Novel (40000+ words)��6#   5�      �� 5�      $��Writing Style��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      -   5        $ writingStyleSelector��5�      #��[ 5�      $��0,0,0,20q�5�      .@�5�      �� $��Descriptive��6#   �� 5�      $��	Narrative��6#   �� 5�      $��
Persuasive��6#   �� 5�      $��
Expository��6#   �� 5�      $��Poetic��6#   5�      �� 5�      $��Point of View��6   $
��Bold"�5�      $��0,0,0,5q�5�      �� 5�      -   5        $ povSelector��5�      #��[ 5�      $��0,0,0,20q�5�      .@�5�      �� $��First Person��6#   �� 5�      $��Second Person��6#   �� 5�      $��Third Person Limited��6#   �� 5�      $��Third Person Omniscient��6#   �� 5�      $��Multiple POVs��6#   5�      5�      �� 5     $��1��6   .+�5     �� $��Prompt Complexity��6   $
��Bold"�5     $��0,0,0,5q�5     �� 5     -   5        $ complexitySelector��5     #��[ 5     $��0,0,0,20q�5     .@�5     �� $��Simple��6#   �� 5	     $��Detailed��6#   �� 5
     $��
Convoluted��6#   �� 5     $��Hyperspecific��6#   5     �� 5     $
��Type��6   $
��Bold"�5     $��0,0,0,5q�5     �� 5     $ 
Horizontal=�6   $��0,0,0,20q�5     .+�5     	� -   5         	�Name$ oneShotOption��5  "   $��One Shot��5  "    	� 	GroupName$
 type��5  "    d� 	IsChecked$
 TrueY�5  "   #��[ 5  "   	� 5     -   5        $ seriesOption��5  "   $��Series��5  "   $
 type��5  "   #��[ 5  "   5     �� 5     $1��+Time Period (Chronological Setting in Time)��6   $
��Bold"�5     $��0,0,0,5q�5      �� 5!     -   5        $ timePeriodSelector��5!     #��[ 5"     $��0,0,0,20q�5#     .@�5$     �� $��Ancient��6#   �� 5%     $��Medieval��6#   �� 5&     $��Renaissance��6#   �� 5'     $��Industrial Revolution��6#   �� 5(     $��Modern��6#   �� 5)     $��Near Future��6#   �� 5*     $��
Far Future��6#   5+     �� 5-     $F��@Story Setting (Physical Setting, Like A Location or Environment)��6   $
��Bold"�5.     $��0,0,0,5q�5/     �� 50     -   5        $ settingInput��50     #��[ 51     $��0,0,0,20q�52     �� 54     $8��2Main Character Traits (Habits, Quirks, Mannerisms)��6   $
��Bold"�55     $��0,0,0,5q�56     �� 57     -   5        $ characterTraitsInput��57     #��[ 58     $��0,0,0,20q�59     � 5<      5=     �� 5>     $��1* �6+   �� 5?     $��1* �6+   .+�5A     �� -   5         ��Name$ GeneratePromptButton��5B     $��Generate Prompt��5A     #��[ 5C     $��	0,10,5,10q�5E     $��0��5F     �� 5G     -   5        $ ReadPromptButton��5G     $��Read Prompt��5H     #��[ 5I     {�.  $��	5,10,0,10q�5L     $��1��5M     5N     r� 5P     -   5        $��audioPlayer��5P      r� LoadedBehavior$ Manual=�5Q      r� UnloadedBehavior$
 Stop=�5R     �� 5S     $��Generated Prompt��6   $
��Bold"�5T     $��0,0,0,5q�5U     �� 5V     -   5        $ outputDisplay��5V     #��[ 5W     $	��265��5X      �� TextWrapping$
 Wrap=�5Y     " �� VerticalScrollBarVisibility$
 Auto=�5Z      �� 
IsReadOnly . 5[     5\     5]     5^     5_     