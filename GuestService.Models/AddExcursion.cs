using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models
{
    public class AddExcursion : BaseApiParam
    {
        public string[] photos { get; set; }

        public string lang { get; set; }

        public string exc_region_name{ get; set; }

        public int? ex_id
        {
            get;
            set;
        }
        public int? guide
        {
            get;
            set;
        }
        public int? food
        {
            get;
            set;
        }
        public int? entryfees
        {
            get;
            set;
        }

        public string en_name
        {
            get;
            set;
        }
        
        public string ru_name
        {
            get;
            set;
        }

        public int? exc_type
        {
            get;
            set;
        }

        public int? exc_cat
        {
            get;
            set;
        }

        public string exc_en_routetext
        {
            get;
            set;
        }
        public string exc_ru_routetext
        {
            get;
            set;
        }

        public string exc_en_details
        {
            get;
            set;
        }
        public string exc_ru_details
        {
            get;
            set;
        }
        public string exc_ru_cancelations
        {
            get;
            set;
        }
        public string exc_en_cancelations
        {
            get;
            set;
        }

        public string exc_en_stuff
        {
            get;
            set;
        }
        public string exc_ru_stuff
        {
            get;
            set;
        }

        public string exc_en_route
        {
            get;
            set;
        }
        public string exc_ru_route
        {
            get;
            set;
        }
        public string exc_comis
        {
            get;
            set;
        }
      
        public int? exc_region
        {
            get;
            set;
        }

        public string exc_video
        {
            get;
            set;
        }

        public int[] ad_price { get; set; }
        public int[] ch_price { get; set; }
        public int[] inf_price { get; set; }
        public int[] total { get; set; }

        public int[] netto_ad_price { get; set; }
        public int[] netto_ch_price { get; set; }
        public int[] netto_inf_price { get; set; }
        public int[] netto_total { get; set; }
        public int[] price_comis { get; set; }
        public int[] comission_type { get; set; }


        public int[] group_from { get; set; }
        public int[] group_to { get; set; }
        public int[] group_type { get; set; }
        public int[] currency { get; set; }
        public int[] pr_lang { get; set; }
        public int[] price_region { get; set; }


        public string[] adate { get; set; }
        public string[] edate { get; set; }
        public string[] sdate_from { get; set; }
        public string[] sdate_to { get; set; }

        public string[] days { get; set; }
    }
}
