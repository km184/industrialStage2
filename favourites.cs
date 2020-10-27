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
    public partial class favourites : Form
    {
        public favourites()
        {
            InitializeComponent();
        }
        /// <summary>
        /// returns the binding source for the favourites
        /// </summary>
        /// <returns>favouritesBindingSource</returns>
        public BindingSource getfavBingSource()
        {
            return favouritesBindingSource;
        }

        /// <summary>
        /// This method return the contents of a cell from the data grid view
        /// </summary>
        /// <returns>string of url</returns>
        public string getContentsOfCell()
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            string url = row.Cells[0].Value.ToString();
            return url;
        }
        /// <summary>
        /// This method facilitates in configuring what happens the name corresponding to the url is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
                WebBrowserController bc = new WebBrowserController(this);
                bc.loadFav();
                this.Hide();

        }
    }
}
