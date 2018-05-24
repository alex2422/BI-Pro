using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RevisionFyn.BI_Pro.Database;
using RevisionFyn.BI_Pro.Model;

namespace RevisionFyn.BI_Pro.Controller
{
    public class OverviewController
    {
        #region Variables / Properties
        private static OverviewController _ControllerInstance { get; set; }
        private StoredProcedure _StoredProcedure { get; set; }
        private List<int> _YearList { get; set; }
        private ObservableCollection<Client> _DefaultClientData { get; set; }
        private ObservableCollection<Client> _ChoosenClientData { get; set; }
        #endregion

        #region Constructor
        private OverviewController()
        {
            _StoredProcedure = new StoredProcedure();
            _YearList = new List<int>();
        }
        #endregion

        #region Public methods
        public static OverviewController GetInstance()
        {
            if (_ControllerInstance == null)
            {
                _ControllerInstance = new OverviewController();
            }
            return _ControllerInstance;
        }

        public void LoadIntoListBox(ListBox ClientsToBeChosenListBox, ListBox ChoosenClientsListBox)
        {
            ClientsToBeChosenListBox.ItemsSource = _DefaultClientData;
            ClientsToBeChosenListBox.DisplayMemberPath = "ClientName";
            ChoosenClientsListBox.ItemsSource = _ChoosenClientData;
            ChoosenClientsListBox.DisplayMemberPath = "ClientName";
        }

        public void ClearData()
        {
            if (_DefaultClientData != null)
            {
                _DefaultClientData.Clear();
            }
            if (_ChoosenClientData != null)
            {
                _ChoosenClientData.Clear();
            }
        }

        public void GetDataFromDB()
        {
            _DefaultClientData = new ObservableCollection<Client>(_StoredProcedure.GetClient());
            _ChoosenClientData = new ObservableCollection<Client>();
        }

        public void ButtonAdd(ListBox ClientsToBeChosenListBox)
        {
            _ChoosenClientData.Add((Client)ClientsToBeChosenListBox.SelectedItem);
            _DefaultClientData.Remove((Client)ClientsToBeChosenListBox.SelectedItem);
        }

        public void ButtonRemove(ListBox ChoosenClientsListBox)
        {
            _DefaultClientData.Add((Client)ChoosenClientsListBox.SelectedItem);
            _ChoosenClientData.Remove((Client)ChoosenClientsListBox.SelectedItem);
        }

        public void AddAllButton()
        {
            int timesToDo = _DefaultClientData.Count();

            for (int i = 0; i < timesToDo; i++)
            {
                _ChoosenClientData.Add(_DefaultClientData[0]);
                _DefaultClientData.Remove(_DefaultClientData[0]);
            }
        }

        public void RemoveAllButton()
        {
            int timesToDo = _ChoosenClientData.Count();
            for (int i = 0; i < timesToDo; i++)
            {
                _DefaultClientData.Add(_ChoosenClientData[0]);
                _ChoosenClientData.Remove(_ChoosenClientData[0]);
            }
        }

        public void LoadValuesIntoComoBox(ComboBox YearsComboBox)
        {
            foreach (AccountCard accCard in _StoredProcedure.GetYear())
            {
                if (!_YearList.Contains(accCard.Year))
                {
                    _YearList.Add(accCard.Year);
                }
            }

            _YearList.Sort();
            YearsComboBox.ItemsSource = _YearList;
            YearsComboBox.SelectedItem = _YearList[0];
        }

        public void ExportData(ListBox ChoosenClientsListBox, ComboBox StartYearComboBox)
        {
            if (ChoosenClientsListBox.Items.Count != 0)
            {
                ExcelExport export = new ExcelExport();
                string path = export.GetExportPath(ChoosenClientsListBox);

                if (path != null && path != "")
                {
                    export.Export(ChoosenClientsListBox, StartYearComboBox, path, _StoredProcedure.GetEmployee());
                }
                else
                {
                    MessageBox.Show("Eksportering annulleret");
                }
            }
            else
            {
                MessageBox.Show("Vælg venligst firmaer");
            }
        }
        #endregion
    }
}
