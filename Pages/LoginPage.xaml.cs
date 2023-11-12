using Microsoft.EntityFrameworkCore;
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
    public partial class LoginPage : Page
    {
        ApplicationDbContext dbContext;

        public LoginPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
        }

        private void loginButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredUsername = loginTextBox.Text;
            string enteredPassword = passwordBox.Password.ToString();

            if (IsLoginValid(enteredUsername, enteredPassword))
            {
                
                MainWindow.currentUser = dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Login == enteredUsername);
                NavigationService.Navigate(new ActiveOrdersPage()); ;

            }
            else
            {
                // Уведомить пользователя о неправильном логине или пароле
                MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void registerButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage()); ;
        }

            private bool IsLoginValid(string username, string password)
        {
            // Проверьте в базе данных, существует ли пользователь с введенным именем пользователя
            var user = dbContext.Users.FirstOrDefault(u => u.Login == username);

            if (user.Password == password)
                return true;
            else
                return false;
        }


    }
}
