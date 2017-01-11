
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SkypeSimulator
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Window win = Application.Current.MainWindow;
            if (item != null && item is Chat)
            {
                Chat p = item as Chat;
                if (p.Visibility == "Visible")
                {
                    return win.FindResource("AnswerTemplate") as DataTemplate;
                }
                else
                {
                    return win.FindResource("QuestionTemplate") as DataTemplate;
                }
            }
            return null;
        }
    }
}
