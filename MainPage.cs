using System;
using KlevelBrowser.Controller;
using System.Windows.Forms;
using System.Threading;

namespace KlevelBrowser.View
{
    public partial class MainPage : Form
    {     /// <summary>
    /// Main page of the web browser
    /// </summary>
        public MainPage()
        {
           
            
            InitializeComponent();
            progressBar1.Hide();
            linkLabel1.Hide();
            
            

        }
        /// <summary>
        /// This is the search button after inputing valid url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button1_Click(object sender, EventArgs e)
        {
            WebBrowserController bc = new WebBrowserController(this);

            linkLabel1.Show();
            Thread.Sleep(2000);
            progressBar1.Show();
            bc.updateviewhtml();
        }

       /// <summary>
       /// Returns url input from textbox
       /// </summary>
       /// <returns></returns>
        public String geturlInput()
        {
            return textBox1.Text;
        }

        /// <summary>
        /// Closes the current Form
        /// </summary>
       public void closeTheForm()
        {
            this.Close();
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }


        //private void toolTip1_Popup(object sender, PopupEventArgs e)
        //{
        //    MessageBox.Show("Press to search");
        //}

      
    }
}
