   M S B A M L   `   `   `  ����B  >Hangman, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Hangman.ViewsHangman.Views  5      7 clr-namespace:Hangman.ViewModelsHangman.ViewModels  5           Hangman.Views.GamepageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Hangman.Views     5      0ctx clr-namespace:Hangman.ViewModels     5      
���   �  � 	Resources  5         	BoolToVis��%   &	        �� 5   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��800 �6   �� 5       � RowDefinitions 5   
   �� 5      $	��700 �6   �� 5      �� 5      .+�5   
   � $��0��6   $��0��5      $��2��5       5!      �� 5"      $	��500 �6    �� 5#      $	��300 �6    .+�5&      �� $	��500��5'      $	��500��5(      $��0��5)      $��2��5*      $��Center=�5+      $��Center=�5,      $��138,50,162,200q�6/   ��5-      �� 5.       �� ImageSource 6!   �� *SurvivorPicturePath+50      �� 52      
�������$��22(�54      $��0��55      $	��200��56      $��1��57      $��Center=�58      $��Bottom=�59      $��/Fonts/#Destroy)�5:      ��52      �� *SurvivorUsername+5:      5<      �� 5=   
    �� Orientation$ Vertical=�5>      $��1��5?      $��2��5@      $��Center=�5A      $��Center=�5B      .+�5C      �� $ 
Horizontal=�5D      .+�5E      �� $�� GAMES PLAYED:��6   $��24(�5F      $��/Fonts/#Destroy)�5G      
�������$	��300��5I      �� 5K      $��24(�5L      $��/Fonts/#Destroy)�5M      
�������$	��100��5O      ��5K      �� *GamesPlayed+5O      5Q      �� 5S      $ 
Horizontal=�5T      .+�5U      �� $�� GAMES WON:��6   $��24(�5V      $��/Fonts/#Destroy)�5W      
�������$	��240��5Y      �� 5[      $��24(�5\      $��/Fonts/#Destroy)�5]      
�������$	��100��5_      ��5[      �� *
GamesWon+5_      5a      5c      �� 5f   
   $��1��6   $��1��5g      $	��100��5h      $	��400��5i      $��Right=�5j      $��0q�5k        newGame#��[ 5l      ��5m      �� *NewGamePressedCommand+�� 5p   
   $��1��6   $��2��5q      $	��100��5r      $	��400��5s      $��Right=�5t      $	��Top=�5u      $��0q�5v        loadGame#��[ 5w      ��5x      �� *LoadGamePressedCommand+5z      5{      