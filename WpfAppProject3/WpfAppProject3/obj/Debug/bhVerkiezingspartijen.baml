   M S B A M L   `   `   `  ����I  EWpfAppProject3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfAppProject3WpfAppProject3  5   	   *    $WpfAppProject3.bhVerkiezingspartijen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfAppProject3     5   	     � Title$  bhVerkiezingspartijen��5   	   $	��600��6'   $
��1050��64    � 
ResizeMode$ CanMinimize=�6A   .��5	      � ��5
   
   �� 5       �� ImageSource$ Assets/Stemmen.jpg��6   6<    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��50 �6   �� 5      $��4* �6   �� 5      $��* �6   .+�5   
   �� $��30(�6   
����  �
�������$��40��6K    �� TextAlignment$ Center=�6W   $
��Bold"�6n   Beheer verkiezingspartijen6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��Center=�6;   $��Bottom=�6X   .+�5      �� $��18(�6   ���	#FFFFCF76
�������$	��130��6P   $ Center=�6\   $
��Bold"�6s   Naam Partij6�   �� 5      $��18(�6   ���	#FFFFCF76
�������$	��120��6P   $ Center=�6\   $
��Bold"�6s   	Veriezing6�   �� 5      $��18(�6   ���	#FFFFCF76
�������$	��160��6P   $ Center=�6\   $
��Bold"�6s   Veriezingsoort6�   �� 5      $��14(�6   
����  �
���   �$��80��6K   $ Center=�6V   $
��Bold"�6m   Wijzig6�   �� 5      $��14(�6   
���  ��
���   �$��80��6J   $ Center=�6U   $
��Bold"�6l   	Verwijder6�   5      �� 5   
   -   5         ��Name$ databaseitems��5      $��2��6)   $��Center=�66   $	��Top=�6S   � 5   
   -   5         �Name$ wpAdd��5      $��3��6#    � Orientation$ 
Horizontal=�60   $��Center=�6I   $y�Hidden=�6f   .+�5      �� $��Partij��6   $��0,0,10,0q�6&   $ Center=�68   $��15(�6O   $
��Bold"�6]   �� 5      -   5        	 ��Name$	 cbPartij��5      $��0,0,30,0q�6)   �� 5       $��
Verkiezing��6   $��0,0,10,0q�6*   $ Center=�6<   $��15(�6S   $
��Bold"�6a   �� 5!      -   5        $	 cbVerkiezing��5!      $��0,0,30,0q�6-   �� 5"      -   5        
 ��Name$
 btVerzenden��5"      $��	Toevoegen��6*   $��90��6>   $��30��6I   $��15(�6U   
�������$
��Bold"�6v   6�   5#      �� 5%   
   -   5        $
 btAdd��5%      $��3��6    $
��left=�6-   $��90,0,0,0q�6H   $	��120��6Z   $��40��6f   $��	Toevoegen��6r   $��20(�6�   
������$
��Bold"�6�   6�   �� 5'   
   -   5        $
 btGoBack��5'      $��3��6#   $��Right=�60   $��	0,0,100,0q�6L   $	��100��6_   $��40��6k   $��Terug��6w   $��20(�6�   
�������$
��Bold"�6�   6�   5(      5)      