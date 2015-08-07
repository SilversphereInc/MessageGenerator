using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MessageGenerator.Classes
{
    public class MessageTime : INotifyPropertyChanged
    {
        private int _Hours;
        public int Hours
        {
            get { return _Hours; }
            set
            {
                _Hours = value;
                OnPropertyChanged("Hours");
            }
        }

        private int _Minutes;
        public int Minutes
        {
            get { return _Minutes; }
            set
            { 
                _Minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        private int _Seconds;
        public int Seconds
        {
            get { return _Seconds; }
            set 
            {
                _Seconds = value;
                OnPropertyChanged("Seconds");
            }
        }

        private int _Ms;
        public int Ms
        {
            get { return _Ms; }
            set 
            { 
                _Ms = value;
                OnPropertyChanged("Ms");
            }
        }

        public MessageTime()
        {
            DateTime now = DateTime.Now;

            Hours = now.Hour;
            Minutes = now.Minute;
            Seconds = now.Second;
            Ms = now.Millisecond;
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
