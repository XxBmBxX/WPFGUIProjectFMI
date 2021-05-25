using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WPFGUIProjectFMI
{
    public class CreateCurrentPlayer
    {

        static string[] propertyName = new string[13] { "Tomatoes", "Potatoes", "Aples", "Bananas", "Oranges", "Strawberries", "Beans", "Kiwi", "Pears", "Cucumbers", "Watermelons", "Cantaloupes", "Money" };

        /// <summary>
        /// Reads the player data from the database
        /// </summary>
        /// <param name="username"></param>
        public static void CreateJsonPlayer(string username)
        {
            Dictionary<string, string> userCredentials = new Dictionary<string, string>();
            Dictionary<string, float> userGamedata = new Dictionary<string, float>();
            MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
            loginConnection.Open();
            MySqlDataReader userCredentialsReader = new MySqlCommand("SELECT * FROM Accounts WHERE username = '" + username + "'", loginConnection).ExecuteReader();
                userCredentialsReader.Read();
                userCredentials.Add("username", userCredentialsReader.GetString("username"));
                userCredentials.Add("email", userCredentialsReader.GetString("email"));
            userCredentialsReader.Close();
            MySqlDataReader userGamedataReader = new MySqlCommand("SELECT * FROM gameData WHERE username = '" + username + "'", loginConnection).ExecuteReader();
                userGamedataReader.Read();
            userCredentials.Add("picture", userGamedataReader.GetString("picture"));
            userCredentials.Add("autosave", userGamedataReader.GetString("autosave"));
            foreach (string property in propertyName)
            {
                userGamedata.Add(property, userGamedataReader.GetInt32(property));
            }
                userGamedataReader.Close();
            loginConnection.Close();

            SetPlayerInfo(userCredentials, userGamedata);
        }


        /// <summary>
        /// Sets the current player info
        /// </summary>
        /// <param name="usrCred"></param>
        /// <param name="userGame"></param>
        private static void SetPlayerInfo(Dictionary<string, string> usrCred, Dictionary<string, float> userGame)
        {
            #region Set Player Credentials

            CombinedViewModel.Username = usrCred["username"];
            CombinedViewModel.Email = usrCred["email"];
            CombinedViewModel.ImageSourceString = usrCred["picture"];
            SettingsViewModel.ImageSource = usrCred["picture"];
            if (usrCred["autosave"] == "True")
            {
                SettingsViewModel.AutoSaveChecked = true;
                CombinedViewModel.AutoSave = true;
            }

            #endregion

            #region Set Player Current Stock  

            Type myType = typeof(CombinedViewModel);
            int CurrentStorage = 25000;
            foreach (string property in propertyName)
            {
                PropertyInfo setProperty = myType.GetProperty(property);
                setProperty.SetValue(setProperty, (int)userGame[property]);
                if (property != "Money")
                {
                    CurrentStorage -= (int)userGame[property];
                }              
            }
            SettingsViewModel.Email = usrCred["email"];
            SettingsViewModel.Username = usrCred["username"];
            CombinedViewModel.CurrentStorage = CurrentStorage;
            CombinedViewModel.FreeSpace = CurrentStorage;
            #endregion
        }

    }
}
