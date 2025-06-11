using System.Collections.Generic;

namespace Model.Custom.PreguntaGridView
{
    public class PreguntaForGridView
    {
        public int IdPregunta { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<PreguntaDetalleForGridView> PreguntaDetalle { get; set; }
        public bool Deleted { get; set; }
    }
}
