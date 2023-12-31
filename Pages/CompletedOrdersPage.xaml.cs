﻿using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для CompletedOrders.xaml
    /// </summary>
    public partial class CompletedOrdersPage : Page
    {
        private ApplicationDbContext dbContext;

        public CompletedOrdersPage()
        {
            InitializeComponent();

            dbContext = new ApplicationDbContext();

            if (MainWindow.currentUser.Role.RoleName == "Manager" || MainWindow.currentUser.Role.RoleName == "Executor")
            {
                var activeOrders = dbContext.Orders
                    .Where(o => o.StatusId == 2 || o.StatusId == 3)
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
                    .Where(o => (o.StatusId == 2 || o.StatusId == 3) && o.ClientId == MainWindow.currentUser.Id)
                    .Include(o => o.Client)
                    .Include(o => o.Defect)
                    .Include(o => o.Device)
                    .Include(o => o.Executor)
                    .Include(o => o.Status)
                    .ToList();
                activeOrdersDataGrid.ItemsSource = activeOrders;
            }
        }
    }
}
