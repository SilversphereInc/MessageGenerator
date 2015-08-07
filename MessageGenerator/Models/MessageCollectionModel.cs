using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.Models
{
    public class MessageCollectionModel
    {
        private static MessageCollectionModel _Current;
        public static MessageCollectionModel Current
        {
            get 
            { 
                if(_Current == null)
                {
                    _Current = new MessageCollectionModel();
                }

                return _Current;
            }
        }

        private List<List<byte>> _Messages = new List<List<byte>>();

        private MessageCollectionModel() { }

        public int MessageCount
        {
            get { return _Messages.Count; }
        }

        public void AddMessage(List<byte> message)
        {
            _Messages.Add(message);
        }

        public List<List<byte>> GetAllMessages()
        {
            return _Messages;
        }

    }
}
