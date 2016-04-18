using GuestService.Code;
using GuestService.Data;
using GuestService.Models;
using GuestService.Models.Excursion;
using GuestService.Resources;
using Sm.System.Exceptions;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using Sm.System.Database;
using WebMatrix.WebData;
using System.Threading;
using System.Threading.Tasks;
using GuestService.Notifications;
using System.Configuration;
using System.Data;
namespace GuestService.Controllers.Api
{
    [HttpUrlLanguage]
    public class PartnerExcursionController : ApiController
    {
        [ActionName("about"), HttpGet]
        public void About([FromUri] SearchParam param)
        {
            return;
        }

        [ActionName("search"), HttpGet]
        public SearchExcursionResult Search([FromUri] SearchParam param)
        {
            if (!WebSecurity.IsAuthenticated)
                throw new System.ArgumentNullException("auth");

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            if (string.IsNullOrEmpty(param.SearchText))
            {
                throw new ArgumentNullExceptionWithCode(101, "s");
            }
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            int limit = (param.SearchLimit.HasValue && param.SearchLimit.Value > 0) ? param.SearchLimit.Value : Settings.ExcursionGeographySearchLimit;
            
            var excs = ExcursionProvider.SearchExcursionObjects(param.Language, param.StartPoint, param.SearchText, limit);

            return excs;
        }

        //создаем новый объект "экскурсия"
        //с привязкой к поставщику
        //с привязкой к региону
        //проставляем имя
        //проставляем маршрут
        //получаем новый айдишник

        //проставляем описание
        //добавляем фото
        private static int CreateNewExcursion(AddExcursion data, int provider)
        {
            try
            {
                #region Addexcursion
                #region Insert query
                var query = " INSERT INTO dbo.excurs( " +
      "                                                                       name " +
      "                                                                     , lname " +
      "                                                                     , duration " +
      "                                                                     , region " +
      "                                                                     , horder " +
      "                                                                     , distance " +
      "                                                                     , maxinfage " +
      "                                                                     , maxchildage " +
      "                                                                     , note " +
      "                                                                     , freeexcurs" +
      "                                                                     , voucher" +
      "                                                                     , roomnum" +
      "                                                                     , people" +
      "                                                                     , _individual" +
      "                                                                     , route" +
      "                                                                     , skey" +
      "                                                                     , excurstype" +
      "                                                                     , partner" +
      "                                                                     , _withoutplan" +
      "                                                                     , author" +
      "                                                                     , editor" +
      "                                                                     , adate" +
      "                                                                     , edate" +
      "                                                                     , _periodonly" +
      "                                                                     , _datebeg" +
      "                                                                     , _dateend" +
      "                                                                     , _exdays" +
      "                                                                     , reserv" +
      "                                                                     , stopsale" +
      "                                                                     , url" +
      "                                                                     , video_url" +
      "                                                                     , vouchernote" +
      "                                                                     , active" +
      "                                                                     , lasting" +
      "                                                                     , area" +
      "                                                                     , logoid" +
      "                                                                     , _agerequired" +
      "                                                                     , reqparams"+
      "                                                                     , guide" +
      "                                                                     , food" +
      "                                                                     , entryfees )" +

      "                                                           VALUES   (  @exname -- name - varchar(64) \n" +
      "                                                                     , @lname -- lname - varchar(64) \n" +
      "                                                                     , ''-- duration - varchar(64)\n" +
      "                                                                     , -2147483647 -- region - int\n" +
      "                                                                     , 0-- horder - bit\n" +
      "                                                                     , 0-- distance - money\n" +
      "                                                                     , 0-- maxinfage - int\n" +
      "                                                                     , 0-- maxchildage - int\n" +
      "                                                                     , @note -- note - varchar(255)\n" +
      "                                                                     , 1-- freeexcurs - bit NOT NULL\n" +
      "                                                                     , DEFAULT-- voucher - bit NOT NULL\n" +
      "                                                                     , DEFAULT-- roomnum - bit NOT NULL\n" +
      "                                                                     , DEFAULT-- people - bit NOT NULL\n" +
      "                                                                     , DEFAULT-- _individual - bit NOT NULL\n" +
      "                                                                     , @route -- route - varchar(255)\n" +
      "                                                                     , ''-- skey - varchar(8)\n" +
      "                                                                     , @excurstype-- excurstype - int NOT NULL\n" +
      "                                                                     , @partner-- partner - int\n" +
      "                                                                     , DEFAULT-- _withoutplan - bit NOT NULL\n" +
      "                                                                     , 9 -- author - smallint NOT NULL\n" +
      "                                                                     , 9 -- editor - smallint NOT NULL\n" +
      "                                                                     , DEFAULT-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- adate - datetime\n" +
      "                                                                     , DEFAULT-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- edate - datetime\n" +
      "                                                                     , DEFAULT-- _periodonly - bit NOT NULL\n" +
      "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- _datebeg - datetime\n" +
      "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- _dateend - datetime\n" +
      "                                                                     , ''-- _exdays - char(7)\n" +
      "                                                                     , DEFAULT-- reserv - bit NOT NULL\n" +
      "                                                                     , 0-- stopsale - int\n" +
      "                                                                     , ''-- url - varchar(255)\n" +
      "                                                                     , @videourl-- url - varchar(255)\n" +
      "                                                                     , ''-- vouchernote - varchar(1024)\n" +
      "                                                                     , 0 -- active - bit NOT NULL\n" +
      "                                                                     , NULL-- 'YYYY-MM-DD hh:mm:ss'-- lasting - smalldatetime\n" +
      "                                                                     , NULL-- area - int\n" +
      "                                                                     , 0-- logoid - int\n" +
      "                                                                     , DEFAULT-- _agerequired - bit NOT NULL\n" +
      "                                                                    , ''-- reqparams - xml\n" +
      "                                                                    , @guide-- guide - int\n" +
      "                                                                    , @food-- food - bit\n" +
      "                                                                    , @entryfees-- entryfees - bit\n" +
      "                                                                    ); SELECT SCOPE_IDENTITY()";
                #endregion

                 var note = "";// string.Format("{0}\ncom:{1}", (string.IsNullOrEmpty(data.exc_region_name) ? "" : "newReg:" + data.exc_region_name), data.exc_comis);

                 if (note.Length > 255)
                    note = note.Substring(0, 255);

                 var res = DatabaseOperationProvider.Query(   query, 
                                                              "excurs",
                                                              new
                                                              {
                                                                  exname = (!string.IsNullOrEmpty(data.ru_name) ? data.ru_name : data.en_name),
                                                                  lname = data.en_name,
                                                                  note = note,
                                                                  route = data.exc_en_route,
                                                                  partner = provider,
                                                                  excurstype = (data.exc_type.Value == 4) ? 3 : 2,
                                                                  guide = (data.guide.HasValue) ? data.guide.Value : 0,
                                                                  food = (data.food.HasValue) ? data.food.Value : 0,
                                                                  entryfees = (data.entryfees.HasValue) ? data.entryfees.Value : 0,
                                                                  videourl = data.exc_video

                                                              });
                #endregion

                int newId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                #region categories 
                query = "INSERT INTO dbo.excatlist (excurs, excurscategory) VALUES(@excurs, @excurscategory)";

                DatabaseOperationProvider.Query(query, "exc_cat", new { excurs = newId, excurscategory = data.exc_cat.Value });
                DatabaseOperationProvider.Query(query, "exc_cat", new { excurs = newId, excurscategory = data.exc_type.Value });
                #endregion

                #region 2 - description 

                query = "INSERT INTO dbo.exdsc(excurs, tree, sorder, description) " +
                            "VALUES (@excurs, 2, 1, @description); SELECT SCOPE_IDENTITY()";

                    res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = newId, description = (data.lang == "ru" ? data.exc_ru_details : "") });

                    int descId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                #endregion 

                #region 2 - langDescription 
                query =  "INSERT INTO dbo.exdsclang(  exdsc , lang , description , changed) "+
                         "VALUES( @exdsc , @lang , @description , 0)";


                DatabaseOperationProvider.Query(query, "exc_cat", new { exdsc = descId,
                                                                        description = data.exc_en_details ,
                                                                        lang = 1
                                                                        });

                if(data.lang != "ru")
                    DatabaseOperationProvider.Query(query, "exc_cat", new
                    {
                        exdsc = descId,
                        description = data.exc_ru_details,
                        lang = GetLanguageId(data.lang)
                    });
                #endregion

                #region 6  - cancel

                query = "INSERT INTO dbo.exdsc(excurs, tree, sorder, description) " +
                            "VALUES (@excurs, 6, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = newId, description = (data.lang == "ru" ?  data.exc_ru_cancelations : "") });

                int cancId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                #endregion 

                #region 6 - langCancel
                query = "INSERT INTO dbo.exdsclang(  exdsc , lang , description , changed) " +
                         "VALUES( @exdsc , @lang , @description , 0)";


                DatabaseOperationProvider.Query(query, "exc_cat", new
                {
                    exdsc = cancId,
                    description = data.exc_en_cancelations,
                    lang = 1
                });

                if (data.lang != "ru")
                    DatabaseOperationProvider.Query(query, "exc_cat", new
                    {
                        exdsc = cancId,
                        description =  data.exc_ru_cancelations,
                        lang = GetLanguageId(data.lang)
                    });
                #endregion

                #region 3  - route

                query = "INSERT INTO dbo.exdsc(excurs, tree, sorder, description) " +
                            "VALUES (@excurs, 3, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = newId, description = (data.lang == "ru" ? data.exc_ru_routetext : "") });

                int routeId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                #endregion 

                #region 3 - langRoute
                query = "INSERT INTO dbo.exdsclang(  exdsc , lang , description , changed) " +
                         "VALUES( @exdsc , @lang , @description , 0)";


                DatabaseOperationProvider.Query(query, "exc_cat", new
                {
                    exdsc = routeId,
                    description = data.exc_en_routetext,
                    lang = 1
                });

                if (data.lang != "ru")
                    DatabaseOperationProvider.Query(query, "exc_cat", new
                    {
                        exdsc = routeId,
                        description = data.exc_ru_routetext,
                        lang = GetLanguageId(data.lang)
                    });
                #endregion

                #region 5  - special

                query = "INSERT INTO dbo.exdsc(excurs, tree, sorder, description) " +
                            "VALUES (@excurs, 5, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = newId, description = (data.lang == "ru" ? data.exc_ru_stuff : "") });

                int specId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                #endregion 

                #region 5 - langSpecial
                query = "INSERT INTO dbo.exdsclang(  exdsc , lang , description , changed) " +
                         "VALUES( @exdsc , @lang , @description , 0)";


                DatabaseOperationProvider.Query(query, "exc_cat", new
                {
                    exdsc = specId,
                    description = data.exc_en_stuff,
                    lang = 1
                });

                if (data.lang != "ru")
                    DatabaseOperationProvider.Query(query, "exc_cat", new
                    {
                        exdsc = specId,
                        description = data.exc_ru_stuff,
                        lang = GetLanguageId(data.lang)
                    });
                #endregion

                #region photos
                query = " INSERT INTO dbo.excurspicture ( excurs , sorder , description , image , width , height ) "+
                        " VALUES(  @excurs, @sortOrder , '', @image, 0, 0)";

                if (data.photos != null)
                {
                    int cnt = 0;
                    foreach (string fileName in data.photos)
                    {
                        DatabaseOperationProvider.Query(query, "exc_cat", new
                        {
                            excurs = newId,
                            sortOrder = cnt++,
                            image = GetImageByName(fileName)
                        });
                    }

                    foreach (string fileName in data.photos)
                        DeleteImage(fileName);
                }
                #endregion

                #region prices

                for (var i = 0; i < data.ad_price.Length; i++) {

                    #region AddPrice
                    try
                    {
                        //получить id новой цены

                        query = "INSERT INTO dbo.exprice ( excurs , datebeg , dateend , adult , child , inf , currency, partner, " +
                                " hotel , region , grouptown , author , editor , adate , edate , language , exgrouptype , complete, " +
                                " total , groupfrom , grouptill , days , extime , grouppartner , fortourist , forpartner , isgroupsuppl, " +
                                " groupsuplfrom , groupsuplsum , saledatefrom , saledatetill ) " +
                                " VALUES (@exc, @adate, @edate, @ad_price, @ch_price, @inf_price, @currency, 2," +
                                " DEFAULT, @region, DEFAULT, 9 , 9, GETDATE(), GETDATE(), @pr_lang, @group_type, @complete, " +
                                " @total , @group_from , @group_to, @days, DEFAULT , DEFAULT , 1 , 1, DEFAULT, " +
                                " DEFAULT , DEFAULT, @sdate_from, @sdate_to); SELECT SCOPE_IDENTITY()";


                        var ad_price = data.group_type[i] == 1 ? data.ad_price[i] : 0;
                        var ch_price = data.group_type[i] == 1 ? data.ch_price[i] : 0;
                        var inf_price = data.group_type[i] == 1 ? data.inf_price[i] : 0;
                        var total = data.group_type[i] != 1 ? data.total[i] : 0;


                        res = DatabaseOperationProvider.Query(query, "exc_cat", new
                        {
                            region = data.price_region[i] > 0 ? data.price_region[i] : -2147483647,
                            exc = newId,
                            ad_price = ad_price,
                            ch_price = ch_price,
                            inf_price = inf_price,
                            total = total,
                            group_from = data.group_from[i],
                            group_to = data.group_to[i],
                            group_type = data.group_type[i],
                            currency = data.currency[i],
                            pr_lang = data.pr_lang[i],
                            adate = data.adate[i],
                            edate = data.edate[i],
                            days = data.days[i],
                            sdate_from = data.sdate_from[i],
                            sdate_to = data.sdate_to[i],
                            complete = data.group_type[i] == 2 ? 1 : 0
                        });

                        newId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                        //рассчитать нетто и брутто 
                        string insQuery = "insert into  "+
                                          " dbo.exprice_ext (price_inc, adult_netto, adult_brutto, child_netto, child_brutto, inf_netto, inf_brutto, total_netto, total_brutto) "+
                                          " values(@newid, @adult_netto, @adult_brutto, @child_netto, @child_brutto, @inf_netto, @inf_brutto, @total_netto, @total_brutto)";

                        //рассчитать коеффициент
                        var comis = 0;
                        if (data.comission_type[i] == 1)
                            comis = data.price_comis[i];

                        decimal coef = (100.0m - comis) / 100.0m;

                        DatabaseOperationProvider.Query(insQuery, "price_ext", new
                        {
                            newid = newId,
                            adult_netto = data.comission_type[i] == 1 ? Math.Round(coef * ad_price, 2): Convert.ToDecimal(data.netto_ad_price[i]),
                            adult_brutto = ad_price,

                            child_netto = data.comission_type[i] == 1 ? Math.Round(coef * ch_price, 2) : Convert.ToDecimal(data.netto_ch_price[i]),
                            child_brutto = ch_price,

                            inf_netto = data.comission_type[i] == 1 ? Math.Round(coef * inf_price, 2) : Convert.ToDecimal(data.netto_inf_price[i]),
                            inf_brutto = inf_price,

                            total_netto = data.comission_type[i] == 1 ? Math.Round(coef * total, 2) : Convert.ToDecimal(data.netto_total[i]),
                            total_brutto = total
                        });


                        //внести их
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion

                    #region Add exdetplan

                    try
                    {
                        query = "INSERT INTO dbo.exdetplan ( excurs , datebeg , dateend , exdays , author , editor , adate , edate " +
                                ", language , exgrouptype , stopsale ,  region " +
                                ",  starttime ,  customroute ) " +
                                " VALUES ( @exc , @adate , @edate , @days, 9 , 9 , GETDATE() , GETDATE() "+
                                " , @pr_lang , @group_type , -360 , @region " +
                                " ,  GETDATE() ,  DEFAULT)";

                        DatabaseOperationProvider.Query(query, "exc_cat", new
                        {
                            exc = newId,
                            group_type = data.group_type[i],
                            pr_lang = data.pr_lang[i],
                            adate = data.adate[i],
                            edate = data.edate[i],
                            days = data.days[i],
                            region = (data.price_region[i] > 0 ? (int?)data.price_region[i] : null)
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                }

                #endregion

                #region sendMessages
           
                var exName = new GuestService.Data.AddExcursionData() { ExcursionName = data.en_name };

                var service = new SimpleEmailService();

                service.SendEmail<GuestService.Data.AddExcursionData>(WebSecurity.CurrentUserName, 
                                                                        "addexcursion", 
                                                                        "en", 
                                                                        exName, 
                                                                        false, 
                                                                        null, ConfigurationManager.AppSettings["edit_excursion_email"]);
                #endregion
                return newId;
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw ex;
            }

            return 0;
        }
        
        private static int UpdateExcursion(UpdateExcursion data, int provider, Dictionary<string,string> langs, Dictionary<string, string> routes, Dictionary<string, string> stuffs, Dictionary<string, string> cancels)
        {
            try
            {
                //общая часть
                //обновить цены в живой базе
                //обновить расписания
                InsertLifePrices(data);

                //если редактирутся копия
                if (data.ex_copy_id.HasValue && data.ex_copy_id.Value > 0)
                {
                    //обновляем dbo.excurs_temp
                    #region UpdateExcursionCopy
                    #region Update query
                    var updateQuery = " update dbo.excurs_temp set " +
          "                                                                       name  = @exname" +
          "                                                                     , lname = @lname" +

          "                                                                     , note = @note" +
          "                                                                     , route = @route" +
          "                                                                     , excurstype = @excurstype" +
          "                                                                     , partner = @partner" +
          "                                                                     , author = 9" +
          "                                                                     , editor = 9" +
          "                                                                     , freeexcurs = 1" +
          "                                                                     , guide = @guide" +

          "                                                                     , food = @food" +

          "                                                                     , entryfees = @entryfees" +

          "                                                                     , edate = GETDATE()" +

          "                                                           Where inc = @exc_id";
                    #endregion

                    var note = "";// "newReg:" + data.exc_region_name;

                   // if (note.Length > 255)
                   //     note = note.Substring(0, 255);

                    var insertRes = DatabaseOperationProvider.Query(updateQuery,
                                                                 "excurs",
                                                                 new
                                                                 {
                                                                     exname = (!string.IsNullOrEmpty(data.ru_name) ? data.ru_name : data.en_name),
                                                                     lname = data.en_name,
                                                                     note = note,
                                                                     route = data.exc_en_route,
                                                                     partner = provider,
                                                                     excurstype = (data.exc_type.Value == 4) ? 3 : 2,
                                                                     exc_id = data.ex_copy_id.Value,
                                                                     guide = (data.guide.HasValue) ? data.guide.Value : 0,
                                                                     food = (data.food.HasValue) ? data.food.Value : 0,
                                                                     entryfees = (data.entryfees.HasValue) ? data.entryfees.Value : 0
                                                                 });
                    #endregion

                    //удаляем старые descriptions + lang descriptions
                    var deleteQuery = "delete from dbo.exdsclang_temp where exdsc in (select inc from exdsc_temp where excurs = @excurs)";

                    DatabaseOperationProvider.Query(deleteQuery, "exc_desc_lang", new { excurs = data.ex_copy_id.Value });

                    deleteQuery = "delete from exdsc_temp where excurs = @excurs";

                    DatabaseOperationProvider.Query(deleteQuery, "exc_desc", new { excurs = data.ex_copy_id.Value });

                    //удаляем старые типы
                    var deleteCatsQuery = "delete from dbo.excatlist_temp where excurs = @excurs";

                    DatabaseOperationProvider.Query(deleteCatsQuery, "exc_cat", new { excurs = data.ex_copy_id.Value});

                }
                else //иначе
                {
                    //добавляем копию
                    #region AddExcursionCopy
                    #region Insert query
                    var insertQuery = "delete from  dbo.excurs_temp where excurs_id =  @exc_id ; "+
          "                                                 INSERT INTO dbo.excurs_temp( " +
          "                                                                       name " +
          "                                                                     , lname " +
          "                                                                     , duration " +

          "                                                                     , horder " +
          "                                                                     , distance " +
          "                                                                     , maxinfage " +
          "                                                                     , maxchildage " +
          "                                                                     , note " +
          "                                                                     , freeexcurs" +
          "                                                                     , voucher" +
          "                                                                     , roomnum" +
          "                                                                     , people" +
          "                                                                     , _individual" +
          "                                                                     , route" +
          "                                                                     , skey" +
          "                                                                     , excurstype" +
          "                                                                     , partner" +
          "                                                                     , _withoutplan" +
          "                                                                     , author" +
          "                                                                     , editor" +
          "                                                                     , adate" +
          "                                                                     , edate" +
          "                                                                     , _periodonly" +
          "                                                                     , _datebeg" +
          "                                                                     , _dateend" +
          "                                                                     , _exdays" +
          "                                                                     , reserv" +
          "                                                                     , stopsale" +
          "                                                                     , url" +
          "                                                                     , video_url" +
          "                                                                     , vouchernote" +
          "                                                                     , active" +
          "                                                                     , lasting" +
          "                                                                     , area" +
          "                                                                     , logoid" +
          "                                                                     , _agerequired" +
          "                                                                     , reqparams"+
          "                                                                     , guide" +
          "                                                                     , food" +
          "                                                                     , entryfees " +
          "                                                                     , excurs_id)" +

          "                                                           VALUES   ( @exname -- name - varchar(64) \n" +
          "                                                                     , @lname -- lname - varchar(64) \n" +
          "                                                                     , ''-- duration - varchar(64)\n" +

          "                                                                     , 0-- horder - bit\n" +
          "                                                                     , 0-- distance - money\n" +
          "                                                                     , 0-- maxinfage - int\n" +
          "                                                                     , 0-- maxchildage - int\n" +
          "                                                                     , @note -- note - varchar(255)\n" +
          "                                                                     , 1-- freeexcurs - bit NOT NULL\n" +
          "                                                                     , DEFAULT-- voucher - bit NOT NULL\n" +
          "                                                                     , DEFAULT-- roomnum - bit NOT NULL\n" +
          "                                                                     , DEFAULT-- people - bit NOT NULL\n" +
          "                                                                     , DEFAULT-- _individual - bit NOT NULL\n" +
          "                                                                     , @route -- route - varchar(255)\n" +
          "                                                                     , ''-- skey - varchar(8)\n" +
          "                                                                     , @excurstype-- excurstype - int NOT NULL\n" +
          "                                                                     , @partner-- partner - int\n" +
          "                                                                     , DEFAULT-- _withoutplan - bit NOT NULL\n" +
          "                                                                     , 9 -- author - smallint NOT NULL\n" +
          "                                                                     , 9 -- editor - smallint NOT NULL\n" +
          "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- adate - datetime\n" +
          "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- edate - datetime\n" +
          "                                                                     , DEFAULT-- _periodonly - bit NOT NULL\n" +
          "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- _datebeg - datetime\n" +
          "                                                                     , GETDATE()-- 'YYYY-MM-DD hh:mm:ss[.nnn]'-- _dateend - datetime\n" +
          "                                                                     , ''-- _exdays - char(7)\n" +
          "                                                                     , DEFAULT-- reserv - bit NOT NULL\n" +
          "                                                                     , 0-- stopsale - int\n" +
          "                                                                     , ''-- url - varchar(255)\n" +
          "                                                                     , @videourl-- url - varchar(255)\n" +
          "                                                                     , ''-- vouchernote - varchar(1024)\n" +
          "                                                                     , 0 -- active - bit NOT NULL\n" +
          "                                                                     , NULL-- 'YYYY-MM-DD hh:mm:ss'-- lasting - smalldatetime\n" +
          "                                                                     , NULL-- area - int\n" +
          "                                                                     , 0-- logoid - int\n" +
          "                                                                     , DEFAULT-- _agerequired - bit NOT NULL\n" +
          "                                                                     , ''-- reqparams - xml\n" +
          "                                                                     , @guide " +
          "                                                                     , @food " +
          "                                                                     , @entryfees " +
          "                                                                     , @exc_id " +
          "                                                                    ); SELECT SCOPE_IDENTITY()";
                    #endregion

                    var note = "newReg:" + data.exc_region_name;

                    if (note.Length > 255)
                        note = note.Substring(0, 255);

                    var insertRes = DatabaseOperationProvider.Query(insertQuery,
                                                                 "excurs",
                                                                 new
                                                                 {
                                                                     exname = (!string.IsNullOrEmpty(data.ru_name) ? data.ru_name : data.en_name),
                                                                     lname = data.en_name,
                                                                     note = note,
                                                                     route = data.exc_en_route,
                                                                     partner = provider,
                                                                     excurstype = (data.exc_type.Value == 4) ? 3 : 2,
                                                                     exc_id = data.ex_id.Value,
                                                                     guide = (data.guide.HasValue) ? data.guide.Value : 0,
                                                                     food = (data.food.HasValue) ? data.food.Value : 0,
                                                                     entryfees = (data.entryfees.HasValue) ? data.entryfees.Value : 0,
                                                                     videourl = data.exc_video
                                                                 });
                    #endregion

                    data.ex_copy_id = Convert.ToInt32(insertRes.Tables[0].Rows[0][0]);
                }

                //добавляем типы
                #region categories 
                var query = "INSERT INTO dbo.excatlist_temp (excurs, excurscategory) VALUES(@excurs, @excurscategory)";

                DatabaseOperationProvider.Query(query, "exc_cat", new { excurs = data.ex_copy_id.Value, excurscategory = data.exc_cat.Value });
                DatabaseOperationProvider.Query(query, "exc_type", new { excurs = data.ex_copy_id.Value, excurscategory = data.exc_type.Value });
                #endregion

                //добавляем описания
                #region description 
                query = "INSERT INTO dbo.exdsc_temp(excurs, tree, sorder, description) " +
                        "VALUES (@excurs, 2, 1, @description); SELECT SCOPE_IDENTITY()";

                var res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = data.ex_copy_id.Value, description = langs.ContainsKey("2")? langs["2"] : ""});

                int descId = Convert.ToInt32(res.Tables[0].Rows[0][0]);
                #endregion
                #region langDescription 

                foreach (string langId in langs.Keys)
                {
                    query = "INSERT INTO dbo.exdsclang_temp(  exdsc , lang , description , changed) " +
                             "VALUES( @exdsc , @lang , @description , 0)";

                    DatabaseOperationProvider.Query(query, "exc_desc", new
                    {
                        exdsc = descId,
                        description = langs[langId],
                        lang = Convert.ToInt32(langId)
                    });
                }
                #endregion

                //добавляем описания
                #region routes 
                query = "INSERT INTO dbo.exdsc_temp(excurs, tree, sorder, description) " +
                        "VALUES (@excurs, 3, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = data.ex_copy_id.Value, description = routes.ContainsKey("2") ? routes["2"] : "" });

                descId = Convert.ToInt32(res.Tables[0].Rows[0][0]);
                #endregion
                #region langDescription 

                foreach (string langId in routes.Keys)
                {
                    query = "INSERT INTO dbo.exdsclang_temp(  exdsc , lang , description , changed) " +
                             "VALUES( @exdsc , @lang , @description , 0)";

                    DatabaseOperationProvider.Query(query, "exc_desc", new
                    {
                        exdsc = descId,
                        description = routes[langId],
                        lang = Convert.ToInt32(langId)
                    });
                }
                #endregion

                //добавляем описания
                #region cancels 
                query = "INSERT INTO dbo.exdsc_temp(excurs, tree, sorder, description) " +
                        "VALUES (@excurs, 6, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = data.ex_copy_id.Value, description = cancels.ContainsKey("2") ? cancels["2"] : "" });

                descId = Convert.ToInt32(res.Tables[0].Rows[0][0]);
                #endregion
                #region langCancels 

                foreach (string langId in cancels.Keys)
                {
                    query = "INSERT INTO dbo.exdsclang_temp(  exdsc , lang , description , changed) " +
                             "VALUES( @exdsc , @lang , @description , 0)";

                    DatabaseOperationProvider.Query(query, "exc_desc", new
                    {
                        exdsc = descId,
                        description = cancels[langId],
                        lang = Convert.ToInt32(langId)
                    });
                }
                #endregion

                //добавляем описания
                #region stuffs 
                query = "INSERT INTO dbo.exdsc_temp(excurs, tree, sorder, description) " +
                        "VALUES (@excurs, 5, 1, @description); SELECT SCOPE_IDENTITY()";

                res = DatabaseOperationProvider.Query(query, "exc_desc", new { excurs = data.ex_copy_id.Value, description = stuffs.ContainsKey("2") ? cancels["2"] : "" });

                descId = Convert.ToInt32(res.Tables[0].Rows[0][0]);
                #endregion
                #region langStuffs

                foreach (string langId in stuffs.Keys)
                {
                    query = "INSERT INTO dbo.exdsclang_temp(  exdsc , lang , description , changed) " +
                             "VALUES( @exdsc , @lang , @description , 0)";

                    DatabaseOperationProvider.Query(query, "exc_desc", new
                    {
                        exdsc = descId,
                        description = stuffs[langId],
                        lang = Convert.ToInt32(langId)
                    });
                }
                #endregion

                //для копии добавляем фото
                #region photos
                query = " INSERT INTO dbo.excurspicture_temp ( excurs, sorder , description , image , width , height ) " +
                        " VALUES(  @excurs, @sortOrder , '', @image, 0, 0)";

                if (data.photos != null)
                {
                    int cnt = 0;
                    foreach (string fileName in data.photos)
                    {
                        DatabaseOperationProvider.Query(query, "exc_pict", new
                        {
                            excurs = data.ex_copy_id.Value,
                            sortOrder = cnt++,
                            image = GetImageByName(fileName)
                        });
                    }

                    foreach (string fileName in data.photos)
                        DeleteImage(fileName);
                }
                #endregion

                #region sendMessages
                var exName = new GuestService.Data.AddExcursionData() { ExcursionName = data.en_name };

                var service = new SimpleEmailService();

                service.SendEmail<GuestService.Data.AddExcursionData>(WebSecurity.CurrentUserName, 
                                                                    "editexcursion", 
                                                                    "en", 
                                                                    exName, 
                                                                    false, 
                                                                    null,
                                                                    ConfigurationManager.AppSettings["edit_excursion_email"]);
                #endregion
                return data.ex_copy_id.Value;
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw ex;
            }
        }

        private static void InsertLifePrices(UpdateExcursion data)
        {
            var query = "";

            #region Prices
            for (var i = 0; i < data.ad_price.Length; i++)
            {

                #region AddPrice
                var ad_price = data.group_type[i] == 1 ? data.ad_price[i] : 0;
                var ch_price = data.group_type[i] == 1 ? data.ch_price[i] : 0;
                var inf_price = data.group_type[i] == 1 ? data.inf_price[i] : 0;
                var total = data.group_type[i] != 1 ? data.total[i] : 0;

                try
                {
                    query = "INSERT INTO dbo.exprice ( excurs , datebeg , dateend , adult , child , inf , currency, partner, " +
                            " hotel , region , grouptown , author , editor , adate , edate , language , exgrouptype , complete, " +
                            " total , groupfrom , grouptill , days , extime , grouppartner , fortourist , forpartner , isgroupsuppl, " +
                            " groupsuplfrom , groupsuplsum , saledatefrom , saledatetill ) " +
                            " VALUES (@exc, @adate, @edate, @ad_price, @ch_price, @inf_price, @currency, 2," +
                            " DEFAULT, @region, DEFAULT, 9 , 9, GETDATE(), GETDATE(), @pr_lang, @group_type, @complete, " +
                            " @total , @group_from , @group_to, @days, DEFAULT , DEFAULT , 1 , 1, DEFAULT, " +
                            " DEFAULT , DEFAULT, @sdate_from, @sdate_to); SELECT SCOPE_IDENTITY() ";

                    var res = DatabaseOperationProvider.Query(query, "exc_price", new
                            {
                                region = data.price_region[i] > 0 ? data.price_region[i] : -2147483647,
                                exc = data.ex_id.Value,
                                ad_price = ad_price,
                                ch_price = ch_price,
                                inf_price = inf_price,
                                total = total,
                                group_from = data.group_from[i],
                                group_to = data.group_to[i],
                                group_type = data.group_type[i],
                                currency = data.currency[i],
                                pr_lang = data.pr_lang[i],
                                adate = data.adate[i],
                                edate = data.edate[i],
                                days = data.days[i],
                                sdate_from = data.sdate_from[i],
                                sdate_to = data.sdate_to[i],
                                complete = data.group_type[i] == 2 ? 1 : 0
                            });


                    var newId = Convert.ToInt32(res.Tables[0].Rows[0][0]);

                    //рассчитать нетто и брутто 
                    string insQuery = "insert into  " +
                                      " dbo.exprice_ext (price_inc, adult_netto, adult_brutto, child_netto, child_brutto, inf_netto, inf_brutto, total_netto, total_brutto) " +
                                      " values(@newid, @adult_netto, @adult_brutto, @child_netto, @child_brutto, @inf_netto, @inf_brutto, @total_netto, @total_brutto)";

                    //рассчитать коеффициент
                    var comis = 0;
                    if (data.comission_type[i] == 1)
                        comis = data.price_comis[i];

                    decimal coef = (100.0m - comis) / 100.0m;

                    DatabaseOperationProvider.Query(insQuery, "price_ext", new
                    {
                        newid = newId,
                        adult_netto = data.comission_type[i] == 1 ? Math.Round(coef * ad_price, 2) : Convert.ToDecimal(data.netto_ad_price[i]),
                        adult_brutto = ad_price,

                        child_netto = data.comission_type[i] == 1 ? Math.Round(coef * ch_price, 2) : Convert.ToDecimal(data.netto_ch_price[i]),
                        child_brutto = ch_price,

                        inf_netto = data.comission_type[i] == 1 ? Math.Round(coef * inf_price, 2) : Convert.ToDecimal(data.netto_inf_price[i]),
                        inf_brutto = inf_price,

                        total_netto = data.comission_type[i] == 1 ? Math.Round(coef * total, 2) : Convert.ToDecimal(data.netto_total[i]),
                        total_brutto = total
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                #endregion

                #region Add exdetplan

                try
                {
                    query = "INSERT INTO dbo.exdetplan ( excurs, datebeg, dateend, exdays, author, editor, adate, edate " +
                            " , language, exgrouptype, stopsale,  region " +
                            " , starttime, customroute ) " +
                            " VALUES ( @exc , @adate , @edate , @days, 9 , 9 , GETDATE() , GETDATE() " +
                            " , @pr_lang , @group_type , -360 , @region " +
                            " ,  GETDATE() ,  DEFAULT)";

                    DatabaseOperationProvider.Query(query, "exc_cat", new
                    {
                        exc = data.ex_id.Value,
                        group_type = data.group_type[i],
                        pr_lang = data.pr_lang[i],
                        adate = data.adate[i],
                        edate = data.edate[i],
                        days = data.days[i],
                        region = (data.price_region[i] > 0 ? (int?)data.price_region[i] : null)
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                #endregion
            }
            #endregion
        }

        private static void DeleteImage(string fileName)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/MediaUploader_small/") + fileName;

            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch (Exception ex) { }

            filePath = HttpContext.Current.Server.MapPath("~/MediaUploader/") + fileName;

            try
            { 
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch (Exception ex) { }
        }

        private static byte[] GetImageByName(string fileName) {

            var res = new byte[0];
                       
            var filePath = HttpContext.Current.Server.MapPath("~/MediaUploader_small/") + fileName;

            if (File.Exists(filePath))
                res = File.ReadAllBytes(filePath);

            return res;
        }

        public static int GetLanguageId(string code)
        {
            var res = DatabaseOperationProvider.Query("select inc from language where alias = @lang", "lang", new { lang = code });

            if (res.Tables[0].Rows.Count > 0)
                return res.Tables[0].Rows[0].ReadNullableInt("inc").Value;
            else
                return 0;
        }
        
        [HttpPost, HttpGet, ActionName("addexcursion")]
        public System.Web.Mvc.JsonResult AddExcursion([FromUri] AddExcursion data)
        {
            System.Web.Mvc.JsonResult result = new System.Web.Mvc.JsonResult();

            try
            {
                var providerId = GetProviderId(WebSecurity.CurrentUserId);

                result.Data = new { id = CreateNewExcursion(data, providerId) };
            }
            catch (Exception ex)
            {
                result.Data = new { error = ex.Message};
            }

            return result;
        }

        [HttpPost, HttpGet, ActionName("editexcursion")]
        public System.Web.Mvc.JsonResult EditExcursion([FromBody] UpdateExcursion data)
        {
            System.Web.Mvc.JsonResult result = new System.Web.Mvc.JsonResult();

            try
            {
                var providerId = GetProviderId(WebSecurity.CurrentUserId);

                var req = HttpContext.Current.Request;

                var descriptions = new Dictionary<string, string>();
                var routes = new Dictionary<string, string>();
                var cancells = new Dictionary<string, string>();
                var stuffs = new Dictionary<string, string>();

                var langs = PartnerProvider.GetPartnerLangs();

                foreach (var langItem in langs)
                {
                    if (req["exc_" + langItem.Key + "_details"] != null)
                        descriptions.Add(langItem.Key, req["exc_" + langItem.Key + "_details"]);

                    if (req["exc_" + langItem.Key + "_routes"] != null)
                        routes.Add(langItem.Key, req["exc_" + langItem.Key + "_routes"]);

                    if (req["exc_" + langItem.Key + "_cancels"] != null)
                        cancells.Add(langItem.Key, req["exc_" + langItem.Key + "_cancels"]);

                    if (req["exc_" + langItem.Key + "_stuffs"] != null)
                        stuffs.Add(langItem.Key, req["exc_" + langItem.Key + "_stuffs"]);
                }

                UpdateExcursion(data, providerId, descriptions, routes, stuffs, cancells);

                result.Data = new { id = data.ex_id.Value };
            }
            catch (Exception ex)
            {
                result.Data = new { error = ex.Message };
            }

            return result;
        }

        [ActionName("categories"), HttpGet]
        public CategoryWithGroupList Categories([FromUri] CategoryParam param)
        {
            if (!WebSecurity.IsAuthenticated)
                throw new System.ArgumentNullException("auth");

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }

            var res = new CategoryWithGroupList(ExcursionProvider.GetCategories(param.Language, param.StartPoint));

            return res;
        }
        [ActionName("categoriesbygroup"), HttpGet]
        public CategoryGroupWithCategoriesList CategoriesByGroup([FromUri] CategoryParam param)
        {
            if (!WebSecurity.IsAuthenticated)
                throw new System.ArgumentNullException("auth");

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }

            var res =  new CategoryGroupWithCategoriesList(ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint /*int provider*/));
            
            //FILTER BY PROVIDER
            return res;// new CategoryGroupWithCategoriesList(ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint /*int provider*/));

        }

        [ActionName("destinationsandcategorygroups"), HttpGet]
        public DestinationsAndCategoryGroupsResult DestinationsAndCategoryGroups([FromUri] DestinationAndCategoryParam param)
        {
            if (!WebSecurity.IsAuthenticated)
                throw new System.ArgumentNullException("auth");

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            DestinationsAndCategoryGroupsResult result = new DestinationsAndCategoryGroupsResult();
            FilterDetailsResult filterDetails = PartnerExcursionController.GetCachedFilterDetails(param, partner);
            result.destinationstates = filterDetails.destinationstates;

            result.categorygroups = ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint /*int provider*/);

            return result;
        }
        [ActionName("departures"), HttpGet]
        public DeparturesResult Departures([FromUri] DepartureParam param)
        {
            if (!WebSecurity.IsAuthenticated)
                throw new System.ArgumentNullException("auth");

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            string departureResultCacheKey = string.Format("cache_departuresResult[ln:{0}][p:{1}][sp:{2}][st:{3}]", new object[]
            {
                param.Language,
                partner.id,
                param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
                param.DestinationState.HasValue ? param.DestinationState.ToString() : "-"
            });
            DeparturesResult result = HttpContext.Current.Cache[departureResultCacheKey] as DeparturesResult;
            if (result == null || Settings.IsCacheDisabled)
            {
                System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[]
                {
                    param.DestinationState.Value
                } : null, null, null, null, null);
                result = new DeparturesResult();

                //FILTER BY PROVIDER
                excursions = FilterExcursions(excursions, GetProviderId(userId));

                result.departures = ExcursionProvider.BuildDepartureList(excursions);
                HttpContext.Current.Cache.Add(departureResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }
        [ActionName("categoryimage"), HttpGet]
        public HttpResponseMessage CategoryImage(int id, [FromUri] ImageParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            HttpResponseMessage response = new HttpResponseMessage();
            string imageCacheKey = string.Format("categoryImage[id:{0}][w:{1}][h:{2}][m:{3}][d:{4}]", new object[]
            {
                id,
                param.h,
                param.w,
                param.Mode,
                param.ShowDefault ?? true
            });
            ImageCacheItem cacheResult = null;
            if (cacheResult == null || Settings.IsCacheDisabled)
            {
                Image image = ExcursionProvider.GetCategoryImage(id);
                if (image == null && param.ShowDefault == false)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    ImageFormatter formatter = new ImageFormatter(image, Pictures.nophoto);
                    formatter.Format = ((image != null) ? ImageFormat.Jpeg : ImageFormat.Png);
                    param.ApplyFormat(formatter);
                    System.IO.Stream stream = formatter.CreateStream();
                    if (stream != null)
                    {
                        cacheResult = ImageCacheItem.Create(stream, formatter.MediaType);
                        HttpContext.Current.Cache.Add(imageCacheKey, cacheResult, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        response.Content = new StreamContent(stream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
                        response.Headers.CacheControl = new CacheControlHeaderValue();
                        response.Headers.CacheControl.Public = true;
                        response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            else
            {
                response.Content = new StreamContent(cacheResult.CraeteStream());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(cacheResult.MediaType);
            }
            response.Headers.CacheControl = new CacheControlHeaderValue();
            response.Headers.CacheControl.Public = true;
            response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
            return response;
        }

        [HttpPost, HttpGet, ActionName("removeimage")]
        public string RemoveFile()
        {
            string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader_small/");

            System.IO.File.Delete(dirFullPath + HttpContext.Current.Request["id"]);

            dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");

            if (System.IO.File.Exists(dirFullPath + HttpContext.Current.Request["id"]))
                System.IO.File.Delete(dirFullPath + HttpContext.Current.Request["id"]);

            return "success";
        }

        [HttpPost, HttpGet, ActionName("uploadimage")]
        public string UploadFile()
        {
            var str_image = "";
            try
            {
                HttpContext.Current.Response.ContentType = "text/plain";

                string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;


                ImageHandler iha = new ImageHandler();
                foreach (string s in HttpContext.Current.Request.Files)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[s];
                    //  int fileSizeInBytes = file.ContentLength;
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = "MyPHOTO_" + Guid.NewGuid() + fileExtension;

                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;

                        string pathToSave_small = HttpContext.Current.Server.MapPath("~/MediaUploader_small/") + str_image;

                        file.SaveAs(pathToSave_100);


                        using (var fs = new System.IO.FileStream(pathToSave_100, System.IO.FileMode.Open))
                        {
                            var bmp = new Bitmap(fs);
                            iha.Save(bmp, 2000, 10000, 50, pathToSave_small);

                            File.Delete(pathToSave_100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //HttpContext.Current.Response.Write(str_image);
            return str_image;
        }

        [HttpGet, ActionName("deleteexcursion")]
        public string DeleteExcursion(int id)
        {
            if (!WebSecurity.IsAuthenticated)
                return "not auth";

            var providerId = GetProviderId(WebSecurity.CurrentUserId);

            var query = "update dbo.excurs set name ='DELETE: ' + name, active=0, reserv=0 where partner = " + providerId + " and inc = " + id;

            DatabaseOperationProvider.Query(query, "exc_desc", new { });

            return id.ToString();
        }

        [HttpGet, ActionName("deleteprice")]
        public string DeletePrice(int id)
        {
            if (!WebSecurity.IsAuthenticated)
                return "not auth";

            var providerId = GetProviderId(WebSecurity.CurrentUserId);

            ExcursionProvider.DeleteOldPrice(id, providerId);

            return id.ToString();
        }

        [ HttpGet, ActionName("deletephoto")]
        public string DeleteExcursion(int id, bool ismain)
        {
            if (!WebSecurity.IsAuthenticated)
                return "not auth";

            var providerId = GetProviderId(WebSecurity.CurrentUserId);

            ExcursionProvider.DeleteOldPhoto(id, ismain, providerId);

            return id.ToString();
        }

        [ActionName("fullcatalog"), HttpGet]
        public CatalogResult FullCatalog([FromUri] CatalogParam param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                throw new System.ArgumentNullException("auth");
            }

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            ExcursionProvider.ExcursionSorting sorting = (!string.IsNullOrEmpty(param.SortOrder)) ? ((ExcursionProvider.ExcursionSorting)System.Enum.Parse(typeof(ExcursionProvider.ExcursionSorting), param.SortOrder)) : ExcursionProvider.ExcursionSorting.name;

            //FILTER BY PARTNER
            var excs = ExcursionProvider.FindAllExcursions(param.Language, partner.id, param.FirstDate, param.LastDate, param.SearchLimit, param.StartPoint, param.SearchText, param.Categories, param.Departures, (param.Destinations != null && param.Destinations.Length > 0) ? param.Destinations : (param.DestinationState.HasValue ? new int[]
                {
                    param.DestinationState.Value
                } : null), param.ExcursionLanguages, param.MinDuration, param.MaxDuration, new ExcursionProvider.ExcursionSorting?(sorting), true);

            //FILTER BY PARTNER
            excs = FilterExcursions(excs, GetProviderId(userId));

            return new CatalogResult
            {
                excursions = excs
            };
        }

        [ActionName("catalog"), HttpGet]
        public CatalogResult Catalog([FromUri] CatalogParam param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                throw new System.ArgumentNullException("auth");
            }

            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            ExcursionProvider.ExcursionSorting sorting = (!string.IsNullOrEmpty(param.SortOrder)) ? ((ExcursionProvider.ExcursionSorting)System.Enum.Parse(typeof(ExcursionProvider.ExcursionSorting), param.SortOrder)) : ExcursionProvider.ExcursionSorting.name;

            //FILTER BY PARTNER
            var excs = ExcursionProvider.FindExcursions(param.Language, partner.id, param.FirstDate, param.LastDate, param.SearchLimit, param.StartPoint, param.SearchText, param.Categories, param.Departures, (param.Destinations != null && param.Destinations.Length > 0) ? param.Destinations : (param.DestinationState.HasValue ? new int[]
                {
                    param.DestinationState.Value
                } : null), param.ExcursionLanguages, param.MinDuration, param.MaxDuration, new ExcursionProvider.ExcursionSorting?(sorting));

            //FILTER BY PARTNER
            excs = FilterExcursions(excs, GetProviderId(userId));

            return new CatalogResult
            {
                excursions = excs
            };
        }

        [ActionName("filters"), HttpGet]
        public FiltersResult Filters([FromUri] FiltersParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            string filtersResultCacheKey = string.Format("cache_filterResult[ln:{0}][p:{1}][sp:{2}][st:{3}]", new object[]
            {
                param.Language,
                partner.id,
                param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
                param.DestinationState.HasValue ? param.DestinationState.ToString() : "-"
            });
            FiltersResult result = HttpContext.Current.Cache[filtersResultCacheKey] as FiltersResult;
            if (result == null || Settings.IsCacheDisabled)
            {
                result = new FiltersResult();
                System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[]
                {
                    param.DestinationState.Value
                } : null, null, null, null, null);

                //FILTER EXCURSIONS BY PROVIDER
                excursions = FilterExcursions( excursions, GetProviderId(userId));

                result.categorygroups = ExcursionProvider.BuildFilterCategories(excursions, null);
                result.departures = ExcursionProvider.BuildFilterDepartures(excursions, null);
                result.destinations = ExcursionProvider.BuildFilterDestinations(excursions, null);
                result.languages = ExcursionProvider.BuildFilterLanguages(excursions, null);
                result.durations = ExcursionProvider.BuildFilterDurations(excursions);
                HttpContext.Current.Cache.Add(filtersResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }

        public static System.Collections.Generic.List<CatalogExcursionMinPrice> FilterExcursions(System.Collections.Generic.List<CatalogExcursionMinPrice>  excursions, int provider)
        {
            //получить актуальные имена и статусы
            var res = DatabaseOperationProvider.Query("select inc, lname as name, freeexcurs from excurs where partner = @partner " + 
                                                    "union select excurs_id as inc,  lname as name, freeexcurs from excurs_temp where partner = @partner ", 
                                                    "info", new { partner = provider} );

            if(UrlLanguage.CurrentLanguage == "ru")
                res = DatabaseOperationProvider.Query("select inc, name, freeexcurs from excurs where partner = @partner " +
                                                    "union select excurs_id as inc,  name, freeexcurs from excurs_temp where partner = @partner ",
                                                    "info", new { partner = provider });

            var excInfo = new Dictionary<int, KeyValuePair<string, bool>>();

            foreach (DataRow row in res.Tables[0].Rows)
                excInfo[row.ReadInt("inc")] = new KeyValuePair<string, bool>(row.ReadString("name"), row.ReadBoolean("freeexcurs"));

            System.Collections.Generic.List<CatalogExcursionMinPrice> list = new List<CatalogExcursionMinPrice>();

            foreach (CatalogExcursionMinPrice exc in excursions)
                if (provider == exc.excursion.excursionPartner.id)
                {
                    if (excInfo.ContainsKey(exc.excursion.id))
                    {
                        exc.excursion.name = excInfo[exc.excursion.id].Key;

                        if (excInfo[exc.excursion.id].Value)
                            exc.excursion.name += ExcursionStrings.Get("ChangesNotConfirmed"); // " (changes not confirmed)";
                    }

                    list.Add(exc);
                }

            return list;
        }

        public static string GetProviderName(int userId)
        {
            var res = DatabaseOperationProvider.Query("select b.name from guestservice_UserProfile as a, partner as b where a.userId = @userId and a.providerid = b.inc ", "services", new { userId = userId });

            if (res.Tables[0].Rows.Count > 0)
                return res.Tables[0].Rows[0].ReadNullableTrimmedString("name").ToString();
            else
                return "";
        }

        public static int GetProviderId(int userId)
        {
            var res = DatabaseOperationProvider.Query("select providerID from guestservice_UserProfile where userId = @userId", "services", new { userId = userId });

            if (res.Tables[0].Rows.Count > 0)
                return res.Tables[0].Rows[0].ReadNullableInt("providerID").Value;
            else
                return 0;
        }

        [ActionName("filterdetails"), HttpGet]
        public FilterDetailsResult FilterDetails([FromUri] FiltersParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            return PartnerExcursionController.GetCachedFilterDetails(param, partner);
        }
        [ActionName("catalogimage"), HttpGet]
        public HttpResponseMessage CatalogImage(int id, [FromUri] CatalogImageParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            HttpResponseMessage response = new HttpResponseMessage();
            string imageCacheKey = string.Format("catalogImage[id:{0}][w:{1}][h:{2}][i:{3}][m:{4}][d:{5}]", new object[]
            {
                id,
                param.h,
                param.w,
                param.i,
                param.Mode,
                param.ShowDefault ?? true
            });
            ImageCacheItem cacheResult = HttpContext.Current.Cache[imageCacheKey] as ImageCacheItem;
            if (cacheResult == null || Settings.IsCacheDisabled)
            {
                Image image = ExcursionProvider.GetCatalogImage(id, param.Index);
                if (image == null && param.ShowDefault == false)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    ImageFormatter formatter = new ImageFormatter(image, Pictures.nophoto);
                    formatter.Format = ((image != null) ? ImageFormat.Jpeg : ImageFormat.Png);
                    param.ApplyFormat(formatter);
                    System.IO.Stream stream = formatter.CreateStream();
                    if (stream != null)
                    {
                        cacheResult = ImageCacheItem.Create(stream, formatter.MediaType);
                        HttpContext.Current.Cache.Add(imageCacheKey, cacheResult, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        response.Content = new StreamContent(stream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            else
            {
                response.Content = new StreamContent(cacheResult.CraeteStream());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(cacheResult.MediaType);
            }
            response.Headers.CacheControl = new CacheControlHeaderValue();
            response.Headers.CacheControl.Public = true;
            response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
            return response;
        }
        [ActionName("price"), HttpGet]
        public ExcursionPriceList Price(int id, [FromUri] PriceParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.Date.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(202, "date");
            }
            if (!param.StartPoint.HasValue && param.StartPointAlias != null)
            {
                param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
            }
            ExcursionPriceList result;
            if (param.Date.Value.Date < System.DateTime.Today)
            {
                result = new ExcursionPriceList(new System.Collections.Generic.List<ExcursionPrice>());
            }
            else
            {
                System.Collections.Generic.List<ExcursionPrice> prices = ExcursionProvider.GetPrice(param.Language, partner.id, id, param.Date.Value, param.StartPoint);
                result = new ExcursionPriceList((
                    from m in prices
                    where !m.issaleclosed && !m.isstopsale && m.price != null
                    select m).ToList<ExcursionPrice>());
            }
            return result;
        }
        [ActionName("dates"), HttpGet]
        public ExcursionDateList Dates(int id, [FromUri] DatesParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.FirstDate.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(103, "firstadate");
            }
            if (!param.LastDate.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(104, "lastdate");
            }
            return new ExcursionDateList(ExcursionProvider.GetDates(partner.id, id, param.FirstDate.Value, param.LastDate.Value));
        }
        [ActionName("description"), HttpGet]
        public ExcursionDescriptionList Description([FromUri] DescriptionParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            if (param.Excursions == null)
            {
                throw new ArgumentNullExceptionWithCode(105, "ex");
            }
            return new ExcursionDescriptionList(ExcursionProvider.GetDescription(param.Language, param.Excursions));
        }
        [ActionName("exdescription"), HttpGet]
        public ExcursionExtendedDescriptionList ExtendedDescription([FromUri] ExtendedDescriptionParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            if (param.Excursions == null)
            {
                throw new ArgumentNullExceptionWithCode(105, "ex");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (!param.FirstDate.HasValue)
            {
                param.fd = new System.DateTime?(System.DateTime.Now.Date);
            }
            if (!param.LastDate.HasValue)
            {
                param.ld = new System.DateTime?(param.FirstDate.Value.AddDays((double)Settings.ExcursionCheckAvailabilityDays));
            }
            ExcursionExtendedDescriptionList result = new ExcursionExtendedDescriptionList();
            System.Collections.Generic.List<ExcursionDescription> descriptions = ExcursionProvider.GetDescription(param.Language, param.Excursions);
            foreach (ExcursionDescription description in descriptions)
            {
                ExcursionExtendedDescription resultItem = new ExcursionExtendedDescription(description);
                if (description != null && description.excursion != null)
                {
                    resultItem.categorygroups = ExcursionProvider.BuildDescriptionCategories(description.excursion);
                    resultItem.excursiondates = ExcursionProvider.GetDates(partner.id, description.excursion.id, param.FirstDate.Value, param.LastDate.Value);
                }
                result.Add(resultItem);
            }
            return result;
        }
        private static FilterDetailsResult GetCachedFilterDetails(IStartPointAndLanguageParam param, WebPartner partner)
        {
            string filtersResultCacheKey = string.Format("cache_filterDetails[ln:{0}][p:{1}][sp:{2}]", param.Language, partner.id, param.StartPoint.HasValue ? param.StartPoint.ToString() : "-");
            FilterDetailsResult result = HttpContext.Current.Cache[filtersResultCacheKey] as FilterDetailsResult;
            if (result == null || Settings.IsCacheDisabled)
            {
                result = new FilterDetailsResult();
                System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, null, null, null, null, null);
                result.categorygroups = ExcursionProvider.BuildFilterCategories(excursions, null);
                result.departures = ExcursionProvider.BuildFilterDepartures(excursions, null);
                result.languages = ExcursionProvider.BuildFilterLanguages(excursions, null);
                result.durations = ExcursionProvider.BuildFilterDurations(excursions);
                System.Collections.Generic.List<CatalogFilterLocationItem> destinations = ExcursionProvider.BuildFilterDestinations(excursions, null);
                if (destinations != null)
                {
                    result.destinations = new System.Collections.Generic.List<CatalogFilterLocationWithStateItem>();
                    if (destinations.Count > 0)
                    {
                        ExcursionProvider.LoadStatesResult stateResult = ExcursionProvider.LoadStatesForPoints(param.Language, (
                            from m in destinations
                            select m.id).ToArray<int>());
                        foreach (CatalogFilterLocationItem item in destinations)
                        {
                            int stateId;
                            if (stateResult.Links.TryGetValue(item.id, out stateId))
                            {
                                result.destinations.Add(new CatalogFilterLocationWithStateItem(item, stateId.ToString()));
                            }
                            else
                            {
                                result.destinations.Add(new CatalogFilterLocationWithStateItem(item, null));
                            }
                        }
                        result.destinationstates = stateResult.States;
                    }
                }
                HttpContext.Current.Cache.Add(filtersResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return result;
        }
    }
}
