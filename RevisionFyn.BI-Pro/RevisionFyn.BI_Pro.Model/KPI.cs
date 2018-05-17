﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionFyn.BI_Pro.Model
{
    public class KPI
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }
        public int DataID { get; set; }
        public string Unit { get; set; }
        public string Color { get; set; }
        public int ColorIndex { get; set; }
        public string IsActive { get; set; }
    }
}
