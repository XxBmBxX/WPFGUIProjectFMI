using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    public class RegisterViewModel : BaseViewModel
    {

        /// <summary>
        /// Regsiter model public properties
        /// </summary>
        #region Public Members

        public string Email { get; set; }
        public string Username { get; set; }
        public bool LoginIsRunning { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        #endregion

        /// <summary>
        /// Register view model Tasks and commands
        /// </summary>
        #region Tasks and commands

        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterViewModel()
        {
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(LoginAsync);
        }

        /// <summary>
        /// Registers the user int
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task RegisterAsync(object parameter)
        {
            await RunCommand(() => LoginIsRunning, async () =>
            {
                string email = Email;
                string user = Username;
                string pass = (parameter as IHavePassword).SecurePassword.Unsecure();
                if (await Task.Run(() => LoginRegisterCalls.RegisterAsync(user, pass, email)))
                {
                    WindowViewModel.CurrentPage = ApplicationPage.Login;
                }
            });
        }

        /// <summary>
        /// Sends the user to the Login page
        /// </summary>
        /// <returns></returns>
        public void LoginAsync()
        {
            WindowViewModel.CurrentPage = ApplicationPage.Login;
        }
        #endregion
    }
}