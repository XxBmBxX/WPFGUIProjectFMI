using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace WPFGUIProjectFMI
{
    public class ScoreBoardScrollViewModel : BaseViewModel
    {

        /// <summary>
        /// Static Observable collection
        /// </summary>
        private static ObservableCollection<PlayerScoreViewModel> _Items;

        /// <summary>
        /// The static property notifier
        /// </summary>
        #region Static property notifier

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// The observavble collection we bind to
        /// </summary>
        public static ObservableCollection<PlayerScoreViewModel> Items { get =>_Items; set { _Items = value; NotifyStaticPropertyChanged("Items"); } }
    }
}
