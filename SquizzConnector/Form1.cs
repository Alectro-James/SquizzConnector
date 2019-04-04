using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SquizzConnector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Curl c = new Curl();


        //  c.getLineItems();
        //  c.getNames();
         //  c.joinTables();
       //  Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Curl c = new Curl();
            Cursor.Current = Cursors.WaitCursor;
            c.getLineItems();
            Cursor.Current = Cursors.Default;
        }

        private void cardItemsButton_Click(object sender, EventArgs e)
        {
            Curl c = new Curl();
            Cursor.Current = Cursors.WaitCursor;
            c.getNames();
            Cursor.Current = Cursors.Default;
        }

        private void joinTables_Click(object sender, EventArgs e)
        {
            Curl c = new Curl();
            Cursor.Current = Cursors.WaitCursor;

            c.getLineItems();
            c.getNames();
            c.joinTables();

            Cursor.Current = Cursors.Default;
        }
    }
}
