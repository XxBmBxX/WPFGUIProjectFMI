using System.Windows;
using System.Windows.Controls;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// Interaction logic for SettingsTextBox.xaml
    /// </summary>
    public partial class SettingsTextBox : UserControl
    {
        public SettingsTextBox()
        {
            InitializeComponent();
            DataContext = new CombinedViewModel();
        }      
    }
}
