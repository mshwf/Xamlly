# Xamlly [![](https://img.shields.io/badge/nuget-1.0.2-135DF4)](https://www.nuget.org/packages/Xamlly/)


Xamlly is a controls library for Xamarin.Forms, written entirely in XAML (or declarative code). It aims to provide good looking controls, without having to write custom renderers.


Currentlly, the library has these controls:
1. [ProgressBar](#progressbar)
2. [Switch](#switch)
3. [ToggleButton](#togglebutton)
4. [TogglesBar](#togglesbar)
5. [RadioButtonsGroup](#radiobuttonsgroup)

<img src="https://raw.githubusercontent.com/mshwf/Xamlly/master/Xamlly.Sample/xamlly.gif" width="500" />

Include the namespace in the XAML file:

```xaml
xmlns:xamlly="clr-namespace:Xamlly.XamllyControls;assembly=Xamlly"
```
# ProgressBar
```xaml 
<xamlly:ProgressBar Progress=".3" ProgressColor="CadetBlue" OutlineColor="Black"
                    ProgressTextColor="White"
                    BackgroundColor="Black"
                    CornerRadius="10"
                    Padding="5"/>
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

# RadioButtonsGroup
```xaml
<xamlly:RadioButtonsGroup ItemsSource="{Binding Options}" DisplayMemberPath="Name" SelectedValuePath="ID"
                          SelectedIndex="2" OnSelectionChanged="rbg_OnSelectionChanged" RadioButtonColor="White" 
                          TextColor="White"/>
```

### Install:

`Install-Package Xamlly -Version 1.0.2`

In the .NET standard project only.
