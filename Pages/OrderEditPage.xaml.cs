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
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEditPage : Page
    {

        private Order editableOrder = new Order();
        ApplicationDbContext dbContext;
        public OrderEditPage(int orderId)
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();


            editableOrder = dbContext.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.Executor)
                .First();

            headLabel.Content += $"{editableOrder.Id}";

            if (editableOrder.Executor != null)
                currentExecutor.Content = (currentExecutor.Content + $"Id исполнителя: {editableOrder.Executor.Id} Фамилия исп.: {editableOrder.Executor.Surname}");
            else
                currentExecutor.Content = "Испонитель еще не назначен!";

            currentDescription.Content = editableOrder.Description;

            if (MainWindow.currentUser.Role.RoleName == "Manager")
            {
                executorIdEditLabel.Visibility = Visibility.Visible;
                executorIdEditTextBox.Visibility = Visibility.Visible;
                confirmIdButton.Visibility = Visibility.Visible;
            }

            if (MainWindow.currentUser.Role.RoleName == "Client")
            {
                descriptionEditLabel.Visibility = Visibility.Visible;
                descriptionEditTextBox.Visibility = Visibility.Visible;
                confirmDescriptionButton.Visibility = Visibility.Visible;
            }

            if (MainWindow.currentUser.Role.RoleName == "Executor")
            {
                executorCommentEditLabel.Visibility = Visibility.Visible;
                executorCommentEditTextBox.Visibility = Visibility.Visible;
                confirmExecutorCommentButton.Visibility = Visibility.Visible;

                completeOrderNegativeButton.Visibility = Visibility.Visible;
                completeOrderPositiveButton.Visibility = Visibility.Visible;
            }
        }

        public void ConfirmIdButtonClick(object sender, RoutedEventArgs e)
        {
            if (executorIdEditTextBox.Text != "")
            {
                User executor = new User();
                try
                {
                    executor = dbContext.Users
                        .Include(e => e.Role)
                        .First(e => e.Id == Int32.Parse(executorIdEditTextBox.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Внимание! пользователь с таким Id не найден: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (executor.Role.RoleName != "Executor")
                {
                    MessageBox.Show($"Внимание! Исполнитель с таким Id не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    editableOrder.ExecutorId = executor.Id;
                    dbContext.SaveChanges();
                    MessageBox.Show($"Изменения применены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow.instance.Content = new OrderEditPage(editableOrder.Id);
                }
            }
        }

        public void ConfirmDescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            if (descriptionEditTextBox.Text != "")
            {
                editableOrder.Description = descriptionEditTextBox.Text;
                dbContext.SaveChanges();
                MessageBox.Show($"Изменения применены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.instance.Content = new OrderEditPage(editableOrder.Id);
            }
            else
            {
                MessageBox.Show($"Введите данные", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void ConfirmExecutorCommentButtonClick(object sender, RoutedEventArgs e)
        {
            if (executorCommentEditTextBox.Text != "")
            {
                editableOrder.ExecutorComment = executorCommentEditTextBox.Text;
                dbContext.SaveChanges();
                MessageBox.Show($"Изменения применены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.instance.Content = new OrderEditPage(editableOrder.Id);
            }
            else
            {
                MessageBox.Show($"Введите данные", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        
        public void CompleteOrderButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name == "completeOrderPositiveButton")
            {
                editableOrder.StatusId = dbContext.Statuses
                    .Where(s => s.StatusName == "Выполнено")
                    .First().Id;
                editableOrder.CompletionDate = DateTime.Now;
                dbContext.SaveChanges();
                MessageBox.Show($"Изменения применены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.instance.Content = new OrderEditPage(editableOrder.Id);
            }
            else
            {
                editableOrder.StatusId = dbContext.Statuses
                    .Where(s => s.StatusName == "Не выполнено")
                    .First().Id;
                editableOrder.CompletionDate = DateTime.Now;
                dbContext.SaveChanges();
                MessageBox.Show($"Изменения применены", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.instance.Content = new OrderEditPage(editableOrder.Id);
            }
        }
    }
}
