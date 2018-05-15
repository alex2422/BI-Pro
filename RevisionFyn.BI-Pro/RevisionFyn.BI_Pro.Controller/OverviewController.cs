using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class OverviewController
    {
        #region Variables
        private static OverviewController controllerInstance;

        ObservableCollection<Company> dummyData = new ObservableCollection<Company>();
        ObservableCollection<Company> dummyData2 = new ObservableCollection<Company>();
        ObservableCollection<string> years = new ObservableCollection<string>();
        string encoding;
        string filePath;
        public ListBox LeftBox { get; set; }
        public ListBox RightBox { get; set; }
        #endregion

        #region Private methods
        private OverviewController(ListBox leftBox, ListBox rightBox)
        {
            LeftBox = leftBox;
            RightBox = rightBox;
            rightBox.ItemsSource = dummyData2;
            leftBox.ItemsSource = dummyData;
        }
        #endregion

        #region Public methods
        public static OverviewController GetInstance(ListBox leftBox, ListBox rightBox)
        {
            if (controllerInstance == null)
            {
                controllerInstance = new OverviewController(leftBox, rightBox);
            }
            return controllerInstance;
        }

        public void ButtonTest(ListBox companies)
        {
            StoredProcedure sp = new StoredProcedure();
            companies.ItemsSource = sp.GetCompanies();
            companies.DisplayMemberPath = "CompanyName"; //dette skulle gerne virke :D
            //dummyData.Add("Mærsk");
            //dummyData.Add("FiskeTorvet");
            //dummyData.Add("Guby");
            //dummyData.Add("Grillen");

            //companies.ItemsSource = dummyData;
        }

        public void ButtonAdd()
        {
            dummyData2.Add((Company)LeftBox.SelectedItem);
            dummyData.Remove((Company)LeftBox.SelectedItem);
        }

        public void ButtonRemove()
        {
            dummyData.Add((Company)RightBox.SelectedItem);
            dummyData2.Remove((Company)RightBox.SelectedItem);
        }

        public void ComboBoxYear()
        {
            years.Add("2017");
            years.Add("2018");
            years.Add("2019");
            years.Add("2020");
            years.Add("2021");
            years.Add("2022");
            years.Add("2023");
            years.Add("2024");
            years.Add("2025");
        }

        public void LoadIntoComoBox(ComboBox yearsBox)
        {
            yearsBox.ItemsSource = years;
        }

        public void ExportButton()
        {

        }
        //public void CSVExportToOverview(string path)
        //{
        //    filePath = path;
        //}
        //public void CreateFile(string filePath)
        //{
        //    try
        //    {
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        using (StreamWriter streamWriter = new StreamWriter(@filePath, true, Encoding.GetEncoding("iso-8859-1")))
        //        {
        //            Company lowestYear = dummyData2[0];
        //            foreach (Company company in dummyData2)
        //            {
        //                if (company.CompanyStartYear < lowestYear.CompanyStartYear)
        //                {
        //                    lowestYear = company;
        //                }
        //            }
        //            string yearsString = lowestYear.CompanyStartYear.ToString()+";";
        //            foreach (var year in lowestYear.years)
        //            {
        //                yearsString += ";" + year.ToString();
        //            }
        //            streamWriter.WriteLine("Firma" + yearsString);
        //            string balanceString = "";
        //            foreach (Company company in dummyData2)
        //            {
        //                company.accountCards.Sort((x, y) => x.Year.CompareTo(y.Year));
        //                for (int year = 0; year < yearsString.Split(';').Count();)
        //                {
        //                    int timesRun = 0;
        //                    if (company.accountCards[year].Year == lowestYear.CompanyStartYear+timesRun)
        //                    {
        //                        balanceString += ";" + company.accountCards[year].Balance;
        //                        year++;
        //                        timesRun++;
        //                    }
        //                    else
        //                    {
        //                        balanceString += ";N/A";
        //                        timesRun++;
        //                    }
        //                }
        //                streamWriter.WriteLine(company.CompanyName+balanceString);
        //            }
        //            streamWriter.Close();
        //        }
        //        using (StreamReader sr = File.OpenText(filePath))
        //        {
        //            string s = "";
        //            while ((s = sr.ReadLine()) != null)
        //            {
        //                Console.WriteLine(s);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Filen blev ikke gemt");
        //    }
        }
        #endregion


    }
}
