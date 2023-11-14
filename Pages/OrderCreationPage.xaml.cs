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
    /// Логика взаимодействия для OrderCreationPage.xaml
    /// </summary>
    public partial class OrderCreationPage : Page
    {
        ApplicationDbContext dbContext;
        Order newOrder = new Order();

        public OrderCreationPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();

            deviceComboBox.ItemsSource = dbContext.Devices.ToList();
            defectComboBox.ItemsSource = dbContext.Defects.ToList();
        }

        
        public void CreateOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if(deviceComboBox.SelectedItem != null && defectComboBox.SelectedItem != null)
            {
                newOrder.ClientId = MainWindow.currentUser.Id;
                //newOrder.DeviceId = Int32.Parse(deviceComboBox.SelectedValue.ToString());
                //newOrder.DefectId = Int32.Parse(defectComboBox.SelectedItem.ToString());
                newOrder.Device = (Device)deviceComboBox.SelectedItem;
                newOrder.DeviceId = newOrder.Device.Id;
                newOrder.Defect = (Defect)defectComboBox.SelectedItem;
                newOrder.DefectId = newOrder.Defect.Id; 
                newOrder.CreationDate = DateTime.Now;
                newOrder.Description = descriptionTextBox.Text;
                newOrder.Status = dbContext.Statuses.Where(s => s.StatusName == "В работе").First();

                dbContext.Add(newOrder);
                dbContext.SaveChanges();

                MessageBox.Show($"Заявка создана", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.instance.Content = new ActiveOrdersPage();

            }
        }
    }
}
