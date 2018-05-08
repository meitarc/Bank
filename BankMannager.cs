using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    /// <summary>
    /// this is the bank manager class.
    /// this class control and manages all of the classes in one acount data list
    /// </summary>
    public class BankMannager
    {
        //the acount data list.
        List<AcountProgram> bankAcounts;

        public BankMannager()//constructor
        {
            this.bankAcounts = new List<AcountProgram>();
        }

        public void RemoveSavingAccount(int index, int index2)
        //this function removes a saving program from an acount
        {
            bankAcounts[index].NumOfSavings.Remove(bankAcounts[index].NumOfSavings[index2]);
        }

        public void Add(AcountProgram newAccount)//adding a new acount to the list.
        {
            this.bankAcounts.Add(newAccount);
        }

        public void Remove(int index)//removing an acount from the list.
        {
            this.bankAcounts.RemoveAt(index);
        }

        public int AcountsCount//returns the list.count.
        {
            get { return this.bankAcounts.Count; }
        }

        public int CheckId(int id)
            //this function gets an id and returns the acount relayted by the ID's index .
        {
            for (int i = 0; i < bankAcounts.Count; i++)
            {
                if (bankAcounts[i].IdNum == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public AcountProgram this[int ind]//accesability to the list.
        {
            get
            {
                if (ind >= 0 && ind <= this.bankAcounts.Count)
                {
                    return this.bankAcounts[ind];
                }
                else
                    return null;
            }
            set
            {
                bankAcounts[ind] = value;

            }
        }

        public int GetNumOfSavingPrograms(AcountProgram acount)
            //this function gets an acount program and returns its number of the saving programs.
        {
            return acount.NumOfSavings.Count;
        }

        public string GetSavingPrograms(AcountProgram acount, int index)
            //returns a string with the saving program details.
        {
            return string.Format("Opening date: {0} \nClosing date: {1} \nAmount: {2:f3}$",
                acount.NumOfSavings[index].OpeningDate, acount.NumOfSavings[index].ClosingDate, acount.NumOfSavings[index].Amount);
        }

        public void SortByOld()
            //sorts the list by oldest opening date.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].OpeningDate < bankAcounts[j + 1].OpeningDate)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByBalance()
            //sorts the list by highest balance.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].Balance < bankAcounts[j + 1].Balance)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByLowBalance()
            //sorts the list by the lowest balance.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].Balance > bankAcounts[j + 1].Balance)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByTotal()
            //sorts by the highest total money amount.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].TotalAmount < bankAcounts[j + 1].TotalAmount)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByLowTotal()
            //sorts by the lowest total money amount.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].TotalAmount > bankAcounts[j + 1].TotalAmount)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByFirstName()
            //sorts the list in an alphabetic order acourding to the first name.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (string.Compare(bankAcounts[j].FirstName, bankAcounts[j + 1].FirstName, true) > 0)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByNew()
            //sorts the list by newest opening date.
        {
            for (int i = bankAcounts.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (bankAcounts[j].OpeningDate > bankAcounts[j + 1].OpeningDate)
                    {
                        AcountProgram temp = bankAcounts[j];
                        bankAcounts[j] = bankAcounts[j + 1];
                        bankAcounts[j + 1] = temp;
                    }
                }
            }
        }
    }
}
