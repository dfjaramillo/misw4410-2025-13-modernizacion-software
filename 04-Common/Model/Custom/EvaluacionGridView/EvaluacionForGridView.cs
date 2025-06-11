using System;
using Model.Domain.Evaluacion;

namespace Model.Custom.EvaluacionGridView
{
    public class EvaluacionForGridView
    {
        public int IdEvaluacion { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool Deleted { get; set; }

        public EvaluacionForGridView() { }

        public EvaluacionForGridView(Evaluacion model)
        {
            IdEvaluacion = model.IdEvaluacion;
            Nombre = model.Nombre;
            FechaCreacion = model.CreatedAt;
            Deleted = model.Deleted;
        }
    }
}
