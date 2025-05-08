using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using NuGet.Configuration;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CommerceCore.ML.cc.sale.Quotes;

namespace CommerceCore.BL.cc.Menu
{
    public class MenuPlan : LogicBase
    {
        public MenuPlan(Configuration settings) {
            configuration = settings;
        }

        public dynamic GetMenuPlan(int aplicacion, string plan)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var menu = db.Menus
                        .Join(db.Agrupadors, m => m.Id, a => a.Menu, (m, a) => new { m, a })
                        .Join(db.Opcions, ma => ma.a.Id, o => o.Menu, (ma, o) => new
                        {
                            MenuId = ma.m.Id,
                            MenuNombre = ma.m.Nombre,
                            Aplicacion = ma.m.Aplicacion,
                            Plan = ma.m.plan,
                            AgrupadorId = ma.a.Id,
                            AgrupadorNombre = ma.a.Nombre,
                            OpcionId = o.Id,
                            OpcionNombre = o.Nombre,
                            OpcionRuta = o.Url
                        })
                        .Where(x => x.Aplicacion == aplicacion && x.Plan == plan)
                        .ToList();

                    return menu;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
