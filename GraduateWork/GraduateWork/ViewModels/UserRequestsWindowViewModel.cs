using GraduateWork.Models;
using GraduateWork.Models.Entities;
using GraduateWork.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraduateWork.ViewModels
{
    internal class UserRequestsWindowViewModel:ViewModelBase
    {
        private User _currentUser;
        private Request _selectedRequest;
        private string _elementsCount;

        private string _searchValue;
        private string _filtherValue;

        private List<Request> _displayingRequests;
        public List<string> FiltherValues { get; } = new List<string>()
        {
            "Все статусы",
            "Ожидает ответа",
            "В процессе",
            "Выполнен",
        };
        public User CurrentUser 
        { 
            get => _currentUser; 
            set
            {
                Set(ref _currentUser, value, nameof(CurrentUser));
            }
        }

        public List<Request> DisplayingRequests 
        { 
            get => _displayingRequests;
            set
            {
                Set(ref _displayingRequests, value, nameof(DisplayingRequests));
                ElementsCount = $"{value.Count} / {GetRequests().Count}";
            }
        }
        public string ElementsCount 
        { 
            get => _elementsCount;
            set => Set(ref _elementsCount, value, nameof(ElementsCount));
        }
        public Request SelectedRequest 
        { 
            get => _selectedRequest;
            set => Set(ref _selectedRequest, value, nameof(SelectedRequest));
        }
        public string FiltherValue 
        { 
            get => _filtherValue; 
            set
            {
                Set(ref _filtherValue, value, nameof(FiltherValue));
                DisplayRequests();
            } 
        }
        public string SearchValue 
        { 
            get => _searchValue; 
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayRequests();
            }
        }

        public UserRequestsWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            using (ApplicationDbContext context = new())
            {
                DisplayingRequests= new List<Request>(GetRequests());
            }
            SearchValue = null;
            FiltherValue = FiltherValues[0];
        }

        private List<Request> GetRequests()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Requests
                    .Include(s => s.Service)
                    .Where(r => r.UserId == CurrentUser.Id)
                    .OrderBy(r => r.Id)
                    .ToList();
            }
        }

        private void DisplayRequests()
        {
            DisplayingRequests = Search(Filther(GetRequests()));
        }

        private List<Request> Search(List<Request> requests)
        {
            if (SearchValue == null || SearchValue == string.Empty)
                return requests;
            else
                return requests.Where(o => o.Service.Title.ToLower().Contains(SearchValue.ToLower())).ToList();
        }
        private List<Request> Filther(List<Request> requests)
        {
            if (FiltherValue == FiltherValues[0])
                return requests;
            else
                return requests.Where(o => o.RequestStatus == FiltherValue).ToList();
        }
        internal void OpenAddRequestWindow()
        {
            new AddRequestWindow(CurrentUser).ShowDialog();
        }
        internal void DeleteRequest()
        {
            using (ApplicationDbContext context = new())
            {
                var result = MessageBox.Show("Вы точно хотите отменить этот запрос?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Requests.Remove(SelectedRequest);
                    context.SaveChanges();
                    DisplayRequests();
                }
            }
        }
    }
}
