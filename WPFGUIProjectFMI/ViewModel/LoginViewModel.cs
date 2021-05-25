using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    public class LoginViewModel : BaseViewModel
    {
        #region Public Members

        public string Username { get; set; }
        public SecureString Password { get; set; }

        public bool LoginIsRunning { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Tasks and commands
        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }        
        public async Task LoginAsync(object parameter)
        {
            await RunCommand(() => LoginIsRunning, async () =>
            {
                string user = Username;
                string pass = (parameter as IHavePassword).SecurePassword.Unsecure();               
                if (await Task.Run(() => Login(user, pass)))
                {
                    ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.ProfilePage;
                }
            });
        }
        public Task<bool> Login(string username, string password)
        {
            try
            {
                MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_XxBmBxX;password=19072001qwerty123;database=freedbtech_guiproject");
                loginConnection.Open();
                MySqlCommand checkCredentials = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + username + "'AND password ='" +password+ "' ", loginConnection);
                int resultCheck = Convert.ToInt32(checkCredentials.ExecuteScalar());
                if (resultCheck > 0)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    MessageBox.Show("Invalid user credentials");                   
                }
                loginConnection.Close();
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public async Task RegisterAsync()
        {
            ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.Register;
            await Task.Delay(1);
        }
        #endregion
    }
}
