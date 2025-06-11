using Model.Custom.Utils;
using Model.Domain.Evaluacion;
using System.Collections.Generic;

namespace Model.Custom.EvaluacionGridView
{
    public class EvaluacionAsignadaForGridView
    {
        public int IdEvaluacionAsignada { get; set; }
        public int IdEvaluacion { get; set; }
        public string IdEvaluador { get; set; }
        public List<CatalogModel> Evaluadores { get; set; }
        public List<CatalogModel> Evaluaciones { get; set; }
        public bool Deleted { get; set; }

        public EvaluacionAsignadaForGridView()
        {
        }

        public EvaluacionAsignadaForGridView(EvaluacionAsignada model)
        {
            IdEvaluacionAsignada = model.IdEvaluacionAsignada;
            IdEvaluacion = model.IdEvaluacion;
            IdEvaluador = model.IdEvaluador;
            Deleted = model.Deleted;
        }
    }
}
