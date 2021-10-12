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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchView : Page
    {
        public SearchView()
        {
            this.InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems.First() as TopicViewModel;
                var context = DataContext as SearchViewModel;
                if (context != null && item != null)
                {
                    context.SelectedTopic = item;
                }
            }
        }

        private bool isDownPressed;

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!isDownPressed)
            {
                var context = DataContext as SearchViewModel;
                if (context != null) context.SearchBoxLostFocus();
            }
            isDownPressed = false;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var context = DataContext as SearchViewModel;
            if (context != null) context.SearchBoxGotFocus();
        }

        private void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var context = DataContext as SearchViewModel;
            if (context != null)
            {
                if (e.Key == Windows.System.VirtualKey.Escape)
                {
                    LoseFocus(textBox);
                }
                if (e.Key == Windows.System.VirtualKey.Down)
                {
                    isDownPressed = true;

                    int index = listBox.SelectedIndex;

                    //if (index >= 0)
                    {
                        var item = ((ItemsControl)listBox).ContainerFromIndex(0) as ListBoxItem;

                        if (item != null)
                            item.Focus(FocusState.Programmatic);
                    }
                    
                }
            }
        }

        private void LoseFocus(Control control)
        {
            control.IsEnabled = false;
            control.IsEnabled = true;
        }
    }
}
