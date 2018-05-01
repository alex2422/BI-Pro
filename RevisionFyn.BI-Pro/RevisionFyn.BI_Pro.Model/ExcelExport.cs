using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace RevisionFyn.BI_Pro.Model
{
    class ExcelExport
    {
        #region Variables
        object misValue = System.Reflection.Missing.Value;

        #endregion

        #region Public methods

        //public string ExcelNotInstalled()
        //{
        //    if (excelApp == null)
        //    {
        //        Console.WriteLine("Excel er ikke installeret ordenligt!!"); 

        //    }
        //    return null;
        //}

        //excelApp findes ikke i denne context, find ud af noget med det.

        public void CreateExcelDoc()
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;


            Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(misValue);
            //der kan tilføjes hvordan workbooken kommer til at se ud, http://csharp.net-informations.com/excel/csharp-create-excel.htm
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

            excelWorkbook.SaveAs("Under/overdækning", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //find ud af noget med navnet og hvor det bliver gemt henne.

            excelApp.Workbooks.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(excelWorksheet);
            Marshal.ReleaseComObject(excelWorkbook);
        }

        #endregion
    }
}
