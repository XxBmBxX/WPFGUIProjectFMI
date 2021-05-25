using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUIProjectFMI
{
    public class UserCredentials
    {
        #region Private Members
        private static string name;
        private static string email;
        #endregion

        #region Public Members
        public static string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public static string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        #endregion
    }
}
