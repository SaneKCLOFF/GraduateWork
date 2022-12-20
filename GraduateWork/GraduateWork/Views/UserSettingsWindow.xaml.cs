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
    /// Логика взаимодействия для UserSettingsWindow.xaml
    /// </summary>
    public partial class UserSettingsWindow : Window
    {
        private UserSettingsWindowViewModel _viewModel;
        public UserSettingsWindow(User user)
        {
            InitializeComponent();
            _viewModel=new UserSettingsWindowViewModel(user);
            DataContext= _viewModel;
            if (user.Role.Id!=1)
            {
                TextBlockRole.Visibility = Visibility.Hidden;
                ComboBoxRoles.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSaveUser_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveNewUserData();
        }
    }
}
