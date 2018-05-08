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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Final_Project
{
    /// <summary>
    /// in this window all of the data is displayed to the user in 2 list boxes.
    /// in the left list box all of the acounts will display by ID and total money amount(by default) and if an acount is selected then in the
    /// right list box will show all of that acount in details.
    /// the user can also sort and display the information from different options acourding to his choice by using the menu items.
    /// the user can save and load the information from this window.
    /// </summary>
    public partial class ShowDetails : Window
    {
        BankMannager manager;
        public ShowDetails(BankMannager manager)// inharits the acount data list.
        {
            this.manager = manager;
            InitializeComponent();
            showDetails();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//returns to main menu.
        {
            MainMenu main = new MainMenu(manager);
            this.Close();
            main.ShowDialog();
        }

        public void showDetails()
        //a function that shows all of the saved acounts in the left listbox by ID and Total money amount only.
        {
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdTotal());
            }
        }

        private void mbNewClients_Click_1(object sender, RoutedEventArgs e)
        //sorts the list by newest clients and displays the latest 5 acounts that has been registered into the bank.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            if (manager.AcountsCount >= 5)
            {

                listShowDetails.Items.Clear();
                manager.SortByNew();

                for (int i = 0; i < 5; i++)
                {
                    listShowDetails.Items.Add(manager[i].GetIdTotal());
                }
            }
            else
            {
                MessageBox.Show("you have less then 5 acounts ");
            }
        }

        private void mbOldestButtens_Click_1(object sender, RoutedEventArgs e)
        //sorts the list by oldest clients and displays the oldest 5 acounts that has been registered into the bank.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            if (manager.AcountsCount >= 5)
            {
                listShowDetails.Items.Clear();
                manager.SortByOld();

                for (int i = 0; i < 5; i++)
                {
                    listShowDetails.Items.Add(manager[i].GetIdTotal());
                }
            }
            else
            {
                MessageBox.Show("you have less then 5 acounts");
            }
        }

        private void listShowDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //when an acount is selected at the left list box that acount informationg is displayd in details at the right list box.
        {
            txtBdetails.Items.Clear();
            txtBdetails.Items.Add("");
            int index = listShowDetails.SelectedIndex;
            if (manager[index] == null)
            {
                return;
            }
            txtBdetails.Items.Add(manager[index].GetDitails());
            txtBdetails.Items.Add(manager[index].GetSavingProgramms());
            txtBdetails.Items.RemoveAt(0);

            if (manager[index].GetImage() == " ")
            {
                image.Source = new BitmapImage();
                return;
            }

            image.Source = new BitmapImage(new Uri(manager[index].GetImage()));
        }

        private void mbCalculate_Click(object sender, RoutedEventArgs e)
        //calculates and change all of the acount details as instructed at the program's specification file.
        {
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                manager[i].Calculate(i, manager);
                bool? isVip = manager[i].CheckVip(manager[i]);
                if (isVip == true)//if acount total money amount is over 100000 then the acount becomes a vip acount.
                {
                    AcountProgram temp = manager[i];
                    for (int j = 0; j < manager[i].NumOfSavings.Count; j++)
                    {
                        temp.NumOfSavings[j] = new NewSavingAcount(manager[i].NumOfSavings[j].Amount, manager[i].NumOfSavings[j].ClosingDate);
                    }
                    manager[i] = new Vip(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                    for (int j = 0; j < temp.NumOfSavings.Count; j++)
                    {
                        manager[i].AddSaveProgramLoad(temp.NumOfSavings[j]);
                    }
                }
                else if (isVip == false)
                {
                    AcountProgram temp = manager[i];
                    for (int j = 0; j < manager[i].NumOfSavings.Count; j++)
                    {
                        temp.NumOfSavings[j] = new NewSavingAcount(manager[i].NumOfSavings[j].Amount, manager[i].NumOfSavings[j].ClosingDate);
                    }
                    manager[i] = new AcountProgram(temp.FirstName, temp.LastName, temp.IdNum, temp.OpeningDate, temp.Balance, temp.GetImage());
                    for (int j = 0; j < temp.NumOfSavings.Count; j++)
                    {
                        manager[i].AddSaveProgramLoad(temp.NumOfSavings[j]);
                    }
                }
            }

            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;
            listShowDetails.Items.Clear();
            txtBdetails.Items.Clear();
            showDetails();
        }

        private void mbClear_Click(object sender, RoutedEventArgs e)// clears the text boxes.
        {
            clear();
        }

        private void mbFLName_Click(object sender, RoutedEventArgs e)
        //displays the information on the left list box by first and last name.
        {
            lbId.Visibility = Visibility.Hidden;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Visible;
            lbLname.Visibility = Visibility.Visible;
            lbTotal.Visibility = Visibility.Hidden;
            clear();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetFirstLastName());
            }
        }

        public void clear()
        {
            txtBdetails.Items.Clear();
            listShowDetails.Items.Clear();
            this.image.Source = new BitmapImage();
        }

        private void mbIDBalance_Click(object sender, RoutedEventArgs e)
        //displays the information on the left list box by ID and balance.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Visible;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Hidden;

            clear();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdBalance());
            }
        }

        private void mbIDTotal_Click(object sender, RoutedEventArgs e)
        //displays the information on the left list box by ID and total money amount.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            clear();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdTotal());
            }
        }

        private void mbSortFName_Click(object sender, RoutedEventArgs e)
        //sorts the acount data list by an alphabetic order acording to the first name and displays the information on the left list box 
        //by first and last name.
        {
            lbId.Visibility = Visibility.Hidden;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Visible;
            lbLname.Visibility = Visibility.Visible;
            lbTotal.Visibility = Visibility.Hidden;

            clear();
            manager.SortByFirstName();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetFirstLastName());
            }
        }

        private void mbSortBalnce_Click(object sender, RoutedEventArgs e)
        //sorts the list by highest balance and displays the information on the left list box by ID and balance.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Visible;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Hidden;

            clear();
            manager.SortByBalance();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdBalance());
            }

        }

        private void mbSortTotal_Click(object sender, RoutedEventArgs e)
        //sorts the list by the highest total money amount and displays the information on the left list box by ID and Total.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            clear();
            manager.SortByTotal();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdTotal());
            }
        }

        private void mbHighBalance_Click(object sender, RoutedEventArgs e)
        //sorts the list by highest balance and displays the information on the left list box by ID and balance.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Visible;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Hidden;

            clear();
            manager.SortByBalance();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdBalance());
            }
        }

        private void mbLowBalance_Click(object sender, RoutedEventArgs e)
        //sorts the list by lowest balance and displays the information on the left list box by ID and balance.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Visible;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Hidden;

            clear();
            manager.SortByLowBalance();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdBalance());
            }
        }

        private void mbHighTotal_Click(object sender, RoutedEventArgs e)
        //sorts the list by highest total money amount and displays the information on the left list box by ID and total money amount.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            clear();
            manager.SortByTotal();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdTotal());
            }
        }

        private void mbLowTotal_Click(object sender, RoutedEventArgs e)
        //sorts the list by lowest total money amount and displays the information on the left list box by ID and total money amout.
        {
            lbId.Visibility = Visibility.Visible;
            lblBalance.Visibility = Visibility.Hidden;
            lbFname.Visibility = Visibility.Hidden;
            lbLname.Visibility = Visibility.Hidden;
            lbTotal.Visibility = Visibility.Visible;

            clear();
            manager.SortByLowTotal();
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                listShowDetails.Items.Add(manager[i].GetIdTotal());
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
