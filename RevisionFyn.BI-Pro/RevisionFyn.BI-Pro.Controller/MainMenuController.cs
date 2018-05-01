using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace RevisionFyn.BI_Pro.Controller
{
    public class MainMenuController
    {
        #region Variables
        private static MainMenuController controllerInstance;
        #endregion

        private MainMenuController()
        { }

        #region Public Methods
        public static MainMenuController GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new MainMenuController();
            }

            return controllerInstance;
        }

        public void CreateKpiElements()
        {

        }
        #endregion

        #region Private Methods

        #endregion
    }
}
