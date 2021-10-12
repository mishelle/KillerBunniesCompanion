using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KillerBunniesCompanion.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopicView : Page
    {
        public TopicView()
        {
            this.InitializeComponent();
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var richTB = sender as RichTextBlock;
            var textPointer = richTB.GetPositionFromPoint(e.GetPosition(richTB));

            var element = textPointer.Parent as TextElement;
            while (element != null && !(element is Underline))
            {
                if (element.ContentStart != null
                    && element != element.ElementStart.Parent)
                {
                    element = element.ElementStart.Parent as TextElement;
                }
                else
                {
                    element = null;
                }
            }

            if (element == null) return;

            var underline = element as Underline;
            //underline.Name should be the topic title
            //use controller to go to that topic
            App.Controller.GoToTopic(underline.Name);

        }
    }
}
