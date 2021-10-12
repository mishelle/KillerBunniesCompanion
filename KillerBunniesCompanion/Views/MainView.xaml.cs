using KillerBunniesCompanion.ViewModels;
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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KillerBunniesCompanion.Views
{
    /// <summary>
    /// A page that shows a navigation/search bar and the details of the current topic
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
            DataContext = App.Controller.GetMainViewModel();
            listBox.AddHandler(KeyDownEvent,
            new KeyEventHandler((sender,e)=>{
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    DisplayTopic();

                }
                                                  }), 
                                                     true);
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems.First() as TopicViewModel;
                var context = DataContext as MainViewModel;
                if (context != null && item != null)
                {
                    context.AddTopic(item);
                    //context.IsHistoryVisible = false;
                }

            }
        }
        private void History_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as MainViewModel;
            if (context != null)
            {
                context.IsHistoryVisible = true;  //!context.IsHistoryVisible;
                //hide search?
                test.IsOpen = true;
                //var item = ((ItemsControl)listBox).ContainerFromIndex(0) as ListBoxItem;
                //if (item != null)
                //    item.Focus(FocusState.Programmatic);
            }
        }

        private void Button_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var context = DataContext as SearchViewModel;
            if (context != null)
            {
                if (e.Key == Windows.System.VirtualKey.Down)
                {
                    var item = ((ItemsControl)listBox).ContainerFromIndex(0) as ListBoxItem;

                    if (item != null)
                        item.Focus(FocusState.Programmatic);

                }
            }
        }

        private void listBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DisplayTopic();
        }

        private void DisplayTopic()
        {
            var item = listBox.SelectedItem as TopicViewModel;
            if (item == null) return;
            var context = DataContext as MainViewModel;
            if (context != null)
            {
                context.AddTopic(item);
                //context.IsHistoryVisible = false;
                test.IsOpen = false;
            }

        }

        private void test_GotFocus(object sender, RoutedEventArgs e)
        {
            var item = ((ItemsControl)listBox).ContainerFromIndex(0) as ListBoxItem;

            if (item != null)
                item.Focus(FocusState.Programmatic);
        }

        private void listBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //this does not fire for Enter or key up/down, but does for tab
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    DisplayTopic();

                }
            
        }

    }
}
