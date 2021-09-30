using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompleteCRUD.BLL;

namespace CompleteCRUD.Validation
{
    public class Validator
    {

        public static bool IsValidID(string input)
        {
            int tempID;

            if (input.Length != 5 || (Int32.TryParse(input, out tempID)))
            {
                MessageBox.Show("Invalid ID, must have 5 digits");
                return false;
            }
            return true;
        }

        public static bool IsValidID(TextBox text)
        {
            int tempID;

            if (text.TextLength != 5 || !(Int32.TryParse(text.Text, out tempID)))
            {
                MessageBox.Show("Invalid ID, must have 5 digits");
                return false;
            }
            return true;
        }



        public static bool IsUniqueID(List<Customer> listC, int id) {

            foreach (Customer c in listC)
            {
                if (c.CustomerId == id)
                {
                    MessageBox.Show("Duplicated, ID User ID must be unique");
                    return false;
                }
            }
            return true;
        }


        public static bool isValidName(TextBox text) {

            for(int i=0;i<text.TextLength;i++)
            {
                if(char.IsDigit(text.Text, i)|| char.IsWhiteSpace(text.Text, i))
                {
                    MessageBox.Show("Invalid Name, Please select another name", "INVALID NAME");
                    text.Clear();
                    text.Focus();
                    return false;
                }
            }
            return true;
        }


    }
}
