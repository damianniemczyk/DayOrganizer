using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DayOrganizer
{
    public class DailyTaskViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private int _dailyTaskId;
        private IList<DailyTaskModel> _currentDailyTasks;
        private ICommand _getDailyTaskCommand;
        private ICommand _saveDailyTaskCommand;
        #endregion

        #region Public Properties/Commands
        public string Name
        {
            get { return "Daily Tasks"; }
        }

        public int DailyTaskId
        {
            get { return _dailyTaskId; }
            set
            {
                if (value != _dailyTaskId)
                {
                    _dailyTaskId = value;
                    OnPropertyChanged("DailyTaskId");
                }
            }
        }

        public IList<DailyTaskModel> CurrentDailyTasks
        {
            get
            {
                if (_currentDailyTasks == null)
                    GetDailyTasks();
                return _currentDailyTasks;
            }
            set
            {
                if (value != _currentDailyTasks)
                {
                    _currentDailyTasks = value;
                    OnPropertyChanged("CurrentDailyTasks");
                }
            }
        }

        public ICommand SaveDailyTaskCommand
        {
            get
            {
                if (_saveDailyTaskCommand == null)
                {
                    _saveDailyTaskCommand = new RelayCommand(
                    param => SaveDailyTask(),
                    param => (CurrentDailyTasks != null)
                    );
                }
                return _saveDailyTaskCommand;
            }
        }

        public ICommand GetDailyTaskCommand
        {
            get
            {
                if (_getDailyTaskCommand == null)
                {
                    _getDailyTaskCommand = new RelayCommand(
                    param => GetDailyTasks(),
                    param => DailyTaskId > 0
                    );
                }
                return _getDailyTaskCommand;
            }
        }
        #endregion

        #region Private Helpers
        private void GetDailyTasks()
        {
            // You should get the product from the database
            // but for now we'll just return a new object
            DailyTaskModel p1 = new DailyTaskModel();
            p1.RecordId = DailyTaskId;
            p1.DailyTaskName = "Test Daily Task";
            p1.UnitPrice = 10.00M;

            DailyTaskModel p2 = new DailyTaskModel();
            p2.RecordId = DailyTaskId + 1;
            p2.DailyTaskName = "Test Daily Task2";
            p2.UnitPrice = 20.00M;

            CurrentDailyTasks = new List<DailyTaskModel> { p1, p2 };
        }
        private void SaveDailyTask()
        {
            // You would implement your Product save here
        }
        #endregion
    }
}