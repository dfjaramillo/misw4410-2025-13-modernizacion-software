using System.Collections.Generic;
using Model.Custom.FormularioGridView;
using Model.Custom.Utils;

namespace Model.Custom.EvaluacionGridView
{
    public class EvaluacionConfigForGridView
    {
        public int IdEvaluacionConfig { get; set; }
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int IdFormulario { get; set; }        
        public List<CatalogModel> Forms { get; set; }
        public List<FormularioForGridView> Formularios { get; set; }
    }
}
