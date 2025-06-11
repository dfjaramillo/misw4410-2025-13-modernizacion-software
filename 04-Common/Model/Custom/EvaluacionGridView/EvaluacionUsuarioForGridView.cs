using System.Collections.Generic;
using Model.Auth;
using Model.Custom.FormularioGridView;
using Model.Custom.PreguntaGridView;
using Model.Custom.UsuarioGridView;
using Model.Domain;
using Model.Domain.Evaluacion;

namespace Model.Custom.EvaluacionGridView
{
    public class EvaluacionUsuarioForGridView
    {
        public string Identificacion { get; set; }
        public string IdEvaluado { get; set; }
        public string IdEvaluacionAsignada { get; set; }
        public Evaluacion Evaluacion { get; set; }
        public UserForGridView Evaluado { get; set; }
        public List<EvaluacionDetalleForGridView> EvaluacionDetalles { get; set; }
        public List<EvaluacionConfigForGridView> EvaluacionConfig { get; set; }       
        public ExampleViewModel RespuestasModel { get; set; } 
       
    }
}