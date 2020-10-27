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
    public partial class ViewHTML : Form
    {
        private MainPage Home;
        public ViewHTML()
        {
            InitializeComponent();
        }


        public RichTextBox getrichTextbo1()
        {
            return this.richTextBox1;
        }

        public TextBox displayUrlTextbox()
        {
            return this.textBox2;
        }

        public void hideHTMLView()
        {
            this.Hide();
        }

        public TextBox displaystatusCode()
        {
            return this.textBox1;
        }

        public String geturlInputFromViewPageHTML()
        {
            return textBox2.Text;
        }
        public TextBox getTimeTextBox()
        {
            return textBox3;
        }
        public TextBox getTextBoxOfURL()
        {
            return textBox2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.updatePlusviewHisroty();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.deleteHistory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.addtohistoryFavourites();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
           WebBrowserController bc = new WebBrowserController(this);
           bc.viewFavs();
           //this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.NextUrl();
        }

     

        private void button5_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            bc.prevUrl();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //WebBrowserController bc = new WebBrowserController(this);
            //bc.showMainPage();
            //this.Dispose();
            Home = new MainPage();
            Home.Show();
            this.Dispose();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);
            string url = textBox2.Text;
            bc.updateviewhtml(url);
            

           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
