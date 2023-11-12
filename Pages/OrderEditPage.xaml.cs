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

        private Order editableOrder;
        ApplicationDbContext dbContext;
        public OrderEditPage(int orderId)
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();

            editableOrder = dbContext.Orders
                .Include(o => o.Executor)
                .FirstOrDefault(o => o.Id == orderId);

            currentExecutor.Content = (currentExecutor.Content + $"id: {editableOrder.Executor.Id} Фамилия: {editableOrder.Executor.Surname}");

            if (MainWindow.currentUser.Role.RoleName == "Manager")
            {
                executorIdEditTextBox.Visibility = Visibility.Visible;
                confirmButton.Visibility = Visibility.Visible;    
            }
        }

        public void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
