using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommerceCore.ML.cc.sale.Warehouse;

namespace CommerceCore.ML.cc.sale.Warehouse
{
    public class SubCategoriesList
    {
        public int IdSubcategoria { get; set; }

        public int categoryId { get; set; }

        public string name { get; set; } = null!;
        public bool status { get; set; }
        public virtual CategoriesList IdCategoriaNavigation { get; set; } = null!;
    }
}
