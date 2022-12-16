using GraduateWork.Models;
using GraduateWork.Models.Entities;
using GraduateWork.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraduateWork.ViewModels
{
    internal class LoginWindowViewModel:ViewModelBase
    {
        private User _currentUser=new();
        public User CurrentUser 
        { 
            get => _currentUser; 
            set => Set(ref _currentUser, value, nameof(CurrentUser)); 
        }

        internal void Login()
        {
            using(ApplicationDbContext context = new())
            {
                if((CurrentUser.Login==string.Empty || CurrentUser.Login==null)
                    ||(CurrentUser.Password==null || CurrentUser.Password==string.Empty))
                    MessageBox.Show("Введите данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (context.Users.Where(u => u.Login == CurrentUser.Login && u.Password == CurrentUser.Password).Count() == 0)
                    MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    new MainWindow(context.Users.Where(u => u.Login == CurrentUser.Login && u.Password == CurrentUser.Password).Single()).ShowDialog();
            }
        }
    }
}
