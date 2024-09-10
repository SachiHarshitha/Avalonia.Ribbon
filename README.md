# AvaloniaRibbon

This is a Ribbon Control Component library that replicates Microsoft's Ribbon UI, as seen in Windows 8+'s File Explorer, Microsoft Office 2007+, and in various other places, for Avalonia. In its present state, it is reasonably usable, but some features are still missing, so it should not considered complete.

## Project Background
The original project was developed by [amazerol](https://github.com/amazerol/AvaloniaRibbon) and was then optimized by [Splitwirez](https://github.com/Splitwirez/AvaloniaRibbon). Once the Avalonia 11 was released it was then ported to 11.0.1 by [NOoBODDY](https://github.com/NOoBODDY/AvaloniaRibbon).
The original version of the component is used in **[Jaya File Manager](https://github.com/JayaFM/Jaya)**, but other projects are welcome to use it as well.

## Cross-Platform Support
Given that Avalonia is a cross-platform UI framework and to support the availability, currently, this project is being optimized by [Sachith Liyanagama](https://github.com/SachiHarshitha/Avalonia.Ribbon). 
The overall controls library is refactored as follows.

|         VS Project        | Usecase             |
|:-------------------------:|---------------------|
| AvaloniaUI.Ribbon         | Desktop, WASM, etc. |
| AvaloniaUI.Ribbon.Windows | Desktop Only        |

## Available Controls

In order to support the [ControlTheme](https://docs.avaloniaui.net/docs/next/basics/user-interface/styling/control-themes) architecture, the controls are currently being either optimized or re-developed if necessary.
Based on the platform availability the components are summarized as follows.

### Cross-Platform

|   Control Type   | Original             | Status               | New Component (Under 'Controls' Path) |
|:----------------:|----------------------|----------------------|---------------------------------------|
| Ribbon           | Ribbon               | Original State       |                                       |
| Button           | RibbonButton         |:construction: Under Reconstruction |                                       |
| Toggle Button    | RibbonToggleButton   | Original State       |                                       |
| Split Button     | RibbonSplitButton    | Recreated            | SplitButtonControl (Will be renamed)  |
| Gallery          | RibbonGallery        |:construction: Under development    | RibbonGallery                         |
| GalleryItem      | GalleryItem          |:construction: Under Development    | GalleryItem                           |
| Drop Down Button | RibbonDropDownButton | Original State       |                                       |
| File Menu        | RibbonMenu           | Original State       |                                       |
| File Menu Item   | RibbonMenuItem       | Original State       |                                       |
| Tab              | RibbonTab            | Original State       |                                       |
| Ribbon Group Box | RibbonGroupBox       | Original State       |                                       |

### Desktop Only

Apart from the above-mentioned global components, the desktop-specific controls are presented below.

|   Control Type   | Original             | Status               | New Component (Under 'Controls' Path) |
|:----------------:|----------------------|----------------------|---------------------------------------|
| Ribbon Window    | RibbonWindow         |:construction: Under Reconstruction |                                       |
| Quick Access Bar | QuickAccessToolBar   |:construction: Under Reconstruction |                                       |


## Previews: Desktop
![Fluent-Light theme Desktop app preview, horizontal orientation](https://github.com/SachiHarshitha/Avalonia.Ribbon/assets/76457616/4e92ca1c-04c3-4aff-84a8-4ce9ef27ceb4)

## Previews: WASM
![Fluent-Light theme WASM web app preview, horizontal orientation](https://github.com/SachiHarshitha/Avalonia.Ribbon/assets/76457616/7f85672f-0bbf-4d49-a4ba-190b2de46163)

## How to Use

1. Refer the package according to your usecase as mentioned in above **Cross-Platform Support** Section.

2. Include ribbon styles to App.xaml as shown below.

    Fluent theme:
    ```xaml
        <StyleInclude Source="avares://AvaloniaUI.Ribbon/Styles/Fluent/AvaloniaRibbon.xaml" />
    ```
    "Default" theme:
    ```xaml
        <StyleInclude Source="avares://AvaloniaUI.Ribbon/Styles/Default/AvaloniaRibbon.xaml" />
    ```
    and localized text (same for both themes):
    ```xaml
        <ResourceInclude Source="avares://AvaloniaUI.Ribbon/Locale/en-ca.xaml" />
    ```

3. Use the below mentioned sample as an example to use the ribbon control. 
    ```xaml
            <ribbon:Ribbon Name="RibbonControl" DockPanel.Dock="Top" Orientation="Horizontal" HelpButtonCommand="{Binding HelpCommand}">
                <ribbon:Ribbon.Menu>
                    <ribbon:RibbonMenu ribbon:KeyTip.KeyTipKeys="F">
                        <ribbon:RibbonMenu.MenuItems>
                            <MenuItem Header="Item 1">
                                <MenuItem Header="SubItem 1"/>
                                <MenuItem Header="SubItem 2"/>
                            </MenuItem>
                            <MenuItem Header="Item 2">
                                <MenuItem Header="SubItem 1"/>
                                <MenuItem Header="SubItem 2"/>
                                <MenuItem Header="SubItem 3"/>
                            </MenuItem>
                        </ribbon:RibbonMenu.MenuItems>
                        <ribbon:RibbonMenu.MenuPlacesItems>
                            <ListBoxItem Content="Place 1"/>
                            <ListBoxItem Content="Place 2"/>
                            <ListBoxItem Content="Place 3"/>
                        </ribbon:RibbonMenu.MenuPlacesItems>
                    </ribbon:RibbonMenu>
                </ribbon:Ribbon.Menu>
                <ribbon:RibbonTab Header="Home" ribbon:KeyTip.KeyTipKeys="H">
                    <ribbon:RibbonTab.Groups>
                        <ribbon:RibbonGroupBox Header="Test Group 1" Command="{Binding OnClickCommand}" ribbon:KeyTip.KeyTipKeys="D1">
                            <ribbon:RibbonButton Content="Button 1" MinSize="Medium" ToolTip.Tip="Button 1" ribbon:KeyTip.KeyTipKeys="A">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="32" Height="32">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="16" Height="16">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton Content="Button 2" MinSize="Medium" ToolTip.Tip="Button 2" ribbon:KeyTip.KeyTipKeys="B">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Grid Width="32" Height="32">
                                        <Rectangle Width="24" Height="24" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2,0"/>
                                    </Grid>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Rectangle Width="8" Height="8" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2,0"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton Content="Button 3" MinSize="Medium" ToolTip.Tip="Button 3" ribbon:KeyTip.KeyTipKeys="C">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Grid Width="32" Height="32">
                                        <Rectangle Width="20" Height="20" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Rectangle Width="8" Height="8" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="Test Group 2" ribbon:KeyTip.KeyTipKeys="D2">
                            <ribbon:RibbonButton MaxSize="Medium" Content="Button 1" ToolTip.Tip="Button 4" ribbon:KeyTip.KeyTipKeys="D">
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 16 0 L 8 8 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton MaxSize="Medium" Content="Button 2" ToolTip.Tip="Button 5" ribbon:KeyTip.KeyTipKeys="E">
                                <ribbon:RibbonButton.Icon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="16" Height="16">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton MaxSize="Large" MinSize="Medium" Content="Button 6" ToolTip.Tip="Button 6" ribbon:KeyTip.KeyTipKeys="F">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Path Data="M 0 16 L 16 0 L 32 16 L 16 32 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Path Data="M 0 8 L 8 0 L 16 8 L 8 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton MaxSize="Large" MinSize="Medium" Content="Button 7" ToolTip.Tip="Button 7" ribbon:KeyTip.KeyTipKeys="G">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Grid Width="32" Height="32">
                                        <Rectangle Width="20" Height="20" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Rectangle Width="8" Height="8" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="Test Group 3" ribbon:KeyTip.KeyTipKeys="D3">
                            <ribbon:RibbonButton MaxSize="Medium" Content="Button 8" ToolTip.Tip="Button 8" ribbon:KeyTip.KeyTipKeys="H">
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 16 0 L 8 8 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton MaxSize="Medium" Content="Button 9" ToolTip.Tip="Button 9" ribbon:KeyTip.KeyTipKeys="I">
                                <ribbon:RibbonButton.Icon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="16" Height="16">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton MaxSize="Large" MinSize="Medium" Content="Button 10" ToolTip.Tip="Button 10" ribbon:KeyTip.KeyTipKeys="J">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Grid Width="32" Height="32">
                                        <Rectangle Width="20" Height="20" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.LargeIcon>
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Rectangle Width="8" Height="8" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                        <Border BorderBrush="{DynamicResource ThemeForegroundBrush}" BorderThickness="2"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                        </ribbon:RibbonGroupBox>
                    </ribbon:RibbonTab.Groups>
                </ribbon:RibbonTab>
                <ribbon:RibbonTab Header="Button Controls" ribbon:KeyTip.KeyTipKeys="B">
                    <ribbon:RibbonTab.Groups>
                        <ribbon:RibbonGroupBox Header="RibbonButtons" Command="{Binding OnClickCommand}" ribbon:KeyTip.KeyTipKeys="B">
                            <ribbon:RibbonButton Content="Large" MinSize="Large" MaxSize="Large">
                                <ribbon:RibbonButton.LargeIcon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="32" Height="32">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonButton.LargeIcon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton Content="Medium" MinSize="Medium" MaxSize="Medium">
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 16 0 L 8 8 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                            <ribbon:RibbonButton ToolTip.Tip="Small" MinSize="Small" MaxSize="Small">
                                <ribbon:RibbonButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 8 8 L 16 0 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonButton.Icon>
                            </ribbon:RibbonButton>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="RibbonToggleButtons" Command="{Binding OnClickCommand}" ribbon:KeyTip.KeyTipKeys="T">
                            <ribbon:RibbonToggleButton Content="Large" MinSize="Large" MaxSize="Large">
                                <ribbon:RibbonToggleButton.LargeIcon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="32" Height="32">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonToggleButton.LargeIcon>
                            </ribbon:RibbonToggleButton>
                            <ribbon:RibbonToggleButton Content="Medium" MinSize="Medium" MaxSize="Medium">
                                <ribbon:RibbonToggleButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 16 0 L 8 8 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonToggleButton.Icon>
                            </ribbon:RibbonToggleButton>
                            <ribbon:RibbonToggleButton ToolTip.Tip="Small" MinSize="Small" MaxSize="Small">
                                <ribbon:RibbonToggleButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 8 8 L 16 0 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonToggleButton.Icon>
                            </ribbon:RibbonToggleButton>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="RibbonSplitButtons" Command="{Binding OnClickCommand}" ribbon:KeyTip.KeyTipKeys="S">
                            <ribbon:RibbonSplitButton Content="Large" MinSize="Large" MaxSize="Large">
                                <ribbon:RibbonSplitButton.LargeIcon>
                                    <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="32" Height="32">
                                        <Rectangle.OpacityMask>
                                            <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                        </Rectangle.OpacityMask>
                                    </Rectangle>
                                </ribbon:RibbonSplitButton.LargeIcon>
                                <ComboBoxItem>Item 1</ComboBoxItem>
                                <ComboBoxItem>Item 2</ComboBoxItem>
                            </ribbon:RibbonSplitButton>
                            <ribbon:RibbonSplitButton Content="Medium" MinSize="Medium" MaxSize="Medium">
                                <ribbon:RibbonSplitButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 16 0 L 8 8 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonSplitButton.Icon>
                                <ComboBoxItem>Item 1</ComboBoxItem>
                                <ComboBoxItem>Item 2</ComboBoxItem>
                            </ribbon:RibbonSplitButton>
                            <ribbon:RibbonSplitButton ToolTip.Tip="Small" MinSize="Small" MaxSize="Small">
                                <ribbon:RibbonSplitButton.Icon>
                                    <Grid Width="16" Height="16">
                                        <Path Data="M 0 0 L 8 8 L 16 0 L 16 16 L 0 16 Z" Fill="{DynamicResource ThemeForegroundBrush}"/>
                                    </Grid>
                                </ribbon:RibbonSplitButton.Icon>
                                <ComboBoxItem>Item 1</ComboBoxItem>
                                <ComboBoxItem>Item 2</ComboBoxItem>
                            </ribbon:RibbonSplitButton>
                        </ribbon:RibbonGroupBox>
                    </ribbon:RibbonTab.Groups>
                </ribbon:RibbonTab>
                <ribbon:RibbonTab Header="Galleries" ribbon:KeyTip.KeyTipKeys="G">
                    <ribbon:RibbonTab.Groups>
                        <ribbon:RibbonGroupBox Header="Large gallery" Command="{Binding OnClickCommand}" ribbon:KeyTip.KeyTipKeys="L">
                            <ribbon:Gallery>
                                <ListBoxItem Content="Item 1"/>
                                <ListBoxItem Content="Item 2"/>
                                <ListBoxItem Content="Item 3"/>
                                <ListBoxItem Content="Item 4"/>
                                <ListBoxItem Content="Item 5"/>
                                <ListBoxItem Content="Item 6"/>
                                <ListBoxItem Content="Item 7"/>
                                <ListBoxItem Content="Item 8"/>
                                <ListBoxItem Content="Item 9"/>
                                <ListBoxItem Content="Item 10"/>
                                <ListBoxItem Content="Item 11"/>
                                <ListBoxItem Content="Item 12"/>
                                <ListBoxItem Content="Item 13"/>
                                <ListBoxItem Content="Item 14"/>
                                <ListBoxItem Content="Item 15"/>
                            </ribbon:Gallery>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="Medium gallery" ribbon:KeyTip.KeyTipKeys="M">
                            <ribbon:Gallery MaxSize="Medium">
                                <ListBoxItem Content="Item 1"/>
                                <ListBoxItem Content="Item 2"/>
                                <ListBoxItem Content="Item 3"/>
                                <ListBoxItem Content="Item 4"/>
                                <ListBoxItem Content="Item 5"/>
                                <ListBoxItem Content="Item 6"/>
                                <ListBoxItem Content="Item 7"/>
                                <ListBoxItem Content="Item 8"/>
                                <ListBoxItem Content="Item 9"/>
                                <ListBoxItem Content="Item 10"/>
                                <ListBoxItem Content="Item 11"/>
                                <ListBoxItem Content="Item 12"/>
                                <ListBoxItem Content="Item 13"/>
                                <ListBoxItem Content="Item 14"/>
                                <ListBoxItem Content="Item 15"/>
                            </ribbon:Gallery>
                        </ribbon:RibbonGroupBox>
                        <ribbon:RibbonGroupBox Header="Small gallery" ribbon:KeyTip.KeyTipKeys="S">
                            <ribbon:Gallery MaxSize="Small">
                                <ListBoxItem Content="Item 1"/>
                                <ListBoxItem Content="Item 2"/>
                                <ListBoxItem Content="Item 3"/>
                                <ListBoxItem Content="Item 4"/>
                                <ListBoxItem Content="Item 5"/>
                                <ListBoxItem Content="Item 6"/>
                                <ListBoxItem Content="Item 7"/>
                                <ListBoxItem Content="Item 8"/>
                                <ListBoxItem Content="Item 9"/>
                                <ListBoxItem Content="Item 10"/>
                                <ListBoxItem Content="Item 11"/>
                                <ListBoxItem Content="Item 12"/>
                                <ListBoxItem Content="Item 13"/>
                                <ListBoxItem Content="Item 14"/>
                                <ListBoxItem Content="Item 15"/>
                            </ribbon:Gallery>
                        </ribbon:RibbonGroupBox>
                    </ribbon:RibbonTab.Groups>
                </ribbon:RibbonTab>
            </ribbon:Ribbon>
    ```

## Change Log
### Update (21/11/2023)
- Relocate WindowIcontoImageConverter.
- Solve the issue with the icon to image converter.
- Optimize Quick access bar design with separators.
- Optimize Ribbon Window title Height.
- Update Windows Sample project with quick access toolbar and DecorationsMode Selector.
- Create 3 types of gallery themes.(Large, Medium. Small)
  
### Update (05/11/2023)
- Create Interfaces
- Refactor Code Base
- Rename xaml to axaml.
- Add Cleanup folder script.
- Update to Avalonia 11.0.5
- Create Browser Sample Project
- Optimize references and Desktop Sample Project.
  
### Update (01/03/2021)
- Update to Avalonia 0.10
- Added Fluent Theme
- Added contextual tabs
- Added Quick Access Toolbar (Experimental)
- Assorted bugfixes
- Probably something else I forgot to mention lol

### Update (06/03/2020)
- Re-organized some stuff
- `RibbonWindow` is now an actual Window in its own right
- Control size variants are now properties of a control, rather than entirely separate controls
- Added dynamic control size adjustment
- Added `Gallery` control
- Major XAML cleanup
- Switched to using standard Avalonia colours/brushes
- Added `RibbonMenu`
- `Ribbon` can now be collapsed or expanded (shows selected groups temporarily in a `Popup` when a tab is clicked if the `Ribbon` is collapsed)
- `Ribbon` can now be horizontally or vertically oriented (real-time value changes are not yet fully functional, but compile-time/startup-time changes should work without a hitch)
- `KeyTip`s and Keyboard navigation via ALT are now mostly functional
- Probably something else I forgot to mention lol

### Update (14/11/2019)
- Added separate sample project to demonstrate the usage.
- Architectural improvements have been done.
- Standardized control to be an assembly instead of executable.
- Added ribbon classes to Avalonia's namespace.

### Update (23/06/2019)
- In `App.xaml`, only one line of `<StyleInclude>` is required now.
- Adddition of the special buttons (top part: button, lower part: combobox)
- The entire control is themable now.
- Small button added and its HorizontalGroup.
- Tootips added.

### Update (17/06/2019)
- Control is now fully usable.
- NuGet done.
- Wiki has been written.

Below mentioned are some plans for the future.
- Implement more controls, especially toggle button.
- Allow color stylesheets for a fully customizable control.
- Take care of the resizing matters.

### Update (16/06/2019)
- Improvement of the look &amp; feel of the ribbon.
- Update of the previous preview image with new look &amp; feel.

The remaining things which have be done are as follows.
- Take care of the disapearance of icons in case of window resizing.
- Handle click actions easily.

# Avalonia Ribbon
Please acknowledge Splitwirez for all the last new features added to the ribbon. As far as I'm concerned, I'm just centralizing code and updating the nuget.

You can also acknowledge Rubal Walia as well for cleaning up all my messy code.

