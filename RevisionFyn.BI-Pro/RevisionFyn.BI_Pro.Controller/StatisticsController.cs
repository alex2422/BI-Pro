using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class StatisticsController
    {
        #region Variables
        private static StatisticsController controllerInstance;
        #endregion

        #region Constructor
        private StatisticsController()
        { }
        #endregion

        #region Public methods
        public static StatisticsController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new StatisticsController();
            }

            return controllerInstance;
        }

        public void LoadStatisticsTypeButtonsOnPanel(StackPanel statisticsTypeStackPanel)
        {
            StoredProcedure sp = new StoredProcedure();
            List<StatisticsType> activeStatisticsType = sp.GetActiveStatisticsType();

            foreach (StatisticsType st in activeStatisticsType)
            {
                Button typeChooseButton = new Button
                {
                    Name = String.Format("TypeChooseButton{0}Button", st.ID),
                    Content = st.Name,
                    Margin = new Thickness(10),
                    FontSize = 30,
                };

                typeChooseButton.Click += TypeChooseButton_Click;

                statisticsTypeStackPanel.Children.Add(typeChooseButton);
            }
        }

        private void TypeChooseButton_Click(object sender, RoutedEventArgs e)
        {
            Button statisticsTypeSender = (Button)sender;

            MessageBox.Show(statisticsTypeSender.Name);
        }
        #endregion

        #region Private methods

        #endregion
    }
}
