using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace RevisionFyn.BI_Pro.Model
{
    class ExcelExport
    {
        #region Variables
        //SaveFileDialog saveFileDialog { get; set; }
        StringBuilder csvImport { get; set; }
        List<string> Header { get; set; } = new List<string>();

        


        #endregion

        #region Public methods

        public void trialExport()
        {
            SaveFileDialog saveDlg = new SaveFileDialog();

            saveDlg.Filter = "CSV filer (*.csv)|*.csv|All files (*.*)|*.*";
            saveDlg.InitialDirectory = @"C:\%USERNAME%\";
            saveDlg.ShowDialog();

            string path = saveDlg.FileName;
        }


        #endregion

        #region Private methods

        private void TrialBuilder()
        {
            csvImport = new StringBuilder();

            csvImport.AppendLine(String.Format("{0},{1}", Header[0], Header[1]));

            File.WriteAllText(@"C:\Users\Bruger\Desktop\test.csv", csvImport.ToString());
        }

        #endregion
    }
}
