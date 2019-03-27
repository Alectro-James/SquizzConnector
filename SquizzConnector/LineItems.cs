using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquizzConnector
{
   
  public  class LineItems
    {
        public  string saleID;
        public string invoiceNumber;
        public string cardRecordID;
        public string invoiceDate;
        public string name;
        public string personNumber;
        public string email1;
        public string email2;

      public LineItems()
        {
             saleID= "";
             invoiceNumber= "";
             cardRecordID= "";
             invoiceDate="";
             name="";
             personNumber="";
             email1="";
             email2="";
        }
        public LineItems(string psaleID, string pinvoiceNumber, string pcardRecordID,string pinvoiceDate,
            string pname, string ppersonNumber, string pemail1, string pemail2)
        {
            saleID = psaleID;
            invoiceNumber = pinvoiceNumber;
            cardRecordID = pcardRecordID;
            invoiceDate = pinvoiceDate;
            name = pname;
            personNumber = ppersonNumber;
            email1 = pemail1;
            email2 = pemail2;
        }


    }
}
