using Model.Domain.Respuesta;
using System;

namespace Model.Custom.RespuestaGridView
{
    public class RespuestaDetalleForGridView
    {
        public int IdRespuestaDetalle { get; set; }
        public int IdREspuesta { get; set; }
        public int IdPreguntaDetalle { get; set; }
        public string EnunciadoPregunta { get; set; }
        public string ValorRespuesta;
        public string ValorRequerido { get; set; }
        public string ResultadoFinal { get; set; }
        public string Brecha { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool Deleted { get; set; }
        public RespuestaDetalleForGridView() { }
        public RespuestaDetalleForGridView(RespuestaDetalle model)
        {
            IdPreguntaDetalle = model.IdPreguntaDetalle;
            IdREspuesta = model.IdREspuesta;
            IdPreguntaDetalle = model.IdPreguntaDetalle;
            ValorRespuesta = model.ValorRespuesta;
            FechaCreacion = model.CreatedAt;
            Deleted = model.Deleted;
        }
    }
}
