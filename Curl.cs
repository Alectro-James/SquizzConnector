using System;


namespace SquizzConnector
{
    public class Curl
    {
        public Curl()
        {
             Curl myRequest = new Curl("https://mail.alectro.com.au:1155/squizzconnector/data/products", "GET", "keyProductId=002");
            //show the response string on the console screen.
            //Console.WriteLine(myRequest.GetResponse());

      
            Stream dataStream;


            ServicePointManager.ServerCertificateValidationCallback = (obj, X509certificate, chain, errors) => true;
            String finalUrl = string.Format("{0}{1}", "https://mail.alectro.com.au:1155/squizzconnector/data/customer_account_enquiry_line_report?keyCustomerAccountID=&recordType=INVOICE&reportID=invoice_lines&orderByField=keyInvoiceLineID&orderByDirection=desc&pageNumber=1&numberOfRecords=100000");
         //   Console.WriteLine(finalUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(finalUrl);
            // request = WebRequest.Create(finalUrl);
          //  Console.WriteLine("here");

            request.Method = "GET";
            request.ContentType = "application/json";
            String username = "CreateConnection";
            String password = "CreateConnection";
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;





            WebResponse response = request.GetResponse();
           // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //var response = request.GetResponse();
            //Now, we read the response (the string), and output it.

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            // using (var reader = new StreamReader(response.GetResponseStream()))
            {

                //      var json = reader.ReadToEnd();

                //      Console.Write(json);

            }

            // Read the content fully up to the end.

            string responseFromServer = reader.ReadToEnd();


           // Console.WriteLine(responseFromServer);

            //JObject o = JObject.Parse(responseFromServer);

            // string name = (string)o["dataFields"];
            // Apple

            //string sizes = (string)o["dataRecords"];
            //Console.WriteLine(name);
            //Console.WriteLine(sizes);



            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();


        }
    }

}

