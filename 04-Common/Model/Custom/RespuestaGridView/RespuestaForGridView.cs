using System;
using System.Collections.Generic;
using Model.Domain.Respuesta;

namespace Model.Custom.RespuestaGridView
{
    public class RespuestaForGridView
    {
        public int IdRespuesta { get; set; }
        public string IdEvaluador { get; set; }
        public string NombreEvaluador { get; set; }
        public string IdEvaluado { get; set; }
        public string NombreEvaluado { get; set; }
        public string EmailEvaluador { get; set; }
        public string EmailEvaluado { get; set; }
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool Deleted { get; set; }
        public List<RespuestaDetalle> Detalles { get; set; }

        public RespuestaForGridView() { }
        public RespuestaForGridView(Respuesta model)
        {
            IdRespuesta = model.IdRespuesta;
            IdEvaluador = model.IdEvaluador;
            IdEvaluado = model.IdEvaluado;
            IdEvaluacion = model.IdEvaluacion;
            FechaCreacion = model.CreatedAt;
            Deleted = model.Deleted;                    
        }
    }
}
