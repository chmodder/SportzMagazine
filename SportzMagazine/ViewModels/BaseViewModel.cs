using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SportzMagazine.Helpers;
using Prism.Windows.Validation;

namespace SportzMagazine.Models
{
    public class BaseViewModel : ValidatableBindableBase
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
        }

        //public void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
