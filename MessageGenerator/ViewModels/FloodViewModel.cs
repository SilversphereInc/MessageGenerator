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

        private int _Port = 1337;
        public int Port
        {
            get { return _Port; }
            set 
            { 
                _Port = value;
                OnPropertyChanged("Port");
            }
        }

        private string _Address = "127.0.0.1";
        public string Address
        {
            get { return _Address; }
            set 
            {
                _Address = value;
                OnPropertyChanged("Address");
            }
        }

        private bool _EnableButtons = true;
        public bool EnableButtons
        {
            get { return _EnableButtons; }
            set
            { 
                _EnableButtons = value;
                OnPropertyChanged("EnableButtons");
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

        private string _StatusMessage;
        public string StatusMessage
        {
            get { return _StatusMessage; }
            set 
            {
                _StatusMessage = value;
                OnPropertyChanged("StatusMessage");
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
            EnableButtons = false;
            bool connected = false;

            NetworkStream serverStream = null;
            System.Net.Sockets.TcpClient _ClientSocket = null;

            try
            {
                _ClientSocket = new System.Net.Sockets.TcpClient();
                _ClientSocket.Connect(Address, Port);

                serverStream = _ClientSocket.GetStream();
                
                connected = true;
            }
            catch
            {
                StatusMessage = "Error! Could not connect.";
                EnableButtons = true;
            }

            if (connected == true)
            {

                List<List<byte>> list = Models.MessageCollectionModel.Current.GetAllMessages();

                System.ComponentModel.BackgroundWorker bgw = new System.ComponentModel.BackgroundWorker();

                bgw.DoWork += (sender, evnt) =>
                    {
                        for (int i = 0; i < RunCount; i++)
                        {
                            byte[] bytes = new Classes.CrcTelTron().Modify(list[SelectedIndex].ToArray());

                            serverStream.Write(bytes, 0, bytes.Length);
                            serverStream.Flush();

                            StatusMessage = "Currently sending " + (i + 1).ToString() + " of " + RunCount.ToString() + " events.";

                            System.Threading.Thread.Sleep(1);
                        }
                    };

                bgw.RunWorkerCompleted += (sender, evnt) =>
                {
                    StatusMessage = "Finished sending " + RunCount.ToString() + " events.";
                    EnableButtons = true;
                };

                bgw.RunWorkerAsync();
            }
            
            

            //_ClientSocket.Close();

          
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
