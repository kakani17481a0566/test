﻿using Microsoft.EntityFrameworkCore;

namespace test.ViewModels.Lead
{
    public class MonthlyLeadStatusSummaryViewModel
    {
        public string StatusName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int LeadCount { get; set; }
    }
}
