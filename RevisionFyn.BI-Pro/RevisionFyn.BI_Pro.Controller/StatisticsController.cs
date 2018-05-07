using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RevisionFyn.BI_Pro.Controller
{
    public class StatisticsController
    {
        #region Variables
        private static StatisticsController controllerInstance;

        ObservableCollection<string> dummyData = new ObservableCollection<string>();
        ObservableCollection<string> dummyData2 = new ObservableCollection<string>();
        ObservableCollection<string> years = new ObservableCollection<string>();

        public ListBox LeftBox { get; set; }
        public ListBox RightBox { get; set; }
        
        #endregion

        #region Public methods

        private StatisticsController(ListBox leftBox, ListBox rightBox)
        {
            LeftBox = leftBox;
            RightBox = rightBox;
            rightBox.ItemsSource = dummyData2;
            leftBox.ItemsSource = dummyData;
            
        }
        public static StatisticsController GetInstance(ListBox leftBox, ListBox rightBox)
        {
            if (controllerInstance == null)
            {
                controllerInstance = new StatisticsController(leftBox, rightBox);
            }
            return controllerInstance;
        }

        public void ButtonTest(ListBox companies)
        {
           
            dummyData.Add("Mærsk");
            dummyData.Add("FiskeTorvet");
            dummyData.Add("Guby");
            dummyData.Add("Grillen");

            companies.ItemsSource = dummyData;
        }

        public void ButtonAdd()
        {
            dummyData2.Add(Convert.ToString(LeftBox.SelectedItem));
            dummyData.Remove(Convert.ToString(LeftBox.SelectedItem));

            
        }

        public void ButtonRemove()
        {
            dummyData.Add(Convert.ToString(RightBox.SelectedItem));
            dummyData2.Remove(Convert.ToString(RightBox.SelectedItem));

           
            
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
        #endregion


        #region Private methods

        #endregion
    }
}
