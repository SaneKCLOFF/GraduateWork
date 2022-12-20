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
    /// Логика взаимодействия для AdminControlWindow.xaml
    /// </summary>
    public partial class AdminControlWindow : Window
    {
        private AdminControlWindowViewModel _viewModel;
        public AdminControlWindow()
        {
            InitializeComponent();
            _viewModel = (AdminControlWindowViewModel)DataContext;
        }

        private void MenuItemDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteUser();
        }

        private void MenuItemUpdateRoleToUser_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetRoleToUser();
        }

        private void MenuItemUpdateRoleToAdmin_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetRoleToAdmin();
        }

        private void MenuItemDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteOrder();
        }

        private void MenuItemStatusToWaiting_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetStatusToWaiting();
        }

        private void MenuItemStatusInProgress_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetStatusToInProgress();
        }

        private void MenuItemStatusCompleted_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SetStatusToCompleted();
        }

        private void MenuItemDeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteRequest();
        }

        private void RequestMenuItemStatusToWaiting_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RequestSetStatusToWaiting();
        }

        private void RequestMenuItemStatusToInProgress_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RequestSetStatusToInProgress();
        }

        private void RequestMenuItemStatusToCompleted_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RequestSetStatusToCompleted();
        }
    }
}
