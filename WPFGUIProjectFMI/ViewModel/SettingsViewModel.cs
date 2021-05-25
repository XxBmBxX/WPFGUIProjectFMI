using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {


        /// <summary>
        /// Private members
        /// </summary>
        #region Private Members

        private static string _ImageSource;
        private static string _Email;
        private static string _Username;
        private static bool _AutoSaveChecked = false;
        private static bool _OverlayWindowSettings = false;


        /// <summary>
        /// Static property notifier
        /// </summary>
        /// <param name="propertyName"></param>
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// Public members
        /// </summary>
        #region Public Members

        public static bool OverlayWindowSettings { get => _OverlayWindowSettings; set { _OverlayWindowSettings = value; NotifyStaticPropertyChanged("OverlayWindowSettings"); } }
        public bool SavingIsRunning { get; set; }
        public static bool AutoSaveChecked { get => _AutoSaveChecked; set { _AutoSaveChecked = value; NotifyStaticPropertyChanged("AutoSaveChecked"); } }
        public static string Username { get => _Username; set { _Username = value; NotifyStaticPropertyChanged("Username"); } }
        public static string Email { get => _Email; set { _Email = value; NotifyStaticPropertyChanged("Email"); } }
        public static string ImageSource { get => _ImageSource; set { _ImageSource = value; NotifyStaticPropertyChanged("ImageSource"); } }
        public int TitleHeight { get; set; } = 40;
        public int WindowCornerRadius { get; set; } = 8;
        public static string Message { get; set; }
        public static string Title { get; set; }
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        #endregion


        /// <summary>
        /// View model commands and constructor
        /// </summary>
        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand ChosePicture { get; set; }
        public ICommand SaveGame { get; set; }
        public ICommand TurnAutoSave { get; set; } 

        /// <summary>
        /// Consctructor
        /// </summary>
        /// <param name="window"></param>
        public SettingsViewModel(SettingsWindow window)
        {
            CloseCommand = new RelayCommand(() => WindowClose(window));
            ChosePicture = new RelayCommand(SelectPicture);
            SaveGame = new RelayCommand(async () => await SaveGameAsync());
            TurnAutoSave = new RelayCommand(TurnAutoSaveCommand);
        }

        public void WindowClose(SettingsWindow window)
        {
            WindowViewModel.OverlayWindow = false;
            window.Close();
        }

        /// <summary>
        /// Saves tha game async
        /// </summary>
        /// <returns></returns>
        public async Task SaveGameAsync()
        {
            await RunCommand(() => SavingIsRunning, async () =>
            {
                await Task.Run(() => LoginRegisterCalls.SaveGameAsync());
            });
        }

        public void TurnAutoSaveCommand()
        {
            AutoSaveChecked ^= true;
            CombinedViewModel.AutoSave ^= true;
        }
        public void SelectPicture()
        {
            OpenFileDialog chosePicture = new OpenFileDialog();
            chosePicture.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (chosePicture.ShowDialog() == true)
            {
                if (Directory.Exists(@"ProfileImages"))
                {
                    if (File.Exists(Path.Combine(@"ProfileImages", Path.GetFileName(chosePicture.FileName))) && Path.GetFileName(chosePicture.FileName) == ImageSource)
                        return;
                    File.Copy(chosePicture.FileName, Path.Combine(@"ProfileImages", Path.GetFileName(chosePicture.FileName)), true);
                    CombinedViewModel.ImageSourceString = Path.GetFileName(chosePicture.FileName);
                    ImageSource = Path.GetFileName(chosePicture.FileName);
                }
                else
                {
                    Directory.CreateDirectory(@"ProfileImages");
                    File.Copy(chosePicture.FileName, Path.Combine(@"ProfileImages", Path.GetFileName(chosePicture.FileName)), true);
                    CombinedViewModel.ImageSourceString = Path.GetFileName(chosePicture.FileName);
                    ImageSource = Path.GetFileName(chosePicture.FileName);
                }                              
            }
        }
            
        #endregion
    }
}
