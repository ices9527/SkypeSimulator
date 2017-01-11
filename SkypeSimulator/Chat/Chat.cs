using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeSimulator
{
    public class Chat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string visibility;
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    ProChanged("visibility");
                }
            }
        }

        private string chatContent;
        public string ChatContent
        {
            get
            {
                return chatContent;
            }
            set
            {
                if (value != chatContent)
                {
                    chatContent = value;
                    ProChanged("chatcontent");
                }
            }
        }

        private string chatTime;
        public string ChatTime
        {
            get
            {
                return chatTime;
            }
            set
            {
                if (value != chatTime)
                {
                    chatTime = value;
                    ProChanged("chattime");
                }
            }
        }
        
        private void ProChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
