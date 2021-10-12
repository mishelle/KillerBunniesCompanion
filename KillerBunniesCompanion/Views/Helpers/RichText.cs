using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KillerBunniesCompanion.Views.Helpers
{

    public class RichTextC : DependencyObject
    {
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }

        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.Register("RichText", typeof(string), typeof(RichTextC), new PropertyMetadata(string.Empty, callback));

        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //newValue is the value of the bound property e.g. topic.RichDescription
            var reb = (RichEditBox)d;
            reb.Document.SetText(TextSetOptions.FormatRtf, (string)e.NewValue);
        }
    }
}
