   M S B A M L   `   `   `  ����H  DOpenGLMapping, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:OpenGLMapping;assembly=OpenGLMappingOpenGLMapping  5   	    
MapperView   MapperView.Mapper   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   BMWC2clr-namespace:OpenGLMapping;assembly=OpenGLMapping     5   	     � Title$  Mapper��5   	   $	��768��6   $
��1024��6%   .��5      �  � RowDefinitions 5   
   �� 5      -   5         �Name$ 
ToolBarRow��5   *   $
��Auto �6   6>   �� 5	      -   5        $ ModelRow��5	   (   $��1* �6   6:   �� 5
      -   5        $ NotificationRow��5
   (   $��22 �6   6A    � ColumnDefinitions 5   
   �� 5      -   5        $ TableOfContentsColumn��5   +   $	��210 �6   6J   �� 5      -   5        $ MapWindowColumn��5   *   $��1* �6   6C   .+�5   
       OpenGLMapping.WPFMapWindow  -   5        $��	MapWindow��5      $��1��6,   $��1��6<   $��3,0,0,0q�6I   �� 5   
   -   5        $��TOC_GridSplitter��5   �   $��1��6   $��0��6$   $��0q�64   
�������
�������$��1q�6h   $��Right=�6|   $��3��6�   6�   �� 5   
   -   5        $��Map_GridSplitter��5   �   $��1��6   $��1��6$   $��0q�64   
�������
�������$��1q�6h   $
��Left=�6|   $��3��6�   6�   �� 5   
   -	   5        $��TOCTabControl��5   T   $��0��6   $��0��6%   $��2��62   $��5,5,3,0q�6C   .@�5      �� -
   5        $��MapsTab��5   *   $��
Map Layers��6   .��5      � -   5        $��MapTreeGrid��5      .+�5         OpenGLMapping.MapTreeView  -   5        $��MapTreeView��5   &   6;   5      5      5      `� 5!   
   -   5        $��MapToolBarTray��5!   >   $��1��6   $��0��6&   $��0q�63   $��Bottom=�6T   $
��Left=�6o    `� 
Background �	#FFEBEBEB.�5"      #   OpenGLMapping.BasicMapToolbar  -   5        $��
MapToolbar��5"   "   $��27��64    `� IsLocked . $��Bottom=�6\   6y   c� 5#      -   5        $��SelectionToolbar��5#      $��27��6.    . $��Bottom=�6V   .@�5$      +   %OpenGLMapping.SelectableLayersControl  -   5        $��SelectableLayers��5$   .   $	��300��6F   5%      &    OpenGLMapping.FeatureEditToolbar  5'      -   5        $��FeatureEditorToolbar��5'   %    c� 	BandIndex$ 2��6A   $��27��6O    . $��Bottom=�6w   6�   5(      � 5+   
   -   5        $��StudyStatusPanel��5+      ���	#FFAEBCD1$��2��6>   $��0��6K   $��2��6[   .+�5,      � -   5        $��StudyProgressBar��5,      $��5,0,0,0q�62   $��18��6C   $��Center=�6O   $
��Left=�6j   $	��100��6�   �� 5-      -   5        $��StudyProgressText��5-      $��	110,0,0,0q�6T   $��Center=�6g   $
��Left=�6�   ��61   ��  �� Path$ ProgressText�6�   �� 5.      -   5        $��StatusBorder��5.      $��0,0,5,0q�6)   $��Center=�6:   $��Right=�6U   5/      50      51      52      