using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using HotelReservation.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Commands
{
    internal class DeleteReservationCommand : CommandBase
    {
        private readonly ReservationListViewModel _viewModel;
        private readonly Hotel _hotel;
        private readonly NavigationService _reservationViewNavigationService;

        public DeleteReservationCommand(ReservationListViewModel viewModel, Hotel Hotel, NavigationService reservationViewNavigationService)
        {
            _viewModel = viewModel;
            _hotel = Hotel;
            _reservationViewNavigationService = reservationViewNavigationService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            Reservation reservation = ToReservation(_viewModel.SelectedItem);
            _hotel.DeleteReservation(reservation);
            _reservationViewNavigationService.Navigate();
        }

        public override bool CanExecute(object parameter)
        { 
            return _viewModel.SelectedItem is not null &&
                base.CanExecute(parameter);
        }

        private Reservation ToReservation(ReservationViewModel r)
        {
            int floorNumber = int.Parse(r.RoomID[0].ToString());
            int roomNumber = int.Parse(r.RoomID[1].ToString());

            DateTime.TryParse(r.StartDate, out DateTime startDate);
            DateTime.TryParse(r.EndDate, out DateTime endDate);

            return new Reservation(new RoomID(floorNumber, roomNumber), r.Username, startDate, endDate);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ReservationListViewModel.SelectedItem))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
