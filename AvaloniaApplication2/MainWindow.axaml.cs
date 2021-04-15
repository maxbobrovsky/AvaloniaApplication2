using Avalonia;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using System;
using System.Windows;
using RoutedEventArgs = Avalonia.Interactivity.RoutedEventArgs;
using Window = Avalonia.Controls.Window;

namespace AvaloniaApplication2
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new UrlViewModel { Url = "Enter Url Here and press button", ArtistName = string.Empty};


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
            var urlViewModel = this.DataContext as UrlViewModel;
            var playlistDataProcessor = new PlaylistDataProcessor();

            var playlistDataResult = playlistDataProcessor.Process(urlViewModel.Url);

            if (!playlistDataResult.IsSuccess)
            {
                urlViewModel.ArtistName = string.Empty;
                urlViewModel.PlaylistSongs = Array.Empty<PlaylistWithSongs>();

                MessageBox.Show(playlistDataResult.ErrorMessage);
                return;
            }

            urlViewModel.ArtistName = playlistDataResult.ArtistName;
            urlViewModel.PlaylistSongs = playlistDataResult.Playlists;
        }
    }
}
