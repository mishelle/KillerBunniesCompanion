using System;
using System.Collections.Generic;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Documents;

namespace KillerBunniesCompanion.Views.Helpers
{
    public class String2Blocks
    {
        public IEnumerable<Block> Convert(string value)
        {
            var document = new XmlDocument();
            document.LoadXml("<r>" + value + "</r>");
            var p = new Paragraph();
            foreach (var item in ParseElement(document.DocumentElement))
                p.Inlines.Add(item);
            yield return p ;
        }

        private static IEnumerable<Inline> ParseElement(XmlElement element)
        {
            foreach (var child in element.ChildNodes)
            {
                if (child is Windows.Data.Xml.Dom.XmlText)
                {
                    if (string.IsNullOrEmpty(child.InnerText) ||
                        child.InnerText == "\n")
                    {
                        continue;
                    }

                    yield return new Run { Text = child.InnerText };
                }
                else if (child is XmlElement)
                {
                    XmlElement e = (XmlElement)child;
                    switch (e.TagName.ToUpper())
                    {
                        //case "P":
                        //    var paragraph = new Paragraph();
                        //    foreach (Inline item in ParseElement(e))
                        //        paragraph.Inlines.Add(item);
                        //    yield return paragraph;
                        //    break;
                        case "STRONG":
                        case "B":
                        case "SELF":
                            var bold = new Bold();
                            foreach (var item in ParseElement(e))
                                bold.Inlines.Add(new Run { Text = child.InnerText });
                            yield return bold;
                            break;
                        case "REF":
                            var title = e.GetAttribute("title");
                            if (string.IsNullOrEmpty(title)) title = e.InnerText;
                            var u = new Hyperlink();
                            u.NavigateUri = new Uri("title:" + title);
                            u.Click += u_Click;
                            foreach (var item in ParseElement(e))
                                u.Inlines.Add(new Run { Text = child.InnerText });
                            yield return u;
                            break;
                        //case "U":
                        //    var underline = new Underline();
                        //    parent.Add(underline);
                        //    ParseElement(e, new SpanTextContainer(underline));
                        //    break;
                        //case "A":
                        //    ParseElement(e, parent);
                        //    break;
                        //case "BR":
                        //    parent.Add(new LineBreak());
                        //    break;
                    }
                }


            }
        }

        static void u_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            var title = sender.NavigateUri.LocalPath;
            sender.NavigateUri = null;
            App.Controller.GoToTopic(title);
        } 
    }
}
