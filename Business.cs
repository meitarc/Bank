using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Final_Project
{
    /// <summary>
    /// this class is the business acount type.
    /// this class inharits from the AcountProgram class and includes another element, the business name.
    /// a new acount is saved as a business acount if the business radio button in the registering window is clicked.
    /// this class have various functions like calculate,add saving program, getting different details from this calss and more.
    /// </summary>
    class Business : AcountProgram
    {
        protected string businessName;
        public Business(string firstName, string lastName, int idNum, DateTime openingDate, string businessName,
            double balance, string image)//constructor,inharits from the AcountProgram window.
            : base(firstName, lastName, idNum, openingDate, balance, image)
        {
            this.businessName = businessName;
        }

        public override void AddSaveProgram(NewSavingAcount newsaving)
        //adding a saving program and displays the apropriate message.
        {
            savingPrograms.Add(newsaving);
            MessageBox.Show("Save acount has been added");
        }

        public override void AddSaveProgramLoad(NewSavingAcount newsaving)
        //adding a saving program without the message that was shown in the previous function.
        {
            savingPrograms.Add(newsaving);
        }

        public override string CheckClass()//return a string that tells the class type(business).
        {
            return "business";
        }

        public override void Calculate(int index, BankMannager manager)
        //calculate the acount money as instructed in the specification file:adds to each saing program 3% and another 0.1% for each click.
        //if balance is positive it gets another 3% and if the balance is negative then 4% is substruced from the balance.
        {
            int counterMounth = 0;
            for (int i = 0; i < manager[index].NumOfSavings.Count; i++)
            {
                manager[index].NumOfSavings[i].Amount += (manager[index].NumOfSavings[i].Amount / 100) * 3
                    + (counterMounth * (savingPrograms[i].Amount / 1000));
                TotalAmount += manager[index].NumOfSavings[i].Amount;
            }

            if (TotalAmount > 0)
            {
                Balance += 3 * (Balance / 100);
                totalAmount += 3 * (Balance / 100);
            }

            else
            {
                Balance += 4 * (Balance / 100);
                totalAmount += 4 * (Balance / 100);
            }
            counterMounth++;
        }

        public override string GetDitails()
        //returns a string with all of the acount information.
        {
            return string.Format("Business acount. \nBusiness name: {0}\nFirst name: {1} \nLast name: {2} \nID: {3} \nOpening date: {4} \nBalance: {5} ",
                businessName, firstName, lastName, idNum, openingDate, balance);
        }

        public override string GetImage()//returns the image path value.
        {
            return base.GetImage();
        }

        protected string BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }

        public override string GetSavingProgramms()//returns a string with the saving programm details.
        {
            return base.GetSavingProgramms();
        }

        public override string SaveDitails()
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
                path = string.Format(image);
            }
            s += string.Format("business|{0}|{1}|{2}|{3}|{4}|{5}|{6}|", firstName, lastName, idNum,
                openingDate, balance, path, businessName);

            for (int i = 0; i < savingPrograms.Count; i++)
            {
                s += string.Format("!{0}~{1}~{2}", savingPrograms[i].OpeningDate, savingPrograms[i].ClosingDate,
                    savingPrograms[i].Amount);
            }
            return s;
        }

        public override bool? CheckVip(AcountProgram acount)//returns null becouse it isnt normal nor a VIP acount.
        {
            return null;
        }
    }
}
