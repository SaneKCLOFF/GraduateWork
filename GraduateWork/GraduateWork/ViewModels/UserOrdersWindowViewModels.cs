using GraduateWork.Models;
using GraduateWork.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraduateWork.ViewModels
{
    internal class UserOrdersWindowViewModels : ViewModelBase
    {
        private User _currentUser;
        private Order _selectedOrder;
        private string _elementsCount;

        private string _searchValue;
        private string _filtherValue;
        private string _sortValue;

        private List<Order> _displayingOrders;
        public List<string> FiltherValues { get; } = new List<string>()
        {
            "Все статусы",
            "Ожидает ответа",
            "В процессе",
            "Выполнен",
        };
        public List<string> SortValues { get; } = new List<string>()
        {
            "Без сортировки",
            "По цене (возр.)",
            "По цене (убыв.)",
        };

        public User CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value, nameof(CurrentUser));
        }
        public List<Order> DisplayingOrders 
        { 
            get => _displayingOrders; 
            set
            {
                Set(ref _displayingOrders, value, nameof(DisplayingOrders));
                ElementsCount = $"{value.Count} / {GetOrders().Count}";
            }
        }
        public Order SelectedOrder 
        { 
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value, nameof(SelectedOrder));
        }
        public string ElementsCount 
        { 
            get => _elementsCount;
            set => Set(ref _elementsCount, value, nameof(ElementsCount));
        }
        public string SearchValue 
        { 
            get => _searchValue; 
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayOrders();
            } 
        }

        public string FiltherValue 
        { 
            get => _filtherValue; 
            set
            {
                Set(ref _filtherValue, value, nameof(FiltherValue));
                DisplayOrders();
            }
        }
        public string SortValue 
        {
            get => _sortValue; 
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayOrders();
            }
        }

        public UserOrdersWindowViewModels(User currentUser)
        {
            CurrentUser = currentUser;
            using (ApplicationDbContext context = new())
            {
                DisplayingOrders= new List<Order>(GetOrders());
            }
            SearchValue = null;
            SortValue = SortValues[0];
            FiltherValue = FiltherValues[0];
        }

        private List<Order> GetOrders()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Orders
                    .Include(p=>p.Product)
                    .Include(u=>u.User)
                    .OrderBy(o => o.Id)
                    .Where(o => o.UserId == CurrentUser.Id)
                    .ToList();
            }
        }
        private void DisplayOrders()
        {
            DisplayingOrders = Sort(Search(Filther(GetOrders())));
        }
        private List<Order> Search(List<Order> orders)
        {
            if (SearchValue == null || SearchValue == string.Empty)
                return orders;
            else
                return orders.Where(o => o.Product.Title.ToLower().Contains(SearchValue.ToLower())).ToList();
        }
        private List<Order> Sort(List<Order> orders)
        {
            if (SortValue == SortValues[1])
                return orders.OrderBy(o => o.Product.Cost).ToList();
            else if (SortValue == SortValues[2])
                return orders.OrderByDescending(o => o.Product.Cost).ToList();
            else
                return orders;
        }
        private List<Order> Filther(List<Order> orders)
        {
            if (FiltherValue == FiltherValues[0])
                return orders;
            else
                return orders.Where(o => o.OrderStatus == FiltherValue).ToList();
        }
        internal void DeleteUserOrder()
        {
            using (ApplicationDbContext context = new())
            {
                var result = MessageBox.Show("Вы точно хотите отменить этот заказ?","Внимание!",MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    context.Orders.Remove(SelectedOrder);
                    context.SaveChanges();
                    DisplayOrders();
                }
            }
        }
    }
}
