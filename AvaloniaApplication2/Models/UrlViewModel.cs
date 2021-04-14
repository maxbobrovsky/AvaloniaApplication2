using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.Models
{
    internal class UrlViewModel : INotifyPropertyChanged
    {

        private string _url;
        private string _artistName;
        private string _playlistTitle;
        private List<string> _songs;
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

        public List<string> Songs
        {
            get { return _songs; }
            set {
                _songs = value;
                OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
