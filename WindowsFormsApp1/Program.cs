using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (var dbContext = new ApplicationDbContext())
            {
                //dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                //var query = dbContext.Database.SqlQuery<FT01>("SELECT TOP 1 * FROM FT01");
                //var configTable = query.FirstOrDefault();

                var configTable = dbContext.FT01s.FirstOrDefault();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
