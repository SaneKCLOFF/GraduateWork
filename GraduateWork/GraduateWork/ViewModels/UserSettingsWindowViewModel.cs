using GraduateWork.Models;
using GraduateWork.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.ViewModels
{
    internal class UserSettingsWindowViewModel:ViewModelBase
    {
        private User _currentUser;
        private User _editedUser;


        public User CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value, nameof(CurrentUser));
        }
        public User EditedUser
        {
            get => _editedUser;
            set => Set(ref _editedUser, value, nameof(EditedUser));
        }
        public List<Role> Roles { get; } = new List<Role>();

        public UserSettingsWindowViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            
            using (ApplicationDbContext context = new())
            {
                Roles.AddRange(context.Roles);
            }
            EditedUser = currentUser;
        }

        internal void SaveNewUserData()
        {
            using (ApplicationDbContext context = new())
            {
                CurrentUser = EditedUser;
                context.Users.Update(CurrentUser);
                context.SaveChanges();
            }
        }
    }
}
