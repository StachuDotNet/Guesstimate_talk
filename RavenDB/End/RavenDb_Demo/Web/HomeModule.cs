using Nancy;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class HomeModule : NancyModule
    {
        DocumentStore docStore;

        public HomeModule()
        {
            docStore = new DocumentStore { Url = "http://localhost:8081//" };
            docStore.Initialize();

            Get["/"] = _ =>
            {
                var session = docStore.OpenSession();

                return Response.AsJson(session.Query<Person>().ToList());
            };

            Get["/add/{name}"] = ctx =>
            {
                var session = docStore.OpenSession();
                
                var entity = new Person{Name = ctx.name};
                session.Store(entity);
                session.SaveChanges();

                return Response.AsText(entity.Id.ToString()).WithStatusCode(HttpStatusCode.Created);
            };
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}