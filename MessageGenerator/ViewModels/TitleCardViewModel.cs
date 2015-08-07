using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.ViewModels
{
    class TitleCardViewModel : Classes.ViewModelBase
    {

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set 
            { 
                _Text = value;
                OnPropertyChanged("Text");
            }
        }

        public TitleCardViewModel()
        {
            Text = "Click the Silversphere globe to begin.";
        }
        

        
    }
}
