﻿using System;
using System.Collections.Generic;
using System.Text;
using RevisionFyn.BI_Pro.Model;

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
            // Get this from database later
            int numberOfKPI = 0;

            if (numberOfKPI != 0)
            {
                for (int i = 0; i < numberOfKPI; i++)
                {
                    
                }
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
