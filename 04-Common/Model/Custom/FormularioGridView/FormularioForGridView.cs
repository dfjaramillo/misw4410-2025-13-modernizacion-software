using System.Collections.Generic;
using Model.Custom.PreguntaGridView;

namespace Model.Custom.FormularioGridView
{
    public class FormularioForGridView
    {
        public int IdFormulario { get; set; }
        public string Nombre { get; set; }
        public List<FormDetalleForGridView> FormDetalle { get; set; }
        public List<FormConfigForGridView> FormConfig{ get; set; }
        public List<PreguntaForGridView> Preguntas { get; set; }
        public bool Deleted { get; set; }
    }
}
