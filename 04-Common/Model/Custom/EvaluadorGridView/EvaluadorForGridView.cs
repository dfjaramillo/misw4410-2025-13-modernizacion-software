using System.Collections.Generic;
using Model.Custom.EvaluacionGridView;
using Model.Custom.UsuarioGridView;

namespace Model.Custom.EvaluadorGridView
{
    public class EvaluadorForGridView
    {
        public EvaluacionForGridView Evaluacion { get; set; }
        public EvaluacionAsignadaForGridView EvaluacionesAsignadas  { get; set; }
        public List<UsuarioAsignadoForGridView> UsuariosAsignados { get; set; }        
        public List<UsuarioForGridView> Usuarios{ get; set; }
    }
}
