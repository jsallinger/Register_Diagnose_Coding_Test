using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Register_Diagnose
{
    public class SieveViewModel : ICommand
    {
        private readonly DelegateCommand<object> _clickCommand;
        private ItemsViewModel _displayList;

        public ItemsViewModel displayList
        {
            get { return _displayList; }
            set { _displayList = value; }
        }



        public SieveViewModel()
        {
            _clickCommand = new DelegateCommand<object>(Execute, CanExecute);
            _displayList = new ItemsViewModel();
        }

        public DelegateCommand<object> ButtonClickCommand
        {
            get { return _clickCommand; }
        }

        public void Execute(object? sender)
        {
            displayList.Items.Clear();
            List<int> list = Enumerable.Range(1, 100).ToList();

            /*

                 replacement key

                 -1 .. Register (3)
                 -2 .. Patient  (5 || 7)
                 -3 .. Register Patient (3 && 5)

                 -4 .. Diagnose (2)
                 -5 .. Diagnose Patient (2 && 7)



            */




            TextBlock workingItem = new TextBlock();

            if (sender == null)
            {
                //handle it
                return;
            }

            if (sender.GetType() == typeof(TextBlock))
            {
                workingItem = (TextBlock)sender;
                if (workingItem.Text == "Register")
                {
                    list.Replace(i => i % 3 == 0 && i % 5 == 0, -3);
                    list.Replace(i => i > 0 && i % 3 == 0, -1);
                    list.Replace(i => i > 0 && i % 5 == 0, -2);
                }
                else if (workingItem.Text == "Diagnose")
                {
                    list.Replace(i => i % 2 == 0 && i % 7 == 0, -5);
                    list.Replace(i => i > 0 && i % 2 == 0, -4);
                    list.Replace(i => i > 0 && i % 7 == 0, -2);
                }
                else if (workingItem.Text == "Clear")
                {
                    return;
                }
                else if (workingItem.Text == "Exit")
                {
                    App.Current.MainWindow.Close();
                }
                else
                {
                    //Handle odd button event
                }
            }
            else
            {
                //handle unknown event
            }

            foreach (int num in list)
            {
                if (num > 0)
                {
                    displayList.Items.Add(num.ToString());
                }
                else if (num == -1)
                {
                    displayList.Items.Add("Register");
                }
                else if (num == -2)
                {
                    displayList.Items.Add("Patient");
                }
                else if (num == -3)
                {
                    displayList.Items.Add("Register Patient");
                }
                else if (num == -4)
                {
                    displayList.Items.Add("Diagnose");
                }
                else if (num == -5)
                {
                    displayList.Items.Add("Diagnose Patient");
                }
                else
                {
                    displayList.Items.Add("How did this get in here");
                }
            }

        }

        public bool CanExecute(object? sender)
        {
            DateTime rightNow = DateTime.Now;

            //Implemented rules on when this will work
            //Not between 12 am and 6 am
            if (rightNow.Hour > 23 || rightNow.Hour < 6)
            {
                return false;
            }

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        static bool IsEmpty(List<string> list)
        {
            if (list == null || !list.Any())
            {
                return true;
            }

            return false;
        }
    }

    public static class ListExtensions
    {
        public static void Replace<T>(this List<T> list, Predicate<T> oldItemSelector, T newItem)
        {
            int oldItemIndex = list.FindIndex(oldItemSelector);

            while (oldItemIndex >= 0)
            {
                list[oldItemIndex] = newItem;
                oldItemIndex = list.FindIndex(oldItemSelector);
            }

        }
    }
}
