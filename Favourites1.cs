using KlevelBrowser.Model;
using System;
using System.Collections;

namespace Model
{    /// <summary>
/// This class is important for facilitating the favourites data grid view and object bindings
/// with public variables so that they are visible to the binding source
/// </summary>
    internal class Favourites: HistoryLinkedList

         
    {
         public string url { get; set; }
         public string name { get; set; }
          
        public Favourites(string name,string URL)
        {
            this.name = name;
            this.url = URL;
        }



        /// <summary>
        /// Reads data into a hashTable to make use of the key-value feature of the data structure
        /// </summary>
        /// <returns>hash table of favourites with their links</returns>
        public new Hashtable readDatabase()
        {
           Hashtable data= getHistoryDatafavs();
           return data;
        }





    }
}