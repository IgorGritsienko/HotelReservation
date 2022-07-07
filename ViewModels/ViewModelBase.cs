using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        // определяет, какую привязку Binding обновлять
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            // вызываем, если есть подписанные
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
