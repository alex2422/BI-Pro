using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows;

namespace RevisionFyn.BI_Pro.Model
{
    public class ExcelExport
    {
        #region Variables
        //SaveFileDialog saveFileDialog { get; set; }
        StringBuilder csvImport { get; set; }
        List<string> Header { get; set; } = new List<string>();

        public int ClientStartYear { get; set; }
        public List<int> Years = new List<int>();
        public List<Client> clients = new List<Client>();
        public List<Employee> employees = new List<Employee>();
        Encoding encoding;

        #endregion

        #region Constructor
        public ExcelExport()
        {

        }
        #endregion

        #region Public methods
        public string GetExportPath(ListBox rightBox)
        { 
            SaveFileDialog saveDlg = new SaveFileDialog();

            saveDlg.Filter = "CSV filer (*.csv)|*.csv|All files (*.*)|*.*";
            saveDlg.InitialDirectory = @"C:\%USERNAME%\";
            saveDlg.ShowDialog();

            string path = saveDlg.FileName;
            return path;
        }
        public void Export(ListBox listBox, ComboBox startYear, string path, List<Employee> employees)
        {
            List<AccountCard> accCards = new List<AccountCard>();
            List<Employee> employeesList = employees;
            encoding = Encoding.GetEncoding("iso-8859-1");
            Header.Add("Medarbejder");
            Header.Add("Firma");
            foreach (int year in startYear.Items)
            {
                Years.Add(year);
            }
            foreach (int year in Years)
            {
                Header.Add(year.ToString());
            }
            Header.Add(" ");
            Header.Add("I alt");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (StreamWriter streamWriter = new StreamWriter(@path, true, encoding))
                {
                    streamWriter.WriteLine("sep=;");
                    streamWriter.WriteLine(String.Join<string>(";", Header));
                    streamWriter.WriteLine("");
                    List<Client> listOfClients = new List<Client>();
                    foreach (Client client in listBox.Items)
                    {
                        listOfClients.Add(client);
                    }
                    listOfClients.Sort((x, y) => x.MainEmployee.EmployeeID.CompareTo(y.MainEmployee.EmployeeID));
                    foreach (Client clients in listOfClients)
                    {
                        string balanceString = ";";
                        int totalBalance = 0;
                        foreach (int year in Years)
                        {
                            if (clients.accountCards[0].Year > year)
                            {
                                balanceString += "N/A ;";
                            }
                            else
                            {
                                if (clients.accountCards[0].Year == year)
                                {
                                    for (int i = 0; i < clients.accountCards.Count; i++)
                                    {
                                        totalBalance += clients.accountCards[i].Balance;
                                        balanceString +=clients.accountCards[i].Balance + ";";
                                    }
                                }
                            }
                        }
                        string employeeName = "kunne ikke finde navn";
                        foreach (Employee employee in employeesList)
                        {
                            if (employee.EmployeeID == clients.MainEmployee.EmployeeID)
                            {
                                employeeName = employee.FirstName;
                            }
                        }
                        balanceString += " "+";" + totalBalance;
                        streamWriter.WriteLine(employeeName+";"+clients.ClientName+balanceString);
                    }
                }
                MessageBox.Show("filen blev eksporteret", "Succes");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private methods
        private void TrialBuilder()
        {
            csvImport = new StringBuilder();

            csvImport.AppendLine(String.Format("{0},{1},{2},{3},{4}", Header[0], Header[1], Header[2], Header[3], Header[4]));

            File.WriteAllText(@"C:\Users\Bruger\Desktop\test.csv", csvImport.ToString());
        }

        #endregion
    }
}
