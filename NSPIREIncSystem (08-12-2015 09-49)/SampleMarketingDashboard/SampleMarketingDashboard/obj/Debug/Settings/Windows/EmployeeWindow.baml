   M S B A M L   `   `   `  ����  NSPIREIncSystem5    /NSPIREIncSystem.Settings.Windows.EmployeeWindow   5   	   a ]DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  DeepBlue 5   	    � Title$ Employee Window��5   	   $	��490��6!   $	��800��6.    � WindowStyle$
 None=�6:   
������� � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�66   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ lblEmployee��5      $��Employee��6)   $��0��69   $��10 40 10 10q�6F   $��Segoe UI)�6[   $
��Bold"�6q   
�������$��25(�6�   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5!      �� $��EMPLOYEE ID #��6   
�������$
��20 5q�6@    �� Padding$	 0 5q�6N   $��Center=�6\   %  DevExpress.Xpf.Editors.TextEdit  5"      -   5        	  Name$	 txtEmployeeId��5"      $��1��62   $	��0 5q�6B   $��5q�6P   $	��243��6[   $
��Left=�6g   �� 5#      $��EMPLOYEE NAME *��6   $��1��6/   
�������$
��20 5q�6O   $	 0 5q�6]   $��Center=�6k   � 5$      $��1��6   $��1��6     5%      �� 5&      �� 5'      �� 5(      .+�5*        -   5        $	 txtFirstName��5*      $	��0 5q�65   $��5q�6C   %  DevExpress.Xpf.Editors.BaseEdit
   NullText$
 
First Name��6N     5+      -   5        $	 txtMiddleName��5+      $��1��66   $	��0 5q�6F   $��5q�6T   $
 Middle Name��6_     5,      -   5        $	 txtLastName��5,      $��2��64   $	��0 5q�6D   $��5q�6R   $
 	Last Name��6]   5-      �� 5.      $��ADDRESS��6   $��2��6'   
�������$
��20 5q�6G   $	 0 5q�6U   $��Center=�6c     5/      -   5        $	 
txtAddress��5/      $��2��6/   $��1��6<   $'
 !No./Street, Municipal/City, State��6L   $	��0 5q�6y   $��5q�6�   )  #DevExpress.Xpf.Editors.TextEditBase   TextWrapping$
 Wrap=�6�   $
��Left=�6�   $	��500��6�   �� 50      $��	TERRITORY��6   $��3��6)   
�������$
��20 5q�6I   $	 0 5q�6W   $��Center=�6e   �� 51      -   5         ��Name$ cbTerritory��51      $��1��6,   $��3��6<   $	��0 5q�6I   $��5q�6W   $
��Left=�6b   $	��243��6}    �� 
IsReadOnly . .@�52      �� $��Bataan��6   53      �� 54      $��E-MAIL ADDRESS��6   $��4��6.   
�������$
��20 5q�6N   $	 0 5q�6\   $��Center=�6j     55      -	   5        $	 txtEmailAddress��55      $
 example@yahoo.com��64   $��4��6Q   $��1��6^   $	��0 5q�6n   $��5q�6|   $
��Left=�6�   $	��300��6�   �� 56      $��PHONE #��6   $��5��6'   
�������$
��20 5q�6G   $	 0 5q�6U   $��Center=�6c     57      -
   5        $	 
txtPhoneNo��57      $��5��6/   $��1��6<   $	��0 5q�6L   $��5q�6Z   $
��Left=�6e   $	��243��6�   �� 58      $��FAX #��6   $��6��6%   
�������$
��20 5q�6E   $	 0 5q�6S   $��Center=�6a     59      -   5        $	 txtFaxNo��59      $��6��6-   $��1��6:   $	��0 5q�6J   $��5q�6X   $
��Left=�6c   $	��243��6~   �� 5:      $��POSITION��6   $��7��6(   
�������$
��20 5q�6H   $	 0 5q�6V   $��Center=�6d     5;      -   5        $	 txtPosition��5;      $��7��60   $��1��6=   $	��0 5q�6M   $��5q�6[   $
��Left=�6f   $	��243��6�   5<      �� 5>   
    �� Orientation$ 
Horizontal=�6   $��2��6.   .+�5?      �� -   5         ��Name$ btnSave��5?       � Cursor$
 Hand��6&    � ToolTip$ Save changes��64   
�������   ButtonStyle1#���   6_   $��5q�6�   $��50��6�   $��50��6�   ��5@      �� 5A       �� ImageSource$ /images/Save2.png��6!   5C      �� 5D      -   5        $ btnClose��5D      $
 Hand��6'   $ Close��65   
�������#���   6Y   $��5q�6�   $��50��6�   $��50��6�   ��5E      �� 5F      $ /images/Close.png��6!   5H      5I      5J      5K      