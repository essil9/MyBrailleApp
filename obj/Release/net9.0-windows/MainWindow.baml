   M S B A M L   `   `   `  ����   MyBrailleApp%    BrailleTranslatorApp.MainWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Braille Translator��$	��450��$	��800��.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��* �.+��� $��0��$��0��$��Enter Text:��$��Center=��� $��0��$��1��$��Braille Output:��$��Center=��� -    ��Name$ 	textInput��$��1��$��0�� �� AcceptsReturn$
 True��" �� VerticalScrollBarVisibility$
 Auto=��� -   $ brailleOutput��$��1��$��1��$
 True�� �� 
IsReadOnly$
 True���� $��2��$��0��$��2��$��Select Translation Table:��$��Center=��� -    ��Name$ tableComboBox��$��3��$��0��$��2��