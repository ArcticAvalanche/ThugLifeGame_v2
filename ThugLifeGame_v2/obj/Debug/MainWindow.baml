   M S B A M L   `   `   `  ����J  FThugLifeGame_v2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ThugLifeGame_v2ThugLifeGame_v2  5   	        ThugLifeGame_v2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ThugLifeGame_v2     5   	     � Title$  
MainWindow��5   	   $	��840��6   $
��1260��6)    � WindowStartupLocation$ CenterScreen=�66   
�������.��5	      � $
��1250��6   $	��800��6   .+�5
   
   �� -   5         ��Name$ canvas��5
      $
��Left=�6!   $	��775��6<   $��	10,10,0,0q�6I   $	��Top=�6\   $	��775��6t   ���	#FFF0EEEE6�   �� 5   
   -   5         ��Name$ 	drawLines��5      $��
Draw lines��6&   $
��Left=�6;   $��
805,10,0,0q�6V   $	��Top=�6j   
�������}�.  d� 	IsChecked$
 TrueY�6�   6�   �� 5   
   -   5         ��Name$ makeStep��5      $��	Make step��6#   $
��Left=�67   $��1100,10,0,0q�6R   $	��Top=�6g   $	��120��6   $��50��6�   ���	#FFF0EEEE���	#FFA4A4A4$��1q�6�   6�   �� 5   
   -   5        $ reset��5      $��Reset��6    $
��Left=�60   $��1100,65,0,0q�6K   $	��Top=�6`   $	��120��6x   $��50��6�   ���	#FFF0EEEE���	#FFA4A4A4$��1q�6�   6�   �� 5   
   -   5         ��Name$ stepTime��5      $
��Left=�6$   $��23��6?   $��1111,699,0,0q�6K    �� TextWrapping$
 Wrap=�6a   $��0��6u   $	��Top=�6~   $	��120��6�    �� TextAlignment$ Right=�6�   |�.  6�   �� 5   
   -   5        $ cellsNumber��5      $
��Left=�6'   $��23��6B   $��1111,727,0,0q�6N   $
 Wrap=�6d   $��70��6x   $	��Top=�6�   $	��120��6�   $ Right=�6�   |�.  6�   �� 5   
   -   5        $ changePercent��5      $
��Left=�6)   $��23��6D   $��1111,755,0,0q�6P   $
 Wrap=�6f   $��0%��6z   $	��Top=�6�   $	��120��6�   $ Right=�6�   |�.  6�   �� 5   
   -   5        $ patternsCanvas��5      $
��Left=�6)   $	��775��6D   $��	10,10,0,0q�6Q   $	��Top=�6d   $	��775��6|   ���	#FFF0EEEE{�.  $y�Hidden=�6  �� 5   
   -	   5        $ showPatterns��5      $��Show patterns��6'   $
��Left=�6?   $��
975,10,0,0q�6Z   $	��Top=�6n   $	��120��6�   $��50��6�   ���	#FFF0EEEE���	#FFA4A4A4$��1q�6�   6�   �� 5   
   $��
Statistics��6   $
��Left=�6'   $	��112��6B   $��995,678,0,0q�6O   $	��Top=�6d   $	��245��6|   5      5      