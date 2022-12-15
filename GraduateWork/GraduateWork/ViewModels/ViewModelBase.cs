using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.ViewModels
{
    abstract internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool Set<T>(ref T field,T value,string propertyName)
        {
            if(EqualityComparer<T>.Default.Equals(field,value))
                return false;
            field = value;
            OnProperyChnged(propertyName);
            return true;
        }
        private void OnProperyChnged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
