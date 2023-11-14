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

            if (MainWindow.currentUser.Role.RoleName == "Admin" || MainWindow.currentUser.Role.RoleName == "Executor")
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

            //DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            Order selectedOrder = (Order)((Button)e.Source).DataContext;
            NavigationService.Navigate(new OrderEditPage(selectedOrder.Id));
        }

        private void NewOrderButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderCreationPage());
        }

    }
}
