using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models.Partner
{
    public class EditExcursionContext
    {
        //айдишник оригинальной экскурсии
        public int ExcursionId;

        //айдишник копии экскурсии
        public int CopyExcursionId;

        public KeyValuePair<string, string>[] Regions;

        public KeyValuePair<string, string>[] Languages;

        public KeyValuePair<int, string>[] Names; //+

        public KeyValuePair<int, string>[] Descriptions; //+

        public int[] Types; //+

        public string Route; //+

        public int Region; //+

        public KeyValuePair<int, PriceInfo>[] OldPrices; //+

        public KeyValuePair<int, bool>[] OldPhotos;//+
    }
}
