using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquizzConnector
{
    class DatabaseParse
    {
        public static SqlConnection sql = new SqlConnection("Data Source=" + Properties.Settings.Default.defaultDatabaseSource + ";Initial Catalog=" + Properties.Settings.Default.defaultDatabase + ";Integrated Security=True");

        public static void insertLineItems(DataTable updateDataTable, string tableName, bool flgTruncate)
        {
            sql.Open();
            //SqlTransaction tran = sql.BeginTransaction();
            try
            {
                
               
                string selectQuery = "select top 1 * from " + tableName;
                string truncateQuery = "truncate table " + tableName;

                SqlDataAdapter dAdapter = new SqlDataAdapter(selectQuery, sql);
                SqlCommandBuilder cb = new SqlCommandBuilder(dAdapter);
                SqlCommand cmdSelect = new SqlCommand(truncateQuery, sql);



                dAdapter.Fill(updateDataTable);   //fill the DataTable

                if (flgTruncate)
                    cmdSelect.ExecuteNonQuery();//truncate the table 

                int removeCount = 0;

                //if (!flgTruncate)
                //    foreach (DataRow row in updateDataTable.Rows)
                //{
                        
                //    Console.WriteLine();
                //    for (int x = 0; x < updateDataTable.Columns.Count; x++)
                //    {
                //        Console.Write(row[x].ToString() + " ");
                //            if (row[0].ToString() == "31959")
                //            {
                //                updateDataTable.Rows[removeCount].Delete();
                //            }
                //    }
                //        removeCount += 1;
                //    }
                dAdapter.Update(updateDataTable);//insert into the table

                dAdapter.Dispose();


            }
            catch (SqlException e)
            {
                try
                {
                   // tran.Rollback();
                }
                catch(Exception exRollback)
                {
                    MessageBox.Show(exRollback.Message);
                }
                MessageBox.Show(e.Message);

            }

            if (sql.State == ConnectionState.Open)
                sql.Close();
        }


        public static DataTable ParseDatabase(string query)
        {



            SqlCommand cmd = new SqlCommand(query, sql);
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);



            //create a DataTable to hold the query results
            DataTable dTable = new DataTable();



            try
            {
                //fill the DataTable
                dAdapter.Fill(dTable);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.LineNumber);
                MessageBox.Show(e.Message);
            }


            dAdapter.Dispose();

            return dTable;
        }


        public static void updateDatabase(DataTable updateDataTable, string tableName)
        {
            string selectQuery = "select top 1 * from " + tableName;
            string truncateQuery = "truncate table " + tableName;

            SqlDataAdapter dAdapter = new SqlDataAdapter(selectQuery, sql);
            SqlCommandBuilder cb = new SqlCommandBuilder(dAdapter);
            SqlCommand cmd = new SqlCommand(truncateQuery, sql);


            try
            {
                sql.Open();
                dAdapter.Fill(updateDataTable);   //fill the DataTable
                cmd.ExecuteNonQuery();//truncate the table 
                dAdapter.Update(updateDataTable);//insert into the table

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }

            if (sql.State == ConnectionState.Open)
                sql.Close();

            dAdapter.Dispose();

        }

    }
}
