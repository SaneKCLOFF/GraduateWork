using GraduateWork.Models;
using GraduateWork.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraduateWork.ViewModels
{
    internal class RegisterWindowViewModel:ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _email;
        private string _phone;
        private string _login;
        private string _password;
        public const int RoleId = 2;

        public string FirstName 
        { 
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }
        public string LastName 
        { 
            get => _lastName; 
            set => Set(ref _lastName, value, nameof(LastName));
        }
        public string MiddleName 
        { 
            get => _middleName;
            set => Set(ref _middleName, value, nameof(MiddleName));
        }
        public string Email 
        { 
            get => _email;
            set => Set(ref _email, value, nameof(Email));
        }
        public string Phone 
        { 
            get => _phone;
            set => Set(ref _phone, value, nameof(Phone));
        }
        public string Login 
        { 
            get => _login;
            set => Set(ref _login, value, nameof(Login));
        }
        public string Password 
        { 
            get => _password; 
            set => Set(ref _password, value, nameof(Password));
        }

        internal void RegisterNewUser()
        {
            using (ApplicationDbContext context = new())
            {
                var IsExist = context.Users
                    .Any(u=>u.Login==Login || u.Password==Password || u.Email==Email || u.PhoneNumber==Phone);
                if(!IsExist)
                {
                    context.Users.Add(new User { FirstName=FirstName, LastName=LastName, MiddleName=MiddleName, PhoneNumber=Phone, Email=Email,Login=Login, Password=Password,RoleId=RoleId});
                    context.SaveChanges();
                    MessageBox.Show("Регистраия успешна!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Некоторые из введённых данных уже используются!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
