   M S B A M L   `   `   `  ����  BreadFactory$    BreadFactory.Views.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Авторизация��5   	   $	��275��6   $	��350��6*    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $ ��Вход в систему��6!   $��18(�5      $
��Bold"�6"   $��Center=�5      $��0,0,0,15q�61   �� 5   
   $��1��6   $&�� Имя пользователя:��6!   $	��5,0q�6:   �� 5   
   $��1��6   $��5,25,5,0q�69   $��25��6K   ��6   �� *
Username+6K   �� 5   
   $��2��6   $��Пароль:��6!   $	��5,5q�60   G� 5   
   -   5         G�Name$ PasswordBox��5   #   $��2��6   $��5,30,5,0q�68   $��25��6J   �� 5   
   -   5        $��3��5      $��
Войти��6   $��Center=�5      $	��100��6.   $
��0,20q�6:   $��30��6H   ��5   .   �� *LoginCommand+5   T   �� 5    
   $��4��6   
���  ��$��Center=�5!      $��Bottom=�61   $
��Wrap=�5"      $��5q�6(   ��5    !   �� *ErrorMessage+5"   (   5#      5$      