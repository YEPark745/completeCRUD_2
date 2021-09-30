using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompleteCRUD.BLL;
using CompleteCRUD.DAL;
using CompleteCRUD.Validation;

namespace CompleteCRUD.GUI
{
    public partial class CustomerForm : Form
    {
        List<Customer> listC = new List<Customer>();

        public CustomerForm()
        {
            InitializeComponent();
            buttonListCustomers.Enabled = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure that you want to Leave?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }

            
        }

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            Customer aCustomer = new Customer();
            if (Validator.IsValidID(textBoxCustomerId) && (Validator.isValidName(textBoxFirstName)) &&
               (Validator.isValidName(textBoxLastName)) && Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerId.Text)))
            {

                aCustomer.CustomerId = Convert.ToInt32(textBoxCustomerId.Text);
                aCustomer.FirstName = textBoxFirstName.Text;
                aCustomer.LastName = textBoxLastName.Text;
                aCustomer.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                listC.Add(aCustomer);

                buttonListCustomers.Enabled = true;

                ClearAll();

            }
        }


        private void ClearAll()
        {
            textBoxCustomerId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxCustomerId.Focus();
        
        }

        private void buttonListCustomers_Click(object sender, EventArgs e)
        {
            listViewCustomer.Items.Clear();
            CustomerDA.ListCustomers(listViewCustomer);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
  //          Customer aCustomer = new Customer();
            List<Customer> listC = CustomerDA.ListCustomers();

            if (Validator.IsValidID(textBoxCustomerId) && (Validator.isValidName(textBoxFirstName)) && 
                (Validator.isValidName(textBoxLastName)) && Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerId.Text)))
            {

            Customer aCustomer = new Customer();

            aCustomer.CustomerId = Convert.ToInt32(textBoxCustomerId.Text);
            aCustomer.FirstName = textBoxFirstName.Text;
            aCustomer.LastName = textBoxLastName.Text;
            aCustomer.PhoneNumber = maskedTextBoxPhoneNumber.Text;

            CustomerDA.Save(aCustomer);

            ClearAll();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Customer aCustomer = new Customer();
            if (Validator.IsValidID(textBoxCustomerId) && (Validator.isValidName(textBoxFirstName)) &&
                (Validator.isValidName(textBoxLastName)) && Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerId.Text)))
            {

                aCustomer.CustomerId = Convert.ToInt32(textBoxCustomerId.Text);
                aCustomer.FirstName = textBoxFirstName.Text;
                aCustomer.LastName = textBoxLastName.Text;
                aCustomer.PhoneNumber = maskedTextBoxPhoneNumber.Text;


                DialogResult answer = MessageBox.Show("Do you really want to update the customer?", "Update Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                {
                    CustomerDA.Update(aCustomer);
                    MessageBox.Show("Customers Updated! Thanks :)");


                }
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Validator.IsValidID(textBoxCustomerId) && (Validator.isValidName(textBoxFirstName)) &&
                (Validator.isValidName(textBoxLastName)) && Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerId.Text)))
            {
                CustomerDA.Delete(Convert.ToInt32(textBoxCustomerId.Text));
                MessageBox.Show("Customers successfully deleted! :)");

            }
        }

        private void comboBoxChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            int choice = comboBoxChoice.SelectedIndex;

            switch (choice)
            {

                case 0:
                    labelInfo.Text = "Please enter the Customer ID";
                    textBoxInput.Focus();
                        break;

                case 1:
                    labelInfo.Text = "Please enter the First Name";
                    textBoxInput.Focus();
                    break;

                case 2:
                    labelInfo.Text = "Please enter the Last Name";
                    textBoxInput.Focus();
                    break;

                case 3:
                    labelInfo.Text = "Please enter the Phone Number";
                    textBoxInput.Focus();
                    break;
                default:
                    break;
            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxChoice.SelectedIndex;
            if (Validator.IsValidID(textBoxCustomerId) && (Validator.isValidName(textBoxFirstName)) &&
                (Validator.isValidName(textBoxLastName)) && Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerId.Text)))
            {
                switch (choice)
                {

                    case -1: //The user dint select any search option
                        MessageBox.Show("Please enter at least one option <3");
                        break;

                    case 0: //Search by Customer ID

                        Customer cust = CustomerDA.Search(Convert.ToInt32(textBoxInput.Text));
                        if (cust != null)
                        {
                            textBoxCustomerId.Text = (cust.CustomerId).ToString();
                            textBoxFirstName.Text = cust.FirstName;
                            textBoxLastName.Text = cust.LastName;
                            maskedTextBoxPhoneNumber.Text = cust.PhoneNumber;
                        }

                        else
                        {
                            MessageBox.Show("Customer Not Found!");
                            textBoxInput.Clear();
                            textBoxInput.Focus();
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
