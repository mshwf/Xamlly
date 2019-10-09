# Xamlly

Xamlly is a controls library for Xamarin.Forms, written entirely in XAML. It aims to provide good looking controls, without having to write custom renderers.

Currentlly, the library has these controls:
1. ProgressBar
2. RadioButtonsGroup
3. ToggleButton
4. TogglesBar
5. Switch

[![Demo CountPages alpha](https://im4.ezgif.com/tmp/ezgif-4-d3022729d355.gif)

# ProgressBar
```xaml 
<xamlly:ProgressBar Margin="0"
                    Progress=".3" ProgressColor="CadetBlue" OutlineColor="Black"
                    CornerRadius="10"
                    Padding="5"
                    ProgressTextColor="White"
                    BackgroundColor="Black"/>
```

# RadioButtonsGroup
```xaml
<xamlly:RadioButtonsGroup HorizontalOptions="CenterAndExpand" ItemsSource="{Binding Colors}" DisplayMemberPath="Name" SelectedValuePath="ID"
                          SelectedIndex="2" OnSelectionChanged="rbg_OnSelectionChanged" RadioButtonColor="White" 
                          TextColor="White"/>
```

# ToggleButton
```xaml
<xamlly:ToggleButton Text="ON" SelectedColor="White" UnselectedColor="Gray" BackgroundColor="Black" HorizontalOptions="CenterAndExpand"/>
```

# TogglesBar
```xaml
<xamlly:TogglesBar IsMultiSelect="False" ItemsSource="{Binding Colors}" DisplayMemberPath="Name" SelectedColor="White" UnselectedColor="Gray" BackgroundColor="Black" HorizontalOptions="Fill"/>
```
More controls to come soon!
