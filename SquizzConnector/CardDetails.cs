using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquizzConnector
{
    class CardDetails
    {
        public string firstName;
        public string lastName;
        public string cardRecordID;
        public string cardName;
        public string cardEmail;
       
       public CardDetails()
        {
            firstName = "";
            lastName = "";
            cardRecordID = "";
            cardName = "";
            cardEmail = "";
        }

        public CardDetails(string pfirstName, string plastName, string pcardRecordID,string pcardName,string pcardEmail)
        {
            firstName = pfirstName;
            lastName = plastName;
            cardRecordID = pcardRecordID;
            cardName = pcardName;
            cardEmail = pcardEmail;
        }


    }
}
