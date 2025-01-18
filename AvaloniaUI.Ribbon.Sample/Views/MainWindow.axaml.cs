using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

using AvaloniaUI.Ribbon.Windows;

using System;

namespace AvaloniaUI.Ribbon.Samples.Views
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //Ribbon ribbon = this.Find<Ribbon>("RibbonControl");
            Button verticalRibbonButton = this.Find<Button>("VerticalRibbonButton");
            Button horizontalRibbonButton = this.Find<Button>("HorizontalRibbonButton");
            verticalRibbonButton.Click += (sneder, args) =>
            {
                /*ribbon.Orientation = Orientation.Vertical;
                DockPanel.SetDock(ribbon, Dock.Left);*/
                Orientation = Orientation.Vertical;
                verticalRibbonButton.IsVisible = false;
                horizontalRibbonButton.IsVisible = true;
            };
            horizontalRibbonButton.Click += (sneder, args) =>
            {
                /*ribbon.Orientation = Orientation.Horizontal;
                DockPanel.SetDock(ribbon, Dock.Top);*/
                Orientation = Orientation.Horizontal;
                horizontalRibbonButton.IsVisible = false;
                verticalRibbonButton.IsVisible = true;
            };
            //this.Find<Button>("TestItemsButton").Click += (sneder, args) => this.Find<QuickAccessToolbar>("QAT").TestItems();

            /*var lightsToggleSwitch = this.Find<ToggleSwitch>("LightsToggleSwitch");
            lightsToggleSwitch.Checked += (sneder, e) => RefreshLights(FluentThemeMode.Light);
            lightsToggleSwitch.Unchecked += (sneder, e) => RefreshLights(FluentThemeMode.Dark);*/
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG

            this.AttachDevTools();
#endif
        }

        private Uri _baseUri = new Uri("avares://AvaloniaUI.Ribbon.Samples/Styles");
        /*void RefreshLights(FluentThemeMode mode)
        {
            App.Current.Styles[0] = new StyleInclude(_baseUri)
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/Base" + mode + ".xaml")
            };

            App.Current.Styles[2] = new StyleInclude(_baseUri)
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/FluentBase" + mode + ".xaml")
            };

            App.Current.Styles[3] = new StyleInclude(_baseUri)
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/Accents/FluentControlResources" + mode + ".xaml")
            };
        }*/
    }
}