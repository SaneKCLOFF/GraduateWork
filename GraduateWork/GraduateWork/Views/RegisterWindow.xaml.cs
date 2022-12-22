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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private RegisterWindowViewModel _viewModel;
        public RegisterWindow()
        {
            InitializeComponent();
            _viewModel = (RegisterWindowViewModel)DataContext;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RegisterNewUser();
        }
    }
}
