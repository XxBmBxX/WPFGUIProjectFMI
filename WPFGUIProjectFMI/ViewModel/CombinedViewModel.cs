using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPFGUIProjectFMI
{
    public class CombinedViewModel : BaseViewModel
    {
        /// <summary>
        /// Profile page and storage view model
        /// </summary>
        #region Profile View Model

        #region Private Members
        private static bool TimerStarted = false;
        private static float _Money;
        private static int _FreeSpace = 25000;
        private static int _CurrentStorage = 25000;
        private static float _CurrentReceipt;
        private static int _TimeToPrices = 120;
        private static string _Email;
        private static string _Username;
        private static bool _ProfileVisibility = false;
        private static bool _AutoSave = false;
        private static int _ProfileSideMenuWidth = 285;
        private static string _ImageSourceString;
        #endregion

        /// <summary>
        /// Members for the storage page
        /// </summary>
        #region Public Storage Members

        public static int Tomatoes { get; set; }
        public static int Potatoes { get; set; }
        public static int Bananas { get; set; }
        public static int Oranges { get; set; }
        public static int Aples { get; set; }
        public static int Strawberries { get; set; }
        public static int Beans { get; set; }
        public static int Kiwi { get; set; }
        public static int Pears { get; set; }
        public static int Cucumbers { get; set; }
        public static int Watermelons { get; set; }
        public static int Cantaloupes { get; set; }

        #endregion

        /// <summary>
        /// The main members for the view
        /// </summary>
        #region Public Members     

        /// <summary>
        /// Width calc for the side menu
        /// </summary>
        public static int ProfileSideMenuWidth { get => _ProfileSideMenuWidth; set { _ProfileSideMenuWidth = value; NotifyStaticPropertyChanged("ProfileSideMenuWidth"); } }

        /// <summary>
        /// The visibility property for the profile user control
        /// </summary>
        public static bool ProfileVisibility { get => _ProfileVisibility; set { _ProfileVisibility = value; NotifyStaticPropertyChanged("ProfileVisibility"); } }

        /// <summary>
        /// Everything related to the page look
        /// </summary>
        public static bool AutoSave { get => _AutoSave; set { _AutoSave = value; NotifyStaticPropertyChanged("AutoSave"); } }
        public static string ImageSourceString { get => _ImageSourceString; set { _ImageSourceString = value; NotifyStaticPropertyChanged("ImageSourceString"); } }
        public static string Username { get => _Username; set { _Username = value; NotifyStaticPropertyChanged("Username"); } }
        public static string Email { get => _Email; set { _Email = value; NotifyStaticPropertyChanged("Email"); } }
        public static float Money { get => _Money; set { _Money = value; NotifyStaticPropertyChanged("Money"); } }
        public int TitleHeight { get; set; } = 42;
        public bool LoginOut { get; set; }

        /// <summary>
        /// The boolean for autosave
        /// </summary>
        public static bool SavingIsRunningProfile { get => savingIsRunning; set {savingIsRunning = value; NotifyStaticPropertyChanged("SavingIsRunningProfile"); } }

        /// <summary>
        /// The boolean for SaveCommand
        /// </summary>
        public bool SavingIsRunningPublic { get; set; }
        public int CornerRadius { get; set; } = 15;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight); } }

        #endregion

        #endregion

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
        /// The main view model
        /// </summary>
        #region Shop View Model

        #region Timers and Autosave

        /// <summary>
        /// Timer constructor
        /// </summary>
        public static void Timer()
        {
            if (TimerStarted == false)
            {
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += Ticker;
                dt.Start();
                TimerStarted = true;
            }
        }

        /// <summary>
        /// Timer ticker method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Ticker(object sender, EventArgs e)
        {
            if (TimeToPrices != 0)
            {
                TimeToPrices--;
            }
            else
            {
                SetPrices();
                TimeToPrices = 120;
                TimeToPrices--;
                if (AutoSave)
                {
                    Task.Run(() => SaveGameMethod());
                }
            }
        }

        /// <summary>
        /// The timer property that binds to the view
        /// </summary>
        public static int TimeToPrices
        {
            get
            {
                return _TimeToPrices;
            }
            set
            {
                _TimeToPrices = value;
                NotifyStaticPropertyChanged("TimeToPrices");
            }
        }

        /// <summary>
        /// The method that runs AutoSave
        /// </summary>
        /// <returns></returns>
        public static async Task SaveGameMethod()
        {
            SavingIsRunningProfile = true;
            await Task.Run(() => LoginRegisterCalls.SaveGameAsync());
            SavingIsRunningProfile = false;
        }

        #endregion

        #region Stock price Private Members
        private static float _priceTomatoes;
        private static float _pricePotatoes;
        private static float _priceBananas;
        private static float _priceOranges;
        private static float _priceAples;
        private static float _priceStrawberries;
        private static float _priceBeans;
        private static float _priceKiwi;
        private static float _pricePears;
        private static float _priceCucumbers;
        private static float _priceWatermelons;
        private static float _priceCantaloupes;

        #endregion

        #region Stock price Public Members
        public static float priceTomatoes { get => _priceTomatoes; set { _priceTomatoes = value; NotifyStaticPropertyChanged("priceTomatoes"); } }
        public static float pricePotatoes { get => _pricePotatoes; set { _pricePotatoes = value; NotifyStaticPropertyChanged("pricePotatoes"); } }
        public static float priceBananas { get => _priceBananas; set { _priceBananas = value; NotifyStaticPropertyChanged("priceBananas"); } }
        public static float priceOranges { get => _priceOranges; set { _priceOranges = value; NotifyStaticPropertyChanged("priceOranges"); } }
        public static float priceAples { get => _priceAples; set { _priceAples = value; NotifyStaticPropertyChanged("priceAples"); } }
        public static float priceStrawberries { get => _priceStrawberries; set { _priceStrawberries = value; NotifyStaticPropertyChanged("priceStrawberries"); } }
        public static float priceBeans { get => _priceBeans; set { _priceBeans = value; NotifyStaticPropertyChanged("priceBeans"); } }
        public static float priceKiwi { get => _priceKiwi; set { _priceKiwi = value; NotifyStaticPropertyChanged("priceKiwi"); } }
        public static float pricePears { get => _pricePears; set { _pricePears = value; NotifyStaticPropertyChanged("pricePears"); } }
        public static float priceCucumbers { get => _priceCucumbers; set { _priceCucumbers = value; NotifyStaticPropertyChanged("priceCucumbers"); } }
        public static float priceWatermelons { get => _priceWatermelons; set { _priceWatermelons = value; NotifyStaticPropertyChanged("priceWatermelons"); } }
        public static float priceCantaloupes { get => _priceCantaloupes; set { _priceCantaloupes = value; NotifyStaticPropertyChanged("priceCantaloupes"); } }

        #endregion

        #region SpaceAndStockPriceSetter
        public static void SetPrices()
        {
            priceTomatoes = StockPrice();
            pricePotatoes = StockPrice();
            priceBananas = StockPrice();
            priceOranges = StockPrice();
            priceAples = StockPrice();
            priceStrawberries = StockPrice();
            priceBeans = StockPrice();
            priceKiwi = StockPrice();
            pricePears = StockPrice();
            priceCucumbers = StockPrice();
            priceWatermelons = StockPrice();
            priceCantaloupes = StockPrice();
        }

        private static float[] prices = new float[] { 2.17f, 2.11f, 3.26f, 2.15f, 2.13f, 2.19f, 2.33f, 3.23f, 4.53f, 2.04f, 2.08f, 3.07f, 2.14f, 4.35f, 2.28f, 3.24f, 3.25f, 4.18f };

        static Random randomPrice = new Random();

        private static float StockPrice()
        {
            return prices[randomPrice.Next(0, 17)];
        }

        public static int FreeSpace { get => _FreeSpace; set { _FreeSpace = value; NotifyStaticPropertyChanged("FreeSpace"); } }
        public static int CurrentStorage { get => _CurrentStorage; set { _CurrentStorage = value; FreeSpaceCalcShop(); FreeSpaceCalcShop(); NotifyStaticPropertyChanged("CurrentStorageSell"); } }
        public static float CurrentReceipt { get => _CurrentReceipt; set { _CurrentReceipt = value; NotifyStaticPropertyChanged("CurrentReceipt"); } }

        #endregion

        /// <summary>
        /// Variables for the shop methods
        /// </summary>
        #region Public Shop Members

        public static int shopTomatoes { get => _shopTomatoes; set { _shopTomatoes = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopTomatoes"); } }
        public static int shopPotatoes { get => _shopPotatoes; set { _shopPotatoes = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopPotatoes"); } }
        public static int shopBananas { get => _shopBananas; set { _shopBananas = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopBananas"); } }
        public static int shopOranges { get => _shopOranges; set { _shopOranges = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopOranges"); } }
        public static int shopAples { get => _shopAples; set { _shopAples = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopAples"); } }
        public static int shopStrawberries { get => _shopStrawberries; set { _shopStrawberries = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopStrawberries"); } }
        public static int shopBeans { get => _shopBeans; set { _shopBeans = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopBeans"); } }
        public static int shopKiwi { get => _shopKiwi; set { _shopKiwi = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopKiwi"); } }
        public static int shopPears { get => _shopPears; set { _shopPears = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopPears"); } }
        public static int shopCucumbers { get => _shopCucumbers; set { _shopCucumbers = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopCucumbers"); } }
        public static int shopWatermelons { get => _shopWatermelons; set { _shopWatermelons = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopWatermelons"); } }
        public static int shopCantaloupes { get => _shopCantaloupes; set { _shopCantaloupes = value; FreeSpaceCalcShop(); NotifyStaticPropertyChanged("shopCantaloupes"); } }

        #endregion

        #region Private Shop Members
        private static int _shopTomatoes;
        private static int _shopPotatoes;
        private static int _shopBananas;
        private static int _shopOranges;
        private static int _shopAples;
        private static int _shopStrawberries;
        private static int _shopBeans;
        private static int _shopKiwi;
        private static int _shopPears;
        private static int _shopCucumbers;
        private static int _shopWatermelons;
        private static int _shopCantaloupes;

        #endregion

        #endregion

        /// <summary>
        /// All commands for both pages
        /// </summary>
        #region Commands

        /// <summary>
        /// All the commands that the view can bind to
        /// </summary>
        public ICommand LogOut { get; set; }
        public ICommand BuyPage { get; set; }
        public ICommand SellPage { get; set; }
        public ICommand StoragePage { get; set; }
        public ICommand SellStock { get; set; }
        public ICommand BuyStock { get; set; }
        public ICommand OpenSettings { get; set; }
        public ICommand ChangeName { get; set; }
        public ICommand ChangeEmail { get; set; }
        public ICommand SaveGame { get; set; }
        public ICommand OpenScore { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CombinedViewModel()
        {
            BuyPage = new RelayCommand(BuyPageAsync);
            SellPage = new RelayCommand(SellPageAsync);
            StoragePage = new RelayCommand(StoragePageAsync);
            LogOut = new RelayCommand(async () => await LogOutAsync());
            SaveGame = new RelayCommand(async () => await SaveGameAsync());
            BuyStock = new RelayCommand(BuyStockAsync);
            SellStock = new RelayCommand(SellStockAsync);
            OpenSettings = new RelayCommand(OpenSettingsAsync);
            ChangeName = new RelayCommand(async () => await ChangeNameAsync());
            ChangeEmail = new RelayCommand(async () => await ChangeEmailAsync());
            OpenScore = new RelayCommand(OpenScoreAsync);
        }

        /// <summary>
        /// Logs out the player
        /// </summary>
        /// <returns></returns>
        public async Task LogOutAsync()
        {
            await RunCommand(() => LoginOut, async () =>
            {
                await Task.Delay(2000);
                WindowViewModel.CurrentPage = ApplicationPage.Login;
                ProfileVisibility = false;
            });
        }
               
        /// <summary>
        /// Opens the scorewindow
        /// </summary>
        /// <returns></returns>
        public void OpenScoreAsync()
        {
            WindowViewModel.OverlayWindow = true;
            ScoreWindow score = new ScoreWindow();
            score.Owner = Application.Current.MainWindow;
            score.Show();
        }

        /// <summary>
        /// Gets the player to the buy page
        /// </summary>
        public void BuyPageAsync()
        {
            WindowViewModel.CurrentPage = ApplicationPage.BuyPage;
        }

        /// <summary>
        /// Gets the player to the sell page
        /// </summary>
        public void SellPageAsync()
        {
            WindowViewModel.CurrentPage = ApplicationPage.SellPage;
        }

        /// <summary>
        /// Gets the player to the storage page
        /// </summary>
        public void StoragePageAsync()
        {
            if (WindowViewModel.CurrentPage == ApplicationPage.StoragePage)
                return;
            WindowViewModel.CurrentPage = ApplicationPage.StoragePage;
            ShopMembersRestart();
        }

        /// <summary>
        /// The command for buying stock
        /// </summary>
        public void BuyStockAsync()
        {
            if (_FreeSpace >= 0 && CurrentReceipt <= Money && _FreeSpace <= 25000)
            {
                BuySellStock.BuyStock();
            }
            else
            {
                ShowMessageBox.DefineError("MoneyStorage");
            }
            ShopMembersRestart();
        }

        /// <summary>
        /// The command for selling stock
        /// </summary>
        public void SellStockAsync()
        {
            if (_FreeSpace >= 0 && _FreeSpace <= 25000)
            {
                BuySellStock.SellStock();
            }
            else
            {
                ShowMessageBox.DefineError("MoneyStorage");
            }
            ShopMembersRestart();
        }

        /// <summary>
        /// Opens up the settings menu
        /// </summary>
        public void OpenSettingsAsync()
        {
            WindowViewModel.OverlayWindow = true;
            SettingsWindow openSettings = new SettingsWindow();
            openSettings.Owner = Application.Current.MainWindow;
            openSettings.ShowDialog();
        }

        /// <summary>
        /// Saves up the current game data
        /// </summary>
        /// <returns></returns>
        public async Task SaveGameAsync()
        {
            await RunCommand(() => SavingIsRunningPublic, async () =>
            {
                await Task.Run(() => LoginRegisterCalls.SaveGameAsync());
            });
        }

        /// <summary>
        /// Method that changes name
        /// </summary>
        /// <returns></returns>
        public async Task ChangeNameAsync()
        {
            await RunCommand(() => ChangingNameIsRunning, async () =>
            {
                if (await Task.Run(() => LoginRegisterCalls.ChangeUsernameAsync(NewUsername)))
                {
                    Username = NewUsername;
                    SettingsViewModel.Username = NewUsername;
                }
            });
        }

        /// <summary>
        /// Method that changes Email
        /// </summary>
        /// <returns></returns>
        public async Task ChangeEmailAsync()
        {
            await RunCommand(() => ChangingEmailIsRunning, async () =>
            {
                if (await Task.Run(() => LoginRegisterCalls.ChangeEmailAsync(NewEmail)))
                {
                    Email = NewEmail;
                    SettingsViewModel.Email = NewEmail;
                }
            });
        }

        #endregion

        /// <summary>
        /// Calculates the free space in realtime
        /// </summary>
        #region RealTimeCalculators

        public static void FreeSpaceCalcShop()
        {
            int storageCalcHelper = _shopTomatoes
                + _shopPotatoes
                + _shopAples
                + _shopBananas
                + _shopOranges
                + _shopStrawberries
                + _shopBeans
                + _shopKiwi
                + _shopPears
                + _shopCucumbers
                + _shopWatermelons
                + _shopCantaloupes;
            if (WindowViewModel.CurrentPage == ApplicationPage.BuyPage)
            {
                FreeSpace = CurrentStorage - storageCalcHelper;
            }
            else if (WindowViewModel.CurrentPage == ApplicationPage.SellPage)
            {
                FreeSpace = CurrentStorage + storageCalcHelper;
            }
            ReceiptCacl();

        }

        /// <summary>
        /// Calculates the current receipt
        /// </summary>
        public static void ReceiptCacl()
        {
            CurrentReceipt = priceTomatoes * _shopTomatoes
                + pricePotatoes * _shopPotatoes
                + priceBananas * _shopBananas
                + priceOranges * _shopOranges
                + priceAples * _shopAples
                + priceStrawberries * _shopStrawberries
                + priceBeans * _shopBeans
                + priceKiwi * _shopKiwi
                + pricePears * _shopPears
                + priceCucumbers * _shopCucumbers
                + priceWatermelons * _shopWatermelons
                + priceCantaloupes * _shopCantaloupes;
        }

        /// <summary>
        /// Changes shop members value to 0
        /// </summary>
        public void ShopMembersRestart()
        {
            shopTomatoes = 0;
            shopPotatoes = 0;
            shopBananas = 0;
            shopOranges = 0;
            shopAples = 0;
            shopStrawberries = 0;
            shopBeans = 0;
            shopKiwi = 0;
            shopPears = 0;
            shopCucumbers = 0;
            shopWatermelons = 0;
            shopCantaloupes = 0;
        }
        #endregion


        /// <summary>
        /// View Model for the textboxes in settings
        /// </summary>
        #region TextboxSettings View Model

        /// <summary>
        /// Private textbox view members
        /// </summary>
        private static string _NewUsername;
        private static string _NewEmail;
        private static bool savingIsRunning;

        /// <summary>
        /// Public textbox view members
        /// </summary>
        public bool ChangingNameIsRunning { get; set; }
        public bool ChangingEmailIsRunning { get; set; }
        public static string NewUsername { get => _NewUsername; set { _NewUsername = value; NotifyStaticPropertyChanged("NewUsername"); } }
        public static string NewEmail { get => _NewEmail; set { _NewEmail = value; NotifyStaticPropertyChanged("NewEmail"); } }

        #endregion
    }
}
