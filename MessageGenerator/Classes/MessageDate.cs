using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MessageGenerator.Classes
{
    public class MessageDate : INotifyPropertyChanged
    {
        private int _Year;
        public int Year
        {
            get { return _Year; }
            set 
            {
                _Year = value;
                OnPropertyChanged("Year");
            }
        }

        private int _Month;
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                OnPropertyChanged("Month");
            }
        }

        private int _Day;
        public int Day
        {
            get { return _Day; }
            set 
            { 
                _Day = value;
                OnPropertyChanged("Day");
            }
        }

        public MessageDate()
        {
            DateTime now = DateTime.Now;

            
            Year = Convert.ToInt32(now.Year.ToString().Substring(2,2));
            Day = now.Day;
            Month = now.Month;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
