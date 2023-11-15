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
    /// Логика взаимодействия для ActiveOrdersPage.xaml
    /// </summary>
    public partial class ActiveOrdersPage : Page
    {
        private ApplicationDbContext dbContext;
        public ActiveOrdersPage()
        {
            InitializeComponent();
            
            dbContext = new ApplicationDbContext();

            if (MainWindow.currentUser.Role.RoleName == "Manager" || MainWindow.currentUser.Role.RoleName == "Executor")
            {
                var activeOrders = dbContext.Orders
                    .Where(o => o.StatusId == 1)
                    .Include(o => o.Client)
                    .Include(o => o.Defect)
                    .Include(o => o.Device)
                    .Include(o => o.Executor)
                    .Include(o => o.Status)
                    .ToList();
                activeOrdersDataGrid.ItemsSource = activeOrders;
            }
            else
            {
                var activeOrders = dbContext.Orders
                    .Where(o => o.StatusId == 1 && o.ClientId == MainWindow.currentUser.Id)
                    .Include(o => o.Client)
                    .Include(o => o.Defect)
                    .Include(o => o.Device)
                    .Include(o => o.Executor)
                    .Include(o => o.Status)
                    .ToList();
                activeOrdersDataGrid.ItemsSource = activeOrders;
            }

            if(MainWindow.currentUser.Role.RoleName == "Client")
                newOrderButton.Visibility = Visibility.Visible;
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //обьекту selectedOrder присваивается выбранный обьект из DataGrid, кнопка которого вызвала это событие
            Order selectedOrder = (Order)((Button)e.Source).DataContext;
            NavigationService.Navigate(new OrderEditPage(selectedOrder.Id));
        }

        
        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            
            int searchId = Int32.Parse(searchTextBox.Text);

            if(MainWindow.currentUser.Role.RoleName == "Manager" || MainWindow.currentUser.Role.RoleName == "Executor")
            {
                if (dbContext.Orders.Any(o => o.Id == searchId))
                {
                    NavigationService.Navigate(new OrderEditPage(searchId));
                }
                else
                {
                    MessageBox.Show($"Внимание! Заявка с таким Id не найдена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (dbContext.Orders.Where(o => o.ClientId == MainWindow.currentUser.Id).Any(o => o.Id == searchId))
                {
                    NavigationService.Navigate(new OrderEditPage(searchId));
                }
                else
                {
                    MessageBox.Show($"Внимание! Заявка с таким Id не найдена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void NewOrderButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderCreationPage());
        }

    }
}
