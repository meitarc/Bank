using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows;
using System.Windows.Media.Imaging;
namespace Final_Project
{
    /// <summary>
    /// this is the main acount program class.
    /// from this class the VIP and Business classes inharits their structor.
    /// this class have various functions like calculate,add saving program, getting different details from this calss and more.
    /// </summary>
    public class AcountProgram
    {
        protected string firstName;
        protected string lastName;
        protected int idNum;
        protected double balance;
        protected List<NewSavingAcount> savingPrograms;
        protected DateTime openingDate;
        protected double totalAmount;
        protected int counterMounth = 0;
        protected string image;

        public AcountProgram(string firstName, string lastName, int idNum, DateTime openingDate, double balance)//constructor.
        {
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
            this.idNum = idNum;
            this.openingDate = openingDate;
            this.totalAmount += balance;
            image = null;
            savingPrograms = new List<NewSavingAcount>();
        }

        public AcountProgram(string firstName, string lastName, int idNum, DateTime openingDate, double balance, string image)
            : this(firstName, lastName, idNum, openingDate, balance)
        //another constructor that includes an image if the user would like to add one.
        {
            this.image = image;
        }

        public virtual string CheckClass()//return a string that tells the class type(normal).
        {
            return "normal";
        }

        public virtual void AddSaveProgram(NewSavingAcount newsaving)
        //in this class only 1 saving program is allowed 
        //and therefor only one can be added.
        //adding a saving program and displays the apropriate message.
        {
            if (savingPrograms.Count < 1)
            {
                savingPrograms.Add(newsaving);
                MessageBox.Show("Save acount has been added");
                totalAmount += newsaving.Amount;
            }
            else
            {
                MessageBox.Show("Error!! Only one saving program is allowed");
            }
        }

        public virtual void AddSaveProgramLoad(NewSavingAcount newsaving)
        //adding a saving program without the message that was shown in the previous function.
        {
            if (savingPrograms.Count < 1)
            {
                savingPrograms.Add(newsaving);
                totalAmount += newsaving.Amount;
            }
        }

        public string GetIdTotal()//returns a string with ID and Total money amount.
        {
            return string.Format("{0} : {1:f3}$", idNum, totalAmount);
        }

        public string GetIdBalance()//returns a string with ID and Balance.
        {
            return string.Format("{0} : {1:f3}$", idNum, balance);
        }

        public string GetFirstLastName()//returns a string with the first and last name.
        {
            return string.Format("{0} : {1}", firstName, lastName);
        }

        public virtual string GetDitails()//returns a string with all of the acount information.
        {
            if (firstName == null)
            {
                return null;
            }
            return string.Format("Normal acount. \nFirst name: {0} \nLast name: {1} \nID: {2} \nOpening date: {3} \nBalance: {4}$\n\n",
                firstName, lastName, idNum, openingDate, balance);
        }

        public virtual string SaveDitails()
        //returns a string with all of the acount information and devides them with different character in order to have a correct Load 
        //from this information.the '|' character splits between the different main acount elements,the '!' character splits between 
        //the main acount to the different saving programs and between the saving program themselves and the '~' character splits 
        //between the elements of each saving program.
        {
            string s = "";
            string path;
            if (image == null)
            {
                path = " ";
            }
            else
            {
                path = image;
            }
            s += string.Format("Normal|{0}|{1}|{2}|{3}|{4}|{5}|", firstName, lastName, idNum, openingDate, balance, path);

            for (int i = 0; i < savingPrograms.Count; i++)
            {
                s += string.Format("!{0}~{1}~{2}", savingPrograms[i].OpeningDate,
                    savingPrograms[i].ClosingDate, savingPrograms[i].Amount);
            }
            return s;
        }

        public virtual string GetSavingProgramms()//returns a string with the saving programm details.
        {
            string s = "";
            for (int i = 0; i < savingPrograms.Count; i++)
            {
                s += string.Format("Opening date: {0} \nClosing date: {1} \nAmount: {2}$\n",
                savingPrograms[i].OpeningDate, savingPrograms[i].ClosingDate, savingPrograms[i].Amount);
            }
            return s;
        }

        public virtual string GetImage()//returns the image path value.
        {
            if (image == null)
            {
                return " ";
            }
            return image;
        }

        public virtual void Calculate(int index, BankMannager manager)
        //calculate the acount money as instructed in the specification file:adds to each saing program 1% and another 0.1% for each click.
        //if balance is positive it gets another 1% and if the balance is negative then 7% is substruced from the balance.
        {
            for (int i = 0; i < manager[index].NumOfSavings.Count; i++)
            {
                manager[index].NumOfSavings[i].Amount += manager[index].NumOfSavings[i].Amount / 100
                    + (counterMounth * (savingPrograms[i].Amount / 1000));
                totalAmount += manager[index].NumOfSavings[i].Amount;
            }

            if (balance > 0)
            {
                balance += balance / 100;
                totalAmount += balance / 100;
            }

            else
            {
                balance += 7 * (balance / 100);
                totalAmount += 7 * (balance / 100);
            }
            counterMounth++;
        }

        public virtual bool? CheckVip(AcountProgram acount)
        //checks if total money amount is over ,so the program will know if a transferation
        //between normal acount to vip is needed.
        {
            if (acount.totalAmount > 100000)
            {
                return true;
            }
            else { return false; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public List<NewSavingAcount> NumOfSavings
        {
            get { return savingPrograms; }
            set { savingPrograms = value; }
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public DateTime OpeningDate
        {
            get { return openingDate; }
            set { openingDate = value; }
        }

        public int IdNum
        {
            get { return idNum; }
            set { idNum = value; }
        }

        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }
    }
}
