using GraduateWork.Models;
using GraduateWork.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraduateWork.ViewModels
{
    internal class AddRequestWindowViewModel : ViewModelBase
    {
        private User _currentUser;
        private Service _selectedService;
        private List<Service> _displayingServices;
        public User CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value, nameof(CurrentUser));
        }
        public Service SelectedService 
        { 
            get => _selectedService;
            set => Set(ref _selectedService, value, nameof(SelectedService)); 
        }
        public List<Service> DisplayingServices 
        { 
            get => _displayingServices;
            set => Set(ref _displayingServices, value, nameof(DisplayingServices));
        }

        public AddRequestWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            using (ApplicationDbContext context = new())
            {
                DisplayingServices= new List<Service>(GetServices());
            }
            SelectedService = null;
        }

        private List<Service> GetServices()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Services
                    .OrderBy(s=>s.Id)
                    .ToList();
            }
        }

        internal void AddServiceToRequest()
        {
            using (ApplicationDbContext context = new())
            {
                Request newRequest = new()
                {
                    ServiceId=SelectedService.Id,
                    UserId=CurrentUser.Id,
                    RequestStatus="Ожидает ответа",
                };
                context.Requests.Add(newRequest);
                context.SaveChanges();
                MessageBox.Show("Запрос на услугу отправлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
