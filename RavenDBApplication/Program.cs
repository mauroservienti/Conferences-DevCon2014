﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Shard;
using RavenDBApplication.Indexes;
using RavenDBApplication.Model;

namespace RavenDBApplication
{
	class Program
	{
		static void Main( string[] args )
		{
			var _store = new DocumentStore()
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "My"
			};
			_store.Initialize();

			using( var session = _store.OpenSession() )
			{
				var p = new Person()
				{
					FirstName = "Mauro",
					LastName = "Serventi"
				};

				session.Store( p );

				var order = new Order() 
				{
					Customer = p
				};

				session.Store( order );

				session.SaveChanges();

				var x = session.Load<Order>( order.Id );
			}

			using( var session = _store.OpenSession() )
			{
				session.Query<Order>()
					.Include<Order>( o => o.Customer.Id )
					.ToList();
			}




			//var store = Server.CreateDocumentStore();

			//#region setup Person/Order

			////using( var session = store.OpenSession() )
			////{
			////	var p1 = new Person()
			////	{
			////		FirstName = "Mauro",
			////		LastName = "Servienti"
			////	};

			////	session.Store( p1 );
			////	session.Store( new Order()
			////	{
			////		Customer = p1
			////	} );

			////	var p2 = new Person()
			////	{
			////		FirstName = "Nazareno",
			////		LastName = "Manco"
			////	};

			////	session.Store( p2 );
			////	session.Store( new Order()
			////	{
			////		Customer = p2
			////	} );

			////	var c1 = new Company()
			////	{
			////		Name = "topics.it"
			////	};

			////	session.Store( c1 );
			////	session.Store( new Order()
			////	{
			////		Customer = c1
			////	} );

			////	var c2 = new Company()
			////	{
			////		Name = "Managed Designs"
			////	};

			////	session.Store( c2 );
			////	session.Store( new Order()
			////	{
			////		Customer = c2
			////	} );

			////	session.SaveChanges();
			////}

			//#endregion

			//#region sample product setup

			////using( var session = store.OpenSession() )
			////{
			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 40
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );

			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 45
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );

			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 38
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );


			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 41
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );

			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 43
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );

			////	session.Store( new Product()
			////	{
			////		Name = "Trousers",
			////		Attributes = new List<ProductAttribute>()
			////		{
			////			new ProductAttribute()
			////			{
			////				Name = "Size",
			////				Value = new AttributeValue<int>()
			////				{ 
			////					Value = 39
			////				}
			////			},
			////			new ProductAttribute()
			////			{
			////				Name = "Color",
			////				Value = new AttributeValue<String>()
			////				{ 
			////					Value = "Blue"
			////				}
			////			}
			////		}
			////	} );

			////	session.SaveChanges();
			////}

			//#endregion

			//#region search people via linq

			//using( var session = store.OpenSession() )
			//{
			//	var notMoreThanTenPeople = session.Query<Person>()
			//		.Where( p => p.LastName.StartsWith( "S" ) || p.LastName.StartsWith( "M" ) )
			//		.Take( 10 )
			//		.ToList();
			//}

			//#endregion

			//#region full text query

			//using( var session = store.OpenSession() )
			//{
			//	var searchResults = session.Query<FullText_Search.SearchResult, FullText_Search>()
			//		.Search( r => r.Content, "ma*", escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards )
			//		.Take( 10 )
			//		.ProjectFromIndexFieldsInto<FullText_Search.SearchResult>()
			//		.ToList();
			//}

			//#endregion

			////using( var session = store.OpenSession() )
			////{
			////	var p = new Person();
			////	session.Store( p );
			////	session.SaveChanges();

			////	//var meta = session.Advanced.GetMetadataFor( p );

			////	//var etag = session.Advanced.GetEtagFor( p );

			////	RavenQueryStatistics stats;
			////	var result = session.Query<Person>()
			////		.Customize( c => 
			////		{
			////			//c.WaitForNonStaleResultsAsOf(etag);
			////		} )
			////		.Statistics( out stats )
			////		.Take(10)
			////		.ToArray();

			////	//stats.TotalResults
			////}


			//#region dynamic fields search

			//using( var session = store.OpenSession() )
			//{
			//	var query = session.Advanced.DocumentQuery<Product, Product_Search>()
			//		.Where( "Size: [40 TO 44]" )
			//		.AddOrder( "Size_Range", true, typeof( int ) );

			//	foreach( var item in query )
			//	{
			//		Console.WriteLine( "Product: {0} -> {1}", item.Name, item.Attributes.Single( a => a.Name == "Size" ).Value );
			//	}
			//}

			//#endregion
		}
	}
};
