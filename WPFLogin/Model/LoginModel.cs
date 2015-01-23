using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace WPFLogin.Model
{
    public class LoginModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables

        private string _userName { get; set; }
        private SecureString _password { get; set; }

        #endregion

        #region Public Variables

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserName) && Password != null && Password.Length > 0;
        }

        public void Reset()
        {
            UserName = null;
            Password = null;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "UserName")
                {
                    if (string.IsNullOrEmpty(UserName))
                        result = "Please enter a User Name";
                }
                if (columnName == "Password")
                {
                    if (Password == null || (Password != null && Password.Length == 0))
                        result = "Please enter a Password";
                }
                return result;
            }
        }

        public string Error { get; private set; }
    }
}
