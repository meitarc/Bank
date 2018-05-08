using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    class SavingProgramm
    {
        double amount;
        DateTime openingDate;
        DateTime closingDate;
        TimeSpan interval;
        public SavingProgramm(double amount, DateTime closingDate)
        {
            this.amount = amount;
            this.closingDate = closingDate;
            openingDate = DateTime.Now;
            interval = closingDate - openingDate;
        }
        public void AddSavingProgramm
        {

        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public DateTime OpeningDate
        {
            get { return openingDate; }
        }
        public DateTime ClosingDate
        {
            get { return closingDate; }
        }
        public TimeSpan Interval
        {
            get { return interval; }
        }

                         
    }                                   
}                                       
                                        