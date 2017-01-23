using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkypeSimulator
{
    public delegate string GetResponseDelegate(string enterpriseid, string input);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MyTEBotService.MyTEBotService myTeBotService;
        private ChatCollection dc;
        private string answer = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            myTeBotService = new MyTEBotService.MyTEBotService();
            dc = new ChatCollection();
            this.DialogueArea.ItemsSource = dc.ChatContainer;
            //CloseButton.AddHandler(Image.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.CloseButton_OnMouseLeftButtonUp), true);
        }

        private void InputTextbox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !this.InputTextbox.Text.Trim().Equals(string.Empty))
            {
                //this.HistoryTextArea.AppendText("<a>this is a URL</a>");
                //this.InputTextbox.Text = "sdasdasdasdasdasdasdas";
                var input = this.InputTextbox.Text.Replace("\r\n", string.Empty);
                dc.ChatContainer.Add(new Chat() { Visibility = "Hidden", ChatContent = input, ChatTime = DateTime.Now.ToShortTimeString() });

                GetResponseDelegate responseDelegate = new GetResponseDelegate(myTeBotService.GetMyTeBotResponse);
                responseDelegate.BeginInvoke("xinglong.liang", input, this.ResponseCallBack, responseDelegate);
                
                this.LastUpdateTime.Content = string.Format("Last message received on {0} at {1}.",
                    DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
                this.DialogueArea.ScrollIntoView(this.DialogueArea.Items[this.DialogueArea.Items.Count - 1]);
                
                this.InputTextbox.Text = string.Empty;
            }
        }

        private void ResponseCallBack(IAsyncResult result)
        {
            var responseDelegate = result.AsyncState as GetResponseDelegate;
            var answer = responseDelegate.EndInvoke(result);
            answer = answer.Trim('[', ']').Replace('"', ' ').Replace("\\n", "\n").Replace("\\", string.Empty).Replace("|", "\\").Trim();
            if (answer.Trim() != string.Empty)
            {
                App.Current.Dispatcher.Invoke((Action) delegate // <--- HERE
                {
                    dc.ChatContainer.Add(new Chat()
                    {
                        Visibility = "Visible",
                        ChatContent = answer.Replace("<LineBreak/>", "\n"),
                        ChatTime = DateTime.Now.ToShortTimeString()
                    });
                    this.DialogueArea.ScrollIntoView(this.DialogueArea.Items[this.DialogueArea.Items.Count - 1]);
                });
            }
            else
            {
                dc.ChatContainer.Add(new Chat()
                {
                    Visibility = "Visible",
                    ChatContent = "The input is invalid please check and type again.",
                    ChatTime = DateTime.Now.ToShortTimeString()
                });
                this.DialogueArea.ScrollIntoView(this.DialogueArea.Items[this.DialogueArea.Items.Count - 1]);
            }
        }

        private void TopAccount_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
