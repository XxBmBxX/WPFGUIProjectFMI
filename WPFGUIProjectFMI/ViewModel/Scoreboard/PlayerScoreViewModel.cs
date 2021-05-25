using System;
using System.ComponentModel;

namespace WPFGUIProjectFMI
{
    public class PlayerScoreViewModel : BaseViewModel
    {
        private string _Username;
        private float _Money;
        private string _ImageSourceString;


        /// <summary>
        /// The display name of this scoreboard
        /// </summary>
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged("Username"); } }

        /// <summary>
        /// The money of the player
        /// </summary>
        public float Money { get => _Money; set { _Money = value; OnPropertyChanged("Username"); } }

        /// <summary>
        /// The picture of the player if avaible
        /// </summary>
        public string ImageSourceString { get => _ImageSourceString; set { _ImageSourceString = value; OnPropertyChanged("ImageSourceString"); } }
    }
}
 