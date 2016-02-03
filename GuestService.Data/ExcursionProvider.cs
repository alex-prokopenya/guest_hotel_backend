using Sm.System.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GuestService.Models.Partner;
namespace GuestService.Data
{
	public static class ExcursionProvider
	{
		private class ExcursionFactory
		{
			internal CategoryWithGroup CategoryWithGroup(DataRow row)
			{
				return new CategoryWithGroup
				{
					id = row.ReadInt("category$inc"),
					name = row.ReadNullableTrimmedString("category$name"),
					description = row.ReadNullableTrimmedString("category$description"),
					group = row.IsNull("categorygroup$inc") ? null : new CategoryGroup
					{
						id = row.ReadInt("categorygroup$inc"),
						name = row.ReadNullableTrimmedString("categorygroup$name")
					}
				};
			}
			internal CategoryGroupWithCategories CategoryGroupWithCategories(DataRow row)
			{
				return new CategoryGroupWithCategories
				{
					group = row.IsNull("categorygroup$inc") ? null : new CategoryGroup
					{
						id = row.ReadInt("categorygroup$inc"),
						name = row.ReadNullableTrimmedString("categorygroup$name")
					}
				};
			}
			internal Category Category(DataRow row)
			{
				return new Category
				{
					id = row.ReadInt("category$inc"),
					name = row.ReadNullableTrimmedString("category$name"),
					description = row.ReadNullableTrimmedString("category$description")
				};
			}
			internal SearchGeography SearchGeography(DataRow row)
			{
				SearchGeography result = new SearchGeography
				{
					name = row.ReadNullableTrimmedString("point$name"),
					geotype = row.ReadNullableTrimmedString("pointtype$name")
				};
				XElement destinationXml = row.ReadXml("point$destinationpoints");
				if (destinationXml != null)
				{
					result.destinations = (
						from d in destinationXml.Descendants()
						select (int)d.Attribute("inc")).ToArray<int>();
				}
				return result;
			}
			internal CatalogExcursion CatalogExcursion(DataRow row)
			{
				CatalogExcursion result = new CatalogExcursion
				{
					id = row.ReadInt("excurs$inc"),
					name = row.ReadNullableTrimmedString("excurs$name"),
					url = row.ReadNullableTrimmedString("excurs$url"),
					excursionPartner = row.IsNull("expartner$inc") ? null : new Partner
					{
						id = row.ReadInt("expartner$inc"),
						name = row.ReadNullableTrimmedString("expartner$name")
					}
				};
				System.DateTime? durationDate = row.ReadNullableUnspecifiedDateTime("excurs$lasting");
				if (durationDate.HasValue)
				{
					result.duration = new System.TimeSpan?(durationDate.Value - new System.DateTime(1900, 1, 1));
				}
				XElement destinationXml = row.ReadXml("excurs$destinationpoints");
				if (destinationXml != null)
				{
					result.destinations = (
						from d in destinationXml.Descendants()
						select new GeoArea
						{
							id = (int)d.Attribute("inc"),
							alias = (string)d.Attribute("alias"),
							name = (string)d.Attribute("name"),
							location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
							{
								latitude = (decimal)d.Attribute("latitude"),
								longitude = (decimal)d.Attribute("longitude")
							}
						}).ToList<GeoArea>();
				}
				XElement departureXml = row.ReadXml("excurs$departurepoints");
				if (departureXml != null)
				{
					result.departures = (
						from d in departureXml.Descendants()
						select new GeoArea
						{
							id = (int)d.Attribute("inc"),
							alias = (string)d.Attribute("alias"),
							name = (string)d.Attribute("name"),
							location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
							{
								latitude = (decimal)d.Attribute("latitude"),
								longitude = (decimal)d.Attribute("longitude")
							}
						}).ToList<GeoArea>();
				}
				XElement languagesXml = row.ReadXml("excurs$languages");
				if (languagesXml != null)
				{
					result.languages = (
						from d in languagesXml.Descendants()
						select new Language
						{
							id = (int)d.Attribute("inc"),
							alias = (string)d.Attribute("alias"),
							name = (string)d.Attribute("name")
						}).ToList<Language>();
				}
				XElement categoriesXml = row.ReadXml("excurs$categories");
				if (categoriesXml != null)
				{
					result.categories = (
						from d in categoriesXml.Descendants()
						select new ExcursionCategory
						{
							id = (int)d.Attribute("inc"),
							name = (string)d.Attribute("name"),
							categorygroup = (!((int?)d.Attribute("groupinc")).HasValue) ? null : new CategoryGroup
							{
								id = (int)d.Attribute("groupinc"),
								name = (string)d.Attribute("groupname")
							},
							sort = (int?)d.Attribute("sort")
						}).ToList<ExcursionCategory>();
				}
				return result;
			}
			internal PriceSummary CatalogExcursionMinPrice(DataRow row)
			{
				PriceSummary result;
				if (row.IsNull("minprice$forservice") || row.IsNull("minprice$price"))
				{
					result = null;
				}
				else
				{
					result = new PriceSummary
					{
						priceType = (row.ReadInt("minprice$forservice") > 0) ? PriceSummary.PriceType.perService : PriceSummary.PriceType.perPerson,
						price = row.ReadDecimal("minprice$price"),
						currency = row.ReadNullableTrimmedString("minprice$currency")
					};
				}
				return result;
			}
            internal ExcursionPrice ExcursionPrice(DataRow row, System.DateTime date)
            {
                ExcursionPrice result = new ExcursionPrice
                {
                    id = row.ReadInt("excurs$inc"),
                    date = date,
                    language = row.IsNull("lang$inc") ? null : new Language
                    {
                        id = row.ReadInt("lang$inc"),
                        alias = row.ReadNullableTrimmedString("lang$alias"),
                        name = row.ReadNullableTrimmedString("lang$name")
                    },
                    group = row.IsNull("group$inc") ? null : new ExcursionGroup
                    {
                        id = row.ReadInt("group$inc"),
                        name = row.ReadNullableTrimmedString("group$name")
                    },
                    time = row.IsNull("time$inc") ? null : new ExcursionTime
                    {
                        id = row.ReadInt("time$inc"),
                        name = row.ReadNullableTrimmedString("time$name")
                    },
                    issaleclosed = !row.IsNull("excurs$closedsale") && row.ReadInt("excurs$closedsale") > 0,
                    closesaletime = row.ReadNullableDateTime("excurs$closesale"),
                    isstopsale = !row.IsNull("excurs$stopsale") && row.ReadInt("excurs$stopsale") > 0,
                    freeseats = (row.ReadNullableInt("seats$free") > 0) ? row.ReadNullableInt("seats$free") : null

                };

                try
                {
                    result.totalseats = (row.ReadNullableInt("seats$total") >= 0) ? row.ReadNullableInt("seats$total") : null;
                }
                catch (Exception)
                { }

                XElement departureXml = row.ReadXml("excurs$departurepoints");
                if (departureXml != null)
                {
                    result.departures = (
                        from d in departureXml.Descendants()
                        select new GeoArea
                        {
                            id = (int)d.Attribute("inc"),
                            alias = (string)d.Attribute("alias"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            }
                        }).ToList<GeoArea>();
                }
                if (!result.isstopsale && !result.issaleclosed)
                {
                    result.price = (row.IsNull("price$isserviceprice") ? null : new PriceDetails
                    {
                        priceType = (row.ReadInt("price$isserviceprice") > 0) ? PriceDetails.PriceType.perService : PriceDetails.PriceType.perPerson,
                        service = row.ReadDecimal("price$service"),
                        adult = row.ReadDecimal("price$adult"),
                        child = row.ReadDecimal("price$child"),
                        infant = row.ReadDecimal("price$infant"),
                        currency = row.ReadNullableTrimmedString("price$alias"),
                        minGroup = row.ReadInt("price$groupfrom", 1),
                        maxGroup = row.ReadInt("price$grouptill", 999)
                    });
                }
                XElement pickuppointsXml = row.ReadXml("excurs$pickuppoints");
                if (pickuppointsXml != null)
                {
                    result.pickuppoints = (
                        from d in pickuppointsXml.Descendants()
                        select new ExcursionPickup
                        {
                            id = (int)d.Attribute("inc"),
                            name = (string)d.Attribute("name"),
                            location = (d.Attribute("latitude") == null || d.Attribute("longitude") == null) ? null : new GeoLocation
                            {
                                latitude = (decimal)d.Attribute("latitude"),
                                longitude = (decimal)d.Attribute("longitude")
                            },
                            note = (string)d.Attribute("note"),
                            pickuptime = (d.Attribute("time") == null) ? null : ((System.DateTime?)d.Attribute("time"))
                        }).ToList<ExcursionPickup>();
                }
                return result;
            }
            internal ExcursionPicture ExcursionPicture(DataRow row)
			{
				return new ExcursionPicture
				{
					ex = row.ReadInt("excurs"),
					index = row.ReadInt("sorder"),
					description = (!row.IsNull("descriptionlang")) ? row.ReadNullableTrimmedString("descriptionlang") : row.ReadNullableTrimmedString("description")
				};
			}
			internal ExcursionDescriptionSection ExcursionDescriptionSection(DataRow row)
			{
				return new ExcursionDescriptionSection
				{
					title = (!row.IsNull("namelang")) ? row.ReadNullableTrimmedString("namelang") : row.ReadNullableTrimmedString("name")
				};
			}
			internal ExcursionDate ExcursionDate(DataRow row)
			{
				return new ExcursionDate
				{
					date = row.ReadDateTime("date"),
					isprice = row.ReadBoolean("exprice"),
					isstopsale = row.ReadBoolean("exstopsale"),
					allclosed = row.ReadBoolean("allclosed")
				};
			}
			internal GeoArea StatePoint(DataRow row)
			{
				return new GeoArea
				{
					id = row.ReadInt("inc"),
					name = row.ReadNullableTrimmedString("name"),
					alias = row.ReadNullableTrimmedString("alias"),
					location = (row.IsNull("latitude") || row.IsNull("longitude")) ? null : new GeoLocation
					{
						latitude = row.ReadDecimal("latitude"),
						longitude = row.ReadDecimal("longitude")
					}
				};
			}
			internal ExcursionPickupHotel ExcursionPickupHotel(DataRow row)
			{
				return new ExcursionPickupHotel
				{
					id = row.ReadInt("hotel$inc"),
					name = row.ReadNullableTrimmedString("hotel$name"),
					pickuptime = row.ReadNullableDateTime("picktime")
				};
			}
		}
		private class EDSNode
		{
			public int id
			{
				get;
				set;
			}
			public int? parentid
			{
				get;
				set;
			}
			public ExcursionDescriptionSection section
			{
				get;
				set;
			}
			public static bool IsNodeEmpty(List<ExcursionProvider.EDSNode> list, ExcursionProvider.EDSNode node)
			{
				bool result;
				if (node.section.paragraphs != null)
				{
					result = false;
				}
				else
				{
					foreach (ExcursionProvider.EDSNode child in 
						from row in list
						where row.parentid == node.id
						select row)
					{
						if (!ExcursionProvider.EDSNode.IsNodeEmpty(list, child))
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
				return result;
			}
		}
		private class CategoryGroupWithCategoriesComparer : IEqualityComparer<CategoryGroupWithCategories>
		{
			public bool Equals(CategoryGroupWithCategories x, CategoryGroupWithCategories y)
			{
				return object.ReferenceEquals(x, y) || (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null) && ((x.group != null) ? x.group.id : 0) == ((y.group != null) ? y.group.id : 0));
			}
			public int GetHashCode(CategoryGroupWithCategories group)
			{
				int result;
				if (object.ReferenceEquals(group, null))
				{
					result = 0;
				}
				else
				{
					result = ((group.group == null) ? 0 : group.group.id.GetHashCode());
				}
				return result;
			}
		}
		private class CatalogFilterItemsCounterBuilder<T>
		{
			public class CatalogFilterCounterItem<M>
			{
				public M item
				{
					get;
					set;
				}
				public int count
				{
					get;
					set;
				}
			}
			private Dictionary<int, ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>> list;
			public CatalogFilterItemsCounterBuilder()
			{
				this.list = new Dictionary<int, ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>>();
			}
			public void Add(int id, T item)
			{
				ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T> counterItem = null;
				if (this.list.TryGetValue(id, out counterItem))
				{
					counterItem.count++;
				}
				else
				{
					this.list.Add(id, new ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>
					{
						count = 1,
						item = item
					});
				}
			}
			public List<ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>> ToList()
			{
				return (
					from m in this.list
					select m.Value).ToList<ExcursionProvider.CatalogFilterItemsCounterBuilder<T>.CatalogFilterCounterItem<T>>();
			}
		}
		public class LoadStatesResult
		{
			public Dictionary<int, int> Links
			{
				get;
				private set;
			}
			public List<GeoArea> States
			{
				get;
				set;
			}
			public LoadStatesResult()
			{
				this.Links = new Dictionary<int, int>();
			}
		}
		public enum ExcursionSorting
		{
			name,
			price,
			pricedname
		}
		private static ExcursionProvider.ExcursionFactory factory = new ExcursionProvider.ExcursionFactory();
		public static SearchExcursionResult SearchExcursionObjects(string lang, int? startPoint, string searchText, int limit)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionObjects", "geopoints", new
			{
				language = lang,
				startpoint = startPoint,
				searchtext = searchText,
				topcount = limit
			});
			SearchExcursionResult result = new SearchExcursionResult();
			result.geography = (
				from DataRow row in ds.Tables["geopoints"].Rows
				select ExcursionProvider.factory.SearchGeography(row)).ToList<SearchGeography>();
			return result;
		}
		public static List<CategoryWithGroup> GetCategories(string lang, int? startPoint)
		{
			List<CategoryWithGroup> result = new List<CategoryWithGroup>();
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionCategories", "categories", new
			{
				language = lang,
				startpoint = startPoint
			});
			return (
				from DataRow row in ds.Tables["categories"].Rows
				select ExcursionProvider.factory.CategoryWithGroup(row)).ToList<CategoryWithGroup>();
		}
		public static List<CategoryGroupWithCategories> GetCategoriesByGroup(string lang, int? startPoint)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionCategories", "categories", new
			{
				language = lang,
				startpoint = startPoint
			});
			List<CategoryGroupWithCategories> result = (
				from DataRow row in ds.Tables["categories"].Rows
				select ExcursionProvider.factory.CategoryGroupWithCategories(row)).Distinct(new ExcursionProvider.CategoryGroupWithCategoriesComparer()).ToList<CategoryGroupWithCategories>();
			CategoryGroupWithCategories emptyGroup = result.FirstOrDefault((CategoryGroupWithCategories m) => m.group == null);
			if (emptyGroup != null)
			{
				result.Remove(emptyGroup);
				result.Insert(0, emptyGroup);
			}
			foreach (CategoryGroupWithCategories group in result)
			{
				if (group.group == null)
				{
					group.categories = (
						from DataRow row in ds.Tables["categories"].Rows
						where row.IsNull("categorygroup$inc")
						select ExcursionProvider.factory.Category(row)).ToList<Category>();
				}
				else
				{
					group.categories = (
						from row in ds.Tables["categories"].Rows.Cast<DataRow>().Where(delegate(DataRow row)
						{
							int? num = row.ReadNullableInt("categorygroup$inc");
							return num == ((@group.@group != null) ? new int?(@group.@group.id) : null);
						})
						select ExcursionProvider.factory.Category(row)).ToList<Category>();
				}
			}
			return result;
		}
		public static Image GetCategoryImage(int id)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getCategoryImage", "image", new
			{
				id
			});
			DataRow row = ds.Tables["image"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
			Image result;
			if (row != null && !row.IsNull("img$picture"))
			{
				result = Image.FromStream(new System.IO.MemoryStream((byte[])row["img$picture"]));
			}
			else
			{
				result = null;
			}
			return result;
		}

        public static List<CatalogExcursionMinPrice> FindAllExcursions(string lang, int partner, System.DateTime? startDate, System.DateTime? endDate, int? topLimit, int? startPoint, string searchText, int[] categories, int[] departures, int[] destinations, int[] languages, System.TimeSpan? minDuration, System.TimeSpan? maxDuration, ExcursionProvider.ExcursionSorting? sorting, bool withoutPrice)
        {
            System.DateTime _startDate = startDate.HasValue ? startDate.Value : System.DateTime.Now.Date;
            System.DateTime _endDate = endDate.HasValue ? endDate.Value : _startDate.AddMonths(6);
            XName arg_261_0 = "excursionFilters";
            object[] array = new object[8];
            array[0] = ((!topLimit.HasValue) ? null : new XAttribute("topLimit", topLimit.Value));
            array[1] = ((searchText == null) ? null : new XElement("name", searchText));
            object[] arg_D5_0 = array;
            int arg_D5_1 = 2;
            XElement arg_D5_2;
            if (categories != null)
            {
                arg_D5_2 = new XElement("categories",
                    from c in categories
                    select new XElement("category", c));
            }
            else
            {
                arg_D5_2 = null;
            }
            arg_D5_0[arg_D5_1] = arg_D5_2;
            object[] arg_115_0 = array;
            int arg_115_1 = 3;
            XElement arg_115_2;
            if (departures != null)
            {
                arg_115_2 = new XElement("departurepoints",
                    from d in departures
                    select new XElement("departurepoint", d));
            }
            else
            {
                arg_115_2 = null;
            }
            arg_115_0[arg_115_1] = arg_115_2;
            object[] arg_155_0 = array;
            int arg_155_1 = 4;
            XElement arg_155_2;
            if (destinations != null)
            {
                arg_155_2 = new XElement("destinationpoints",
                    from d in destinations
                    select new XElement("destinationpoint", d));
            }
            else
            {
                arg_155_2 = null;
            }
            arg_155_0[arg_155_1] = arg_155_2;
            object[] arg_195_0 = array;
            int arg_195_1 = 5;
            XElement arg_195_2;
            if (languages != null)
            {
                arg_195_2 = new XElement("languages",
                    from l in languages
                    select new XElement("language", l));
            }
            else
            {
                arg_195_2 = null;
            }
            arg_195_0[arg_195_1] = arg_195_2;
            array[6] = new XElement("duration", new object[]
            {
                (!minDuration.HasValue) ? null : new XAttribute("minDuration", new System.DateTime(1900, 1, 1).Add(minDuration.Value)),
                (!maxDuration.HasValue) ? null : new XAttribute("maxDuration", new System.DateTime(1900, 1, 1).Add(maxDuration.Value))
            });
            array[7] = ((!sorting.HasValue) ? null : new XElement("sorting", sorting.ToString()));
            XElement xml = new XElement(arg_261_0, array);
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findAllExcursions", "excursions", new
            {
                language = lang,
                partner = partner,
                startpoint = startPoint,
                startdate = _startDate,
                enddate = _endDate,
                filters = xml,
                withpriceonly = !withoutPrice
            });
            return (
                from DataRow row in ds.Tables["excursions"].Rows
                select new CatalogExcursionMinPrice
                {
                    excursion = ExcursionProvider.factory.CatalogExcursion(row),
                    minPrice = ExcursionProvider.factory.CatalogExcursionMinPrice(row)
                }).ToList<CatalogExcursionMinPrice>();
        }

        public static List<CatalogExcursionMinPrice> FindExcursions(string lang, int partner, System.DateTime? startDate, System.DateTime? endDate, int? topLimit, int? startPoint, string searchText, int[] categories, int[] departures, int[] destinations, int[] languages, System.TimeSpan? minDuration, System.TimeSpan? maxDuration, ExcursionProvider.ExcursionSorting? sorting, bool withoutPrice)
		{
			System.DateTime _startDate = startDate.HasValue ? startDate.Value : System.DateTime.Now.Date;
			System.DateTime _endDate = endDate.HasValue ? endDate.Value : _startDate.AddMonths(6);
			XName arg_261_0 = "excursionFilters";
			object[] array = new object[8];
			array[0] = ((!topLimit.HasValue) ? null : new XAttribute("topLimit", topLimit.Value));
			array[1] = ((searchText == null) ? null : new XElement("name", searchText));
			object[] arg_D5_0 = array;
			int arg_D5_1 = 2;
			XElement arg_D5_2;
			if (categories != null)
			{
				arg_D5_2 = new XElement("categories", 
					from c in categories
					select new XElement("category", c));
			}
			else
			{
				arg_D5_2 = null;
			}
			arg_D5_0[arg_D5_1] = arg_D5_2;
			object[] arg_115_0 = array;
			int arg_115_1 = 3;
			XElement arg_115_2;
			if (departures != null)
			{
				arg_115_2 = new XElement("departurepoints", 
					from d in departures
					select new XElement("departurepoint", d));
			}
			else
			{
				arg_115_2 = null;
			}
			arg_115_0[arg_115_1] = arg_115_2;
			object[] arg_155_0 = array;
			int arg_155_1 = 4;
			XElement arg_155_2;
			if (destinations != null)
			{
				arg_155_2 = new XElement("destinationpoints", 
					from d in destinations
					select new XElement("destinationpoint", d));
			}
			else
			{
				arg_155_2 = null;
			}
			arg_155_0[arg_155_1] = arg_155_2;
			object[] arg_195_0 = array;
			int arg_195_1 = 5;
			XElement arg_195_2;
			if (languages != null)
			{
				arg_195_2 = new XElement("languages", 
					from l in languages
					select new XElement("language", l));
			}
			else
			{
				arg_195_2 = null;
			}
			arg_195_0[arg_195_1] = arg_195_2;
			array[6] = new XElement("duration", new object[]
			{
				(!minDuration.HasValue) ? null : new XAttribute("minDuration", new System.DateTime(1900, 1, 1).Add(minDuration.Value)),
				(!maxDuration.HasValue) ? null : new XAttribute("maxDuration", new System.DateTime(1900, 1, 1).Add(maxDuration.Value))
			});
			array[7] = ((!sorting.HasValue) ? null : new XElement("sorting", sorting.ToString()));
			XElement xml = new XElement(arg_261_0, array);
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findExcursions", "excursions", new
			{
				language = lang,
				partner = partner,
				startpoint = startPoint,
				startdate = _startDate,
				enddate = _endDate,
				filters = xml,
				withpriceonly = !withoutPrice
			});
			return (
				from DataRow row in ds.Tables["excursions"].Rows
				select new CatalogExcursionMinPrice
				{
					excursion = ExcursionProvider.factory.CatalogExcursion(row),
					minPrice = ExcursionProvider.factory.CatalogExcursionMinPrice(row)
				}).ToList<CatalogExcursionMinPrice>();
		}

        public static List<CatalogExcursionMinPrice> FindExcursions(string lang, int partner, System.DateTime? startDate, System.DateTime? endDate, int? topLimit, int? startPoint, string searchText, int[] categories, int[] departures, int[] destinations, int[] languages, System.TimeSpan? minDuration, System.TimeSpan? maxDuration, ExcursionProvider.ExcursionSorting? sorting)
        {
            System.DateTime _startDate = startDate.HasValue ? startDate.Value : System.DateTime.Now.Date;
            System.DateTime _endDate = endDate.HasValue ? endDate.Value : _startDate.AddMonths(6);
            XName arg_261_0 = "excursionFilters";
            object[] array = new object[8];
            array[0] = ((!topLimit.HasValue) ? null : new XAttribute("topLimit", topLimit.Value));
            array[1] = ((searchText == null) ? null : new XElement("name", searchText));
            object[] arg_D5_0 = array;
            int arg_D5_1 = 2;
            XElement arg_D5_2;
            if (categories != null)
            {
                arg_D5_2 = new XElement("categories",
                    from c in categories
                    select new XElement("category", c));
            }
            else
            {
                arg_D5_2 = null;
            }
            arg_D5_0[arg_D5_1] = arg_D5_2;
            object[] arg_115_0 = array;
            int arg_115_1 = 3;
            XElement arg_115_2;
            if (departures != null)
            {
                arg_115_2 = new XElement("departurepoints",
                    from d in departures
                    select new XElement("departurepoint", d));
            }
            else
            {
                arg_115_2 = null;
            }
            arg_115_0[arg_115_1] = arg_115_2;
            object[] arg_155_0 = array;
            int arg_155_1 = 4;
            XElement arg_155_2;
            if (destinations != null)
            {
                arg_155_2 = new XElement("destinationpoints",
                    from d in destinations
                    select new XElement("destinationpoint", d));
            }
            else
            {
                arg_155_2 = null;
            }
            arg_155_0[arg_155_1] = arg_155_2;
            object[] arg_195_0 = array;
            int arg_195_1 = 5;
            XElement arg_195_2;
            if (languages != null)
            {
                arg_195_2 = new XElement("languages",
                    from l in languages
                    select new XElement("language", l));
            }
            else
            {
                arg_195_2 = null;
            }
            arg_195_0[arg_195_1] = arg_195_2;
            array[6] = new XElement("duration", new object[]
            {
                (!minDuration.HasValue) ? null : new XAttribute("minDuration", new System.DateTime(1900, 1, 1).Add(minDuration.Value)),
                (!maxDuration.HasValue) ? null : new XAttribute("maxDuration", new System.DateTime(1900, 1, 1).Add(maxDuration.Value))
            });
            array[7] = ((!sorting.HasValue) ? null : new XElement("sorting", sorting.ToString()));
            XElement xml = new XElement(arg_261_0, array);
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findExcursions", "excursions", new
            {
                language = lang,
                partner = partner,
                startpoint = startPoint,
                startdate = _startDate,
                enddate = _endDate,
                filters = xml,
                withpriceonly = Settings.ExcursionWithPriceOnlyCatalog
            });

            var excs = (
                from DataRow row in ds.Tables["excursions"].Rows
                select new CatalogExcursionMinPrice
                {
                    excursion = ExcursionProvider.factory.CatalogExcursion(row),
                    minPrice = ExcursionProvider.factory.CatalogExcursionMinPrice(row)
                }).ToList<CatalogExcursionMinPrice>();


            return excs;
        }


        public static Image GetEditImage(int id, bool isMain)
        {
            var query = "select image from " + (isMain ? "excurspicture" : "excurspicture_temp") + " where inc = " + id;


            DataSet ds = DatabaseOperationProvider.Query(query, "images", new { });

            DataRow row = ds.Tables["images"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
            Image result;
            if (row != null && !row.IsNull("image"))
            {
                result = Image.FromStream(new System.IO.MemoryStream((byte[])row["image"]));
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static Image GetCatalogImage(int id, int index)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getCatalogImage", "image", new
			{
				id,
				index
			});
			DataRow row = ds.Tables["image"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
			Image result;
			if (row != null && !row.IsNull("img$picture"))
			{
				result = Image.FromStream(new System.IO.MemoryStream((byte[])row["img$picture"]));
			}
			else
			{
				result = null;
			}
			return result;
		}
		public static List<ExcursionPrice> GetPrice(string lang, int partner, int excursionId, System.DateTime date, int? startPoint)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionPrice", "prices", new
			{
				language = lang,
				partner = partner,
				startpoint = startPoint,
				excursionId = excursionId,
				date = date.Date
			});
			return (
				from DataRow row in ds.Tables["prices"].Rows
				select ExcursionProvider.factory.ExcursionPrice(row, date)).ToList<ExcursionPrice>();
		}
		public static List<ExcursionDate> GetDates(int partner, int excursionId, System.DateTime dateFrom, System.DateTime dateTill)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_GetExcursionDates", "dates", new
			{
				partner = partner,
				excurs = excursionId,
				dateBeg = dateFrom,
				dateEnd = dateTill
			});
			return (
				from DataRow row in ds.Tables["dates"].Rows
				select ExcursionProvider.factory.ExcursionDate(row)).ToList<ExcursionDate>();
		}
		public static List<ExcursionDescription> GetDescription(string lang, int[] excursions)
		{
			if (excursions == null)
			{
				throw new System.ArgumentNullException("excursions");
			}
			XElement xml = new XElement("excursions", 
				from e in excursions
				select new XElement("excursion", new XAttribute("id", e)));
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionDescription", "excursions,pictures,dtree,description", new
			{
				language = lang,
				excursions = xml,
				loaddescription = true
			});
			IEnumerable<ExcursionPicture> pictures = 
				from DataRow row in ds.Tables["pictures"].Rows
				select ExcursionProvider.factory.ExcursionPicture(row);
			return ds.Tables["excursions"].Rows.Cast<DataRow>().Select(delegate(DataRow row)
			{
				ExcursionDescription description = new ExcursionDescription();
				description.excursion = ExcursionProvider.factory.CatalogExcursion(row);
				description.pictures = (
					from p in pictures
					where p.ex == description.excursion.id
					select p).ToList<ExcursionPicture>();
				List<ExcursionProvider.EDSNode> tree = (
					from DataRow r in ds.Tables["dtree"].Rows
					select new ExcursionProvider.EDSNode
					{
						id = r.ReadInt("inc"),
						parentid = r.ReadNullableInt("parent_inc"),
						section = ExcursionProvider.factory.ExcursionDescriptionSection(r)
					}).ToList<ExcursionProvider.EDSNode>();
				ExcursionProvider.EDSNode ctree = null;
				foreach (DataRow paragraphRow in 
					from DataRow r in ds.Tables["description"].Rows
					where r.ReadInt("excurs") == description.excursion.id
					select r)
				{
					int treeId = paragraphRow.ReadInt("tree");
					if (ctree == null || ctree.id != treeId)
					{
						ctree = tree.FirstOrDefault((ExcursionProvider.EDSNode r) => r.id == treeId);
					}
					if (ctree != null)
					{
						if (ctree.section.paragraphs == null)
						{
							ctree.section.paragraphs = new List<string>();
						}
						ctree.section.paragraphs.Add(paragraphRow.ReadNullableTrimmedString((!paragraphRow.IsNull("descriptionlang")) ? "descriptionlang" : "description"));
					}
				}
				description.description = new List<ExcursionDescriptionSection>();
				foreach (ExcursionProvider.EDSNode tnode in tree)
				{
					if (!ExcursionProvider.EDSNode.IsNodeEmpty(tree, tnode))
					{
						if (!tnode.parentid.HasValue)
						{
							description.description.Add(tnode.section);
						}
						else
						{
							ExcursionProvider.EDSNode pnode = tree.FirstOrDefault((ExcursionProvider.EDSNode r) => r.id == tnode.parentid.Value);
							if (pnode != null)
							{
								if (pnode.section.sections == null)
								{
									pnode.section.sections = new List<ExcursionDescriptionSection>();
								}
								pnode.section.sections.Add(tnode.section);
							}
						}
					}
				}
				return description;
			}).ToList<ExcursionDescription>();
		}
		public static List<CatalogFilterCategoryGroup> BuildFilterCategories(List<CatalogExcursionMinPrice> catalog, int[] marks)
		{
			ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>();
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.categories != null)
					{
						foreach (ExcursionCategory c in item.excursion.categories)
						{
							builder.Add(c.id, c);
						}
					}
				}
			}
			return (
				from m in (
					from m in builder.ToList()
					group m by (m.item.categorygroup != null) ? m.item.categorygroup.name : null).Select(delegate(IGrouping<string, ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>.CatalogFilterCounterItem<ExcursionCategory>> m)
				{
					CatalogFilterCategoryGroup catalogFilterCategoryGroup = new CatalogFilterCategoryGroup();
					catalogFilterCategoryGroup.name = m.Key;
					catalogFilterCategoryGroup.items = (
						from n in m
						orderby n.item.sort
						select new CatalogFilterItem
						{
							id = n.item.id,
							name = n.item.name,
							count = n.count
						}).ToList<CatalogFilterItem>();
					return catalogFilterCategoryGroup;
				})
				orderby (m.name == null) ? 0 : 1, m.name
				select m).ToList<CatalogFilterCategoryGroup>();
		}
		public static List<CatalogFilterLocationItem> BuildFilterDepartures(List<CatalogExcursionMinPrice> catalog, int[] marks)
		{
			ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea>();
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.departures != null)
					{
						foreach (GeoArea d in item.excursion.departures)
						{
							builder.Add(d.id, d);
						}
					}
				}
			}
			return (
				from m in builder.ToList()
				select new CatalogFilterLocationItem
				{
					id = m.item.id,
					name = m.item.name,
					location = m.item.location,
					count = m.count
				} into m
				orderby m.name
				select m).ToList<CatalogFilterLocationItem>();
		}
		public static List<GeoArea> BuildDepartureList(List<CatalogExcursionMinPrice> catalog)
		{
			Dictionary<int, GeoArea> departures = new Dictionary<int, GeoArea>();
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.departures != null)
					{
						foreach (GeoArea d in item.excursion.departures)
						{
							GeoArea area;
							if (!departures.TryGetValue(d.id, out area))
							{
								departures[d.id] = d;
							}
						}
					}
				}
			}
			return (
				from m in departures.Values
				orderby m.name
				select m).ToList<GeoArea>();
		}
		public static List<CatalogFilterLocationItem> BuildFilterDestinations(List<CatalogExcursionMinPrice> catalog, int[] marks)
		{
			ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<GeoArea>();
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.destinations != null)
					{
						foreach (GeoArea d in item.excursion.destinations)
						{
							builder.Add(d.id, d);
						}
					}
				}
			}
			return (
				from m in builder.ToList()
				select new CatalogFilterLocationItem
				{
					id = m.item.id,
					name = m.item.name,
					location = m.item.location,
					count = m.count
				} into m
				orderby m.name
				select m).ToList<CatalogFilterLocationItem>();
		}
		public static List<CatalogFilterItem> BuildFilterLanguages(List<CatalogExcursionMinPrice> catalog, int[] marks)
		{
			ExcursionProvider.CatalogFilterItemsCounterBuilder<Language> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<Language>();
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.languages != null)
					{
						foreach (Language i in item.excursion.languages)
						{
							builder.Add(i.id, i);
						}
					}
				}
			}
			return (
				from m in builder.ToList()
				select new CatalogFilterItem
				{
					id = m.item.id,
					name = m.item.name,
					count = m.count
				} into m
				orderby m.name
				select m).ToList<CatalogFilterItem>();
		}
		public static CatalogFilterDuration BuildFilterDurations(List<CatalogExcursionMinPrice> catalog)
		{
			System.TimeSpan? min = null;
			System.TimeSpan? max = null;
			if (catalog != null)
			{
				foreach (CatalogExcursionMinPrice item in catalog)
				{
					if (item.excursion != null && item.excursion.duration.HasValue)
					{
						if (!min.HasValue || item.excursion.duration < min)
						{
							min = item.excursion.duration;
						}
						if (!max.HasValue || item.excursion.duration > max)
						{
							max = item.excursion.duration;
						}
					}
				}
			}
			return (min.HasValue && max.HasValue) ? new CatalogFilterDuration
			{
				min = min.Value,
				max = max.Value
			} : null;
		}
		public static ExcursionProvider.LoadStatesResult LoadStatesForPoints(string lang, int[] points)
		{
			if (points == null)
			{
				throw new System.ArgumentNullException("points");
			}
			XElement xml = new XElement("geopoints", 
				from e in points
				select new XElement("geopoint", new XAttribute("id", e)));
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getStatesForPoints", "links,states", new
			{
				language = lang,
				points = xml
			});
			ExcursionProvider.LoadStatesResult result = new ExcursionProvider.LoadStatesResult();
			foreach (DataRow row2 in ds.Tables["links"].Rows.Cast<DataRow>())
			{
				result.Links[row2.ReadInt("pinc")] = row2.ReadInt("sinc");
			}
			result.States = (
				from DataRow row in ds.Tables["states"].Rows
				select ExcursionProvider.factory.StatePoint(row)).ToList<GeoArea>();
			return result;
		}
		public static List<CatalogFilterCategoryGroup> BuildDescriptionCategories(CatalogExcursion catalog)
		{
			ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory> builder = new ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>();
			if (catalog != null && catalog.categories != null)
			{
				foreach (ExcursionCategory c in catalog.categories)
				{
					builder.Add(c.id, c);
				}
			}
			return (
				from m in (
					from m in builder.ToList()
					group m by (m.item.categorygroup != null) ? m.item.categorygroup.name : null).Select(delegate(IGrouping<string, ExcursionProvider.CatalogFilterItemsCounterBuilder<ExcursionCategory>.CatalogFilterCounterItem<ExcursionCategory>> m)
				{
					CatalogFilterCategoryGroup catalogFilterCategoryGroup = new CatalogFilterCategoryGroup();
					catalogFilterCategoryGroup.name = m.Key;
					catalogFilterCategoryGroup.items = (
						from n in m
						orderby n.item.sort
						select new CatalogFilterItem
						{
							id = n.item.id,
							name = n.item.name,
							count = 0
						}).ToList<CatalogFilterItem>();
					return catalogFilterCategoryGroup;
				})
				orderby (m.name == null) ? 0 : 1, m.name
				select m).ToList<CatalogFilterCategoryGroup>();
		}
		public static List<ExcursionPickupHotel> GetExcursionPickupHotels(string lang, int excursion, int? excursionTime, int[] departurePoints)
		{
			string depaturepoints = (departurePoints != null && departurePoints.Length > 0) ? string.Join<int>(",", departurePoints) : null;
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionHotelPickup", "hotels", new
			{
				language = lang,
				excurs = excursion,
				extime = excursionTime,
				depaturepoints = depaturepoints
			});
			return (
				from DataRow row in ds.Tables["hotels"].Rows
				select ExcursionProvider.factory.ExcursionPickupHotel(row)).ToList<ExcursionPickupHotel>();
		}


        public static void GetExcursionTexts(int id,
                                             out KeyValuePair<int, string>[] names,
                                             out string route,
                                             out int[] types,
                                             out int region,
                                             out KeyValuePair<int, string>[] descriptions,
                                             out int copyId,
                                             out int food,
                                             out int guide,
                                             out int entryFees,
                                             out KeyValuePair<int, string>[] routes,
                                             out KeyValuePair<int, string>[] cancel,
                                             out KeyValuePair<int, string>[] stuffs
                                        )
        {
            names = new KeyValuePair<int, string>[2];
            var tempTypes = new List<int>();
            var tempDescriptions = new List<KeyValuePair<int, string>>();
            var tempRoutes = new List<KeyValuePair<int, string>>();
            var tempCancel = new List<KeyValuePair<int, string>>();
            var tempStuff  = new List<KeyValuePair<int, string>>();

            //определить, из какой таблицы брать описание

            //ищем данные в основной и временной таблицах
            var query = "       select 0 as srt, inc, name, lname, route, region, edate, food, guide, entryfees from excurs      where inc = " + id +
                        " union select 1 as srt, inc, name, lname, route, region, edate, food, guide, entryfees from excurs_temp where excurs_id = " + id + " order by srt asc";

            var res = DatabaseOperationProvider.Query(query, "excurs", new { });

            //по умолчанию берем данные из основной
            var row = res.Tables[0].Rows[0];
            string tablesPostfix = "";
            copyId = -1;

            //если есть информация и о временной таблице
            if (res.Tables[0].Rows.Count > 1)
            { 
                DateTime editDateOriginal   = row.ReadDateTime("edate");
                DateTime editDateCopy       = res.Tables[0].Rows[1].ReadDateTime("edate");

                //сравниваем даты изменения
                if (editDateCopy > editDateOriginal)
                {
                    //если во временной таблице более актуальная версия, берем информацию из нее
                    row = res.Tables[0].Rows[1];
                    tablesPostfix = "_temp";

                    copyId = row.ReadInt("inc");
                }
            }

            //читаем имя
            names[0] = new KeyValuePair<int, string>(2, row.ReadNullableString("name"));
            names[1] = new KeyValuePair<int, string>(1, row.ReadNullableString("lname"));

            //маршрут и регион
            route  = row.ReadNullableString("route");
            region = row.ReadInt("region");
            food = row.ReadInt("food");
            guide = row.ReadInt("guide");
            entryFees = row.ReadInt("entryfees");


            var exId = row.ReadInt("inc"); // айдишник оригинальной экскурсии или временной копии, зависит от даты послених изменений

            #region тип экскурсии
            //читаем типы
            query = "select excurscategory from excatlist" + tablesPostfix + " where excurs = " + exId;

            res = DatabaseOperationProvider.Query(query, "categories", new { });

            foreach (DataRow catItem in res.Tables[0].Rows)
                tempTypes.Add(catItem.ReadInt("excurscategory"));

            types = tempTypes.ToArray();
            #endregion

            #region описание экскурсии
            //читаем типы
            query = "select  2 as lang, a.description from exdsc" + tablesPostfix + " as a where a.tree = 2 and a.excurs = " + exId +
             " union select  b.lang, b.description from exdsclang" + tablesPostfix + " as b where (select inc from exdsc" + tablesPostfix + " where tree = 2 and excurs = " + exId + " ) = b.exdsc";

            res = DatabaseOperationProvider.Query(query, "descriptions", new { });

            foreach (DataRow descItem in res.Tables[0].Rows)
                tempDescriptions.Add(new KeyValuePair<int, string>(descItem.ReadInt("lang"), descItem.ReadNullableString("description")));

            descriptions = tempDescriptions.ToArray();
            #endregion

            #region описание маршрута
            //читаем типы
            query = "select  2 as lang, a.description from exdsc" + tablesPostfix + " as a where a.tree = 3 and a.excurs = " + exId +
             " union select  b.lang, b.description from exdsclang" + tablesPostfix + " as b where (select inc from exdsc" + tablesPostfix + " where tree = 3 and  excurs = " + exId + " ) = b.exdsc";

            res = DatabaseOperationProvider.Query(query, "routes", new { });

            foreach (DataRow descItem in res.Tables[0].Rows)
                tempRoutes.Add(new KeyValuePair<int, string>(descItem.ReadInt("lang"), descItem.ReadNullableString("description")));

            routes = tempRoutes.ToArray();
            #endregion

            #region описание Cancel
            //читаем типы
            query = "select  2 as lang, a.description from exdsc" + tablesPostfix + " as a where  a.tree = 6 and  a.excurs = " + exId +
             " union select  b.lang, b.description from exdsclang" + tablesPostfix + " as b where (select inc from exdsc" + tablesPostfix + " where  tree = 6 and excurs = " + exId + " ) = b.exdsc";

            res = DatabaseOperationProvider.Query(query, "descriptions", new { });

            foreach (DataRow descItem in res.Tables[0].Rows)
                tempCancel.Add(new KeyValuePair<int, string>(descItem.ReadInt("lang"), descItem.ReadNullableString("description")));

            cancel = tempCancel.ToArray();
            #endregion

            #region описание Stuff
            //читаем типы
            query = "select  2 as lang, a.description from exdsc" + tablesPostfix + " as a where  a.tree = 5 and  a.excurs = " + exId +
             " union select  b.lang, b.description from exdsclang" + tablesPostfix + " as b where (select inc from exdsc" + tablesPostfix + " where  tree = 5 and excurs = " + exId + " ) = b.exdsc";

            res = DatabaseOperationProvider.Query(query, "descriptions", new { });

            foreach (DataRow descItem in res.Tables[0].Rows)
                tempStuff.Add(new KeyValuePair<int, string>(descItem.ReadInt("lang"), descItem.ReadNullableString("description")));

            stuffs = tempStuff.ToArray();
            #endregion
        }

        public static KeyValuePair<int, bool>[] GetExcursionOldPhotos(int id)
        {
            var result = new List<KeyValuePair<int, bool>>();

            var query = "        select inc, 1 as main from excurspicture where excurs = " + id +
                         " union select inc, 0 as main from excurspicture_temp where excurs = (select inc from excurs_temp where excurs_id = " +id+ ") ";

            var res = DatabaseOperationProvider.Query(query, "categories", new { });

            foreach (DataRow photoItem in res.Tables[0].Rows)
                result.Add( new KeyValuePair<int, bool>(photoItem.ReadInt("inc"), photoItem.ReadInt("main") == 1));

            return result.ToArray();
        }

        public static KeyValuePair<int, PriceInfo>[] GetExcursionOldPrices(int id, KeyValuePair<string, string>[] langs, KeyValuePair<string, string>[] regions)
        {
            var result = new List<KeyValuePair<int, PriceInfo>>();

            var query = "       select inc, region, currency, language, datebeg, dateend, adult, child, inf, total, days, groupfrom, grouptill from exprice where excurs  = " + id;

            var res = DatabaseOperationProvider.Query(query, "categories", new { });

            var langsDict = new Dictionary<string, string>();

            foreach (var langItem in langs)
                if(!langsDict.ContainsKey(langItem.Key))
                    langsDict.Add(langItem.Key, langItem.Value);

            var regionsDict = new Dictionary<string, string>();

            foreach (var regionItem in regions)
                if (!regionsDict.ContainsKey(regionItem.Key))
                    regionsDict.Add(regionItem.Key, regionItem.Value);

            foreach (DataRow priceItem in res.Tables[0].Rows)
            {
                var priceInfo = new PriceInfo();

                priceInfo.Dates = priceItem.ReadDateTime("datebeg").ToString("MM/dd/yyyy") + " " + priceItem.ReadDateTime("dateend").ToString("MM/dd/yyyy");

                if (priceItem.ReadDecimal("total") > 0)
                    priceInfo.Summ = "total " + priceItem.ReadDecimal("total").ToString("N0");
                else
                    priceInfo.Summ = string.Format("adl {0:N0}, chd {1:N0}, inf {2:N0}", new object[] {
                                        
                                        priceItem.ReadDecimal("adult"),
                                        priceItem.ReadDecimal("child"),
                                        priceItem.ReadDecimal("inf")
                                    });

                var lang = priceItem.ReadInt("language").ToString();

                var region = priceItem.ReadInt("region").ToString();

                priceInfo.Summ += priceItem.ReadInt("currency") == 8 ? " eur":" usd";

                priceInfo.Weekdays = ConvertWeekdays(priceItem.ReadNullableString("days"));
                priceInfo.Group = priceItem.ReadInt("groupfrom") + " - " + priceItem.ReadInt("grouptill");
                priceInfo.Lang   = (langsDict.ContainsKey(lang)) ? langsDict[lang] : "-";
                priceInfo.Region = (regionsDict.ContainsKey(region)) ? regionsDict[region] : "-";
                priceInfo.Type = (priceItem.ReadDecimal("total") > 0) ? "Individual":"Group";
                result.Add(new KeyValuePair<int, PriceInfo>(priceItem.ReadInt("inc"), priceInfo));
            }

            return result.ToArray();
        }

        private static string ConvertWeekdays(string inp)
        {
            var result = "Mn, Tu, Wd, Th, Fr, St, Sn";

            if (string.IsNullOrEmpty(inp)) return result;

            var pars = result.Split(',');
            var selectedDays = new List<string>();

            for(var i=0; i< inp.Length; i++)
                if (inp[i] == '1')
                    selectedDays.Add(pars[i]);

            return string.Join(",", selectedDays.ToArray());
        }

        public static void DeleteOldPhoto(int id, bool isMain, int providerId)
        {
            //проверить, принадлежит ли экскурсия пользователю
            //удалить фото

            var table_postfix = isMain ? "" : "_temp";

            var query = "delete from excurspicture"+ table_postfix + " where inc = @pictId and excurs in (select inc from excurs where partner = @partner)";

            var res = DatabaseOperationProvider.Query(query, "partners", new { pictId = id, partner = providerId });
        }

        public static void DeleteOldPrice(int id, int providerId)
        {
            //проверить, принадлежит ли экскурсия пользователю
            //удалить цену

            var query = "delete from exdetplan where excurs = (select excurs  from exprice where inc = @priceId and excurs in (select inc from excurs where partner = @partner)) and " +
                            "isnull(region,   -2147483647) = (select region   from exprice where inc = @priceId and excurs in (select inc from excurs where partner = @partner)) and "+
                            "isnull(language, -2147483647) = (select language from exprice where inc = @priceId and excurs in (select inc from excurs where partner = @partner))";

            var res = DatabaseOperationProvider.Query(query, "partners", new { priceId = id, partner = providerId });


            query = "delete from exprice where inc = @priceId and excurs in (select inc from excurs where partner = @partner)";

            res = DatabaseOperationProvider.Query(query, "partners", new { priceId = id, partner = providerId });

        }
    }
}
