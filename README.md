# Xamlly

Xamlly is a controls library for Xamarin.Forms, written entirely in XAML. It aims to provide good looking controls, without having to write custom renderers.

Currentlly, the library has these controls:
1. [ProgressBar](#progressbar)
2. [RadioButtonsGroup](#radiobuttonsgroup)
3. [Switch](#switch)
4. [ToggleButton](#togglebutton)
5. [TogglesBar](#togglesbar)

<img src="https://raw.githubusercontent.com/mshwf/Xamlly/master/Xamlly.Sample/xamlly.gif" width="300" />

# ProgressBar
```xaml 
<xamlly:ProgressBar Progress=".3" ProgressColor="CadetBlue" OutlineColor="Black"
                    ProgressTextColor="White"
                    BackgroundColor="Black"
                    CornerRadius="10"
                    Padding="5"/>
```

# RadioButtonsGroup
```xaml
<xamlly:RadioButtonsGroup HorizontalOptions="CenterAndExpand" ItemsSource="{Binding Options}" DisplayMemberPath="Name" SelectedValuePath="ID"
                          SelectedIndex="2" OnSelectionChanged="rbg_OnSelectionChanged" RadioButtonColor="White" 
                          TextColor="White"/>
```

# Switch

```xaml
 <xamlly:Switch CornerRadius="10" ButtonWidth="30" HorizontalOptions="Center"
                OnColor="Red"
                OffText="No" OnText="Yes"/>
```

# ToggleButton
```xaml
<xamlly:ToggleButton Text="ON" SelectedColor="White" UnselectedColor="Gray" BackgroundColor="Black" HorizontalOptions="CenterAndExpand"/>
```

# TogglesBar
```xaml
<xamlly:TogglesBar IsMultiSelect="False" ItemsSource="{Binding Options}" DisplayMemberPath="Name" SelectedColor="White" UnselectedColor="Gray" BackgroundColor="Black" HorizontalOptions="Fill"/>
```
More controls to come soon!
