using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using CommerceCore.DAL.Commerce;

namespace CommerceCore.BL
{
    public abstract class LogicBase
    {
        internal Configuration configuration { get; set; }
        internal General general { get; set; }

        protected readonly SoftByte db;
        //protected LogicBase(SoftByte dbContext)
        //{
        //    db = dbContext; // El contexto se comparte con todas las clases derivadas
        //}
    }
}
