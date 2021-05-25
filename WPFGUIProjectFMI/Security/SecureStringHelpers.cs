using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WPFGUIProjectFMI
{
    public static class SecureStringHelpers
    {
        /// <summary>
        /// The method that unsecures the secure string
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;
            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}