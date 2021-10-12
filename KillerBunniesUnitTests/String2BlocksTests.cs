using KillerBunniesCompanion.Views.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;
using Assert = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;

namespace KillerBunniesUnitTests
{
    [TestClass]
    public class String2BlocksTests
    {
        [UITestMethod]
        public void TestStringWithBoldContainsBold()
        {
            var value = "before <b>bold</b> after";
            
            var blocks = new String2Blocks().Convert(value).ToArray();
            Assert.AreEqual(value, Stringify(blocks));
        }
        [UITestMethod]
        public void TestStringWithOnlyBoldContainsBold()
        {
            var value = "<b>bold</b>";

            var blocks = new String2Blocks().Convert(value).ToArray();
            Assert.AreEqual(value, Stringify(blocks));
        }

        private string Stringify(Block[] blocks)
        {
            var sb = new StringBuilder();
            foreach (var block in blocks)
            {
                sb.Append(Stringify(block));
            }
            return sb.ToString();
        }

        private string Stringify(Block block)
        {
            var sb = new StringBuilder();
            if (block is Paragraph)
                foreach (var b in ((Paragraph)block).Inlines)
                    sb.Append(Stringify(b));

            return sb.ToString();
        }

        private string Stringify(Inline inline)
        {
            var sb = new StringBuilder();
            if (inline is Run)
                sb.Append(((Run)inline).Text);
            if (inline is Bold)
            {
                sb.Append("<b>");
                foreach (var b in ((Bold)inline).Inlines)
                    sb.Append(Stringify(b));
                sb.Append("</b>");
            }
                
            return sb.ToString();
        }
    }
}
