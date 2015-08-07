using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageGenerator.ViewModels
{
    class CreationViewModel : Classes.ViewModelBase
    {

        private object _CreationContent;
        public object CreationContent
        {
            get { return _CreationContent; }
            set
            { 
                _CreationContent = value;
                OnPropertyChanged("CreationContent");
            }
        }

        private ICommand _AddMessageCommand;
        public ICommand AddMessageCommand
        {
            get 
            { 
                if(_AddMessageCommand == null)
                {
                    _AddMessageCommand = new Classes.RelayCommand(new Action<object>(AddMessage));
                }

                return _AddMessageCommand;
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
        

        private TypeSelectionViewModel _CreationBase;
        public TypeSelectionViewModel CreationBase
        {
            get 
            { 
                if(_CreationBase == null)
                {
                    _CreationBase = new TypeSelectionViewModel();
                }

                return _CreationBase; 
            }
        }
        
        public CreationViewModel()
        {
            Views.TypeSelectionView tsv = new Views.TypeSelectionView();
            tsv.DataContext = CreationBase;
            CreationContent = tsv;
        }

        private void AddMessage(object obj)
        {

            CreationBase.AddMessageToCollecton();
            StatusMessage = "Storage now contains: " + Models.MessageCollectionModel.Current.MessageCount + " message(s).";
        }

       
    }
}
