using KlevelBrowser.Model;
using KlevelBrowser.View;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KlevelBrowser.Controller
{

    class WebBrowserController
    {
        #region fields
        private Browser browser;
        private MainPage topPage=new MainPage();
        private ViewHTML resultsPage = new ViewHTML();
        private viewHistory historyList = new viewHistory();
        private Favourites favViewForm;    
        private HistoryHashTable historyInTable;
        private favourites viewf = new favourites();
        private HistoryLinkedList historyLog = new HistoryLinkedList();
        string name;    // i need this as a class variable to avoid nullpointer exception
        string url;

        #endregion

        #region constructors
        public WebBrowserController(Browser browser, MainPage topPage, ViewHTML resultsPage)
        {
            this.browser = browser;
            this.topPage = topPage;
            this.resultsPage = resultsPage;

        }

        public WebBrowserController(MainPage topPage)
        {
            this.topPage = topPage;


        }

        public WebBrowserController(viewHistory historyList)
        {
            this.historyList = historyList;


        }

        public WebBrowserController(favourites viewf)
        {
            this.viewf = viewf;


        }

        public WebBrowserController(ViewHTML resultsPage)
        {
            this.resultsPage = resultsPage;


        }


        #endregion


        public void startupView()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
         
        }
        #region methods
        /// <summary>
        /// it updates the page reposible for rendering the html of the given url(+1 overload)
        /// The method makes a web call and extracts html contents while writing the visited url to a history list that is stored 
        /// in a text file
        /// </summary>
        public async void updateviewhtml()
        {
            browser = new Browser();
            url=topPage.geturlInput();
           
            //
            System.DateTime newDate1 = DateTime.Now;
            string urlData = url + "@"+ newDate1.ToString();
            if (url.Contains("http")) { browser.writeURLToHistorfile(urlData); }
            historyLog.makeAcopy();
            
            var watch = Stopwatch.StartNew();
            
            string data = await browser?.geth(url);
            if (String.IsNullOrEmpty(data)) {
                MessageBox.Show("K-level could not make a web call, Please ensure you have entered a valid url","http content empty");
            }
            else
            {
                watch.Stop();
                var elapsed = watch.ElapsedMilliseconds;
                resultsPage.Show();
                
                resultsPage.getrichTextbo1().Text = data;
                name = browser.getBetween(data, "<title>", "</title>");
                resultsPage.Text = name;
                
                resultsPage.displayUrlTextbox().Text = url;
                resultsPage.getTimeTextBox().Text = elapsed.ToString()+"ms";

                //update status code

                string statusCode = browser.returnTheStatusCode();
                resultsPage.displaystatusCode().Text = statusCode;
                topPage.Hide();
                
            }

            
        }
        /// <summary>
        /// This is an overload of the updateviewhtml method but they all responsible for updating the html of the view page
        /// </summary>
        /// <param name="URL">given link to get  information for</param>
        public async void updateviewhtml( string URL)
        {
            browser = new Browser();
            System.DateTime newDate1 = DateTime.Now;
            string urlData = URL + "@" + newDate1.ToString();
            if (URL.Contains("http")) { browser.writeURLToHistorfile(urlData); }
            historyLog.makeAcopy();
            var watch = Stopwatch.StartNew();
            string data = await browser?.geth(URL);
            watch.Stop();
            if (String.IsNullOrEmpty(data))
            {
                MessageBox.Show("K-level could not make a web call, Please ensure you have entered a valid url", "http content empty");
            }
            else
            {

                var elapsed = watch.ElapsedMilliseconds;
                resultsPage.Show();
                resultsPage.getrichTextbo1().Text = data;

                name = browser.getBetween(data, "<title>", "</title>");
                resultsPage.Text = name;
                resultsPage.displayUrlTextbox().Text = URL;
                string statusCode = browser.returnTheStatusCode();
                resultsPage.displaystatusCode().Text = statusCode;
                resultsPage.getTimeTextBox().Text = elapsed.ToString() + "ms";

                topPage.closeTheForm();
            }
            
        }
        /// <summary>
        /// This method  is reposible for displaying contents of the history by traversing through a hash table and creating a history object
        /// and adding it to the history binding source  of the grid view
        /// </summary>
        public  void updatePlusviewHisroty()
        {

            Hashtable data = historyLog.readDatabaseIntoHashTable();
            foreach (DictionaryEntry entry in data)
            {
                string time = entry.Key.ToString();
                string website = entry.Value.ToString();
                historyInTable = new HistoryHashTable(website,time);
                BindingSource bs = historyList.getHistoryBindngSource();
                bs.Add(historyInTable);
            }

            historyList.Show();

        }

        /// <summary>
        /// This method will clear the hisory database/textfile if the yes option of the question messsage box is given as 
        /// a reposnse.All browsing history will be deleted.
        ///
        /// </summary>
        public void deleteHistory()
        {
            browser = new Browser();
            

            if(MessageBox.Show("Are you sure you want to permanently delete all your data?","Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                browser.clearHistory();
                string message = "History Database Cleared";
                string title = "Powered by @klevelThinking";
                SystemSounds.Asterisk.Play();
                MessageBox.Show(message, title);
            }

           
        }

        /// <summary>
        /// This method will add the current url/page to the favourites database to futher use such as viewing and navigation
        /// </summary>

        public async void addtohistoryFavourites()
        {
            
            browser = new Browser();
            
            string fav= resultsPage.geturlInputFromViewPageHTML();
            string data = await browser?.geth(fav);
            name = browser.getBetween(data, "<title>", "</title>");
            string nameAndURL=fav+"@" + name;
            browser.writeURLToHistorfavouriets(nameAndURL);
            string message =$"{name} was added to your favourites successfully!";
            string title = "Powered by @klevelThinking";
            SystemSounds.Asterisk.Play();
            MessageBox.Show(message, title);
        }


      /// <summary>
      /// This method is responsible for viewing the favourites database.
      /// It traverses the hashtable of the favourites and creates an favourite object by using the the key(timestamp) and value(link)
      /// The object is then added to the bindingSource
      /// </summary>
        public void viewFavs()
        {
            Hashtable data = historyLog.getHistoryDatafavs();

            

               foreach (DictionaryEntry entry in data)
            {
                string name = entry.Key.ToString();
                string website = entry.Value.ToString();
                favViewForm = new Favourites(name, website);
                viewf.getfavBingSource().Add(favViewForm);
                
            }

            viewf.Show();
            
        }

        /// <summary>
        /// This method handles the users request to get the desired  html content of the url corresponding to the name which was clicked in
        /// the grid view
        /// </summary>
        public async void loadFav()
        {
            //browser = new Browser();
            
            string url= viewf.getContentsOfCell();
            proccessLoad(url);
       
        }

        /// <summary>
        /// This method 
        /// </summary>
        public async void loadHistory()
        {
            //browser = new Browser();

            string url = historyList.getContentsOfCell();
            proccessLoad(url);
           
        }

        /// <summary>
        /// This is used to process the loading of a page
        /// Its used for loading favourite page and a historu page
        /// </summary>
        /// <param name="cellContents"></param>
        public async void proccessLoad(string cellContents)
        {
            browser = new Browser();
            string data = await browser?.geth(cellContents);
            System.DateTime newDate1 = DateTime.Now;
            string urlData = cellContents + "@" + newDate1.ToString();
            if (cellContents.Contains("http")) { browser.writeURLToHistorfile(urlData); }
            resultsPage.Show();
            resultsPage.getrichTextbo1().Text = data;
            resultsPage.getTextBoxOfURL().Text = cellContents;
            name = browser.getBetween(data, "<title>", "</title>");
            resultsPage.Text = name;
            string statusCode = browser.returnTheStatusCode();
            resultsPage.displaystatusCode().Text = statusCode;
        }


        /// <summary>
        /// Gets next url given the current url and uses  the next url to perform and update to the html page.
        /// </summary>
        public void NextUrl()
        {

           string nextURL= historyLog.getNextUrl();
           updateviewhtml(nextURL);


        }
        /// <summary>
        /// Gets next url given the current url and uses  the previous visited url to perform and update to the html page.
        /// </summary>
        public void prevUrl()
        {

            string nextURL = historyLog.previousURL();
            updateviewhtml(nextURL);


        }

        #endregion


    }
}
