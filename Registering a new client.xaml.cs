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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// in this window the user can create and save a new bank acount and its details.
    /// </summary>
    public partial class Registering_a_new_client
    {
        string imageUri = null;
        BankMannager manager;

        public Registering_a_new_client()
        {
            InitializeComponent();
            txtFirstName.MaxLength = 20;
            txtLastName.MaxLength = 20;
            txtID.MaxLength = 9;
            txtBusinessName.MaxLength = 20;
            txtIdRemove.MaxLength = 9;
            txtBalance.MaxLength = 12;
        }

        public Registering_a_new_client(BankMannager manager)// inharits the acount list data.
            : this()
        {
            this.manager = manager;
        }

        private void cbBusiness_Checked(object sender, RoutedEventArgs e)
        //if business acount is chosen a business name must be entered.
        {
            labalBusinessName.Visibility = Visibility.Visible;
            txtBusinessName.Visibility = Visibility.Visible;
        }

        private void cbBusiness_Unchecked(object sender, RoutedEventArgs e)
        {
            labalBusinessName.Visibility = Visibility.Hidden;
            txtBusinessName.Visibility = Visibility.Hidden;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string error = " ";
            //validation of all the data fields have been entered corectly.
            if (!CheckText(txtFirstName.Text))
            {
                txtFirstName.Text = "";
                error = ", First name is invalid";
            }

            if (!CheckText(txtLastName.Text))
            {
                txtLastName.Text = "";
                error = error + ", Last name is invalid";
            }

            if (txtID.Text.Length != 9 || !CheckNum(txtID.Text))
            {
                txtID.Text = "";
                error = error + ", ID is invalid";
            }

            if (txtBalance.Text.Length == 0 || !CheckNum(txtBalance.Text))
            {
                txtBalance.Text = "";
                error = error + ", Balance is invalid";
            }

            if (cbBusiness.IsChecked == true)
            {
                if (txtBusinessName.Text.Length == 0)
                {
                    error = error + ", Business name is invalid";
                }
            }

            if (!string.IsNullOrWhiteSpace(error))
            //checks if there is an error on one or more of the fields that have been entered and if true
            //displays the error.
            {
                error = error.Remove(0, 2);
                System.Windows.Forms.MessageBox.Show(error);
                return;
            }

            if (manager.CheckId(int.Parse(txtID.Text)) != -1)
            //checks if the ID that have been entered by the user does not allready exists in the system.
            {
                txtID.Clear();
                System.Windows.Forms.MessageBox.Show("ID exists in the system");
                return;
            }

            //checks wich acount type have been chosen and saves acourdingly.
            if (cbBusiness.IsChecked == true)
            {
                manager.Add(new Business(txtFirstName.Text, txtLastName.Text, int.Parse(txtID.Text),
                    DateTime.Now, txtBusinessName.Text, double.Parse(txtBalance.Text), imageUri));
            }
            else if (double.Parse(txtBalance.Text) > 100000)
            {
                manager.Add(new Vip(txtFirstName.Text, txtLastName.Text, int.Parse(txtID.Text),
                    DateTime.Now, double.Parse(txtBalance.Text), imageUri));
            }
            else
            {
                manager.Add(new AcountProgram(txtFirstName.Text, txtLastName.Text, int.Parse(txtID.Text),
                    DateTime.Now, double.Parse(txtBalance.Text), imageUri));
            }
            System.Windows.Forms.MessageBox.Show("Congratulations! Account has been registered");
            Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        //returns to main window and inharits the acount list data.
        {
            MainMenu main = new MainMenu(manager);
            this.Close();
            main.ShowDialog();
        }

        bool IsKeyOk(Key key)
        //a function that will validates that only valid keyboard keys are beeing used. 
        {
            if (key >= Key.A && key <= Key.Z) return true;
            if (key == Key.Space) return true;
            return false;
        }

        private bool CheckText(string s)
        //a function that validates if a valid text have been entered.
        {
            bool isOk = true;
            if (s.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < s.Length; i++)
            {
                if (!((s[i] >= 'a' && s[i] <= 'z') ||
                   (s[i] >= 'A' && s[i] <= 'Z') ||
                    s[i] == ' '))
                {
                    isOk = false;
                    break;
                }
            }
            return isOk;
        }

        private bool CheckNum(string s)
        //a function that validates that only a valid digit number have been entered.
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

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        //letting the user to choos and add a picture to his acount.
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.jpg)|*.jpg|(*.png)|*.png";//allowed file extenstions.
            DialogResult res = dialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                //saves the image path as a class variable.
                imageUri = dialog.FileName;
                this.image.Source = new BitmapImage(new Uri(imageUri));
            }
        }

        private void Clear()
        //a function that clears all the fields in the GUI form.
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtID.Clear();
            txtBalance.Clear();
            txtBusinessName.Clear();
            txtBusinessName.Clear();
            cbBusiness.IsChecked = false;
            this.image.Source = new BitmapImage();
            imageUri = null;
            txtIdRemove.Clear();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnRemoveImage_Click(object sender, RoutedEventArgs e)
        //removing a chosen picture from the GUI form.
        {
            this.image.Source = new BitmapImage();
            imageUri = null;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        //allows the user to delete an acount from the acounts list data
        //by entering the accurate ID.
        {
            if (txtIdRemove.Text.Length != 9 || !CheckNum(txtIdRemove.Text) || manager.CheckId(int.Parse(txtIdRemove.Text)) == -1)
            // making sure that the ID that have been entered is valid and does exist in the acounts data base.
            {
                txtID.Text = "";
                System.Windows.Forms.MessageBox.Show("ID is invalid");
                return;
            }
            manager.Remove(manager.CheckId(int.Parse(txtIdRemove.Text)));
            System.Windows.Forms.MessageBox.Show("The acount has been deleted from the system");
            txtIdRemove.Clear();
        }

        private void btnQuit_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
