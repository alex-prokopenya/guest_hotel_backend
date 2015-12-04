using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
    public class UserBalanceContext
    {
        public int UserId
        {
            get;
            set;
        }

        public string UserAuth
        {
            get;
            set;
        }

        public string FilterDateFrom
        {
            get;
            set;
        }

        public string FilterDateTo
        {
            get;
            set;
        }
    }
}