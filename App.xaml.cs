using HotelReservation.DbContexts;
using HotelReservation.Exceptions;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Services.ReservationConflictValidatiors;
using HotelReservation.Services.ReservationCreators;
using HotelReservation.Services.ReservationDeleter;
using HotelReservation.Services.ReservationDeleters;
using HotelReservation.Services.ReservationProviders;
using HotelReservation.Stores;
using HotelReservation.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=HotelReservation.db";
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private HotelReservationDbContextFactory hotelReservationDbContextFactory;

        public App()
        {
            hotelReservationDbContextFactory = new HotelReservationDbContextFactory(CONNECTION_STRING);
            IReservationProvider reservationProvider = new DatabaseReservationProvider(hotelReservationDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(hotelReservationDbContextFactory);
            IReservationDeleter reservationDeleter = new DatabaseReservationDeleter(hotelReservationDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(hotelReservationDbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationDeleter, reservationConflictValidator);
            _hotel = new Hotel("Apollo's Chariot", reservationBook);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (HotelReservationDbContext dbContext = hotelReservationDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }


        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
        }


        private ReservationListViewModel CreateReservationViewModel()
        {
            return new ReservationListViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel), 
                                                        new NavigationService(_navigationStore, CreateReservationViewModel));
        }
    }
}
