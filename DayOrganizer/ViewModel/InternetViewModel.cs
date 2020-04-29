using System.Windows.Input;

using DayOrganizer.Commons.Tools;

namespace DayOrganizer
{
    public class InternetViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private string _textBoxString = string.Empty;
        private ICommand _clearCommand;
        private ICommand _checkCommand;
        private ICommand _connectCommand;
        private ICommand _disconnectCommand;
        private ICommand _connectVMCommand;
        private ICommand _disconnectVMCommand;
        #endregion

        #region Public Properties/Commands
        public string Name
        {
            get { return "Internet"; }
        }
        public string TextBoxString
        {
            get { return _textBoxString; }
            set
            {
                if (value != _textBoxString)
                {
                    _textBoxString = value;
                    OnPropertyChanged("TextBoxString");
                }
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(
                    param => ClearTextBoxMessage(),
                    param => true
                    );
                }
                return _clearCommand;
            }
        }
        public ICommand CheckCommand
        {
            get
            {
                if (_checkCommand == null)
                {
                    _checkCommand = new RelayCommand(
                    param => Check(),
                    param => true
                    );
                }
                return _checkCommand;
            }
        }
        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                {
                    _connectCommand = new RelayCommand(
                    param => Connect(),
                    param => true
                    );
                }
                return _connectCommand;
            }
        }
        public ICommand DisconnectCommand
        {
            get
            {
                if (_disconnectCommand == null)
                {
                    _disconnectCommand = new RelayCommand(
                    param => Disconnect(),
                    param => true
                    );
                }
                return _disconnectCommand;
            }
        }
        public ICommand ConnectVMCommand
        {
            get
            {
                if (_connectVMCommand == null)
                {
                    _connectVMCommand = new RelayCommand(
                    param => ConnectVM(),
                    param => true
                    );
                }
                return _connectVMCommand;
            }
        }
        public ICommand DisconnectVMCommand
        {
            get
            {
                if (_disconnectVMCommand == null)
                {
                    _disconnectVMCommand = new RelayCommand(
                    param => DisconnectVM(),
                    param => true
                    );
                }
                return _disconnectVMCommand;
            }
        }
        #endregion

        #region Private Helpers
        private void Check()
        {
            string txtMsg = "**********************************************************************************";
            txtMsg += ShellHelper.Cmd("netsh interface show interface");
            AppendTextBoxMessageText(txtMsg);
        }
        private void Connect()
        {
            ShellHelper.Cmd("netsh interface set interface Ethernet enabled");
            Check();
        }
        private void Disconnect()
        {
            ShellHelper.Cmd("netsh interface set interface Ethernet disabled");
            Check();
        }
        private void ConnectVM()
        {
            ShellHelper.Cmd("netsh interface set interface Ethernet disabled");
            ShellHelper.Cmd("netsh interface set interface VirtualBox Host-Only Network enabled");
            Check();
        }
        private void DisconnectVM()
        {
            ShellHelper.Cmd("netsh interface set interface VirtualBox Host-Only Network disabled");
            ShellHelper.Cmd("netsh interface set interface Ethernet enabled");
            Check();
        }

        private void AppendTextBoxMessageText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (_textBoxString.Length == 0)
                    TextBoxString = text.Trim();
                else
                    TextBoxString = _textBoxString + "\n" + text.Trim();
            }
        }
        private void ClearTextBoxMessage()
        {
            TextBoxString = "";
        }
        #endregion

        //netsh interface teredo set state disabled
        //netsh interface teredo set state client
    }
}
