using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Controllers
{    
    public class RestControllerBase  : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;        

        public RestControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;            
        }

        internal Link UrlLink(string relation, string routeName, object values)
        {
            ActionDescriptor route = null;
            foreach (var item in _routes)
            {
                if(item?.AttributeRouteInfo?.Name == routeName)
                {
                    route = item;
                    break;
                }
            }

            if(route == null)
                return new Link();

            //var route = _routes.FirstOrDefault(f => f.AttributeRouteInfo.Name.Equals(routeName));
            var method = route.ActionConstraints.OfType<HttpMethodActionConstraint>().First().HttpMethods.First();
            var url = Url.Link(routeName, values).ToLower();
            return new Link(url, relation, method);
        }

        internal PersonVO RestfulPerson(PersonVO personVO)
        {            
            personVO.Links.Add(UrlLink("all","GetPeople",null));
            personVO.Links.Add(UrlLink("_self","GetPersonId",new {id = personVO.Id}));

            return personVO;
        }

    }
}