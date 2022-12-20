using GraduateWork.Models;
using GraduateWork.Models.Entities;
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
    internal class AdminControlWindowViewModel:ViewModelBase
    {
        #region User
        private List<User> _usersList;
        private User _selectedUser;
        private string _userElementsCount;

        private string _userSearchValue;
        private string _userSortValue;
        private string _userFiltherValue;

        public List<User> UsersList 
        { 
            get => _usersList;
            set 
            {
                Set(ref _usersList, value, nameof(UsersList));
                UserElementsCount = $"{value.Count} / {GetUsers().Count}";
            }
        }
        public User SelectedUser 
        { 
            get => _selectedUser;
            set => Set(ref _selectedUser, value, nameof(SelectedUser));
        }
        public List<string> UserFiltherValues { get; } = new List<string>()
        {
            "Все роли"
        };
        public List<string> UserSortValues { get; } = new List<string>()
        {
            "Без сортировки",
            "По ФИО(возр.)",
            "По ФИО(убыв.)",
        };
        public string UserSearchValue 
        { 
            get => _userSearchValue;
            set 
            {
                Set(ref _userSearchValue, value, nameof(UserSearchValue));
                DisplayUsers();
            }
        }
        public string UserSortValue 
        { 
            get => _userSortValue; 
            set
            {
                Set(ref _userSortValue, value, nameof(UserSortValue));
                DisplayUsers();
            }
        }

        public string UserFiltherValue 
        { 
            get => _userFiltherValue; 
            set
            {
                Set(ref _userFiltherValue, value, nameof(UserFiltherValue));
                DisplayUsers();
            }
        }

        public string UserElementsCount 
        { 
            get => _userElementsCount;
            set => Set(ref _userElementsCount, value, nameof(UserElementsCount)); 
        }

        private List<User> GetUsers()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Users.Include(u => u.Role).OrderBy(u => u.Id).ToList();
            }
        }
        private void DisplayUsers()
        {
            UsersList = Sort(Search(Filther(GetUsers())));
        }

        private List<User> Search(List<User> users)
        {
            if (UserSearchValue == null || UserSearchValue == string.Empty)
                return users;
            else
                return users.Where(u =>
                u.LastName.ToLower().Contains(UserSearchValue.ToLower()) ||
                u.FirstName.ToLower().Contains(UserSearchValue.ToLower()) ||
                u.MiddleName.ToLower().Contains(UserSearchValue.ToLower())
                ).ToList();
        }
        private List<User> Filther(List<User> users)
        {
            if (UserFiltherValue == UserFiltherValues[0])
                return users;
            else
                return users.Where(u => u.Role.Title == UserFiltherValue).ToList();
        }
        private List<User> Sort(List<User> users)
        {
            if (UserSortValue == UserSortValues[1])
                return users.OrderBy(u => u.UserFullName).ToList();
            else if (UserSortValue == UserSortValues[2])
                return users.OrderByDescending(u => u.UserFullName).ToList();
            else
                return users;
        }
        internal void DeleteUser() 
        {
            using (ApplicationDbContext context = new())
            {
                var result = MessageBox.Show("Вы точно удалить этого пользователя?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Users.Remove(SelectedUser);
                    context.SaveChanges();
                    DisplayUsers();
                }
            }
        }
        internal void SetRoleToUser()
        {
            using (ApplicationDbContext context = new())
            {
                if(SelectedUser.RoleId!=2)
                {
                    SelectedUser.Role = null;
                    SelectedUser.RoleId = 2;
                    context.Users.Update(SelectedUser);
                    context.SaveChanges();
                    DisplayUsers();
                }
                else
                {
                    MessageBox.Show("У пользователя уже назначена эта роль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        internal void SetRoleToAdmin()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedUser.RoleId != 1)
                {
                    SelectedUser.Role = null;
                    SelectedUser.RoleId = 1;
                    context.Users.Update(SelectedUser);
                    context.SaveChanges();
                    DisplayUsers();
                }
                else
                {
                    MessageBox.Show("У пользователя уже назначена эта роль!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion

        #region Orders
        private List<Order> _ordersList;
        private Order _selectedOrder;
        private string _orderElementsCount;

        private string _orderSearchValue;
        private string _orderSortValue;
        private string _orderFiltherValue;
        public List<string> OrderFiltherValues { get; } = new List<string>()
        {
            "Все статусы",
            "Ожидает ответа",
            "В процессе",
            "Выполнен"
        };
        public List<string> OrderSortValues { get; } = new List<string>()
        {
            "Без сортировки",
            "По пользователю(возр.)",
            "По пользователю(убыв.)",
        };
        public List<Order> OrdersList 
        { 
            get => _ordersList;
            set 
            {
                Set(ref _ordersList, value, nameof(OrdersList));
                OrderElementsCount = $"{value.Count} / {GetOrders().Count}";
            } 
        }

        public Order SelectedOrder 
        { 
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value, nameof(SelectedOrder));
        }
        public string OrderElementsCount 
        { 
            get => _orderElementsCount;
            set => Set(ref _orderElementsCount, value, nameof(OrderElementsCount));
        }
        public string OrderSearchValue 
        { 
            get => _orderSearchValue;
            set 
            {
                Set(ref _orderSearchValue, value, nameof(OrderSearchValue));
                DisplayOrders();
            } 
        }
        public string OrderSortValue 
        { 
            get => _orderSortValue; 
            set
            {
                Set(ref _orderSortValue, value, nameof(OrderSortValue));
                DisplayOrders();
            }
        }
        public string OrderFiltherValue 
        { 
            get => _orderFiltherValue; 
            set
            {
                Set(ref _orderFiltherValue, value, nameof(OrderFiltherValue));
                DisplayOrders();
            }
        }
        private List<Order> GetOrders()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Orders
                    .Include(o => o.Product)
                    .Include(o => o.User)
                    .OrderBy(o => o.Id)
                    .ToList();
            }
        }
        private void DisplayOrders()
        {
            OrdersList=Sort(Search(Filther(GetOrders())));
        }
        private List<Order> Search(List<Order> orders)
        {
            if (OrderSearchValue == null || OrderSearchValue == string.Empty)
                return orders;
            else
                return orders.Where(o =>
                o.User.LastName.ToLower().Contains(OrderSearchValue.ToLower()) ||
                o.User.FirstName.ToLower().Contains(OrderSearchValue.ToLower()) ||
                o.User.MiddleName.ToLower().Contains(OrderSearchValue.ToLower()) ||
                o.Product.Title.ToLower().Contains(OrderSearchValue.ToLower())
                ).ToList();
        }
        private List<Order> Filther(List<Order> orders)
        {
            if (OrderFiltherValue == OrderFiltherValues[0])
                return orders;
            else
                return orders.Where(o => o.OrderStatus == OrderFiltherValue).ToList();
        }
        private List<Order> Sort(List<Order> orders)
        {
            if (OrderSortValue == OrderSortValues[1])
                return orders.OrderBy(o => o.UserId).ToList();
            else if (OrderSortValue == OrderSortValues[2])
                return orders.OrderByDescending(o => o.UserId).ToList();
            else
                return orders;
        }
        internal void DeleteOrder()
        {
            using (ApplicationDbContext context = new())
            {
                var result = MessageBox.Show("Вы точно удалить этот заказ?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Orders.Remove(SelectedOrder);
                    context.SaveChanges();
                    DisplayOrders();
                }
            }
        }
        internal void SetStatusToWaiting()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedOrder.OrderStatus!= "Ожидает ответа")
                {
                    SelectedOrder.OrderStatus = "Ожидает ответа";
                    context.Orders.Update(SelectedOrder);
                    context.SaveChanges();
                    DisplayOrders();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        internal void SetStatusToInProgress()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedOrder.OrderStatus != "В процессе")
                {
                    SelectedOrder.OrderStatus = "В процессе";
                    context.Orders.Update(SelectedOrder);
                    context.SaveChanges();
                    DisplayOrders();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        internal void SetStatusToCompleted()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedOrder.OrderStatus != "Выполнен")
                {
                    SelectedOrder.OrderStatus = "Выполнен";
                    context.Orders.Update(SelectedOrder);
                    context.SaveChanges();
                    DisplayOrders();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion
        #region Request
        private List<Request> _requestsList;
        private Request _selectedRequest;
        private string _requestElementsCount;

        private string _requestSearchValue;
        private string _requestSortValue;
        private string _requestFiltherValue;
        public List<string> RequestFiltherValues { get; } = new List<string>()
        {
            "Все статусы",
            "Ожидает ответа",
            "В процессе",
            "Выполнен"
        };
        public List<string> RequestSortValues { get; } = new List<string>()
        {
            "Без сортировки",
            "По пользователю(возр.)",
            "По пользователю(убыв.)",
        };
        public List<Request> RequestsList
        {
            get => _requestsList;
            set
            {
                Set(ref _requestsList, value, nameof(RequestsList));
                RequestElementsCount = $"{value.Count} / {GetRequests().Count}";
            }
        }

        public Request SelectedRequest 
        { 
            get => _selectedRequest;
            set => Set(ref _selectedRequest, value, nameof(SelectedRequest));
        }
        public string RequestElementsCount 
        { 
            get => _requestElementsCount;
            set => Set(ref _requestElementsCount, value, nameof(RequestElementsCount));
        }
        public string RequestSearchValue 
        { 
            get => _requestSearchValue; 
            set
            {
                Set(ref _requestSearchValue, value, nameof(RequestSearchValue));
                DisplayRequests();
            }
        }
        public string RequestSortValue 
        { 
            get => _requestSortValue; 
            set
            {
                Set(ref _requestSortValue, value, nameof(RequestSortValue));
                DisplayRequests();
            } 
        }
        public string RequestFiltherValue 
        { 
            get => _requestFiltherValue; 
            set
            {
                Set(ref _requestFiltherValue, value, nameof(RequestFiltherValue));
                DisplayRequests();
            }
        }

        private List<Request> GetRequests()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Requests
                    .Include(r => r.User)
                    .Include(r => r.Service)
                    .OrderBy(r => r.Id)
                    .ToList();
            }
        }
        private void DisplayRequests()
        {
            RequestsList = Sort(Search(Filther(GetRequests())));
        }
        private List<Request> Search(List<Request> requests)
        {
            if (RequestSearchValue == null || RequestSearchValue == string.Empty)
                return requests;
            else
                return requests.Where(r =>
                r.User.LastName.ToLower().Contains(RequestSearchValue.ToLower()) ||
                r.User.FirstName.ToLower().Contains(RequestSearchValue.ToLower()) ||
                r.User.MiddleName.ToLower().Contains(RequestSearchValue.ToLower()) ||
                r.Service.Title.ToLower().Contains(RequestSearchValue.ToLower())
                ).ToList();
        }
        private List<Request> Filther(List<Request> requests)
        {
            if (RequestFiltherValue == RequestFiltherValues[0])
                return requests;
            else
                return requests.Where(r => r.RequestStatus == RequestFiltherValue).ToList();
        }
        private List<Request> Sort(List<Request> requests)
        {
            if (RequestSortValue == RequestSortValues[1])
                return requests.OrderBy(r => r.UserId).ToList();
            else if (RequestSortValue == RequestSortValues[2])
                return requests.OrderByDescending(r => r.UserId).ToList();
            else
                return requests;
        }
        internal void DeleteRequest()
        {
            using (ApplicationDbContext context = new())
            {
                var result = MessageBox.Show("Вы точно удалить этот запрос?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Requests.Remove(SelectedRequest);
                    context.SaveChanges();
                    DisplayRequests();
                }
            }
        }
        internal void RequestSetStatusToWaiting()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedRequest.RequestStatus != "Ожидает ответа")
                {
                    SelectedRequest.RequestStatus = "Ожидает ответа";
                    context.Requests.Update(SelectedRequest);
                    context.SaveChanges();
                    DisplayRequests();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        internal void RequestSetStatusToInProgress()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedRequest.RequestStatus != "В процессе")
                {
                    SelectedRequest.RequestStatus = "В процессе";
                    context.Requests.Update(SelectedRequest);
                    context.SaveChanges();
                    DisplayRequests();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        internal void RequestSetStatusToCompleted()
        {
            using (ApplicationDbContext context = new())
            {
                if (SelectedRequest.RequestStatus != "Выполнен")
                {
                    SelectedRequest.RequestStatus = "Выполнен";
                    context.Requests.Update(SelectedRequest);
                    context.SaveChanges();
                    DisplayRequests();
                }
                else
                {
                    MessageBox.Show("У заказа уже назначен этот статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion
        public AdminControlWindowViewModel()
        {
            _usersList = new List<User>(GetUsers());
            _ordersList = new List<Order>(GetOrders());
            _requestsList= new List<Request>(GetRequests());
            using (ApplicationDbContext context = new())
            {
                UserFiltherValues.AddRange(context.Roles.Select(r => r.Title));
            }
            UserFiltherValue = UserFiltherValues[0];
            UserSortValue = UserSortValues[0];
            OrderFiltherValue= OrderFiltherValues[0];
            OrderSortValue = OrderSortValues[0];
            RequestFiltherValue= RequestFiltherValues[0];
            RequestSortValue = RequestSortValues[0];
        }
        
        
    }
}
