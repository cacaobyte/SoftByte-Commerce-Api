using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommerceCore.ML.cc.sale.Warehouse;

namespace CommerceCore.ML.cc.sale.Warehouse
{
    public class CategoriesList
    {
        public int categoryId { get; set; }

        public string name { get; set; } = null!;
        public bool status { get; set; }

        public virtual ICollection<SubCategoriesList> SubcategoriaList { get; set; } = new List<SubCategoriesList>();
    }
}
