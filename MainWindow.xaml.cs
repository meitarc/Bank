using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// the next program represents a bank acount managment as defined in the project specifications.
    /// this  is the main window, from here you can access all of the different windows of the program in order to
    /// perform different actions.
    /// you can also save and load from this window your acounts information.
    /// </summary>
    public partial class MainMenu : Window
    {
        BankMannager manager = new BankMannager();
        public MainMenu()
        {
            InitializeComponent();
        }

        DateTime cheker = DateTime.Now;
        public MainMenu(BankMannager manager)// inharits the acount list data.
            : this()
        {
            this.manager = manager;
        }

        public void checkSaveAcountDate()
            //this function checks all of the saving program's closing date and whether that date have passed.
            //if true then that saving program will be closed and its money will transfare to that acount's balance.
        {
            DateTime today = DateTime.Now;
            for (int i = 0; i < manager.AcountsCount; i++)
            {
                for (int j = 0; j < manager[i].NumOfSavings.Count; j++)
                {
                    DateTime close;
                    close = manager[i].NumOfSavings[j].ClosingDate;
                    if (close <= today)
                    {
                        manager[i].Balance += manager[i].NumOfSavings[j].Amount;
                        manager[i].NumOfSavings.RemoveAt(j);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        //opens the registering window and inharits the acount list data.
        {
            Registering_a_new_client regClient = new Registering_a_new_client(manager);
            this.Close();
            regClient.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        //open the acount actons window and inharits the acount list data.
        {
            AcountActions addSavProg = new AcountActions(manager);
            this.Close();
            addSavProg.ShowDialog();
        }

        private void btnShowDetails_Click(object sender, RoutedEventArgs e)
        //if there are no saved acounts then an apropriate message box will appear,
        //else the show details window will open and inharit the acount list data.
        {
            if (manager.AcountsCount == 0)
            {
                MessageBox.Show("no accounts are saved in the system to desplay");
                return;
            }
            checkSaveAcountDate();
            ShowDetails showDetails = new ShowDetails(manager);
            this.Close();
            showDetails.ShowDialog();
        }



        private void btnLoad_Click(object sender, RoutedEventArgs e)
        //loads the acount list data from a text file
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            bool? res = dialog.ShowDialog();
            if (res != null && res.Value == true)
            {
                StreamReader loadMe = new StreamReader(dialog.FileName);
                string line;
                while ((line = loadMe.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))//empty line for separete clients
                        continue;

                    string[] subValues = line.Split('|');//last element is All saving programs
                    string[] subSaving = null;

                    if (!string.IsNullOrWhiteSpace(subValues[subValues.Length - 1]))
                    {
                        subSaving = subValues[subValues.Length - 1].Split(new char[] { '!' },
                            StringSplitOptions.RemoveEmptyEntries);
                    }

                    AcountProgram ap;
                    if (subValues[0] == "Normal")
                    {
                        ap = new AcountProgram(subValues[1], subValues[2], int.Parse(subValues[3]),
                            DateTime.Parse(subValues[4]), double.Parse(subValues[5]), subValues[6]);
                    }
                    else if (subValues[0] == "vip")
                    {
                        ap = new Vip(subValues[1], subValues[2], int.Parse(subValues[3]), DateTime.Parse(subValues[4]),
                            double.Parse(subValues[5]), subValues[6]);
                    }
                    else //if(subValues[0] == "business")
                    {
                        ap = new Business(subValues[1], subValues[2], int.Parse(subValues[3]), DateTime.Parse(subValues[4]),
                            subValues[7], double.Parse(subValues[5]), subValues[6]);
                    }

                    if (subSaving != null) //then the client has saving programs
                    {
                        string[] subSavingDetail = null;
                        for (int i = 0; i < subSaving.Length; i++)
                        {
                            subSavingDetail = subSaving[i].Split('~');
                            NewSavingAcount newsave = new NewSavingAcount(double.Parse(subSavingDetail[2]),
                                DateTime.Parse(subSavingDetail[1]), DateTime.Parse(subSavingDetail[0]));
                            ap.AddSaveProgramLoad(newsave);
                        }
                    }
                    manager.Add(ap);
                }
                MessageBox.Show("File has been successfuly loded");
                loadMe.Close();
            }
        }

        private void MenuBSaveAs_Click(object sender, RoutedEventArgs e)
        //saves the client list data to a text file to a path that was chosen by the user.
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "data";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "txt files (*.txt)|*.txt";
            bool? res = dialog.ShowDialog();
            if (res != null && res.Value == true)
            {
                string name = dialog.FileName;
                StreamWriter saveMe = new StreamWriter(name);
                for (int i = 0; i < manager.AcountsCount; i++)
                {
                    saveMe.WriteLine(manager[i].SaveDitails());
                    saveMe.WriteLine();
                }
                saveMe.Close();
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
