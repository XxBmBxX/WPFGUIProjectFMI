using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class SellPage : BasePage<CombinedViewModel>
    {
        public SellPage()
        {
            InitializeComponent();
        }
        public void IsTextAllowed(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
