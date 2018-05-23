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
        #region Variables / Properties
        List<string> Header { get; set; }
        public List<int> Years { get; set; }
        public Encoding Encoding { get; set; }
        #endregion

        #region Constructor
        public ExcelExport()
        {
            Header = new List<string>();
            Years = new List<int>();
        }
        #endregion

        #region Public methods
        public string GetExportPath(ListBox rightBox)
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = "CSV filer (*.csv)|*.csv|All files (*.*)|*.*",
                InitialDirectory = @"C:\%USERNAME%\"
            };

            saveDlg.ShowDialog();

            return saveDlg.FileName;
        }

        public void Export(ListBox listBox, ComboBox startYearComboBox, string path, List<Employee> employees)
        {
            List<AccountCard> accountCards = new List<AccountCard>();
          
            Encoding = Encoding.GetEncoding("iso-8859-1");

            Header.Add("Medarbejder");
            Header.Add("Firma");

            foreach (int year in startYearComboBox.Items)
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

                using (StreamWriter streamWriter = new StreamWriter(@path, true, Encoding))
                {
                    streamWriter.WriteLine("sep=;");
                    streamWriter.WriteLine(String.Join<string>(";", Header));
                    streamWriter.WriteLine("");

                    List<Client> clients = new List<Client>();

                    foreach (Client client in listBox.Items)
                    {
                        clients.Add(client);
                    }

                    clients.Sort((x, y) => x.MainEmployee.EmployeeID.CompareTo(y.MainEmployee.EmployeeID));

                    foreach (Client client in clients)
                    {
                        string balanceString = ";";
                        int totalBalance = 0;

                        foreach (int year in Years)
                        {
                            if (client.AccountCards[0].Year > year)
                            {
                                balanceString += "N/A ;";
                            }
                            else
                            {
                                if (client.AccountCards[0].Year == year)
                                {
                                    for (int i = 0; i < client.AccountCards.Count; i++)
                                    {
                                        totalBalance += client.AccountCards[i].Balance;
                                        balanceString +=client.AccountCards[i].Balance + ";";
                                    }
                                }
                            }
                        }

                        string employeeName = "kunne ikke finde navn";

                        foreach (Employee employee in employees)
                        {
                            if (employee.EmployeeID == client.MainEmployee.EmployeeID)
                            {
                                employeeName = employee.FirstName;
                            }
                        }

                        balanceString += " "+";" + totalBalance;
                        streamWriter.WriteLine(employeeName+";"+client.ClientName+balanceString);
                    }
                }

                MessageBox.Show("Filen blev eksporteret", "Succes");
            }
            catch (Exception e)
            {
                MessageBox.Show("Fejl: " + e.Message);
            }
        }
        #endregion
    }
}
