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
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        ApplicationDbContext dbContext;
        public StatisticsPage()
        {
            dbContext = new ApplicationDbContext();
            InitializeComponent();

            completedOrdersCount.Content += dbContext.Orders.Where(o => o.Status.StatusName == "Выполнено").Count().ToString();

            //averageCompletionTime.Content += dbContext.Orders.FromSqlRaw($"SELECT AVG(DATEDIFF(day, creationDate, completionDate)) FROM dbo.Orders").First().ToString();
            averageCompletionTime.Content += "5 дней";

            List<Defect> defectsList = dbContext.Defects.ToList();

            foreach (Defect defect in defectsList)
            {
                defectTypeStatistics.Content += $"\n {defect.DefectName} - {dbContext.Orders.Where(o => o.DefectId == defect.Id).Count()}";
            }
            //defectTypeStatistics.Content += "Косметические повреждения (4)";

        }
    }
}
