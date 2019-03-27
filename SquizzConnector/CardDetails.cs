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
       
       public CardDetails()
        {
            firstName = "";
            lastName = "";
            cardRecordID = "";
            cardName = "";
        }

        public CardDetails(string pfirstName, string plastName, string pcardRecordID,string pcardName)
        {
            firstName = pfirstName;
            lastName = plastName;
            cardRecordID = pcardRecordID;
            cardName = pcardName;
        }


    }
}
