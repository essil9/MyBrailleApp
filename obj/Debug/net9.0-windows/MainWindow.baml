   M S B A M L   `   `   `  ����  MyBrailleApp%    BrailleTranslatorApp.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Braille Translator��5   	   $	��600��6$   $
��1000��61   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Enter Text:��6-   $��Center=�6C   �� 5   
   $��0��6   $��1��6   $��Braille Output:��6-   $��Center=�6G   �� 5   
   $��1��6   $��0��6$   $
��Auto=�64   .��5      �� -   5         ��Name$ textPagesContainer��5      5      �� 5   
   $��1��6   $��1��6$   $
��Auto=�64   .��5      �� -   5        $ braillePagesContainer��5      5      �� 5   
   $��2��6   $��0��6   $��2��6-   $��Select Translation Table:��6A   $��Center=�6e   �� 5!   
   -   5         ��Name$ tableComboBox��5!      $��3��6*   $��0��67   $��2��6G   o� 5"   	   $��0��6   $��2��6   .@�5#      m� $��Fichier��6   .@�5$   
   m� -   5        $��Nouveau Document��5$      6-   5%      5&      5(      5)      