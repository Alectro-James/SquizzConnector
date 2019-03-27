using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquizzConnector
{
    class DataWriter
    {

        public static void writeLineItems(List<LineItems> lineItems)
        {

            DataTable lineItemDataTable = new DataTable();
            lineItemDataTable.Clear();
          
            lineItemDataTable.Columns.Add("saleID");//0
            lineItemDataTable.Columns.Add("invoiceNumber");//1
            lineItemDataTable.Columns.Add("cardRecordID");//2
            lineItemDataTable.Columns.Add("invoiceDate");//3
            lineItemDataTable.Columns.Add("name");//3
            lineItemDataTable.Columns.Add("personNumber");//4
            lineItemDataTable.Columns.Add("email1");//5
            lineItemDataTable.Columns.Add("email2");//6
          
          

            string multiFileName = "";
            int rowsPerFile = 5000;
            double totalFiles = Math.Ceiling((double)lineItems.Count / rowsPerFile);
            Boolean flagTruncate = true;
            for (int j = 0; j < totalFiles; j++)
            {
                if (j > 0)
                {
                    multiFileName = (j + 1).ToString();
                }

                StreamWriter writer;
                try
                {
                    writer = new StreamWriter(Properties.Settings.Default.filePath+ "\\LineItems" + multiFileName + "." + "csv");
                }
                catch (IOException e)
                {
                    // progressBar.Value = 1;
                 
                    MessageBox.Show("Unable to export out CSV data to file " + Properties.Settings.Default.filePath + ". Please make sure you do not have the file open and you have access to the file.", "Cannot write to file", MessageBoxButtons.OK);
                    return;
                }
                
                writer.WriteLine("saleID,invoiceNumber,cardRecordID, invoiceDate, name,personNumber,email1,email2");

                for (int i = j * rowsPerFile; i < lineItems.Count && i < (j * rowsPerFile) + rowsPerFile; i++)
                {
                              lineItemDataTable.Rows.Add(lineItems[i].saleID, 
                                         lineItems[i].invoiceNumber,
                                         lineItems[i].cardRecordID,
                                         lineItems[i].invoiceDate, 
                                         lineItems[i].name, 
                                         lineItems[i].personNumber, 
                                         lineItems[i].email1, 
                                         lineItems[i].email2);

                                    writer.WriteLine("\"" + lineItems[i].saleID + "\",\""
                                        + lineItems[i].invoiceNumber + "\",\""
                                        + lineItems[i].cardRecordID + "\",\""
                                        + lineItems[i].invoiceDate + "\",\""
                                        + lineItems[i].name + "\",\""
                                        + lineItems[i].personNumber + "\",\""
                                        + lineItems[i].email1 + "\",\""
                                        + lineItems[i].email2 + "\",\""
                                        + "\"");
                        }

                DatabaseParse.insertLineItems(lineItemDataTable, "LineItems", flagTruncate);
                lineItemDataTable.Clear();
                flagTruncate = false;//delete only once
                writer.Close();
            }



            }

        public static void writeCSV(List<string> contactList) {


            

            string multiFileName = "";
            int rowsPerFile = 500;
            double totalFiles = Math.Ceiling((double)contactList.Count / rowsPerFile);
         
            for (int j = 0; j < totalFiles; j++)
            {
                if (j > 0)
                {
                    multiFileName = (j + 1).ToString();
                }

                StreamWriter writer;
                try
                {
                    writer = new StreamWriter(Properties.Settings.Default.filePath + "\\Contacts" + multiFileName + "." + "csv");
                }
                catch (IOException e)
                {
                    // progressBar.Value = 1;

                    MessageBox.Show("Unable to export out CSV data to file " + Properties.Settings.Default.filePath + ". Please make sure you do not have the file open and you have access to the file.", "Cannot write to file", MessageBoxButtons.OK);
                    return;
                }

               // writer.WriteLine("cardRecordID,firstName,lastName,cardName");

                for (int i = j * rowsPerFile; i < contactList.Count && i < (j * rowsPerFile) + rowsPerFile; i++)
                {
                  

                    writer.WriteLine(contactList[i]);
                }

            
                writer.Close();
            }



        }


        public static void writeCardItems(List<CardDetails> cardItems)
        {

            DataTable cardItemDataTable = new DataTable();
            cardItemDataTable.Clear();

            cardItemDataTable.Columns.Add("cardRecordID");//0
            cardItemDataTable.Columns.Add("firstName");//1
            cardItemDataTable.Columns.Add("lastName");//2
            cardItemDataTable.Columns.Add("cardName");//3
             cardItemDataTable.Columns.Add("cardEmail");//4




            string multiFileName = "";
            int rowsPerFile = 5000;
            double totalFiles = Math.Ceiling((double)cardItems.Count / rowsPerFile);
            Boolean flagTruncate = true;
            for (int j = 0; j < totalFiles; j++)
            {
                if (j > 0)
                {
                    multiFileName = (j + 1).ToString();
                }

                StreamWriter writer;
                try
                {
                    writer = new StreamWriter(Properties.Settings.Default.filePath + "\\CardItems" + multiFileName + "." + "csv");
                }
                catch (IOException e)
                {
                    // progressBar.Value = 1;

                    MessageBox.Show("Unable to export out CSV data to file " + Properties.Settings.Default.filePath + ". Please make sure you do not have the file open and you have access to the file.", "Cannot write to file", MessageBoxButtons.OK);
                    return;
                }

                writer.WriteLine("cardRecordID,firstName,lastName,cardName,cardEmail");

                for (int i = j * rowsPerFile; i < cardItems.Count && i < (j * rowsPerFile) + rowsPerFile; i++)
                {
                    cardItemDataTable.Rows.Add(cardItems[i].cardRecordID,
                               cardItems[i].firstName,
                               cardItems[i].lastName,
                               cardItems[i].cardName,
                               cardItems[i].cardEmail
                              );

                    writer.WriteLine("\"" + cardItems[i].cardRecordID + "\",\""
                        + cardItems[i].firstName + "\",\""
                        + cardItems[i].lastName + "\",\""
                        + cardItems[i].cardName + "\",\""
                         + cardItems[i].cardEmail + "\",\""
                        + "\"");
                }

               DatabaseParse.insertLineItems(cardItemDataTable, "cardItems", flagTruncate);
                cardItemDataTable.Clear();
                 flagTruncate = false;//delete only once
                writer.Close();
            }



        }
    }
}
