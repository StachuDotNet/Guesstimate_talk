using Nancy;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

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

            Post["/"] = _ =>
            {
                var newPerson = this.Bind<Person>();
                var session = docStore.OpenSession();

                session.Store(newPerson);
                session.SaveChanges();

                return Response.AsText(newPerson.Id.ToString())
                  .WithStatusCode(HttpStatusCode.Created);
            };
        }
    }
}