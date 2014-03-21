using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyImgur
{
    class History
    {
        static public HistoryServer impl = null;

        public static event HistoryServer.HistoryItemAddedEventHandler historyItemAdded
        {
            add { impl.historyItemAdded += value; }
            remove { impl.historyItemAdded -= value; }
        }
        public static event HistoryServer.HistoryItemRemovedEventHandler historyItemRemoved
        {
            add { impl.historyItemRemoved += value; }
            remove { impl.historyItemRemoved -= value; }
        }

        public static int count
        {
            get { return impl.count; }
        }

        public static int anonymousCount
        {
            get { return impl.anonymousCount; }
        }

        public static int accountCount
        {
            get { return impl.accountCount; }
        }

        public static void InitializeFromDisk()
        {
            impl.InitializeFromDisk();
        }

        public static void StoreHistoryItem(HistoryItem _Item)
        {
            impl.StoreHistoryItem(_Item);
        }

        public static void RemoveHistoryItem(HistoryItem _Item)
        {
            impl.RemoveHistoryItem(_Item);
        }
    }
}
