using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Input;

namespace MessageGenerator.ViewModels
{
    class FloodViewModel: Classes.ViewModelBase
    {

        

        private List<string> _MessageDisplay;
        public List<string> MessageDisplay
        {
            get { return _MessageDisplay; }
            set 
            {
                _MessageDisplay = value;
                OnPropertyChanged("MessageDisplay");
            }
        }

        private int _RunCount = 1;
        public int RunCount
        {
            get { return _RunCount; }
            set 
            { 
                _RunCount = value;
                OnPropertyChanged("RunCount");
            }
        }

        private ICommand _AllCommand;
        public ICommand AllCommand
        {
            get 
            { 
                if(_AllCommand == null)
                {
                    _AllCommand = new Classes.RelayCommand(new Action<object>(SendAll));
                }
                return _AllCommand; 
            }
           
        }

        private ICommand _SelectedCommand;
        public ICommand SelectedCommand
        {
            get 
            { 
                if(_SelectedCommand == null)
                {
                    _SelectedCommand = new Classes.RelayCommand(new Action<object>(SendSelected));
                }
                return _SelectedCommand; 
            }
            
        }

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set 
            { 
                _SelectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        

        public FloodViewModel()
        {
            FillDisplay();
           
        }

        private void FillDisplay()
        {
            List<List<byte>> list = Models.MessageCollectionModel.Current.GetAllMessages();
            List<string> displayList = new List<string>();

            foreach (var byteList in list)
            {
                byte[] bytes = new Classes.CrcTelTron().Modify(byteList.ToArray());

                string byteString = string.Join(" ", bytes);

                displayList.Add(byteString);
            }

            MessageDisplay = displayList;
        }

        private void SendAll(object obj)
        {
            int count = Models.MessageCollectionModel.Current.MessageCount;
            int currentSelection = SelectedIndex;

            for(int i = currentSelection; i < count; i++)
            {
                SendSelected(null);
                SelectedIndex++;
            }
            
        }

        private void SendSelected(object obj)
        {
            System.Net.Sockets.TcpClient _ClientSocket = new System.Net.Sockets.TcpClient();
            _ClientSocket.Connect("127.0.0.1", 1337);

            NetworkStream serverStream = _ClientSocket.GetStream();

            List<List<byte>> list = Models.MessageCollectionModel.Current.GetAllMessages();

            for (int i = 0; i < RunCount; i++)
            {
                byte[] bytes = new Classes.CrcTelTron().Modify(list[SelectedIndex].ToArray());
 
                serverStream.Write(bytes, 0, bytes.Length);
                serverStream.Flush();
            }

            _ClientSocket.Close();

          
        }


       

        //public FloodViewModel()
        //{

        //    clientSocket.Connect("127.0.0.1", 1337);

        //    List<List<byte>> list = Models.MessageCollectionModel.Current.GetAllMessages();

        //    byte[] bytes = new Classes.CrcTelTron().Modify(list[0].ToArray());

        //    NetworkStream serverStream = clientSocket.GetStream();
          
        //    serverStream.Write(bytes, 0, bytes.Length);
        //    serverStream.Flush();
        //}
    }
}
