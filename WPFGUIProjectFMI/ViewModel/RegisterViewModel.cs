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
    public class RegisterViewModel : BaseViewModel
    {
        #region Public Members
        public string Email { get; set; }

        public string Username { get; set; }
        public SecureString Password { get; set; }
        public bool LoginIsRunning { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Tasks and commands
        public RegisterViewModel()
        {
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }
        public async Task RegisterAsync(object parameter)
        {
            await RunCommand(() => LoginIsRunning, async () =>
            {
                string email = Email;
                string user = Username;
                string pass = (parameter as IHavePassword).SecurePassword.Unsecure();
                await Task.Run(() => Register(user, pass, email));
            });
        }
        public void Register(string username, string password, string email)
        {
            try
            {
                MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_XxBmBxX;password=19072001qwerty123;database=freedbtech_guiproject");
                loginConnection.Open();
                MySqlCommand checkUsername = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + username + "' ", loginConnection);
                MySqlCommand checkEmail = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + username + "' ", loginConnection);

                int resultUsername = Convert.ToInt32(checkUsername.ExecuteScalar());
                int resultEmail = Convert.ToInt32(checkUsername.ExecuteScalar());
                if (resultEmail > 0 && resultUsername > 0)
                {
                    MessageBox.Show("Username or/and Email already exists");
                }
                else
                {
                    new MySqlCommand("INSERT INTO freedbtech_guiproject.Accounts(username,password,email) VALUES('" + username + "','" + password + "','" + email + "')", loginConnection).ExecuteNonQuery();
                }
                loginConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public async Task LoginAsync()
        {
            ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.Login;
            await Task.Delay(1);
        }
        #endregion
    }
}