using System.Windows.Controls;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// Interaction logic for ProfileUserControl.xaml
    /// </summary>
    public partial class ProfileUserControl : UserControl
    {
        public ProfileUserControl()
        {
            InitializeComponent();
            DataContext = new CombinedViewModel();
        }
    }
}
