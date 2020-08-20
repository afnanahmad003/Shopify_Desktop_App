using ApplicationLogger;
using RESSWATCH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shppify_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TraceConfig(Path.GetDirectoryName(Application.ExecutablePath) + "\\Logs", 20, 3000, true, true, true, true);


            Logger.WriteLog("DBConServer has started.", LogLevel.GENERALLOG);

            DataAccess.CON_STR = System.Configuration.ConfigurationSettings.AppSettings["SQLConStr"];

            if (!DataAccess.DatabaseAvailable)
            {
                Logger.WriteLog("SQL is not connected.", LogLevel.DBLOG);
            }
            else
                Logger.WriteLog("SQL is connected.", LogLevel.DBLOG);

            ddlCategory.DataSource = BL.Collection.GetDistinctCollection();
            ddlCategory.DisplayMember = "CollectionID";
            ddlCategory.ValueMember = "CollectionID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BL.Collection.UpdateProfitMargin(ddlCategory.SelectedValue.ToString(), txtMargin.Text);
            MessageBox.Show("Profit margin saved.");
        }
        private void TraceConfig(string Path, int RotateFrequency, int FileSize, bool Communication, bool Database, bool Telephony, bool General)
        {
            try
            {
                Logger.EnableCOMMSLOG = Communication;
                Logger.EnableDBLOG = Database;
                Logger.EnableGENERALLOG = General;
                Logger.EnableTELEPHONYLOG = Telephony;
                Logger.EnableERRORLOG = Logger.EnableSECURELOG = true;
                Logger.FilePath = Path;
                Logger.LogFileName = "ShppifyManagement Logs";
                Logger.LogFileSize = FileSize;
                Logger.NoOfLogFiles = RotateFrequency;
                Logger.ActivateOptions();
                Logger.WriteLog("Init logs", LogLevel.GENERALLOG);
            }
            catch (Exception E)
            {
                string strEx = E.Message;
            }
        }

    }
}
