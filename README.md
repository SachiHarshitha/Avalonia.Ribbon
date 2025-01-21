# AvaloniaRibbon

This is a Ribbon Control Component library that replicates Microsoft's Ribbon UI, as seen in Windows 8+'s File Explorer,
Microsoft Office 2007+, and in various other places, for Avalonia. In its present state, it is reasonably usable, but
some features are still missing, so it should not considered complete.

## Project Background

The original project was developed by [amazerol](https://github.com/amazerol/AvaloniaRibbon) and was then optimized
by [Splitwirez](https://github.com/Splitwirez/AvaloniaRibbon). Once the Avalonia 11 was released it was then ported to
11.0.1 by [NOoBODDY](https://github.com/NOoBODDY/AvaloniaRibbon).
The original version of the component is used in **[Jaya File Manager](https://github.com/JayaFM/Jaya)**, but other
projects are welcome to use it as well.

## Cross-Platform Support

Given that Avalonia is a cross-platform UI framework and to support the availability, currently, this project is being
optimized by [Sachith Liyanagama](https://github.com/SachiHarshitha/Avalonia.Ribbon).
The overall controls library is refactored as follows.

|        VS Project         | Usecase             |
|:-------------------------:|---------------------|
|     AvaloniaUI.Ribbon     | Desktop, WASM, etc. |
| AvaloniaUI.Ribbon.Windows | Desktop Only        |

## Available Controls

In order to support
the [ControlTheme](https://docs.avaloniaui.net/docs/next/basics/user-interface/styling/control-themes) architecture, the
controls are currently being either optimized or re-developed if necessary.
Based on the platform availability the components are summarized as follows.

### Cross-Platform

|   Control Type   | Original             | Status                              | New Component (Under 'Controls' Path) |
|:----------------:|----------------------|-------------------------------------|---------------------------------------|
|      Ribbon      | Ribbon               | Original State                      |                                       |
|      Button      | RibbonButton         | :construction: Under Reconstruction |                                       |
|  Toggle Button   | RibbonToggleButton   | Original State                      |                                       |
|   Split Button   | RibbonSplitButton    | Recreated                           | SplitButtonControl (Will be renamed)  |
|     Gallery      | RibbonGallery        | :construction: Under development    | RibbonGallery                         |
|   GalleryItem    | GalleryItem          | :construction: Under Development    | GalleryItem                           |
| Drop Down Button | RibbonDropDownButton | Original State                      |                                       |
|    File Menu     | RibbonMenu           | Modernized to backstage style       |                                       |
|  File Menu Item  | RibbonMenuItem       | Modernized to backstage style       |                                       |
|       Tab        | RibbonTab            | Original State                      |                                       |
| Ribbon Group Box | RibbonGroupBox       | Original State                      |                                       |

### Desktop Only

Apart from the above-mentioned global components, the desktop-specific controls are presented below.

|   Control Type   | Original           | Status                              | New Component (Under 'Controls' Path) |
|:----------------:|--------------------|-------------------------------------|---------------------------------------|
|  Ribbon Window   | RibbonWindow       | :construction: Under Reconstruction |                                       |
| Quick Access Bar | QuickAccessToolBar | :construction: Under Reconstruction |                                       |

## Previews:

| Desktop                                                                                                                                          | WASM                                                                                                                                               |
|--------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| ![Fluent-Light theme Desktop app preview, vertical orientation](https://github.com/user-attachments/assets/145d6530-f60f-4ce6-83e6-d91832c6b5eb) | ![Fluent-Dark theme WASM web app preview, horizontal orientation](https://github.com/user-attachments/assets/c9295d74-1c81-48b4-9d2e-f88eed6e1718) |
| Ribbon Menu / Backstage                                                                                                                          |                                                                                                                                                    |
| ![Ribbon Menu with grouping of controls and modern design](https://github.com/user-attachments/assets/06125b4b-fa39-49fa-9f20-138428eb3ebf)      |                                                                                                                                                    |

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
       The sample is available in the [AvaloniaUI.Ribbon.Demo]() project.
       ```xaml
               <Ribbon Name="RibbonControl" DockPanel.Dock="Top" Orientation="Horizontal" HelpButtonCommand="{Binding HelpCommand}">
                   <RibbonMenu
                   Width="50"
                   KeyTip.KeyTipKeys="F">
                      <RibbonMenuItem Header="New" Group="File" IsTopDocked="True">
                          <RibbonMenuItem.Icon>
                              <Rectangle
                                  Width="32"
                                  Height="32"
                                  Fill="Red" />
                          </RibbonMenuItem.Icon>
                          <RibbonMenuItem.Content></RibbonMenuItem.Content>
                      </RibbonMenuItem>
                      <RibbonMenuItem Header="Open" Group="File" IsTopDocked="True">
                          <RibbonMenuItem.Icon>
                              <Rectangle
                                  Width="32"
                                  Height="32"
                                  Fill="Green" />
                          </RibbonMenuItem.Icon>
                          <RibbonMenuItem.Content>
                              <DockPanel>
                                  <TabControl DockPanel.Dock="Top">
                                      <TabItem Header="TEST" />
                                  </TabControl>
                              </DockPanel>
                          </RibbonMenuItem.Content>
                      </RibbonMenuItem>
                   </Ribbon.Menu>
                   <RibbonTab Header="Button Controls" KeyTip.KeyTipKeys="B">
                       <RibbonTab.Groups>
                           <RibbonGroupBox Header="RibbonButtons" Command="{Binding OnClickCommand}" KeyTip.KeyTipKeys="B">
                               <RibbonButton Content="Large" MinSize="Large" MaxSize="Large">
                                   <RibbonButton.LargeIcon>
                                       <Rectangle Fill="{DynamicResource ThemeForegroundBrush}" Width="32" Height="32">
                                           <Rectangle.OpacityMask>
                                               <ImageBrush Source="/Assets/RibbonIcons/settings.png"/>
                                           </Rectangle.OpacityMask>
                                       </Rectangle>
                                   </RibbonButton.LargeIcon>
                               </RibbonButton>
                           </RibbonGroupBox>
                           <RibbonGroupBox Header="RibbonToggleButtons" Command="{Binding OnClickCommand}" KeyTip.KeyTipKeys="T">
                              <RibbonToggleButton
                                 Content="Large"
                                 Icon="{DynamicResource Icon1}"
                                 LargeIcon="{DynamicResource Icon1Large}"
                                 MaxSize="Large"
                                 MinSize="Large"
                                 QuickAccessIcon="{DynamicResource Icon1QuickAccess}" />
                              <RibbonToggleButton
                                 Content="Medium"
                                 Icon="{DynamicResource Icon2}"
                                 LargeIcon="{DynamicResource Icon2Large}"
                                 MaxSize="Medium"
                                 MinSize="Medium"
                                 QuickAccessIcon="{DynamicResource Icon2QuickAccess}" />
                           </RibbonGroupBox>
                           <RibbonGroupBox Header="RibbonSplitButtons" Command="{Binding OnClickCommand}" KeyTip.KeyTipKeys="S">
                              <SplitButtonControl
                                  x:Name="RibbonSplitButton1"
                                  Content="Large"
                                  Icon="{DynamicResource Icon1}"
                                  LargeIcon="{DynamicResource Icon1Large}"
                                  QuickAccessIcon="{DynamicResource Icon1QuickAccess}"
                                  Size="Large"
                                  Theme="{StaticResource LargeSplitButton}">
                                  <SplitButtonControl.Flyout>
                                      <MenuFlyout Placement="Bottom">
                                          <MenuItem Header="Item 1">
                                              <MenuItem Header="Subitem 1" />
                                              <MenuItem Header="Subitem 2" />
                                              <MenuItem Header="Subitem 3" />
                                          </MenuItem>
                                          <MenuItem Header="Item 2" InputGesture="Ctrl+A" />
                                          <MenuItem Header="Item 3" />
                                      </MenuFlyout>
                                  </SplitButtonControl.Flyout>
                              </SplitButtonControl>
                           </RibbonGroupBox>
                       </RibbonTab.Groups>
                   </RibbonTab>
                   <RibbonTab Header="Galleries" KeyTip.KeyTipKeys="G">
                       <RibbonTab.Groups>
                           <RibbonGroupBox Header="Large gallery" Command="{Binding OnClickCommand}" KeyTip.KeyTipKeys="L">
                                <Gallery Theme="{StaticResource LargeGallery}">
                                  <GalleryItem Content="Item 1">
                                      <GalleryItem.LargeIcon>
                                          <ControlTemplate>
                                              <Rectangle
                                                  Width="32"
                                                  Height="32"
                                                  Fill="Red" />
                                          </ControlTemplate>
                                      </GalleryItem.LargeIcon>
                                  </GalleryItem>
                                  <GalleryItem Content="Item 2">
                                      <GalleryItem.LargeIcon>
                                          <ControlTemplate>
                                              <Rectangle
                                                  Width="32"
                                                  Height="32"
                                                  Fill="OrangeRed" />
                                          </ControlTemplate>
                                      </GalleryItem.LargeIcon>
                                  </GalleryItem>
                                </Gallery>
                           </RibbonGroupBox>
                       </RibbonTab.Groups>
                   </RibbonTab>
               </Ribbon>
       ```

## Change Log

### Update (21/01/2025)

- Convert RibbonMenu into modern backstage control with different grouping.
- New Gallary Item with three sizes.
- New Cross-Platform Demo projects.
- Handle ribbon control size change when resize.

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
- `Ribbon` can now be collapsed or expanded (shows selected groups temporarily in a `Popup` when a tab is clicked if the
  `Ribbon` is collapsed)
- `Ribbon` can now be horizontally or vertically oriented (real-time value changes are not yet fully functional, but
  compile-time/startup-time changes should work without a hitch)
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

Please acknowledge Splitwirez for all the last new features added to the ribbon. As far as I'm concerned, I'm just
centralizing code and updating the nuget.

You can also acknowledge Rubal Walia as well for cleaning up all my messy code.

