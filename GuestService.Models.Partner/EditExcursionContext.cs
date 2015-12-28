using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models.Partner
{
    public class EditExcursionContext
    {
        public KeyValuePair<string, string>[] Regions;

        public KeyValuePair<string, string>[] Languages;

        public KeyValuePair<int, string>[] Names; //+

        public KeyValuePair<int, string>[] Descriptions; //+

        public int[] Types; //+

        public string Route; //+

        public int Region; //+

        public KeyValuePair<int, string>[] OldPrices; //+

        public KeyValuePair<int, bool>[] OldPhotos;//+
    }
}
