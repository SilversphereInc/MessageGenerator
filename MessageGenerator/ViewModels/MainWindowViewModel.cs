using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MessageGenerator.ViewModels
{
    class MainWindowViewModel : Classes.ViewModelBase
    {
        private object _MainContent;
        public object MainContent
        {
            get 
            { 
                return _MainContent; 
            }
            set 
            { 
                _MainContent = value;
                OnPropertyChanged("MainContent");
            }
        }

        #region The ghetto way of doing this.
        private Visibility _MenuVisibility = Visibility.Collapsed;
        public Visibility MenuVisibility
        {
            get { return _MenuVisibility; }
            set 
            { 
                _MenuVisibility = value;
                OnPropertyChanged("MenuVisibility");
            }
        }

        private bool _MenuChecked;
        public bool MenuChecked
        {
            get { return _MenuChecked; }
            set
            {
                _MenuChecked = value;

                if(value == false)
                {
                    MenuVisibility = Visibility.Collapsed;
                }
                else
                {
                    MenuVisibility = Visibility.Visible;
                }

                OnPropertyChanged("IsChecked");
            }
        }
        #endregion 

        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (_CreateCommand == null)
                {
                    _CreateCommand = new Classes.RelayCommand(new Action<object>(ShowCreate));
                }
                return _CreateCommand;
            }

        }

        private ICommand _FloodCommand;
        public ICommand FloodCommand
        {
            get
            {
                if (_FloodCommand == null)
                {
                    _FloodCommand = new Classes.RelayCommand(new Action<object>(ShowFlood));
                }
                return _FloodCommand;
            }

        }

        public MainWindowViewModel()
        {
            TitleCardViewModel tcvm = new TitleCardViewModel();
            Views.TitleCardView tcv = new Views.TitleCardView();
            tcv.DataContext = tcvm;

            MainContent = tcv;
        }

        private void ShowCreate(object obj)
        {
            CreationViewModel cvm = new CreationViewModel();
            Views.CreationView cv = new Views.CreationView();

            cv.DataContext = cvm;
            MainContent = cv;    
        }

        private void ShowFlood(object obj)
        {
            FloodViewModel fvm = new FloodViewModel();
            Views.FloodView fv = new Views.FloodView();
            fv.DataContext = fvm;

            MainContent = fv;
        
        }
       
    }
}
