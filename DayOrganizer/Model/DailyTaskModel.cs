using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOrganizer
{
    public class DailyTaskModel : ObservableObject
    {
        #region Fields
        private int _dailyTaskId;
        private string _dailyTaskName;
        private decimal _unitPrice;
        #endregion // Fields
        
        #region Properties
        public int RecordId
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
        public string DailyTaskName
        {
            get { return _dailyTaskName; }
            set
            {
                if (value != _dailyTaskName)
                {
                    _dailyTaskName = value;
                    OnPropertyChanged("DailyTaskName");
                }
            }
        }
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value != _unitPrice)
                {
                    _unitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }
        }
        #endregion // Properties
    }   
    
}
