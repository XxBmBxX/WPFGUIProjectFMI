using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows;
using DevOne.Security.Cryptography.BCrypt;

namespace WPFGUIProjectFMI
{
    class LoginRegisterCalls
    {

        /// <summary>
        /// The methods used for loging or registering the player
        /// </summary>
        #region Login and register functions

        /// <summary>
        /// Checks for credentials and logs in user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    if (CheckForNotLatin(username) && CheckForNotLatin(password))
                    {
                        MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
                        loginConnection.Open();
                        MySqlCommand checkCredentials = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + username + "'", loginConnection);

                        //Checks if username exists
                        if (Convert.ToInt32(checkCredentials.ExecuteScalar()) > 0)
                        {
                            MySqlDataReader userGamedataReader = new MySqlCommand("SELECT * FROM Accounts WHERE username = '" + username + "'", loginConnection).ExecuteReader();
                            userGamedataReader.Read();

                            //Checks if password matches
                            if (ValidatePassword(password, userGamedataReader.GetString("password")))
                            {
                                CreateCurrentPlayer.CreateJsonPlayer(username);
                                return Task.FromResult(true);
                            }
                            else
                            {
                                ShowMessageBox.DefineError("WrongPassword");
                            }
                        }
                        else
                        {
                            ShowMessageBox.DefineError("WrongUsername");
                        }
                        loginConnection.Close();
                    }
                    else
                    {
                        ShowMessageBox.DefineError("LatinCharacters");
                    }
                }
                else
                {
                    ShowMessageBox.DefineError("EmptyText");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Checks for credentials and registers user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Task<bool> RegisterAsync(string username, string password, string email)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(email))
            {
                if (CheckForNotLatin(username) && CheckForNotLatin(password) && CheckForNotLatin(email))
                {
                    if (IsValidEmail(email))
                    {
                        try
                        {
                            MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject;Allow User Variables=True");
                            loginConnection.Open();
                            MySqlCommand checkUsername = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + username + "' ", loginConnection);
                            MySqlCommand checkEmail = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + email + "' ", loginConnection);

                            if (Convert.ToInt32(checkUsername.ExecuteScalar()) > 0 || Convert.ToInt32(checkEmail.ExecuteScalar()) > 0)
                            {
                                ShowMessageBox.DefineError("AlreadyExists");
                            }
                            else
                            {
                                new MySqlCommand("INSERT INTO freedbtech_guiproject.Accounts(username,password,email) VALUES('" + username + "','" + HashPassword(password) + "','" + email + "')", loginConnection).ExecuteNonQuery();
                                new MySqlCommand("INSERT INTO freedbtech_guiproject.gameData(username,picture,money,autosave,tomatoes,potatoes,bananas,oranges,aples,strawberries,beans,kiwi,pears,cucumbers,watermelons,cantaloupes) " +
                                    "VALUES('" + username + "','null','10000','false','0','0','0','0','0','0','0','0','0','0','0','0')", loginConnection).ExecuteNonQuery();
                                return Task.FromResult(true);
                            }
                            loginConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        ShowMessageBox.DefineError("InvalidEmail");
                    }
                }
                else
                {
                    ShowMessageBox.DefineError("LatinCharacters");
                }
            }
            else
            {
                ShowMessageBox.DefineError("EmptyText");
            }
            return Task.FromResult(false);
        }

        #endregion

        /// <summary>
        /// Basic data validation
        /// </summary>
        #region Email and string validations

        /// <summary>
        /// Checks if the string is in latin characters
        /// </summary>
        /// <param name="stringToCheck"></param>
        /// <returns></returns>
        static bool CheckForNotLatin(string stringToCheck)
        {
            bool boolToReturn = false;
            foreach (char c in stringToCheck)
            {
                int code = (int)c;
                if ((code > 96 && code < 123) || (code > 64 && code < 91))
                    boolToReturn = true;
            }
            return boolToReturn;
        }

        /// <summary>
        /// Checks if the email is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// Password encryption with BCrypt
        /// </summary>
        #region Password encryption

        /// <summary>
        /// Encrypts the password
        /// </summary>
        /// <returns></returns>
        private static string GetRandomSalt()
        {
            return BCryptHelper.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCryptHelper.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCryptHelper.CheckPassword(password, correctHash);
        }

        #endregion

        /// <summary>
        /// The methods used to change user credentials
        /// </summary>
        #region Usercredentials change

        /// <summary>
        /// The method that tries to change username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Task<bool> ChangeUsernameAsync(string newUsername)
        {
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                try
                {
                    MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
                    loginConnection.Open();
                    MySqlCommand checkUsername = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + newUsername + "'", loginConnection);

                    //Checks if username exists
                    if (Convert.ToInt32(checkUsername.ExecuteScalar()) == 0)
                    {
                        new MySqlCommand("UPDATE freedbtech_guiproject.Accounts SET username = '" + newUsername + "' WHERE username='" + CombinedViewModel.Username + "'", loginConnection).ExecuteNonQuery();
                        new MySqlCommand("UPDATE freedbtech_guiproject.gameData SET username = '" + newUsername + "' WHERE username='" + CombinedViewModel.Username + "'", loginConnection).ExecuteNonQuery();
                        return Task.FromResult(true);
                    }
                    else
                    {
                        ShowMessageBox.DefineError("AlreadyExists");
                    }
                    loginConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                ShowMessageBox.DefineError("EmptyText");
            }          
            return Task.FromResult(false);
        }

        /// <summary>
        /// The method that changes e-mail
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public static Task<bool> ChangeEmailAsync(string newEmail)
        {
            try
            {
                MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
                loginConnection.Open();
                MySqlCommand checkEmail = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE username = '" + newEmail + "'", loginConnection);

                //Checks if username exists
                if (Convert.ToInt32(checkEmail.ExecuteScalar()) == 0)
                {
                    if (IsValidEmail(newEmail))
                    {
                        new MySqlCommand("UPDATE freedbtech_guiproject.Accounts SET email = '" + newEmail + "' WHERE email='" + CombinedViewModel.Email + "'", loginConnection).ExecuteNonQuery();
                        return Task.FromResult(true);
                    }
                    else
                    {
                        ShowMessageBox.DefineError("InvalidEmail");
                    }
                }
                else
                {
                    ShowMessageBox.DefineError("AlreadyExists");
                }
                loginConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Saves the current game
        /// </summary>
        /// <returns></returns>
        public static void SaveGameAsync()
        {
            try
            {
                MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
                loginConnection.Open();
                new MySqlCommand("UPDATE freedbtech_guiproject.gameData SET " +
                    "autosave = '" + CombinedViewModel.AutoSave + "', " +
                    "picture = '" + CombinedViewModel.ImageSourceString + "', " +
                    "Tomatoes = '" + CombinedViewModel.Tomatoes + "', " +
                    "Potatoes = '" + CombinedViewModel.Potatoes + "', " +
                    "Bananas = '" + CombinedViewModel.Bananas + "', " +
                    "Oranges = '" + CombinedViewModel.Oranges + "', " +
                    "Aples = '" + CombinedViewModel.Aples + "', " +
                    "Strawberries = '" + CombinedViewModel.Strawberries + "', " +
                    "Beans = '" + CombinedViewModel.Beans + "', " +
                    "Kiwi = '" + CombinedViewModel.Kiwi + "', " +
                    "Pears = '" + CombinedViewModel.Pears + "', " +
                    "Cucumbers = '" + CombinedViewModel.Cucumbers + "', " +
                    "Watermelons = '" + CombinedViewModel.Watermelons + "', " +
                    "Cantaloupes = '" + CombinedViewModel.Cantaloupes + "', " +
                    "Money = " + CombinedViewModel.Money +" " +                   
                    "WHERE username='" + CombinedViewModel.Username + "'", loginConnection).ExecuteNonQuery();
                loginConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
