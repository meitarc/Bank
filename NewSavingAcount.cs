using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    /// <summary>
    /// with this class a saving program is created.
    /// this class is used in a list in the other classes as one of thier elements.
    /// </summary>
    public class NewSavingAcount
    {
        double amount;
        DateTime openingDate;
        DateTime closingDate;
        TimeSpan interval;

        public NewSavingAcount(double amount, DateTime closingDate)//constructor for creating a new saving program.
        {
            this.amount = amount;
            this.closingDate = closingDate;
            openingDate = DateTime.Now;
            interval = closingDate - openingDate;
        }

        public NewSavingAcount(double amount, DateTime closingDate, DateTime openingDate)
            //this constructor we use in order to load old data without loosing the opening date.
        {
            this.amount = amount;
            this.closingDate = closingDate;
            this.openingDate = openingDate;
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
