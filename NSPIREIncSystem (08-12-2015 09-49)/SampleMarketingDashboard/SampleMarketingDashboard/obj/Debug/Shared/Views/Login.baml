   M S B A M L   `   `   `  ����  NSPIREIncSystem(    "NSPIREIncSystem.Shared.Views.Login   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      a ]DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a<dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      X TXceed.Wpf.Toolkit, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5	      -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5
      $��Arial)�5   T   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $	��200 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $	��400 �6   �� 5      .+�5   
   �� $��1��6   $��1��6    �� CornerRadius$ 4��6.   
�������$��3q�6R   ._�5      � 
������� 5      �� 5      �� 5      �� 5       5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      .+�5%      �� $��/images/User-Profile.png��6   $	��160��6:   $	��160��6G   $��1��6S   $��3��6`   $��Center=�6q   $
��Left=�6�   %  DevExpress.Xpf.Editors.TextEdit  5'      -   5          Name$ txtUsername��5'      $
��Left=�64   $	��200��6O   $��2��6[   $��1��6o   $��1��6|   $��3q�6�   $��Center=�6�   
��� � �$��5q�6�   %  DevExpress.Xpf.Editors.BaseEdit   NullText$ Username��6�   ,  &DevExpress.Xpf.Editors.PasswordBoxEdit  5(      -   5          Name$ txtPassword��5(   &   $
��Left=�6;   $	��200��6V   $��2��6b   $��2��6o   $��1��6�   $��3q�6�   $��Center=�6�   
��� � �$��5q�6�   $ 
  Password��6�   �� 5)      $��1��6   $��3��6-   $��2��6:    �� Orientation$ 
Horizontal=�6N   .+�5*      �� -   5         ��Name$ btnOK��5*      	 � Cursor$
	 Hand��6,      ButtonMetroStyle1#���   6:   
�������$��OK��6x   $��97��6�   $��3��6�   $��1��6�   $��3q�6�   
�������
 �� 	IsDefault
 . 6�   �� 5+      -   5        $ btnExit��5+      $
	 Hand��6.   #���   6<   
�������$
��EXIT��6z   $��97��6�   $��3��6�   $��2��6�   
�������$��3q�6�   6�   5,      %  Xceed.Wpf.Toolkit.BusyIndicator  5-      -   5        $��busyIndicator��5-   %   $��Center=�5.   %   $��Bottom=�6B   $��1��5/   %   $��3��62   $��116,0,116,13q�6F   $	��162��50   %   $��68��61   $��3��6=   $
��Bold"�6N   
���   �51   %   52      53      54      55      