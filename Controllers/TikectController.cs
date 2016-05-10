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
    public class TikectController : ApiController
    {
        private YeiConnection yeiConnection = new YeiConnection(Config.nameserver, Config.namedatabase, Config.username, Config.Userpass, false);
        List<Tikect> areadata = new List<Tikect>();

        public IEnumerable<Tikect> Get()
        {
            GetTikect();
            return areadata;
        }
        private string GetTikect()
        {

            Tikect tikect;
            if (!yeiConnection.OpenConnection())
            {
                yeiConnection.CloseConnection();
            }
            yeiConnection.SQL = "select f.IdFactura, c.Nombres as Cliente,cc.Nombres as Camarero,m.Ubicacion,f.FechaFactura from tblFactura f inner join tblCliente c on f.IdCliente = c.IdCliente inner join tblCamarero cc on f.IdCamarero = cc.IdCamarero inner join tblMesa m on f.IdMesa = m.IdMesa";
            DataTable dt = yeiConnection.Search(false);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tikect = new Tikect();
                tikect.IdFactura = Convert.ToInt32(dt.Rows[i]["IdFactura"].ToString());
                tikect.Cliente =dt.Rows[i]["Cliente"].ToString();
                tikect.Camarero = dt.Rows[i]["Camarero"].ToString();
                tikect.Ubicacion = dt.Rows[i]["Ubicacion"].ToString();
                tikect.FechaFactura = dt.Rows[i]["FechaFactura"].ToString();

                areadata.Add(tikect);
            }
            var json = JsonConvert.SerializeObject(areadata);
            yeiConnection.CloseConnection();
            return json;
        }
    }
}
