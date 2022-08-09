using HotelReservation.Commands;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModels
{
    internal class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private int _floorNumber;
        public int FloorNumber
        {
            get
            {
                return _floorNumber;
            }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private DateTime _startDate = DateTime.Now.Date;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("Дата начала бронирования не должна превосходить дату конца бронирования", nameof(StartDate));                   
                }
            }
        }

        private DateTime _endDate = DateTime.Now.Date;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("Дата начала бронирования не должна превосходить дату конца бронирования", nameof(EndDate));
                }
            }
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if(!_propertynameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertynameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertynameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel Hotel, 
            NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, Hotel, reservationViewNavigationService);
            CancelCommand = new NavigateCommand(reservationViewNavigationService);
            _propertynameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

         
        private readonly Dictionary<string, List<string>> _propertynameToErrorsDictionary;

        public bool HasErrors => _propertynameToErrorsDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertynameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }
        private void ClearErrors(string propertyName)
        {
            _propertynameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
        
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(propertyName)));
        }
    }
}
