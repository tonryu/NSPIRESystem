   M S B A M L   `   `   `  ����  NSPIREIncSystemS    MNSPIREIncSystem.CustomerServiceManagement.Dashboards.CustomerServiceDashboard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      f bDevExpress.Xpf.Scheduler.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxsch7http://schemas.devexpress.com/winfx/2008/xaml/scheduler  5      a ]DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      <dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  5      a ]DevExpress.Xpf.Grid.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v15.1.Core, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5	      -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5
      $��Arial)�5   T   ��5      �� 5   
    �� ImageSource$ /new images/05.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      �� 5      $
��Auto �6   .+�5   
   �� $+��%CUSTOMER SERVICE MANAGEMENT DASHBOARD��6   $��2��6A   
�������$��50 5 5 1q�6h   $��25(�6z   $
��Bold"�6�   �� 5   
   $��1��6   $��1��6!   $��2��6.   ���#0069ff$��3q�6U   
�������$��5 0 50 5q�6|   ._�5      �  5       �� 5!      $
��Auto �6$   �� 5"      .+�5$      �� $��ACTIVITY LOGS��6   $��20(�61   $��	10 10 5 5q�6?   $
��Bold"�6R   ���#0069ff
�������6y   %  DevExpress.Xpf.Grid.GridControl  5%      -   5          Name$ dcActivityLogs��5%   "   $��5 0 5 0q�6:   $��1��6K      View 5&      #  DevExpress.Xpf.Grid.TableView  5'      &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .  &   DevExpress.Xpf.Grid.DataViewBase   NavigationStyle$	 Row=�6?      AllowEditing .  6U   	   Columns	 5)      $  DevExpress.Xpf.Grid.GridColumn  5*      $  DevExpress.Xpf.Grid.BaseColumn
   Header)	  #DevExpress.Xpf.Core.ObjectConverter$
 Contact Person	 6)     5+      $
 Description	 6)     5,      $
 Activity Date	 6)     5-      $
 Activity Time	 6)     5.      $

 Cost	 6)   50      51      52      &
   DevExpress.Xpf.Core.DXTabControl
  54   
   $��50 5 5 5q�6   $��1��6-   $��2��6:   .@�55      #  DevExpress.Xpf.Core.DXTabItem  �  �DevExpress.Xpf.Core.Native.HeaderedSelectorItemBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v15.1, Version=15.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   Header$ Tickets��6   $��15(�6-    � Cursor$
 Hand��6;   .��56      � .+�57      � 
���������.  58      �� 59      �� 5:      .+�5<      �� $��2q�6#   $��Pending��6.   �� 5>      $��2q�6#   $��Resolved��6.   $��1��6A   5?      5@      5A        5B      $ Service Requests��6   $��15(�66   $
 Hand��6D   .��5C      � 
���������.  5D      �� 5E      �� 5F      �� 5G      .+�5I      �� $��2q�6   $��Pending��6*   �� 5J      $��2q�6   $��Approved��6*   $��1��6=   �� 5K      $��2q�6   $��Rejected��6*   $��2��6=   5L      5M      5N      � 5P   
   $��3��6   $��2��6   $��5q�60   $��11(�6;   .+�5Q      �� -   5         ��Name$ canvasCustomerServiceMenu��5Q      $��Bottom=�68   $��Center=�6S   .+�5R      �� -   5         ��Name$ stackServiceRequests��5R      .+�5S      �� -   5         ��Name$ btnServiceRequests��5S      $��5 5 5 0q�69   $
 Hand��6K   $��80��6Y   $��80��6e   $��3q�6p   
���   �
���   �$��Segoe UI)�6�   $��12(�6�      ButtonStyle1#���   6�   ��5T      �� 5U      $* $/new images/service-request-icon.png��6)   5W      �� 5X      .+�5Y      �� $��SERVICE��6$   $��Center=�63    �� Padding$ 5 0 5 1q�6P   �� 5Z      $��REQUESTS��6$   $��Center=�64   $ 5 0 5 1q�6Q   5[      5\      �� 5]      -   5        $ stackTickets��5]      .+�5^      �� -   5        $ 
btnTickets��5^      $��5 5 5 0q�61   $
 Hand��6C   $��80��6Q   $��80��6]   $��3q�6h   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5_      �� 5`      $! /new images/ticket-icon.png��6)   5b      �� 5c      .+�5d      �� $��TICKETS��6$   $��Center=�63   $ 5 0 5 1q�6P   �� 5e      $�� ��6$   $��Center=�6,   $ 5 0 5 1q�6I   5f      5g      �� 5h      -   5        $ stackClientAccounts��5h      .+�5i      �� -	   5        $ btnClientAccounts��5i      $��5 5 5 0q�68   $
 Hand��6J   $��80��6X   $��80��6d   $��3q�6o   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5j      �� 5k      $! /new images/client-icon.png��6)   5m      �� 5n      .+�5o      �� $��CUSTOMER��6$   $��Center=�64   $ 5 0 5 1q�6Q   �� 5p      $��ACCOUNTS��6$   $��Center=�64   $ 5 0 5 1q�6Q   5q      5r      �� 5s      -
   5        $ stackProducts��5s      .+�5t      �� -   5        $ btnProducts��5t      $��5 5 5 0q�62   $
 Hand��6D   $��80��6R   $��80��6^   $��3q�6i   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5u      �� 5v      $" /new images/product-icon.png��6)   5x      �� 5y      .+�5z      �� $��PRODUCTS��6$   $��Center=�64   $ 5 0 5 1q�6Q   �� 5{      $�� ��6$   $��Center=�6,   $ 5 0 5 1q�6I   5|      5}      �� 5~      -   5        $ stackServiceRequestForm��5~      .+�5      �� -   5        $ btnServiceRequestForm��5      $��5 5 5 0q�6<   $��3q�6N   $
 Hand��6Y   $��80��6g   $��80��6s   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5�      �� 5�      $/ )/new images/service-request-form-icon.png��6)   5�      �� 5�      .+�5�      �� $��SERVICE REQUEST��6$   $��Center=�6;   $ 5 0 5 1q�6X   �� 5�      $
��FORM��6$   $��Center=�60   $ 5 0 5 1q�6M   5�      5�      �� 5�      -   5        $ stackTicketForm��5�      .+�5�      �� -   5        $ btnTicketForm��5�      $��5 5 5 0q�64   $
 Hand��6F   $��80��6T   $��80��6`   $��3q�6k   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5�      �� 5�      $&  /new images/ticket-form-icon.png��6)   5�      �� 5�      .+�5�      �� $��TICKET��6$   $��Center=�62   $ 5 0 5 1q�6O   �� 5�      $
��FORM��6$   $��Center=�60   $ 5 0 5 1q�6M   5�      5�      �� 5�      -   5        $ 	stackBack��5�      .+�5�      �� -   5        $ btnBack��5�      $��5 5 5 0q�6.   $
 Hand��6@   $��80��6N   $��80��6Z   $��3q�6e   
���   �
���   �$��Segoe UI)�6�   $��12(�6�   #���   6�   ��5�      �� 5�      $ /new images/back-icon.png��6)   5�      �� 5�      .+�5�      �� $
��BACK��6$   $��Center=�60   $ 5 0 5 1q�6M   �� 5�      $�� ��6$   $��Center=�6,   $ 5 0 5 1q�6I   5�      5�      5�      5�      5�      5�      