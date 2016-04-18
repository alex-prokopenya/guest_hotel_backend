using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models
{
    public class CancellationMessageTemplate
    {
        public string NumberOrder;
        public string ReasonCancellation;

        //Client info
        public string Name;
        public string Phone;
        public string Email;
    }
}
