using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register_Diagnose
{
    public class ItemsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> items;
        public ObservableCollection<string> Items
        {
            get { return items; } 
            set 
            {
                this.items = value;
                this.RaisePropertyChanged(nameof(Items));
            }
        }

        public ItemsViewModel()
        {
            items = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName) 
        {
            var handler = PropertyChanged;
            if (handler != null) 
            {
                handler(this, new PropertyChangedEventArgs(propertyName));  
            }
        }

    }
}
