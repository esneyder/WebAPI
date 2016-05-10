using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.App_Start;
using WebAPI.Models;
using YeiBD;

namespace WebAPI.Controllers
{
    public class TikectsController : ApiController
    {
        private YeiConnection yeiConnection = new YeiConnection(Config.nameserver, Config.namedatabase, Config.username, Config.Userpass, false);
        List<Tikects> areadata = new List<Tikects>();

        public IEnumerable<Tikects> Get()
        {
            GetTikect();
            return areadata;
        }
        private string GetTikect()
        {

            Tikects tikect;
            if (!yeiConnection.OpenConnection())
            {
                yeiConnection.CloseConnection();
            }
            yeiConnection.SQL = "select df.IdDetalleFactura,c.Nombres as Cocinero,f.FechaFactura, df.Plato,df.Importe from tblDetalleFactura df inner join tblFactura f on df.IdFactura = f.IdFactura inner join tblCocinero c on df.IdCocinero = c.IdCocinero";
            DataTable dt = yeiConnection.Search(false);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tikect = new Tikects();
                tikect.IdDetalleFactura = Convert.ToInt32(dt.Rows[i]["IdDetalleFactura"].ToString());
                tikect.FechaFactura = dt.Rows[i]["FechaFactura"].ToString();
                tikect.Cocinero = dt.Rows[i]["Cocinero"].ToString();
                tikect.Importe = Convert.ToDecimal(dt.Rows[i]["Importe"].ToString());
                tikect.Plato = dt.Rows[i]["Plato"].ToString();             

                areadata.Add(tikect);
            }
            var json = JsonConvert.SerializeObject(areadata);
            yeiConnection.CloseConnection();
            return json;
        }
    }
}
