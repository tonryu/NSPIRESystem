   M S B A M L   `   `   `  ����C  ?WPFSpark, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:WPFSpark;assembly=WPFSparkWPFSpark  5       NSPIREIncSystem3   -NSPIREIncSystem.LeadManagement.Views.LeadForm   5      a ]DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      7WPFSpark(clr-namespace:WPFSpark;assembly=WPFSpark  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5	      -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5      
�������$��Arial)�5   f   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $
��Lead��6   $��0��6    $��10q�6-   $
��Bold"�69   ���#0069ff$��25(�6`   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      �� 5"      $
��Auto �6    .+�5$      �� -   5         ��Name$ 	lblLeadId��5$      $��	LEAD ID #��6+   ���#0069ff$
��20 5q�6Q    �� Padding$	 0 5q�6_   $��Center=�6m   %  DevExpress.Xpf.Editors.TextEdit  5%      -   5          Name$ 	txtLeadId��5%      $��1��6.   $	��0 5q�6>   $��5q�6L   $	��243��6W   $
��Left=�6c   %  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly . 6~   �� 5&      -   5        $ lblCompanyName��5&      $��COMPANY NAME *��60   $��1��6F   ���#0069ff$
��20 5q�6h   $	 0 5q�6v   $��Center=�6�     5'      -   5        $ txtCompanyName��5'         NullText$ Full Company Name��63   $��1��6P   $��1��6]   $	��0 5q�6m   $��5q�6{   $
��Left=�6�   $	��243��6�   �� 5(      -   5        $ 
lblAddress��5(      $��	ADDRESS *��6,   $��2��6=   ���#0069ff$
��20 5q�6_   $	 0 5q�6m   $��Center=�6{     5)      -   5        $ txtCompanyAddress��5)      $' !Street/No., Municipal/City, State��66   $��2��6c   $��1��6p   $	��0 5q�6�   $��5q�6�   $	��Top=�6�   $
��Left=�6�   $	��243��6�   �� 5*      -   5        $ lblTerritory��5*      $��TERRITORY *��6.   $��3��6A   ���#0069ff$
��20 5q�6c   $	 0 5q�6q   $��Center=�6   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5+      -	   5          Name$ cbTerritory��5+      $��3��64   $��1��6A   $	��0 5q�6Q   $��5q�6_   $
��Left=�6j   $	��243��6�   '  !DevExpress.Xpf.Editors.ButtonEdit	   IsTextEditable$	 FalseX�6�   �� 5,      -
   5        $ lblSalesStageStatus��5,      $��SALES STAGE STATUS *��65   $��4��6Q   ���#0069ff$
��20 5q�6s   $	 0 5q�6�   $��Center=�6�     5-      -   5        $ cbStatus��5-      $��4��61   $��1��6>   $	��0 5q�6N   $��5q�6\   $
��Left=�6g   $	��243��6�   $	 FalseX�6�   �� 5.      -   5        $ lblMarketingStrategy��5.      $��MARKETING STRATEGY *��66   $��5��6R   ���#0069ff$
��20 5q�6t   $	 0 5q�6�   $��Center=�6�     5/      -   5        $ cbMarketingStrategy��5/      $��5��6<   $��1��6I   $	��0 5q�6Y   $��5q�6g   $
��Left=�6r   $	��243��6�   $	 FalseX�6�   �� 50      -   5        $ 
lblProduct��50      $��PRODUCT��6,   $��6��6;   ���#0069ff$
��20 5q�6]   $	 0 5q�6k   $��Center=�6y   � 51      -   5        
 �Name$
 gridProduct��51      $��6��6(   $��1��65   $��5q�6E   $
��Left=�6P   $	��243��6k    52      �� 53      �� 54      $
��Auto �6'   .+�56      (  "DevExpress.Xpf.Editors.ListBoxEdit  -   5          Name$ lbLeadsProducts��56   "   $��1q�6;   $��90��6F   $
��Auto=�6R   $
��Auto=�6�   �� 57      -   5         ��Name$ 	btnSelect��57      $��1q�6,   $��1��67      ButtonStyle1#���   6G    � ToolTip$ Select product��6n   $��30��6�   $��30��6�   $��Bottom=�6�   ��58      �� 59       �� ImageSource$ /new images/05.jpg��6%   5;      5<      �� 5=      -   5        $ lblActiveCheck��5=      $��IS ACTIVE? *��60   $��7��6D   ���#0069ff$
��20 5q�6f   $	 0 5q�6t   $��Center=�6�      WPFSpark.ToggleSwitch  5>      -   5          Name$ tsActiveCheck��5>   $   $��7��5?   $   $��1��5@   $   $��5q�5A   $   $	��0 5q�5B   $   $
��Left=�5C   $   
�������$��2q�5E   $      CheckedBackground
 �����   CheckedForeground
 �   �   CheckedText$	 YES��5H   $   $��Arial)�5I   $   $��12(�5J   $   $��Normal"�5K   $   $��32��5L   $      ThumbBackground �#0069ff   ThumbBorderBrush
 �����   ThumbCornerRadius$ 0��5O   $      ThumbGlowColor$ Silver��5P   $      
ThumbWidth$ 20Q�5Q   $      UncheckedBackground
 �����   UncheckedForeground
 �   �   UncheckedText$ NO��5T   $   $	��160��5U   $   5V      �� 5X   
   -   5        $ btnSave��5X      $��2��6"    � Cursor$
 Hand��6/   $ Save changes��6=   ���#0069ff#���   6j   $��5q�6�   $��50��6�   $��50��6�   ��5Y      �� 5Z      $ /images/Save.png��6!   5\      5]      5^      