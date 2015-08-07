using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.ViewModels
{
    class EventCreationViewModel : Classes.ViewModelBase, Interfaces.IMessageCreation
    {
        private int _PropertyId = 888;
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
            new KeyValuePair<string, int>("Hardwired Zone Event", 0),
            new KeyValuePair<string, int>("Wireless Zone Event", 1),
            new KeyValuePair<string, int>("Transmitter Trouble Event", 2),
            new KeyValuePair<string, int>("Primary System Event", 4),
            new KeyValuePair<string, int>("Secondary System Event", 5),
            new KeyValuePair<string, int>("Power Supply Event", 6)
        };
        public List<KeyValuePair<string, int>> SubMessage
        {
            get { return _SubMessage;}
            set { _SubMessage = value;}
        }

        private KeyValuePair<string, int> _SelectedSubMessage;
        public KeyValuePair<string, int> SelectedSubMessage
        {
            get { return _SelectedSubMessage; }
            set 
            { 
                _SelectedSubMessage = value; 
                OnPropertyChanged("SelectedSubMessage");
            }
        }  

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        
        private int _EventLevel;
        public int EventLevel
        {
            get { return _EventLevel; }
            set
            {
                _EventLevel = value;
                OnPropertyChanged("EventLevel");
            }
        }

        private int _Originator;
        public int Originator
        {
            get { return _Originator; }
            set 
            {
                _Originator = value;
                OnPropertyChanged("Originator");
            }
        }

        public void AddCreatedMessage()
        {
            Classes.HscbEvent evnt = new Classes.HscbEvent()
            {
                PropertyId = this.PropertyId,
                MessageDate = Date,
                MessageTime = Time,
                EventIndex = Id,
                EventLevel = this.EventLevel,
                Originator = this.Originator,
                SubType = SelectedSubMessage.Value,
                SubSubType = this.EventLevel
            };

            Models.MessageCollectionModel.Current.AddMessage(evnt.GetMessageContents());
        }
    }
}
