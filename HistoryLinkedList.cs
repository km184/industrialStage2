using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlevelBrowser.Model
{
    public class HistoryLinkedList
    {
        /// <summary>
        /// This class is responsible to containing the data structures of the browser data for history,navigation and favourites
        /// this can be linkedlist,hashtables depending on what we intend to do with the object
        /// </summary>
        /// 
        #region fields
        private LinkedList<String> history;
        private Hashtable favouritesSites;                                                                   
        #endregion
        public HistoryLinkedList()
        {
            history = new LinkedList<String>();
        }

            /// <summary>
            /// This method will read a data file given a path and deposit the contents into a data structure that will be worked with
            /// </summary>
            /// <param name="path"> where the file is located</param>
            /// <param name="dataContainer">where to deposit the contents of the file after processing</param>
            /// <returns></returns>

          public LinkedList<String> readDatabaseGeneral(string path,LinkedList<string> dataContainer)
        {


            using (StreamReader sr = new StreamReader(path))
            {


                while (sr.Peek() >= 0)
                {
                    string lineRepresentingLink = sr.ReadLine();
                    history.AddFirst(lineRepresentingLink);
                }

                return history;
            }
        }


        /// <summary>
        /// This method processes data from a text file into a data structure ---> hashTable for further use
        /// </summary>
        /// <returns>hashtable of history objects: time and link(key, value)</returns>
        public  Hashtable readDatabaseIntoHashTable()
        {
            LinkedList<string> list1 = new LinkedList<string>();
            string HistoryFile = @"DatabaseH.txt";
            LinkedList<string> list = readDatabaseGeneral(HistoryFile, list1);
            Hashtable HistoryTable = new Hashtable();

            string DefaultName = "unnamed";
            foreach (string str in list)
            {


                string[] nextUrlModified1 = str.Split(new string[] { "@" }, StringSplitOptions.None);


                string time = nextUrlModified1[1];
                if (time == "") { time = DefaultName; }
                string site = nextUrlModified1[0];

                HistoryTable.Add(time, site);

              

            }
            
            return HistoryTable;
        }

        /// <summary>
        /// This methods writesback to a database/text file if the use has navigated forward for use in going back
        /// </summary>
        /// <param name="link">link which traversed to</param>
        /// <param name="path">path to the database</param>
        public void writeURLToBackwardsNavData(string link,string path)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(link);
            sw.Close();


        }

        /// <summary>
        /// this method will make a copy of data from history to a different file for navigation
        /// the copy will be traversed inh such a way that its contents are deleted hence make the copy
        /// </summary>
        public void makeAcopy()
        {

            string pathFrom = @"DatabaseH.txt";
            string pathTo = @"NavigationDatabase1.txt";
            
            File.Copy(pathFrom, pathTo,true);

        }

        /// <summary>
        /// This method reads the database of the favourite urls
        /// </summary>
        /// <returns>LinkedList<String></returns>

        public LinkedList<String> readDatabaseFavs()
        {
            string HistoryFile = @"FavouritesDatabase.txt";
            using (StreamReader sr = new StreamReader(HistoryFile))
            {


                while (sr.Peek() >= 0)
                {
                    string lineRepresentingLink = sr.ReadLine();
                    // Create a new LinkedListNode of type String and add it to the linkedList
                    LinkedListNode<String> lln = new LinkedListNode<String>(lineRepresentingLink);  
                    history.AddFirst(lln);
                }

                return history;
            }

        }

        /// <summary>
        /// This method reads the  database containing favourite links into a hashtable
        /// </summary>
        /// <returns>Hashtable </returns>
        public Hashtable getHistoryDatafavs()
        {
            LinkedList<String> list = readDatabaseFavs();
            favouritesSites = new Hashtable();
            string DefaultName = "unnamed";
            foreach (string str in list)
            {


              string []  nextUrlModified1 = str.Split(new string[] { "@" }, StringSplitOptions.None);
                
                
                string name = nextUrlModified1[1];
                if (name=="") { name = DefaultName; }
                string site = nextUrlModified1[0];
                if (favouritesSites.Contains(name))
                {
                    favouritesSites.Remove(name);
                }
                favouritesSites.Add(name, site);
            }
            return favouritesSites;

        }
        /// <summary>
        /// This method gets the next url in th navigation database
        /// </summary>
        /// <returns>string of next url to go to</returns>
        public string getNextUrl()
        {
            try
            {
                string HistoryFile = @"NavigationDatabase1.txt";

                LinkedList<String> data1 = readDatabaseGeneral(HistoryFile, history);
                string url = data1.First.Value;
                string nextUrl1 = data1.Find(url).Next.Value;
                writeURLToBackwardsNavData(url, @"NavigationDatabaseBackWards.txt");
                string[] nextUrlModified1 = nextUrl1.Split(new string[] { "@" }, StringSplitOptions.None);
                string finalUrl1 = nextUrlModified1[0].Trim();
                data1?.Remove(url);
                string path = @"NavigationDatabase1.txt";
                var lines = System.IO.File.ReadAllLines(path);
                System.IO.File.WriteAllLines(path, lines.Take(lines.Length - 1).ToArray());
                return finalUrl1;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }
        /// <summary>
        /// This method returns the tring off the previous url
        /// </summary>
        /// <returns>string url</returns>
        public string previousURL()
        {
            try
            {
                string HistoryFile = @"NavigationDatabaseBackWards.txt";
                LinkedList<String> data1 = readDatabaseGeneral(HistoryFile, history);
                string nextUrl1 = data1?.First.Value;
                string[] nextUrlModified1 = nextUrl1.Split(new string[] { "@" }, StringSplitOptions.None);
                string finalUrl1 = nextUrlModified1[0].Trim();
                string path1 = @"NavigationDatabase1.txt";
                writeURLToBackwardsNavData(nextUrl1, path1);
                data1?.Remove(nextUrl1);
                string path = @"NavigationDatabaseBackWards.txt";
                var lines = System.IO.File.ReadAllLines(path);
                System.IO.File.WriteAllLines(path, lines.Take(lines.Length - 1).ToArray());
                return finalUrl1;
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show("All previous Websites you visited are Exhausted");
            }
            return null;
        }

    }
}
