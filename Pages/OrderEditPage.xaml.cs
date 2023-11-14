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

            headLabel.Content = headLabel.Content + $" {editableOrder.Id}";

            //здесь надо добавить проверкку на наличие
            if (editableOrder.Executor != null)
                currentExecutor.Content = (currentExecutor.Content + $"id: {editableOrder.Executor.Surname} Фамилия: {editableOrder.Description}");

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
