using KlevelBrowser.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlevelBrowser.View
{
    public partial class viewHistory : Form
    {
        public viewHistory()
        {
            InitializeComponent();
        }

 
        public BindingSource getHistoryBindngSource()
        {
            return historyHashTableBindingSource;
        }

        public string getContentsOfCell()
        {
            DataGridViewRow row = dataGridView.CurrentRow;
            string url = row.Cells[0].Value.ToString();
            return url;
        }

     

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.loadHistory();
            this.Hide();
        }
    }
}
