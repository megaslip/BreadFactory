   M S B A M L   `   `   `  ����  BreadFactory3    -BreadFactory.Views.WarehouseTransactionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $	��400��5   !   $	��500��6.     � WindowStartupLocation$  CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5	   	    � Title 5   	   �� *Title+.��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��16(�68   $
��Bold"�6F   $��0,0,0,10q�6X   ��6!   �� *Title+6X   �� 5   
   $��1��6   $��Продукт:��6!   �� 5   
   $��2��6    �� DisplayMemberPath$
 Name��5      $��0,0,0,10q�6T   ��5       �� *
Products+ �� SelectedItem 6A   �� *Transaction.Item.Product+{�5   ,   �� *IsProductEditable+6T   �� 5   
   $��3��6   $��Количество:��6!   �� 5   
   $��4��6   $��0,0,0,10q�6j   ��6   �� *Transaction.Quantity+ �� UpdateSourceTrigger$ PropertyChanged=�6j   �� 5   
   $��5��6   $(��"Дата производства:��6!   (  "System.Windows.Controls.DatePicker  5    
   $��6��6   $��0,0,0,10q�6[      SelectedDate 6"   �� *!Transaction.Item.ProductionDate+6[   �� 5"   
   $��7��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5#      �� $��OK��6   $��80��6@   $��0,0,10,0q�6K   ��6"   �� *	OkCommand+6K   �� 5$      $��Отмена��6   $��80��6H   ��6&   �� *CancelCommand+6H   5%      5&      5'      