using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeSimulator
{
    public class ChatCollection
    {
        public  ObservableCollection<Chat> ChatContainer { get;set; }
        public ChatCollection()
        {
            ChatContainer = new ObservableCollection<Chat>();
        }
    }
}
