using KlevelBrowser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{   

    /// <summary>
    /// This class is reponsible for facilitating  in the data grid view of the history objects
    /// 
    /// </summary>

    class HistoryHashTable:HistoryLinkedList
    {
        public string url { get; set; }
       public  string time { get; set; }


        /// <summary>
        /// Constructor of a history object that will be passed through to a binding data source
        /// </summary>
        /// <param name="url"></param>
        /// <param name="time"></param>
        public HistoryHashTable(string url, string time)
        {
            this.url = url;
            this.time = time;
        }

    }
}
