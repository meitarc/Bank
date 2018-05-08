using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Final_Project
{
    /// <summary>
    /// this class is the VIP acount type.
    /// this class inharits from the AcountProgram class.
    /// a new acount is saved as a VIP acount if it hase 100,00 or more total money amount.
    /// this class have various functions like calculate,add saving program, getting different details from this calss and more.
    /// </summary>
    class Vip : AcountProgram
    {
        public Vip(string firstName, string lastName, int idNum, DateTime openingDate, double balance, string image)
            : base(firstName, lastName, idNum, openingDate, balance, image)//constructor,inharits from the AcountProgram window.
        { }

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

        public override string CheckClass()//return a string that tells the class type(vip).
        {
            return "vip";
        }

        public override string GetDitails()
        //returns a string with all of the acount information.
        {
            return string.Format("VIP acount. \nFirst name: {0} \nLast name: {1} \nID: {2} \nOpening date: {3} \nBalance: {4} ",
               firstName, lastName, idNum, openingDate, balance);
        }

        public override string GetImage()//returns the image path value.
        {
            return base.GetImage();
        }

        public override void Calculate(int index, BankMannager manager)
        //calculate the acount money as instructed in the specification file:adds to each saing program 2% and another 0.1% for each click.
        //if balance is positive it gets another 2% and if the balance is negative then 5% is substruced from the balance.
        {
            for (int i = 0; i < manager[index].NumOfSavings.Count; i++)
            {
                manager[index].NumOfSavings[i].Amount += (manager[index].NumOfSavings[i].Amount / 100) * 2
                    + (counterMounth * (manager[index].NumOfSavings[i].Amount / 1000));
                TotalAmount += manager[index].NumOfSavings[i].Amount;
            }

            if (TotalAmount > 0)
            {
                Balance += 2 * (Balance / 100);
                totalAmount += 2 * (balance / 100);
            }
            else
            {
                Balance += 5 * (Balance / 100);
                totalAmount += 5 * (balance / 100);
            }
            counterMounth++;
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
            s += string.Format("vip|{0}|{1}|{2}|{3}|{4}|{5}|", firstName, lastName, idNum, openingDate, balance, path);

            for (int i = 0; i < savingPrograms.Count; i++)
            {
                s += string.Format("!{0}~{1}~{2}", savingPrograms[i].OpeningDate, savingPrograms[i].ClosingDate, savingPrograms[i].Amount);
            }
            return s;
        }

        public override bool? CheckVip(AcountProgram acount)
        //checks if total money amount is over ,so the program will know if a transferation
        //between normal acount to vip is needed.
        {
            return base.CheckVip(acount);
        }
    }
}
