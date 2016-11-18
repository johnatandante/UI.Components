# UI.Components
XAML Components for a better UI
It provides:
* `DataGridListView` : Simple ListView with a GridView Layout with binding support for ItemSource List; it supports binding on ListView ItemSourceProperty and also main UserControl

## HowTo Use
Just a quick reference guide:
1. Just Download the repo and include the project into your solution
2. Use Components in your XAML code

### Code Examples
Don't forget to populate Columns Collection attribute with `GridViewColumn` control

#### ToDos
Replace `CustomNamespace.CustomControlClass` with the Control Class name you are going to implement.
Replace `CustomNamespace.CustomModelnamespace` with the ViewModel Namespace you are working in.

`<my:CustomClassViewModel x:Key="model"/>` is the static context reference we're pointing to
`CustomItemsSource` is the custom property (of type ICollectionView) referenced in your ViewModel Class.
`ToDo`, `Field1`, `Field2`, `Field3`, `Field4`  are the custom property of the item in CustomItemsSource collection.

#### Xaml class - CustomNamespace.CustomControlClass.xaml
```
<UserControl x:Class="CustomNamespace.CustomControlClass"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"        
        xmlns:comp="clr-namespace:UI.Components;assembly=UI.Components"
        xmlns:my="clr-namespace:CustomNamespace.CustomModelnamespace">
    <UserControl.Resources>
        <my:CustomClassViewModel x:Key="model"/>
    </UserControl.Resources>
   <StackPanel>
     <Label Content="There is someting in the grid, I Suppose"/>
      <comp:DataGridListView
        ItemsSource="{Binding Source={StaticResource model}, Path=CustomItemsSource}">
        <comp:DataGridListView.Columns>
            <GridViewColumn Header="ToDo">
                <GridViewColumn.CellTemplate>
                    <DataTemplate>
                        <CheckBox Margin="6" IsChecked="{Binding ToDo}" />
                    </DataTemplate>
                </GridViewColumn.CellTemplate>
            </GridViewColumn>
            <GridViewColumn Header="Field1 Header"  DisplayMemberBinding="{Binding Field1}" />
            <GridViewColumn Header="Field2 Header"  DisplayMemberBinding="{Binding Field2}" />
            <GridViewColumn Header="Field3 Header"  DisplayMemberBinding="{Binding Field3}" />
        </comp:DataGridListView.Columns>
    </comp:DataGridListView>
   </StackPanel>
</UserControl> 
```

### C# class
Don't need particular stuffs: collection data is already filtered by the component.
If you are going to use a ICollectionView with a filter, the total and the partial count shown at the bottom of the grid don't thake care of the unfiltered data list.

## Future Desiderata Improvements
- [x] Binding support for ItemSource List
- [x] Binding support for ListView Selected Item
- [ ] Automatic sort feature

## Remarks
Few remarks for this work:
1. Never tested on .Net Framework > 4 or .Net Core Framework
2. Requires xaml features and .Net Framework >= 3.5
