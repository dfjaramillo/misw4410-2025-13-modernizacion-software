using System.Collections.Generic;
using Model.Custom.Utils;

namespace Model.Custom.FormularioGridView
{
    public class FormDetalleForGridView
    {
        public int IdFormularioDetalle { get; set; }
        public int IdFormulario { get; set; }
        public int FormDetalleItem { get; set; }
        public string Valor { get; set; }
        public List<CatalogModel> DetallesItem { get; set; }
    }
}
