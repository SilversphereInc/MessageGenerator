using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.Classes
{
    class HscbEvent : Message
    {

        private int _EventIndex;
        public int EventIndex
        {
            get { return _EventIndex; }
            set { _EventIndex = value; }
        }

        private int _Originator;
        public int Originator
        {
            get { return _Originator; }
            set { _Originator = value; }
        }

        private int _EventLevel;
        public int EventLevel
        {
            get { return _EventLevel; }
            set { _EventLevel = value; }

        }

        public HscbEvent()
        {
            // Events are type 2 messages.
            Type = 2;
        }

        public override List<byte> GetMessageContents()
        {
            // Everything must stay in the correct order.

            List<byte> contents = new List<byte>();

            byte[] bytes = BitConverter.GetBytes(PropertyId);

            contents.Add(bytes[0]);
            contents.Add(bytes[1]);
            
            bytes = BitConverter.GetBytes(MessageDate.Year);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageDate.Month);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(0); // Date
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageDate.Day);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageTime.Hours);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageTime.Minutes);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageTime.Seconds);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(MessageTime.Ms);
            contents.Add(bytes[0]);
            contents.Add(bytes[1]);

            bytes = BitConverter.GetBytes(0); // Sequence Number
            contents.Add(bytes[0]);
            contents.Add(bytes[1]);

            bytes = BitConverter.GetBytes(Type);
            contents.Add(bytes[0]);
            
            bytes = BitConverter.GetBytes(SubType);
            contents.Add(bytes[0]);
            
            bytes = BitConverter.GetBytes(SubSubType);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(0); // Segment Number
            contents.Add(bytes[0]);
            contents.Add(bytes[1]);

            bytes = BitConverter.GetBytes(WebMessageType); 
            contents.Add(bytes[0]);

            // Add future use bytes.
            for (int i = 0; i < 6; i++)
            {
                bytes = BitConverter.GetBytes(0); 
                contents.Add(bytes[0]);
            }

            // Unpacked payload length bytes.
            switch(SubType)
            {
                case 0:
                    bytes = BitConverter.GetBytes(5); 
                    contents.Add(bytes[0]);
                    break;
                case 1:
                    bytes = BitConverter.GetBytes(15); 
                    contents.Add(bytes[0]);
                    break;
                case 2:
                    bytes = BitConverter.GetBytes(6); 
                    contents.Add(bytes[0]);
                    break;
                case 6:
                    bytes = BitConverter.GetBytes(4); 
                    contents.Add(bytes[0]);
                    break;
                default: 
                    bytes = BitConverter.GetBytes(SubType); 
                    contents.Add(bytes[0]);
                    break;
            }

            bytes = BitConverter.GetBytes(SubType);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(EventIndex);
            contents.Add(bytes[0]);

            bytes = BitConverter.GetBytes(Originator);
            contents.Add(bytes[0]);
            contents.Add(bytes[1]);

            bytes = BitConverter.GetBytes(EventLevel);
            contents.Add(bytes[0]);

            // Fill rest of packet.
            for (int i = 0; i < 127; i++)
            {
                bytes = BitConverter.GetBytes(0);
                contents.Add(bytes[0]);
            }

            // Last 2 bytes are CRC.
            bytes = BitConverter.GetBytes(0);
            contents.Add(bytes[0]);
            contents.Add(bytes[1]);

            return contents;

        }
    }
}
