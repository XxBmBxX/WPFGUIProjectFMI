using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGUIProjectFMI
{
    public class ShowMessageBox
    {

        /// <summary>
        /// Defines the error
        /// </summary>
        /// <param name="error"></param>
        public static void DefineError(string error)
        {
            switch (error)
            {
                case "InvalidEmail":
                    InvalidEmail();
                    break;
                case "WrongPassword":
                    WrongPassword();
                    break;
                case "WrongUsername":
                    WrongUsername();
                    break;
                case "MoneyStorage":
                    MoneyOrStorage();
                    break;
                case "AlreadyExists":
                    AlreadyExists();
                    break;
                case "UnsoldItems":
                    UnsoldItems();
                    break;
                case "LatinCharacters":
                    LatinCharacters();
                    break;
                case "EmptyText":
                    EmptyString();
                    break;
                default:
                    break;
            }
        }

        #region Error catchers

        /// <summary>
        /// Invalid email error
        /// </summary>
        private static void InvalidEmail()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Невалиден И-мейл.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Wrong password error
        /// </summary>
        private static void WrongPassword()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Грешна парола.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Wrong password error
        /// </summary>
        private static void WrongUsername()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Грешно потребителско име.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Wrong password error
        /// </summary>
        private static void MoneyOrStorage()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Недостатъчно място или пари.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Wrong password error
        /// </summary>
        private static void AlreadyExists()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Това Име или И-мейл вече съществува.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Unsold items
        /// </summary>
        private static void UnsoldItems()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Някои продукти не бяха продадени поради недостатъчно количество.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Latin characters
        /// </summary>
        private static void LatinCharacters()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Името и паролата трябва да са на латиница.",
                },
            Application.Current.MainWindow);
            });
        }

        /// <summary>
        /// Empty string error
        /// </summary>
        private static void EmptyString()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CreateMessageBox(new MessageViewModel()
                {
                    Title = "Опа",
                    Message = "Моля попълнете полетета.",
                },
            Application.Current.MainWindow);
            });
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// The methods that creates the message box
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="ownerWindow"></param>
        private static void CreateMessageBox(MessageViewModel viewModel, Window ownerWindow)
        {
            CustomMessageBox windowPopup = new CustomMessageBox
            {
                Owner = ownerWindow,
                DataContext = viewModel
            };
            windowPopup.ShowDialog();
        }

        /// <summary>
        /// The method that shows unhandled errors
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="ownerWindow"></param>
        public static void UnhandledError(MessageViewModel viewModel, Window ownerWindow)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CustomMessageBox windowPopup = new CustomMessageBox
                {
                    Owner = ownerWindow,
                    DataContext = viewModel
                };
                windowPopup.ShowDialog();
            });           
        }

        #endregion
    }
}
