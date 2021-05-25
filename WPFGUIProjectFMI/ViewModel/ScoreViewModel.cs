using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class ScoreViewModel : BaseViewModel
    {

        /// <summary>
        /// Window view model public properties
        /// </summary>
        #region Public Members

        public bool ScoreBoardUpdating { get; set; }
        
        #endregion

        /// <summary>
        /// Window view model commands
        /// </summary>
        #region Commands

        public ICommand CloseCommand { get; set; }

        public ICommand UpdateScore { get; set; }

        /// <summary>
        /// Constructor of the window view model
        /// </summary>
        /// <param name="window"></param>
        public ScoreViewModel(ScoreWindow window)
        {
            CloseCommand = new RelayCommand(() => WindowClose(window));
            UpdateScore = new RelayCommand(async() => await UpdateScoreAsync());

        }

        /// <summary>
        /// The command that closes the window
        /// </summary>
        public void WindowClose(ScoreWindow window)
        {
            WindowViewModel.OverlayWindow = false;
            window.Close();
        }


        /// <summary>
        /// The command for updating the score
        /// </summary>
        /// <returns></returns>
        public async Task UpdateScoreAsync()
        {
            await RunCommand(() => ScoreBoardUpdating, async () =>
            {
                await Task.Run(() => GetList());
            });
        }

        /// <summary>
        /// Gets the list from the database
        /// </summary>
        /// <returns></returns>
        public Task GetList()
        {
            
            ScoreBoardUpdating = true;
            List<PlayerScoreViewModel> players = new List<PlayerScoreViewModel>();
            MySqlConnection loginConnection = new MySqlConnection("datasource=freedb.tech;port=3306;username=freedbtech_loginUser;password=19072001qwerty123;database=freedbtech_guiproject");
            loginConnection.Open();
            MySqlDataReader userGamedataReader = new MySqlCommand("SELECT * FROM gameData ORDER BY Money DESC", loginConnection).ExecuteReader();
            while (userGamedataReader.Read())
            {
                players.Add(new PlayerScoreViewModel
                {
                    Username = userGamedataReader.GetString("username"),
                    Money = userGamedataReader.GetFloat("Money"),
                });

            }
            ScoreBoardScrollViewModel.Items = new System.Collections.ObjectModel.ObservableCollection<PlayerScoreViewModel>(players);
            loginConnection.Close();
            ScoreBoardUpdating = false;
            return Task.CompletedTask;
        }
        #endregion

    }
}
