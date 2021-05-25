using System.Windows;
using System.Windows.Controls;

namespace WPFGUIProjectFMI
{
    public class NavigationHistoryClear : BaseAttachedProperty<NavigationHistoryClear, bool>
    {
        /// <summary>
        /// Clears the navigation history and hides the navigaion bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = (sender as Frame);
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }

    }
}
