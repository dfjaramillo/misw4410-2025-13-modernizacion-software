using System.Collections.Generic;
using Model.Custom.EvaluacionGridView;
using Model.Custom.FormularioGridView;
using Model.Custom.UsuarioGridView;

namespace Model.Custom.RespuestaGridView
{
    public class RespuestaViewModel
    {
        public int IdRespuesta { get; set; }
        public RespuestaForGridView Respuesta { get; set; }
        public UserForGridView Evaluador { get; set; }
        public UserForGridView Evaluado { get; set; }
        public EvaluacionForGridView Evaluacion { get; set; }
        public FormularioForGridView Formulario { get; set; }
        public List<RespuestaDetalleForGridView> Detalles { get; set; }
        public string Grafica { get; set; }
    }
}
