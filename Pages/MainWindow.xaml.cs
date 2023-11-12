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
using UPSAB.Pages;

namespace UPSAB.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame instance {  get; set; }

        public static Page loginPage = new LoginPage();

        public MainWindow()
        {
            InitializeComponent();
            instance = MainFrame;
            instance.Content = loginPage;
        }
    }
}
