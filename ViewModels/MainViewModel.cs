using HotelReservation.Models;
using HotelReservation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.ViewModels
{
    // используется для отображения текущей страницы
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
