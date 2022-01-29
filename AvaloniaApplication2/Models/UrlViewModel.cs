using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication2.Models
{
    internal class UrlViewModel : INotifyPropertyChanged
    {   
        //url to parse
        private string _url;

        public string Url
        {
            get { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _artistName;

        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                if (value != _url)
                {
                    _artistName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _playlistTitle;

        public string PlaylistTitle
        {
            get { return _playlistTitle; }
            set
            {
                if (value != _url)
                {
                    _playlistTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private IReadOnlyCollection<PlaylistWithSongs> _songs;

        public IReadOnlyCollection<PlaylistWithSongs> PlaylistSongs
        {
            get { return _songs; }
            set {
                _songs = value;
                OnPropertyChanged(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
