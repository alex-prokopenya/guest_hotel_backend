using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models.Guest
{
    public class CancellationOrderWebParam
    {
        public int claimId { get; set; }

        public string reason { get; set; }
    }
}