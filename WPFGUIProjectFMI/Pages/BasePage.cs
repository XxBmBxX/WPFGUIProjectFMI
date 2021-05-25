using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFGUIProjectFMI
{
    public class BasePage : Page
    {
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;
        public float SlideSeconds { get; set; } = 0.6f;
        public bool ShouldAnimateOut { get; set; }

        public BasePage()
        {
            // If we are animating in, hide to begin with
            if (PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            Loaded += BasePage_LoadedAsync;
        }
        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // If we are setup to animate out on load
            if (ShouldAnimateOut)
                // Animate out the page
                await AnimateOutAsync(sender as Page);
            // Otherwise...
            else
                // Animate the page in
                await AnimateInAsync(sender as Page);
        }

        public async Task AnimateInAsync(Page page)
        {
            // Make sure we have something to do
            if (PageLoadAnimation == PageAnimation.None)
                return;

            switch (page.Name)
            {
                case "LoginRegister":
                    // Start the animation
                    await this.SlideAndFadeInFromRightAsync(SlideSeconds);
                    break;
                case "GamePages":
                    // Start the animation
                    await this.SlideAndFadeInFromDownAsync(SlideSeconds);
                    break;
            }
        }

        public async Task AnimateOutAsync(Page page)
        {
            // Make sure we have something to do
            if (PageUnloadAnimation == PageAnimation.None)
                return;
            switch (page.Name)
            {
                case "LoginRegister":
                    // Start the animation
                    await this.SlideAndFadeOutToLeftAsync(SlideSeconds);
                    break;
                case "GamePages":
                    // Start the animation
                    await this.SlideAndFadeOutToUpAsync(SlideSeconds);
                    break;
            }
        }
    }

    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        private VM mViewModel;
        public VM ViewModel
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Set the data context for this page
                DataContext = mViewModel;
            }
        }
        public BasePage() : base()
        {
            // Create a default view model
            ViewModel = new VM();
        }
    }
}