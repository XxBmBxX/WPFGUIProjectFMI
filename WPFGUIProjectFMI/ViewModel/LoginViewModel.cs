using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    public class LoginViewModel : BaseViewModel
    {

        /// <summary>
        /// Login view model public properties
        /// </summary>
        #region Public Members

        public string Username { get; set; }
        public bool LoginIsRunning { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        #endregion

        /// <summary>
        /// Login view Tasks and commands
        /// </summary>
        #region Tasks and commands

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(RegisterAsync);
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommand(() => LoginIsRunning, async () =>
            {
                string user = Username;
                string pass = (parameter as IHavePassword).SecurePassword.Unsecure();
                if (await Task.Run(() => LoginRegisterCalls.LoginAsync(user, pass)))
                {
                    CombinedViewModel.SetPrices();
                    CombinedViewModel.Timer();
                    WindowViewModel.CurrentPage = ApplicationPage.StoragePage;
                    CombinedViewModel.ProfileVisibility = true;                    
                }
            });
        }

        /// <summary>
        /// Sends the user to the register page
        /// </summary>
        public void RegisterAsync()
        {           
            WindowViewModel.CurrentPage = ApplicationPage.Register;
        }
        #endregion
    }
}
