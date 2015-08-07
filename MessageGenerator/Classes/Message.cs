using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.Classes
{
    public abstract class Message
    {
        private int _PropertyId;
        public int PropertyId
        {
            get { return _PropertyId; }
            set { _PropertyId = value; }
        }

        private MessageDate _MessageDate;
        public MessageDate MessageDate
        {
            get { return _MessageDate; }
            set { _MessageDate = value; }
        }

        private MessageTime _MessageTime;
        public MessageTime MessageTime
        {
            get { return _MessageTime; }
            set { _MessageTime = value; }
        }

        private int _WebMessageType;
        public int WebMessageType
        {
            get { return _WebMessageType; }
            set { _WebMessageType = value; }
        }

        private int _Type;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private int _SubType;
        public int SubType
        {
            get { return _SubType; }
            set { _SubType = value; }
        }

        private int _SubSubType;
        public int SubSubType
        {
            get { return _SubSubType; }
            set { _SubSubType = value; }
        }

        public abstract List<byte> GetMessageContents();
    
    }
}
