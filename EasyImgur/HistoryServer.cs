using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyImgur
{
    public class HistoryServer  : MarshalByRefObject
    {
        private System.Object ThreadLock = new System.Object();

        public event HistoryItemAddedEventHandler historyItemAdded;
        public event HistoryItemRemovedEventHandler historyItemRemoved;
        public delegate void HistoryItemAddedEventHandler( HistoryItem _Item );
        public delegate void HistoryItemRemovedEventHandler( HistoryItem _Item );

        private List<HistoryItem> m_History = new List<HistoryItem>();

        public int count
        {
            get { return m_History.Count; }
        }

        public int anonymousCount
        {
            get 
            {
                int c = 0;
                foreach (HistoryItem item in m_History)
                {
                    if (item.anonymous)
                    {
                        ++c;
                    }
                }
                return c;
            }
        }

        public int accountCount
        {
            get { return count - anonymousCount; }
        }

        public override object InitializeLifetimeService()
        {
            // Live "forever"
            return null;
        }

        public void InitializeFromDisk()
        {
            lock (ThreadLock)
            {
                List<HistoryItem> history = GetHistoryFromDisk();
                if (history != null)
                {
                    m_History = history;
                    foreach (HistoryItem item in m_History)
                    {
                        historyItemAdded(item);
                    }
                }
            }
        }

        public List<HistoryItem> GetHistoryFromDisk()
        {
            lock (ThreadLock)
            {
                string jsonString = string.Empty;
                try
                {
                    jsonString = System.IO.File.ReadAllText("history");
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Log.Info("Couldn't find a history file.");
                    return null;
                }
                catch (System.IO.IOException ex)
                {
                    Log.Error("An I/O error occurred while opening the history file.");
                    return null;
                }
                catch (System.UnauthorizedAccessException ex)
                {
                    Log.Error("Not authorized to open the history file.");
                    return null;
                }
                catch (System.Exception)
                {
                    Log.Error("An unexpected exception occurred while trying to read the history from disk.");
                    return null;
                }

                if (jsonString == null || jsonString == string.Empty)
                {
                    return null;
                }

                List<HistoryItem> history = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HistoryItem>>(jsonString, new ImageConverter());
                return history;
            }
        }

        public void StoreHistoryItem(HistoryItem _Item)
        {
            if (_Item == null)
            {
                Log.Warning("NULL object passed to History.StoreHistoryItem. No item stored.");
                return;
            }

            lock (ThreadLock)
            {
                m_History.Add(_Item);
                historyItemAdded(_Item);

                StoreHistoryOnDisk();
            }
        }

        public void RemoveHistoryItem(HistoryItem _Item)
        {
            lock (ThreadLock)
            {
                if (m_History.RemoveAll(item => item.id == _Item.id) <= 0)
                {
                    Log.Warning("Failed to remove history item from list. Item is not present in list.");
                    return;
                }

                StoreHistoryOnDisk();
                historyItemRemoved(_Item);
            }
        }

        public bool StoreHistoryOnDisk()
        {
            lock (ThreadLock)
            {
                bool success = true;
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(m_History, Newtonsoft.Json.Formatting.None, new ImageConverter());
                try
                {
                    System.IO.File.WriteAllText("history", jsonString);
                }
                catch (System.Exception ex)
                {
                    Log.Error("Something went wrong while trying to store the history on disk. Exception: " + ex.ToString());
                    success = false;
                }
                return success;
            }
        }
    }
}
