using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using System.Collections.Generic;

namespace AvaloniaApplication2
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new UrlViewModel { Url = string.Empty, ArtistName = string.Empty};


#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void GetArtistDataButton_Click(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as UrlViewModel;

            PlaylistDataProcessor pdr = new PlaylistDataProcessor();
            var help_model = pdr.Process(context.Url);
            context.ArtistName = help_model.ArtistName;
            context.PlaylistTitle = help_model.PlaylistTitle;
            context.Songs = help_model.Songs;
        }
    }
}
