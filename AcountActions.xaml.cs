using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// in this window the user can perform different actions on his acount.
    /// by clicking on the correct radio button the user can ether pull\deposit money to his acount or add saving program to his acount.
    /// the user can pull money up to his allowed negativ total money amount as instructed at the specification file.
    /// when opening a saving acount the user can choose its closing date and how much money he intends to put on the saving program.
    /// </summary>
    public partial class AcountActions : Window
    {
        DateTime closingDate;
        BankMannager manager;
        int id, amount;

        public AcountActions()
        {
            InitializeComponent();
            txtId.MaxLength = 9;
            txtSaveAmount.MaxLength = 9;
            DpClosingDate.DisplayDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public AcountActions(BankMannager manager)//inharits the acount data list.
            : this()
        {
            this.manager = manager;
        }

        private void btnAddSaveProgram_Click(object sender, RoutedEventArgs e)
        //if the information that have been input by the user is valid and if it is allowed to that client to open a new saving program
        //then a new saving program is created.
        {
            if (checkIdAmount() == false)//validates that the entered Id does exist in the data base.
            {
                return;
            }

            if (DpClosingDate.SelectedDate == null)
            {
                MessageBox.Show("invalid closing date");
                return;
            }

            closingDate = DpClosingDate.SelectedDate.Value;
            NewSavingAcount saveAcount = new NewSavingAcount(double.Parse(txtSaveAmount.Text), closingDate);
            int index = manager.CheckId(int.Parse(txtId.Text));

            if (index == -1)
            {
                MessageBox.Show("Id is invalide");
                return;
            }

            if (int.Parse(txtSaveAmount.Text) > manager[index].Balance)
            {
                MessageBox.Show("There is not enough money");
                return;
            }

            manager[index].AddSaveProgram(saveAcount);
            manager[index].Balance -= double.Parse(txtSaveAmount.Text);
            txtSaveAmount.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//returns to the main menu.
        {
            MainMenu main = new MainMenu(manager);
            this.Close();
            main.ShowDialog();
        }

        private void radioAdding_Checked(object sender, RoutedEventArgs e)
        //changing between the pull/deposit option to the add saving program option.
        {
            lbAdding.Visibility = Visibility.Visible;
            btnAddSaveProgram.Visibility = Visibility.Visible;
            DpClosingDate.Visibility = Visibility.Visible;
            lbClosingDate.Visibility = Visibility.Visible;
            DpClosingDate.Visibility = Visibility.Visible;

            lbDeposit.Visibility = Visibility.Hidden;
            btnDeposit.Visibility = Visibility.Hidden;
            btnPull.Visibility = Visibility.Hidden;
            lbDeposit.Visibility = Visibility.Hidden;
        }

        private void radioDeposit_Checked(object sender, RoutedEventArgs e)
        //changing between the pull/deposit option to the add saving program option.
        {
            lbAdding.Visibility = Visibility.Hidden;
            btnAddSaveProgram.Visibility = Visibility.Hidden;
            DpClosingDate.Visibility = Visibility.Hidden;
            lbClosingDate.Visibility = Visibility.Hidden;
            DpClosingDate.Visibility = Visibility.Hidden;

            lbDeposit.Visibility = Visibility.Visible;
            btnDeposit.Visibility = Visibility.Visible;
            btnPull.Visibility = Visibility.Visible;
            lbDeposit.Visibility = Visibility.Visible;
        }

        private bool checkIdAmount()
        //validation of the information that have been entered.
        {
            if ((!int.TryParse(txtId.Text, out id)) || !CheckNum(txtId.Text) || txtId.Text.Length != 9)
            {
                MessageBox.Show("invalid Id has been entered");
                return false;
            }

            if ((!int.TryParse(txtSaveAmount.Text, out amount)) || !CheckNum(txtSaveAmount.Text))
            {
                MessageBox.Show("invalid amount has been entered");
                return false;
            }
            return true;
        }

        private bool CheckNum(string s)
        //making sure that only digit numbers have been entered.
        {
            bool isOk = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (!(s[i] >= '0' && s[i] <= '9'))
                {
                    isOk = false;
                    break;
                }

            }
            return isOk;
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        //making sure a valid ID have been entered.
        {
            if (checkIdAmount() == false)
            {
                return;
            }

            int index = manager.CheckId(int.Parse(txtId.Text));//gets the acount Index by its ID.
            if (index != -1)
            {
                manager[index].Balance += double.Parse(txtSaveAmount.Text);
                manager[index].TotalAmount += double.Parse(txtSaveAmount.Text);
                MessageBox.Show("Deposit was made on the total: " + txtSaveAmount.Text + "$  to account: " + txtId.Text);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("invalid Id has been entered");
            }

            bool? isVip = manager[index].CheckVip(manager[index]);//checking if after the deposit the acount's total money amount is over 100,000.

            if (isVip == true)//if isnt Business acount and total money amount is over 100,000.
            {
                AcountProgram temp = manager[index];
                for (int j = 0; j < manager[index].NumOfSavings.Count; j++)
                {
                    temp.NumOfSavings[j] = new NewSavingAcount(manager[index].NumOfSavings[j].Amount, manager[index].NumOfSavings[j].ClosingDate);
                }
                manager[index] = new Vip(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                for (int j = 0; j < temp.NumOfSavings.Count; j++)
                {
                    manager[index].AddSaveProgramLoad(temp.NumOfSavings[j]);
                }
            }

            else if (isVip == false)//if isnt Business acount and total money amount is under 100,000.
            {
                AcountProgram temp = manager[index];
                for (int j = 0; j < manager[index].NumOfSavings.Count; j++)
                {
                    temp.NumOfSavings[j] = new NewSavingAcount(manager[index].NumOfSavings[j].Amount, manager[index].NumOfSavings[j].ClosingDate);
                }
                manager[index] = new AcountProgram(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                for (int j = 0; j < temp.NumOfSavings.Count; j++)
                {
                    manager[index].AddSaveProgramLoad(temp.NumOfSavings[j]);
                }
            }
            txtSaveAmount.Clear();
        }

        private void btnPull_Click(object sender, RoutedEventArgs e)
        //if all the entered data is correct and the action is allowed as instructed by the specification file
        //a pull from the balance will be made.
        {
            if (checkIdAmount() == false)
            {
                return;
            }

            int index = manager.CheckId(int.Parse(txtId.Text));
            if (index != -1)
            {
                int checker = 0;
                //diferent negativ total money amount as instructed at the specification file. 
                if (manager[index].CheckClass() == "normal")
                { checker = -10000; }
                else if (manager[index].CheckClass() == "vip")
                { checker = -25000; }
                else
                { checker = -75000; }

                if ((manager[index].TotalAmount - double.Parse(txtSaveAmount.Text) + 1) > checker)
                {
                    manager[index].Balance -= double.Parse(txtSaveAmount.Text);
                    manager[index].TotalAmount -= double.Parse(txtSaveAmount.Text);
                    MessageBox.Show("Pull was made on the total: " + txtSaveAmount.Text + "$  from account: " + txtId.Text);
                }
                else
                {
                    MessageBox.Show("your acount's total amount will be lower then your allowed negative money");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("invalid Id has been entered");
            }

            bool? isVip = manager[index].CheckVip(manager[index]);//checking if after the deposit the acount's total money amount is over 100,000.
            if (isVip == true)//if isnt Business acount and total money amount is over 100,000.
            {
                AcountProgram temp = manager[index];
                for (int j = 0; j < manager[index].NumOfSavings.Count; j++)
                {
                    temp.NumOfSavings[j] = new NewSavingAcount(manager[index].NumOfSavings[j].Amount, manager[index].NumOfSavings[j].ClosingDate);
                }
                manager[index] = new Vip(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                for (int j = 0; j < temp.NumOfSavings.Count; j++)
                {
                    manager[index].AddSaveProgramLoad(temp.NumOfSavings[j]);
                }
            }
            else if (isVip == false)//if isnt Business acount and total money amount is under  100,000.
            {
                AcountProgram temp = manager[index];
                for (int j = 0; j < manager[index].NumOfSavings.Count; j++)
                {
                    temp.NumOfSavings[j] = new NewSavingAcount(manager[index].NumOfSavings[j].Amount, manager[index].NumOfSavings[j].ClosingDate);
                }
                manager[index] = new AcountProgram(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                for (int j = 0; j < temp.NumOfSavings.Count; j++)
                {
                    manager[index].AddSaveProgramLoad(temp.NumOfSavings[j]);
                }
            }
            txtSaveAmount.Clear();
        }

        private void DpClosingDate_PreviewMouseUp(object sender, MouseButtonEventArgs e)

        // there was a bug with the calender tool that made me click twice on anything for it to work after i clicked on the calender.
        // the next code solves the bug.
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)//close the programm
        {
            Close();
        }
    }
}