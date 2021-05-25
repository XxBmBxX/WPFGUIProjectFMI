using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {

        /// <summary>
        /// Window view model private properties
        /// </summary>
        #region Private Members

        private static bool _OverlayWindow = false;
        private Window mWindow;
        private int mOuterMarginSize = 10;
        private int mWindowRadius = 15;
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// Window view model public properties
        /// </summary>
        #region Public Members

        public static bool OverlayWindow { get => _OverlayWindow; set { _OverlayWindow = value; NotifyStaticPropertyChanged("OverlayWindow"); } }
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static ApplicationPage CurrentPage { get => currentPage; set { currentPage = value; NotifyStaticPropertyChanged(nameof(CurrentPage)); } }
        public bool Borderless { get { return mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked; } }
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);
        private static ApplicationPage currentPage = ApplicationPage.Login;
        
        /// <summary>
        /// Sets the profile side menu max width
        /// </summary>
        public void SetSideMenuWidth()
        {
            if (Borderless)
            {
                CombinedViewModel.ProfileSideMenuWidth = 350;              
            }
            else
            {
                CombinedViewModel.ProfileSideMenuWidth = 285;
            }
        }

        /// <summary>
        /// Calculates outer margin on window state change
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// Window border radius property
        /// </summary>
        public int WindowRadius
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// Title height of the main window
        /// </summary>
        public int TitleHeight { get; set; } = 42;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }
        #endregion

        /// <summary>
        /// Window view model commands
        /// </summary>
        #region Commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        /// <summary>
        /// Constructor of the window view model
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;
            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };
            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }
        #endregion


        /// <summary>
        /// Gets the current mouse position
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);
            if (Borderless)
            {
                return new Point(position.X, position.Y);
            }
            else
            {
                return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
            }
            // Add the window position so its a "ToScreen"

        }

        /// <summary>
        /// Fire off events for all properties that are affected by a resize
        /// </summary>
        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
            SetSideMenuWidth();
        }

    }
}
