using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KillerBunniesCompanion.Views.Helpers
{
    public class RtfConverter
    {
        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        // Using a DependencyProperty as the backing store for Html.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(RtfConverter), new PropertyMetadata("", OnHtmlChanged));

        private static void OnHtmlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            var parent = sender as RichTextBlock;
            var value = eventArgs.NewValue as string;
            if (parent == null || value == null) return;

            parent.Blocks.Clear();

            var blocks = new String2Blocks().Convert(value);
            foreach (var block in blocks)
                parent.Blocks.Add(block);

        } 
    }
}
