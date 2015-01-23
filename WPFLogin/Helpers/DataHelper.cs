using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using WPFLogin.Model;

namespace WPFLogin.Helpers
{
    public class DataHelper
    {
        private static IEnumerable<TestData> Get()
        {
            return new List<TestData>
            {
                new TestData {Username = "test", Password = "test"},
                new TestData {Username = "test1", Password = "test1"},
                new TestData {Username = "test2", Password = "test2"},
                new TestData {Username = "test3", Password = "test3"},
                new TestData {Username = "test4", Password = "test4"}
            };
        }

        #region Private Methods

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion

        public bool Authenticate(LoginModel authModel)
        {
            return Get().Any(user => user.Username.ToLower().Equals(authModel.UserName.ToLower()) && user.Password == ConvertToUnsecureString(authModel.Password));
        }
    }

    public class TestData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
