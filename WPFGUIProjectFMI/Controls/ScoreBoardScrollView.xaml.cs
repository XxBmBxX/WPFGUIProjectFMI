using System.Windows.Controls;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// Interaction logic for ScoreBoardScrollView.xaml
    /// </summary>
    public partial class ScoreBoardScrollView : UserControl
    {
        public ScoreBoardScrollView()
        {
            InitializeComponent();
            DataContext = new ScoreBoardScrollViewModel();
        }
    }
}
