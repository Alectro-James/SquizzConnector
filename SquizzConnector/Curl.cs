using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Security;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;

namespace SquizzConnector
{
    internal class Curl
    {
        List<LineItems> listLineItems=new List<LineItems>();
        List<CardDetails> listCardDetails = new List<CardDetails>();
        public Curl()
        {


        }

        public void getLineItems()
        {

            //string customerAccountReportInvoiceLinesURL = "https://mail.alectro.com.au:1155/squizzconnector/data/customer_account_enquiry_line_report?keyCustomerAccountID=00075986&recordType=INVOICE&reportID=invoice_lines&orderByField=keyInvoiceLineID&orderByDirection=desc&pageNumber=1&numberOfRecords=10000";

            //changed th URL for new "customer accounts" see in the generic adaptor setting export
          string customerAccountReportInvoiceLinesURL = "https://mail.alectro.com.au:1155/squizzconnector/data/customer_accounts";

           // var yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            //Console.WriteLine(yesterday);

            //string customerAccountReportInvoiceLinesURL = "https://mail.alectro.com.au:1155/squizzconnector/data/supplier_account_enquiry_record?keySupplierAccountID=" + yesterday + "&recordType=invoice&keyRecordID=";
            //Console.WriteLine(customerAccountReportInvoiceLinesURL);
            string content = getResponseContent(customerAccountReportInvoiceLinesURL);
            if (content!="")
            {
                var json = "[" + content + "]"; // change this to array
                var objects = JArray.Parse(json); // parse as array  
                string emailValidation = "@\\A(?:[a - z0 - 9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z";
                
                string[] stringSeparators = new string[] { "\r\n" };
                string[] lines;
                List<string> wordsToRemove = "Name: Number: Email1: Email2: : Name Number Email1 Email2".Split(' ').ToList();
                string saleID = "";
                string invoiceNumber = "";
                string cardRecordID = "";
                string invoiceDate="";
                string personName = "";
                string personNumber="";
                string email1="";
                string email2="";


                foreach (JObject o in objects.Children<JObject>())
                {
                    foreach (JProperty p in o.Properties())
                    {

                        string name = p.Name;
                        var value = p.Value;

                       // if (name == "invoiceLineRecords")
                            if (name == "dataRecords")
                        {
                            foreach (var item in value)
                            {
                                var json1 = "[" + item + "]"; // change this to array
                                var objects1 = JArray.Parse(json1); // parse as array  

                                foreach (JObject o1 in objects1.Children<JObject>())
                                {
                                  //previous URL was customer account report invoice lines
                                    //saleID = o1["description"]+"";//SALEID
                                    //invoiceNumber = o1["keyInvoiceLineID"].ToString();//INVOICE NUMBER
                                    //cardRecordID =o1["lineItemID"].ToString();//CardRecordID
                                    //invoiceDate = o1["referenceLineItemCode"].ToString();//INVOICE DATE
                                    // lines = o1["referenceLineCode"].ToString().Split(stringSeparators, StringSplitOptions.None);

                                    saleID = o1["keyCustomerAccountID"] + "";//SALEID
                                    invoiceNumber = o1["accountClass"].ToString();//INVOICE NUMBER
                                    cardRecordID = o1["customerAccountCode"].ToString();//CardRecordID
                                    invoiceDate = o1["contactName"].ToString();//INVOICE DATE
                                    lines = o1["accountName"].ToString().Split(stringSeparators, StringSplitOptions.None);//description

                                    foreach (string s in lines)
                                    {
                                        if (s.Contains("Name"))
                                            personName = (s.Replace("Name","").Replace(":","")).Trim();
                                        else if(s.Contains("Number"))
                                            personNumber = (s.Replace("Number", "").Replace(":", "")).Trim();
                                        else if (s.Contains("Email1"))
                                            email1 = (s.Replace("Email1", "").Replace(":", "")).Trim();
                                        else if (s.Contains("Email2"))
                                            email2 = (s.Replace("Email2", "").Replace(":", "")).Trim();

                                        //Console.WriteLine(string.Join(" ", s.Split(' ').Except(wordsToRemove))); //But will print 3 lines in total.
                                    }

                                
                                    if(personNumber!="" || email1 != "" || email2!="")
                                    {
                                        if (email1 != "")
                                        {
                                            if (!email1.Contains("@"))
                                            {
                                                if (personNumber != "") {
                                                    if (personNumber.Contains("@"))
                                                    {
                                                        string temp = email1;
                                                        email1 = personNumber;
                                                        personNumber = temp;

                                                    }
                                                    else
                                                    {
                                                        if (personNumber == "")
                                                        {
                                                            if(Regex.Replace(email1, @"[^\d]", "").Length > 5)
                                                            {
                                                                personNumber = email1;
                                                            }
                                                        }
                                                       

                                                       
                                                    }


                                                }
                                                 
                                            }
                                            
                                        }
                                        if (personNumber != "")
                                        {
                                            personNumber = Regex.Replace(personNumber, @"[^\d]", "");
                                        }
                                            

                                    }

                                    if (email1 != "" && !email1.Contains("@"))
                                    {
                                        email1 = "";
                                    }

                                    if (email2 != "" && !email2.Contains("@"))
                                    {
                                        email2 = "";
                                    }

                                    listLineItems.Add(new LineItems(saleID, invoiceNumber, cardRecordID, invoiceDate,
                                        personName, personNumber, email1, email2));
                                     
                                    //Console.WriteLine(saleID+"--"+invoiceNumber+ "--" + cardRecordID+ "--" + invoiceDate+ "--" + personName+ "--" +
                                    //    personNumber+ "--" + email1+ "--" + email2);
                                    //Console.WriteLine(saleID.Length + "--" + invoiceNumber.Length + "--" + cardRecordID.Length + "--" + invoiceDate.Length + "--" + personName.Length + "--" +
                                    //   personNumber.Length + "--" + email1.Length + "--" + email2.Length);

                                    

                                }
                                
                            }

                        }
                        
                    }
                }

               DataWriter.writeLineItems(listLineItems);
            }         
           
        }


        public void getNames()
        {

            string customerAccountReportInvoiceLinesURL = "https://mail.alectro.com.au:1155/squizzconnector/data/taxCodes";
            string content = getResponseContent(customerAccountReportInvoiceLinesURL);
            if (content != "")
            {
                var json = "[" + content + "]"; // change this to array
                var objects = JArray.Parse(json); // parse as array  
                //Console.Write(content);
               // string[] stringSeparators = new string[] { "\r\n" };
              //  string[] lines;
               // List<string> wordsToRemove = "Name: Number: Email1: Email2:".Split(' ').ToList();
                string firstName = "";
                string lastName = "";
                string cardRecordID = "";
                string cardName = "";
              

                foreach (JObject o in objects.Children<JObject>())
                {
                    foreach (JProperty p in o.Properties())
                    {

                        string name = p.Name;
                        var value = p.Value;


                        if (name == "dataRecords")
                        {
                            foreach (var item in value)
                            {
                                var json1 = "[" + item + "]"; // change this to array
                                var objects1 = JArray.Parse(json1); // parse as array  

                                foreach (JObject o1 in objects1.Children<JObject>())
                                {

                                   cardRecordID = o1["taxcode"].ToString();
                                    lastName = o1["keyTaxcodeID"].ToString();
                                    firstName = o1["description"].ToString();

                                    if (firstName != "")
                                        cardName = firstName + " " + lastName;
                                    else
                                        cardName = lastName;

                                    listCardDetails.Add(new CardDetails(firstName, lastName, cardRecordID,cardName));

                                }

                            }

                        }

                    }
                }

                DataWriter.writeCardItems(listCardDetails);
            }

        }

        public void joinTables()
        {
            DataTable dt = new DataTable();
            string joinQuery = "select  (case when (LineItems.personNumber LIKE '04%' and LineItems.name!='') then LineItems.name else cardItems.cardName end) as ContactName,personNumber,invoiceNumber" 
             +" from cardItems, LineItems"
             +" where cardItems.cardRecordID = LineItems.cardRecordID and lineItems.personNumber != '' and (name != '' or cardName != '')"
             +" order by invoiceNumber desc";
            dt = DatabaseParse.ParseDatabase(joinQuery);
            List<String> contactCSV=new List<String>();


            DataTable contacts = new DataTable();
            contacts.Columns.Add("name");
            contacts.Columns.Add("number");

            string prev = (string)dt.Rows[0][1];
            contactCSV.Add("\"" + ((string)dt.Rows[0][2]).TrimStart(new Char[] { '0' }) + " " + dt.Rows[0][0] + "\";" + dt.Rows[0][1] + ";");
            contacts.Rows.Add(((string)dt.Rows[0][2]).TrimStart(new Char[] { '0' }) + " " + dt.Rows[0][0], dt.Rows[0][1]);
            bool check = false;

            

            for (int i=1;i<dt.Rows.Count;i++)
            {
             
                foreach (DataRow row in contacts.Rows)
                {
                    if (row["number"].ToString() == (string)dt.Rows[i][1])
                    {
                        check = true;
                    }
                }


                if (!check) {
                    contactCSV.Add("\"" + ((string)dt.Rows[i][2]).TrimStart(new Char[] { '0' }) + " " + dt.Rows[i][0] + "\";" + dt.Rows[i][1] + ";");
                    contacts.Rows.Add(((string)dt.Rows[i][2]).TrimStart(new Char[] { '0' }) + " " + dt.Rows[i][0], dt.Rows[i][1]);
                }
                check = false;

                // prev= (string)dt.Rows[i][1];


            }
            contactCSV.Add("<h1>FATAL ERROR</h1>");
            contactCSV.Add("<h3></h3>");
           DataWriter.writeCSV(contactCSV);
            DatabaseParse.updateDatabase(contacts,"contacts");
            

        }



        public string getResponseContent(string url)
        {
            string content = "";
            try
            {
               
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                string credentials = "MYOBCUSTOMEREXTRACT:MYOBCUSTOMEREXTRACT";
                CredentialCache mycache = new CredentialCache();
                myReq.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
                myReq.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                myReq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                myReq.Method = "GET";
                myReq.Timeout = 5 * 60 * 1000;//minute seconds
                myReq.ContentType = "application/json";
                ServicePointManager.ServerCertificateValidationCallback = new
    RemoteCertificateValidationCallback
    (
       delegate { return true; }
    );
                WebResponse wr = myReq.GetResponse();
               
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                content = reader.ReadToEnd();
             
              
            }
            catch(Exception e)
            {
                MessageBox.Show("connector URL error\r\n"+e.StackTrace);

            }
            return content;


        }

    }
}