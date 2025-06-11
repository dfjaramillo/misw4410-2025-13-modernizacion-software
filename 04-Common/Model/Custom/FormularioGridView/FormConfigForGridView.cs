using System.Collections.Generic;
using Model.Custom.PreguntaGridView;

namespace Model.Custom.FormularioGridView
{
    public class FormConfigForGridView
    {
        public int IdFormularioConfig { get; set; }
        public int IdFormulario { get; set; }
        public int IdPregunta { get; set; }
        public List<PreguntaForGridView> Preguntas { get; set; }

    }
}
