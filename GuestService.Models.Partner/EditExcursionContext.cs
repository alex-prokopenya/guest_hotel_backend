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

        public string VideoUrl;

        public KeyValuePair<string, string>[] Regions;

        public KeyValuePair<string, string>[] Languages;

        public KeyValuePair<int, string>[] Names; //+

        public KeyValuePair<int, string>[] Descriptions; //+
        public KeyValuePair<int, string>[] Routes;
        public KeyValuePair<int, string>[] Cancel;
        public KeyValuePair<int, string>[] Stuffs;

        public int[] Types; //+

        public string Route; //+

        public int Region; //+

        public int Guide; //+
        public int Food; //+
        public int EntryFees; //+

        public KeyValuePair<int, PriceInfo>[] OldPrices; //+

        public KeyValuePair<int, bool>[] OldPhotos;//+
    }
}
