using NWWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NWWebApi.Controllers
{
    [EnableCors("*", headers: "*", methods: "*")]
    public class CodeApiController : ApiController
    {
        public accordionManager GetCodeCode(string code)
        {
            accordionManager result= new accordionManager();

             result.client  = HelperCode.GetCode(code +".json");
             result.server = HelperCode.GetCode(GetServerCode(code));
            foreach( var c in result.client)
                {
                c.copyid = "A" + c.id;
                }
            foreach (var c in result.server)
            {
                c.copyid = "A" + c.id;
            }
            return result;
        }
        private string GetServerCode(string filename)
        {
           if (filename.Contains("category"))
            {
                return "categoryServer.json";
            }
            if (filename.Contains("customer"))
            {
                return "customersServer.json";
            }
            if (filename.Contains("order"))
            {
                return "ordersServer.json";
            }
            if (filename.Contains("CreateOrd"))
            {
                return "CreateOrdServer.json";
            }
            if (filename.Contains("CreateOrd"))
            {
                return "CreateOrdServer.json";
            }
            if (filename.Contains("product"))
            {
                return "productServer.json";
            }
            if (filename.Contains("shipper"))
            {
                return "shipperServer.json";
            }
            if (filename.Contains("suppliers"))
            {
                return "suppliersServer.json";
            }
            return "categoryServer.json";

        }
    }
}
