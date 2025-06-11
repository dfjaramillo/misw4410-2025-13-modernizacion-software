using System.Collections.Generic;
using Model.Custom.Utils;

namespace Model.Custom.PreguntaGridView
{
    public class PreguntaDetalleForGridView
    {
        public int IdPregunta { get; set; }
        public int IdPreguntaDetalle { get; set; }
        public int DetalleItem { get; set; }        
        public string Valor { get; set; }
        public List<CatalogModel> DetallesItem { get; set; }
    }
}
