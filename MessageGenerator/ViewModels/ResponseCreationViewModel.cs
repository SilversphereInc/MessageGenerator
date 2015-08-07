using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.ViewModels
{
    class ResponseCreationViewModel : Classes.ViewModelBase
    {

        private int _PropertyId = 777;
        public int PropertyId
        {
            get { return _PropertyId; }
            set
            {
                _PropertyId = value;
                OnPropertyChanged("PropertyId");
            }
        }

        private Classes.MessageDate _Date;
        public Classes.MessageDate Date
        {
            get
            {
                if (_Date == null)
                {
                    _Date = new Classes.MessageDate();
                }

                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        private Classes.MessageTime _Time;
        public Classes.MessageTime Time
        {
            get
            {
                if (_Time == null)
                {
                    _Time = new Classes.MessageTime();
                }
                return _Time;
            }
            set
            {
                _Time = value;
            }

        }

        private List<KeyValuePair<string, int>> _SubMessage = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Standard Zone Event", 0),
            new KeyValuePair<string, int>("Wireless Zone Event", 1),
            new KeyValuePair<string, int>("Transmitter Trouble Event", 2),
            new KeyValuePair<string, int>("Primary System Event", 4),
            new KeyValuePair<string, int>("Secondary System Event", 5),
            new KeyValuePair<string, int>("Power Supply Event", 6)
        };
        public List<KeyValuePair<string, int>> SubMessage
        {
            get { return _SubMessage; }
            set { _SubMessage = value; }
        }

        private int _SubSubPrefix;
        public int SubSubPrefix
        {
            get { return _SubSubPrefix; }
            set
            {
                _SubSubPrefix = value;
                OnPropertyChanged("SubSubPrefix");
            }
        }

        private int _SubSubSuffix;
        public int SubSubSuffix
        {
            get { return _SubSubSuffix; }
            set
            {
                _SubSubSuffix = value;
                OnPropertyChanged("SubSubSuffix");
            }
        }
    }
}
