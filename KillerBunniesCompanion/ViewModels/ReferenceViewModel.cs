using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerBunniesCompanion.ViewModels
{
    public class ReferenceViewModel : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        private int _linkId;
        public int LinkId
        {
            get { return _linkId; }
            set { SetProperty(ref _linkId, value); }
        }
    }
}
