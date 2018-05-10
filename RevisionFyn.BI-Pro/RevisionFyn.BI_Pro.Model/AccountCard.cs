﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class AccountCard
    {
        public int CaseID { get; set; }
        public int Year { get; set; }
        public Employee MainEmployee { get; set; }
        public List<Employee> otherEmployees;
        public int NumberOfTasks { get; set; }
        public int InvoicePrice { get; set; }
        public int Balance { get; set; }
        public int TotalConsumption { get; set; }
    }
}