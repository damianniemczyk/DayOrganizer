using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

using DayOrganizer.Commons.Tools;

namespace DayOrganizer
{
    public class ToolsViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private string _textBoxString = string.Empty;
        private ICommand _clearCommand;
        private ICommand _closeWinMrgCommand;
        private ICommand _stopCloseWinMrgCommand;
        private ICommand _closeVisualProcessesCommand;
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //TODO
            ProcessStopperInstance.StopWork();
            //workerThread.Abort();
        }

        #region Public Properties/Commands
        public string Name
        {
            get { return "Tools"; }
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
        public ICommand CloseWinMrgCommand
        {
            get
            {
                if (_closeWinMrgCommand == null)
                {
                    _closeWinMrgCommand = new RelayCommand(
                    param => CloseWinMrg(),
                    param => true
                    );
                }
                return _closeWinMrgCommand;
            }
        }
        public ICommand StopCloseWinMrgCommand
        {
            get
            {
                if (_stopCloseWinMrgCommand == null)
                {
                    _stopCloseWinMrgCommand = new RelayCommand(
                    param => StopCloseWinMrg(),
                    param => true
                    );
                }
                return _stopCloseWinMrgCommand;
            }
        }
        public ICommand CloseVisualProcessesCommand
        {
            get
            {
                if (_closeVisualProcessesCommand == null)
                {
                    _closeVisualProcessesCommand = new RelayCommand(
                    param => CloseVisualProcesses(),
                    param => true
                    );
                }
                return _closeVisualProcessesCommand;
            }
        }
        #endregion

        #region Private Helpers
        private void CloseWinMrg()
        {
            _workerThread = new Thread(ProcessStopperInstance.DoWork);
            _workerThread.Start();
        }
        private void StopCloseWinMrg()
        {
            ProcessStopperInstance.StopWork();
            _workerThread.Abort();
        }
        private void CloseVisualProcesses()
        {
            string txtMsg = "**********************************************************************************";
            txtMsg += "\nTerminate Visual Nunit and MSBuild processes";
            AppendTextBoxMessageText(txtMsg);
            Process[] processNUnit = Process.GetProcessesByName("VisualNunitRunner");
            foreach (Process proc in processNUnit)
            {
                string result = ShellHelper.Cmd("taskkill /IM VisualNunitRunner.exe /F");
                AppendTextBoxMessageText(result);
            }
            Process[] processMS = Process.GetProcessesByName("MSBuild");
            foreach (Process proc in processMS)
            {
                string result = ShellHelper.Cmd("taskkill /IM MSBuild.exe /F");
                AppendTextBoxMessageText(result);
            }
        }
        #endregion

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

        #region CodeReview WinMerge Closing
        private Thread _workerThread;
        private ProcessStopper _processStopper;
        private ProcessStopper ProcessStopperInstance
        {
            get
            {
                if (_processStopper == null)
                {
                    _processStopper = new ProcessStopper();
                    _processStopper.TextBoxUpdateEvent += AppendTextBoxMessageHandler;
                }
                return _processStopper;
            }
        }

        private void AppendTextBoxMessageHandler(object sender, string text)
        {
            //textBoxOutput.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            //{
                AppendTextBoxMessageText(text);
            //}));
        }

        private class ProcessStopper
        {
            public event EventHandler<string> TextBoxUpdateEvent;

            private volatile bool _keepGoing;

            const uint WM_KEYDOWN = 0x0100;
            const int VK_ENTER = 0x0D;
            const int VK_DOWN = 0x28;

            public ProcessStopper()
            {
                _keepGoing = true;
            }

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

            [STAThread]
            public void DoWork()
            {
                _keepGoing = true;
                while (_keepGoing)
                {
                    //TODO forward keydown and enter to softdev code  review window
                    //SoftDev - open new WinMerge window
                    //Process[] proccccc = Process.GetProcesses();

                    /*Process[] processSD = Process.GetProcessesByName("SoftDev");
                    foreach (Process proc in processSD)
                    {
                        //var th = proc.Threads;
                        //SetForegroundWindow(proc.MainWindowHandle);
                        //var allChildWindows = new WindowHandleInfo(proc.MainWindowHandle).GetAllChildHandles();
                        foreach (Thread th in proc.Threads)
                        {
                            th.
                        }

                        PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_DOWN, 0);
                        PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_ENTER, 0);
                    }
                    Thread.Sleep(500);*/

                    //Close WinMerge window
                    Process[] processWM = Process.GetProcessesByName("WinMergeU");
                    foreach (Process proc in processWM)
                    {
                        TextBoxUpdateEvent?.Invoke(this, (string.Format("Process: {0} ID: {1}", proc.ProcessName, proc.Id)));
                        string result = ShellHelper.Cmd("taskkill /IM WinMergeU.exe /F");
                        TextBoxUpdateEvent?.Invoke(this, result);
                    }
                    Thread.Sleep(1000);
                }
            }

            public void StopWork()
            {
                _keepGoing = false;
            }
        }
        #endregion
    }
}