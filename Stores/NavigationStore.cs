using HotelReservation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Stores
{
    internal class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
