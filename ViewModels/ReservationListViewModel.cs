using HotelReservation.Commands;
using HotelReservation.Models;
using HotelReservation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HotelReservation.ViewModels
{
    internal class ReservationListViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        // привязка данных у ReservationListView
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _listVisibility;
        public bool ListVisibility
        {
            get { return _listVisibility; }
            set
            {
                _listVisibility = value;
                OnPropertyChanged(nameof(ListVisibility));
            }
        }

        private ReservationViewModel _selectedItem;
        public ReservationViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand MakeReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }

        public ReservationListViewModel(Hotel hotel, NavigationService makeReservationNavigationService,
                                                     NavigationService reservationViewNavigationService)
        {
            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
            DeleteReservationCommand = new DeleteReservationCommand(this, hotel, reservationViewNavigationService);

            UpdateReservations();
        }

        public void UpdateReservations()
        {
            _reservations.Clear();

            try
            {
                foreach (Reservation reservation in _hotel.GetAllReservations())
                {
                    ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                    _reservations.Add(reservationViewModel);
                }

                if (_reservations.Count > 0)
                {
                    _listVisibility = true;
                }
                else
                {
                    _listVisibility = false;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Не удалось обновить данные.";
            }
        }
    }
}
