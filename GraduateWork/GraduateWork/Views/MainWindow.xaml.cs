using GraduateWork.Models.Entities;
using GraduateWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraduateWork.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow(User user)
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(user);
            this.Title = $"Учётная запись: {user.UserFullName}";
            DataContext = _viewModel;

            if (user.RoleId!=1)
            {
                ButtonOrdersControl.Visibility = Visibility.Hidden;
                ButtonRequestsControl.Visibility = Visibility.Hidden;
                ButtonUsersControl.Visibility = Visibility.Hidden;
            }
        }

        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddOrder();
        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenUserOrders();
        }
    }
}
