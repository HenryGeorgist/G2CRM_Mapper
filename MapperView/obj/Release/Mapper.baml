   M S B A M L   `   `   `  ���� H  DOpenGLMapping, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:OpenGLMapping;assembly=OpenGLMappingOpenGLMapping   
MapperView   MapperView.Mapper   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     BMWC2clr-namespace:OpenGLMapping;assembly=OpenGLMapping       � Title$  Mapper��$	��768��$
��1024��.���  � RowDefinitions �� -    �Name$ 
ToolBarRow��$
��Auto ��� -   $ ModelRow��$��1* ��� -   $ NotificationRow��$��22 � � ColumnDefinitions �� -   $ TableOfContentsColumn��$	��210 ��� -   $ MapWindowColumn��$��1* �.+�    OpenGLMapping.WPFMapWindow  -   $��	MapWindow��$��1��$��1��$��3,0,0,0q��� -   $��TOC_GridSplitter��$��1��$��0��$��0q�
�������
�������$��1q�$��Right=�$��3���� -   $��Map_GridSplitter��$��1��$��1��$��0q�
�������
�������$��1q�$
��Left=�$��3���� -	   $��TOCTabControl��$��0��$��0��$��2��$��5,5,3,0q�.@��� -
   $��MapsTab��$��
Map Layers��.��� -   $��MapTreeGrid��.+�   OpenGLMapping.MapTreeView  -   $��MapTreeView��`� -   $��MapToolBarTray��$��1��$��0��$��0q�$��Bottom=�$
��Left=� `� 
Background �	#FFEBEBEB.�#   OpenGLMapping.BasicMapToolbar  -   $��
MapToolbar��$��27�� `� IsLocked . $��Bottom=�c� -   $��SelectionToolbar��$��27�� . $��Bottom=�.@�+   %OpenGLMapping.SelectableLayersControl  -   $��SelectableLayers��$	��300��&    OpenGLMapping.FeatureEditToolbar  -   $��FeatureEditorToolbar�� c� 	BandIndex$ 2��$��27�� . $��Bottom=�� -   $��StudyStatusPanel�����	#FFAEBCD1$��2��$��0��$��2��.+�� -   $��StudyProgressBar��$��5,0,0,0q�$��18��$��Center=�$
��Left=�$	��100���� -   $��StudyProgressText��$��	110,0,0,0q�$��Center=�$
��Left=�����  �� Path$ ProgressText��� -   $��StatusBorder��$��0,0,5,0q�$��Center=�$��Right=�