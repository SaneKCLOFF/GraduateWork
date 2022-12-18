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
    /// Логика взаимодействия для UserRequestsWindow.xaml
    /// </summary>
    public partial class UserRequestsWindow : Window
    {
        private UserRequestsWindowViewModel _viewModel;
        public UserRequestsWindow(User user)
        {
            InitializeComponent();
            _viewModel = new UserRequestsWindowViewModel(user);
            DataContext = _viewModel;
        }

        private void ButtonAddRequest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenAddRequestWindow();
        }

        private void buttonDeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteRequest();
        }
    }
}
