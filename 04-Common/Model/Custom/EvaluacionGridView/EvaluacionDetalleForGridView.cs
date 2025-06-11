using Model.Custom.Utils;
using System.Collections.Generic;

namespace Model.Custom.EvaluacionGridView
{
    public class EvaluacionDetalleForGridView
    {
        public int IdEvaluacionDetalle { get; set; }
        public int IdEvaluacion { get; set; }
        public int EvaluacionDetalleItem { get; set; }
        public string Valor { get; set; }        
        public List<CatalogModel> DetallesItem { get; set; }
    }
}
