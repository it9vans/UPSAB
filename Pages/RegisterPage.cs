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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UPSAB.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private ApplicationDbContext dbContext;

        public RegisterPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
        }

        private void registerButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredLogin = loginTextBox.Text;
            string enteredPassword = passwordBox.Password.ToString();
            string enteredCheckPassword = checkPasswordBox.Password.ToString();
            string enteredSurname= surnameTextBox.Text;
            string enteredName= nameTextBox.Text;
            string enteredPatronymic= patronymicTextBox.Text;
            string enteredPhone= phoneTextBox.Text;

            if (enteredPassword != enteredCheckPassword)
            {
                MessageBox.Show("Пароли не совпадают. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User newUser = new User();
                newUser.Login = enteredLogin;
                newUser.Password = enteredPassword;
                newUser.Surname = enteredSurname; 
                newUser.Name = enteredName;
                newUser.Patronymic = enteredPatronymic;
                newUser.Phone = enteredPhone;
                newUser.RoleId = 2;

                if (IsRegistrationInputValid(newUser) && dbContext.Users.Where(u => u.Login == enteredLogin).Count() != 0 || dbContext.Users.Where(u => u.Phone == enteredPhone).Count() != 0) 
                {
                    MessageBox.Show("Пользователь с такими учетными данными уже существует. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                    
                }
            }
        }

        private bool IsRegistrationInputValid(User newUser)
        {
            if (newUser.Login != null
                && newUser.Name != null
                && newUser.Surname != null
                && newUser.Patronymic != null
                && newUser.Password != null
                && newUser.Phone != null)
                return true;
            else
                return false;
        }


    }
}
