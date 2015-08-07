using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MessageGenerator.ViewModels
{
    class TypeSelectionViewModel : Classes.ViewModelBase
    {
        private object _SelectionContent;
        public object SelectionContent
        {
            get { return _SelectionContent; }
            set
            {
                _SelectionContent = value;
                OnPropertyChanged("SelectionContent");
            }
        }

        private ICommand _SelectResponseCommmand;
        public ICommand SelectResponseCommand
        {
            get 
            { 
                if(_SelectResponseCommmand == null)
                {
                    _SelectResponseCommmand = new Classes.RelayCommand(new Action<object>(ShowResponseCreate));
                }

                return _SelectResponseCommmand;
            }
           
        }

        private ICommand _SelectEventCommand;
        public ICommand SelectEventCommand
        {
            get 
            { 
                if(_SelectEventCommand == null)
                {
                    _SelectEventCommand = new Classes.RelayCommand(new Action<object>(ShowEventCreate));
                }

                return _SelectEventCommand;
            }       
        }

        public TypeSelectionViewModel()
        {

            ShowEventCreate(null);
        }

        private void ShowResponseCreate(object obj)
        {
            ResponseCreationViewModel rcvm = new ResponseCreationViewModel();
            Views.ResponseCreationView rcv = new Views.ResponseCreationView();
            rcv.DataContext = rcvm;

            SelectionContent = rcv;

        }

        private void ShowEventCreate(object obj)
        {
            EventCreationViewModel ecvm = new EventCreationViewModel();
            Views.EventCreationView ecv = new Views.EventCreationView();

            ecv.DataContext = ecvm;
            SelectionContent = ecv;
        }
       

        public void AddMessageToCollecton()
        {
            FrameworkElement fwe = (FrameworkElement)SelectionContent;

            Interfaces.IMessageCreation context = fwe.DataContext as Interfaces.IMessageCreation;
            context.AddCreatedMessage();
        }
        

    }
}
