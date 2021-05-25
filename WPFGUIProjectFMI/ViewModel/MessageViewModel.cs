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
    public class MessageViewModel : BaseViewModel
    {

        /// <summary>
        /// The title of the message box
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The message inside the message box
        /// </summary>
        public string Message { get; set; }

    }
}
