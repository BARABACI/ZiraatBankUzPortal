using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.Helper
{
    public class UIMessage : IUIMessage
    {
        public string Message { get; set; }
        public Boolean Visible { get; set; }
    }
}
