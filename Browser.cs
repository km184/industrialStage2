using System;
using System.Diagnostics;
using System.Media;
using System.Threading.Tasks;

using System.Net.Http;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace KlevelBrowser.Model
{


    /// <summary>
    /// This is a class that will be the blue print for a web-browser  object
    /// This will be able to initiate a web call and return the html contents from the web call
    /// </summary>
    public class Browser
    {

        private string statusCode { get; set; }

        #region methods

        /// <summary>
        /// return the status code of this object
        /// </summary>
        /// <returns>status code e.g ok, not found</returns>
        public string returnTheStatusCode()
        {
            return this.statusCode;
        }

        /// <summary>
        /// Sets the status code of the object
        /// </summary>
        /// <param name="code"></param>
        public void setTheStatusCode(String code)
        {
             this.statusCode=code;
        }

        #region Send request
        /// <summary>
        /// This is an async method which sends returns the html contents of the url provided as an argument
        /// by initiating  a client and making a web call and disposes it resources after the task is done
        /// </summary>
        /// <param name="url">The url/link to be visited</param>
        /// <returns>Task<String> </returns>
        public async Task<String> geth(string url)
        {

            String res =null;
          

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using (HttpResponseMessage mes = await client?.GetAsync(url))

                    {

                        using (HttpContent cont = mes.Content)
                        {
                             this.statusCode = mes.StatusCode.ToString();
                             string data = await cont.ReadAsStringAsync();
                             return res=res+data;
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show("Invalid httpRequest made"+e.Message);

                    SystemSounds.Asterisk.Play();
                }catch(InvalidOperationException IOE)
                {
                    MessageBox.Show("Invalid URL"+IOE.Message);
                }
              
                
                }


            return res;
           
        }




 #endregion
       /// <summary>
       /// This method gets the tittle of the web page from the html tittle tags
       /// </summary>
       /// <param name="strSource">source file/string</param>
       /// <param name="strStart">starting string</param>
       /// <param name="strEnd">end string</param>
       /// <returns></returns>

        public  string getBetween(string strSource, string strStart, string strEnd)
        {
            try
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    int Start, End;
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End - Start);
                }

                
            }
            catch (NullReferenceException NRE)
            {
                
            }
            return "";
        }
        /// <summary>
        /// This method writes the url to the history file using stream writer
        /// </summary>
        /// <param name="link">url to copy to history database</param>
        public void writeURLToHistorfile(string link)
        {
          
          string pathKuda= @"DatabaseH.txt";
         StreamWriter sw = new StreamWriter(pathKuda, true);
            sw.WriteLine(link);
            sw.Close();
        }

        /// <summary>
        /// clears contents of the history directly from the database
        /// </summary>
        public void clearHistory()
        {
            string HistoryFile = @"DatabaseH.txt";
           // string path = @"C:\Users\kumug\Documents\Visual Studio 2015\Projects\KlevelBrowser\KlevelBrowser\KlevelBrowser\DatabaseH.txt";
            FileStream fileStream = File.Open(HistoryFile, FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close();
        }

        /// <summary>
        /// Writes given url to the favourites database
        /// </summary>
        /// <param name="link">link to copy to database</param>
         public void writeURLToHistorfavouriets(string link)
        {


            string HistoryFile = @"FavouritesDatabase.txt";
            StreamWriter sw = new StreamWriter(HistoryFile, true);
            sw.WriteLine(link);
            sw.Close();


        }


        #endregion


    }




}
