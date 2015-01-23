using System.Windows.Input;
using WPFLogin.Command;
using WPFLogin.Helpers;
using WPFLogin.Model;

namespace WPFLogin.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Constants

        const string Success = "Success";
        const string Fail = "Fail";
        const string PleaseLogin = "Please Login";

        #endregion

        #region Private Variables

        private string _loginStatus;
        private bool _failedLoginAttempt;
        private readonly DataHelper _dataHelper;

        #endregion

        #region Public Variables

        public bool FailedLoginAttempt
        {
            get { return _failedLoginAttempt; }
            set
            {
                _failedLoginAttempt = value;
                RaisePropertyChanged();
            }
        }

        public string LoginStatus
        {
            get { return _loginStatus; }
            set
            {
                _loginStatus = value;
                RaisePropertyChanged();
            }
        }

        public LoginModel AuthModel { get; private set; }

        #endregion

        #region Commands

        private RelayCommand _loginCommand;

        public ICommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new RelayCommand(Login_Executed, (o) => AuthModel.IsValid())); }
        }

        private void Login_Executed(object obj)
        {
            FailedLoginAttempt = false;
            if (_dataHelper.Authenticate(AuthModel))
            {
                LoginStatus = Success;
            }
            else
            {
                LoginStatus = Fail;
                FailedLoginAttempt = true;
            }
        }

        private RelayCommand _resetCommand;

        public ICommand ResetCommand
        {
            get { return _resetCommand ?? (_resetCommand = new RelayCommand(Reset_Executed, (o) => !string.IsNullOrWhiteSpace(AuthModel.UserName) || (AuthModel.Password != null) && AuthModel.Password.Length > 0)); }
        }

        private void Reset_Executed(object obj)
        {
            AuthModel.Reset();
            LoginStatus = PleaseLogin;
        }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            AuthModel = new LoginModel();
            LoginStatus = PleaseLogin;
            _dataHelper = new DataHelper();
        }

        #endregion

        #region Overrided Members

        public override string WindowTitle
        {
            get
            {
                return "Login";
            }
        }

        #endregion
    }
}
