using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using CompleteCRUD.BLL;


namespace CompleteCRUD.DAL
{
    public static class CustomerDA
    {
        private static string filePath = Application.StartupPath + @"\Customers.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";


        public static void Save(Customer cust)
        {
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Customer Data has been saved!");
        }


        //will add the value the the LIST VIEW
        public static void ListCustomers(ListView listViewCustomer)
        {
            //Step1: Create an object of type StreamReader

            StreamReader sReader = new StreamReader(filePath);
            listViewCustomer.Items.Clear();

            string line = sReader.ReadLine();

            while (line !=null)
            {
                string[] fields = line.Split(',');

                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);

                listViewCustomer.Items.Add(item);

                line = sReader.ReadLine(); // read the next line
            }
            sReader.Close();
        }


        //will add the value from the component the object
        public static List<Customer> ListCustomers()
        {
            List<Customer> listC = new List<Customer>();

            //Step1: Create an object of type StreamReader
            StreamReader sReader = new StreamReader(filePath);
         
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');

                Customer cust = new Customer();

                cust.CustomerId = Convert.ToInt32(fields[0]);
                cust.FirstName = fields[1];
                cust.LastName = fields[2];
                cust.PhoneNumber = fields[3];

                listC.Add(cust);
          
                line = sReader.ReadLine(); // read the next line
            }
            sReader.Close();
            return listC;



        }

        public static Customer Search(int custId){

            Customer cust = new Customer();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line!=null)
            {
                string[] fields = line.Split(',');

                if (custId == Convert.ToInt32(fields[0])) 
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];
                    sReader.Close();

                    return cust;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static Customer Search(string otherInfo)
        {

            Customer cust = new Customer();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');

                if (otherInfo == fields[1] || otherInfo == fields[2] || otherInfo == fields[3])
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];
                    sReader.Close();

                    return cust;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            return null;
        }

        public static void Delete(int custId) {

            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWrite = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');

                if (custId != Convert.ToInt32(fields[0]))
                {
                    sWrite.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }

                line = sReader.ReadLine();

            }

            sReader.Close();
            sWrite.Close();
            //Delete the old file Customers.dat
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        
        }

        public static void Update(Customer cust) {

            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWrite = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (Convert.ToInt32(fields[0]) != cust.CustomerId)
                {
                    sWrite.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }
                line = sReader.ReadLine();
            }
            sWrite.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
            sReader.Close();
            sWrite.Close();
            //Delete the old file Customers.dat
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }
    }
}
