using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class CustomizeStartScreenController
    {
        List<Company> companies = new List<Company>();
        private static CustomizeStartScreenController controllerInstance;
        private CustomizeStartScreenController()
        { }
        public static CustomizeStartScreenController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new CustomizeStartScreenController();
            }

            return controllerInstance;
        }
        public void CreateGraphValues()
        {
            List<double> Comp3Years = new List<double>() { 2012, 2013, 2014, 2015, 2016, 2017 };
            List<double> Comp2Years = new List<double>() { 2012, 2013, 2014, 2015, 2016, 2017 };
            List<double> Comp1Years = new List<double>() { 2012, 2013, 2014, 2015, 2016, 2017 };
            List<double> Comp1Coverage = new List<double>() { 120, 150, -200, -90, -10, 30 };
            List<double> Comp2Coverage = new List<double>() { 110, 140, -210, -100, -20, 20 };
            List<double> Comp3Coverage = new List<double>() { 50, 0, -50, 0, -5, -30 };
            Company Comp1 = new Company
            {
                CompanyName = "Firma1",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Years = Comp1Years,
                Coverages = Comp1Coverage,
                x = Comp1Years.ToArray(),
                y = Comp1Coverage.ToArray()
            };
            Company Comp2 = new Company
            {
                CompanyName = "Firma2",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Years = Comp2Years,
                Coverages = Comp2Coverage,
                x = Comp2Years.ToArray(),
                y = Comp1Coverage.ToArray()
            };
            Company Comp3 = new Company
            {
                CompanyName = "Firma3",
                CompanyStartYear = 2012,
                CompanyEndYear = 2017,
                Years = Comp3Years, 
                Coverages = Comp3Coverage,
                x = Comp3Years.ToArray(),
                y = Comp1Coverage.ToArray()
            };
            companies.Add(Comp1);
            companies.Add(Comp2);
            companies.Add(Comp3);
        }
        public void LoadValuesIntoComboBox(ComboBox comboBox)
        {
            foreach (Company Comp in companies)
            {
                if (!comboBox.Items.Contains(Comp.CompanyName))
                {
                    comboBox.Items.Add(Comp.CompanyName);
                }
            }
        }
    }
}
