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

namespace GraduateWork.ViewModels
{
    internal class MainWindowViewModel:ViewModelBase
    {
        private User _currentUser;
        private List<Product> _displayingProducts;
        private string _elementsCount;
        private Product _selectedProduct;

        private string _searchValue;
        private string _filtherValue;
        private string _sortValue;

        public List<string> FiltherValues { get; } = new List<string>()
        {
            "Все типы"
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
        public List<Product> DisplayingProducts
        {
            get => _displayingProducts;
            set
            {
                Set(ref _displayingProducts, value, nameof(DisplayingProducts));
                ElementsCount = $"{value.Count} / {GetProducts().Count}";
            }
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
                DisplayProducts();
            } 
        }
        public string FiltherValue 
        { 
            get => _filtherValue;
            set 
            {
                Set(ref _filtherValue, value, nameof(FiltherValue));
                DisplayProducts();
            } 
        }
        public string SortValue 
        { 
            get => _sortValue;
            set 
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayProducts();
            } 
        }
        public Product SelectedProduct 
        { 
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value, nameof(SelectedProduct));
        }
        public MainWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            using (ApplicationDbContext context = new())
            {
                DisplayingProducts= new List<Product>(GetProducts());
                FiltherValues.AddRange(context.ProductCategories.Select(pc=>pc.Title));
            }
            SearchValue = null;
            FiltherValue = FiltherValues[0];
            SortValue = SortValues[0];
        }
        private List<Product> GetProducts()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Products
                    .Include(pc => pc.Category)
                    .OrderBy(p=>p.Id)
                    .ToList();
            }
        }
        private void DisplayProducts()
        {
            DisplayingProducts = Sort(Search(Filther(GetProducts())));
        }
        private List<Product> Search(List<Product> products)
        {
            if (SearchValue == null || SearchValue == string.Empty)
                return products;
            else
                return products.Where(p => p.Title.ToLower().Contains(SearchValue.ToLower())).ToList();
        }
        private List<Product> Filther(List<Product> products)
        {
            if (FiltherValue == FiltherValues[0])
                return products;
            else
                return products.Where(p=>p.Category.Title==FiltherValue).ToList();
        }
        private List<Product> Sort(List<Product> products)
        {
            if (SortValue == SortValues[1])
                return products.OrderBy(p => p.Cost).ToList();
            else if (SortValue == SortValues[2])
                return products.OrderByDescending(p => p.Cost).ToList();
            else
                return products;
        }

        internal void AddOrder()
        {
            using (ApplicationDbContext context = new())
            {
                Order newOrder = new Order()
                {
                    UserId=CurrentUser.Id,
                    ProductId=SelectedProduct.Id,
                    OrderStatus = "Ожидает ответа",
                };
                context.Orders.Add(newOrder);
                context.SaveChanges();
                MessageBox.Show("Продукт успешно заказан!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
        internal void OpenUserOrders()
        {
            new UserOrdersWindow(CurrentUser).ShowDialog();
        }
        internal void OpenUserRequests()
        {
            new UserRequestsWindow(CurrentUser).ShowDialog();
        }
        internal void OpenUserSetings()
        {
            new UserSettingsWindow(CurrentUser).ShowDialog();
        }
        internal void OpenAdminControl(int tabIndex)
        {
            new AdminControlWindow().ShowDialog();
        }
    }
}
