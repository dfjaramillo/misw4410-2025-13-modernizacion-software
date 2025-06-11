using System.Collections.Generic;
using System.Web.Mvc;
using Model.Custom.EvaluacionGridView;
using Model.Custom.Utils;

namespace Model.Custom.UsuarioGridView
{
    public class UsuarioAsignadoForGridView
    {
        public int IdUsuarioAsignado { get; set; }
        public int IdEvaluacionAsignada { get; set; }
        public string IdEvaluado { get; set; }
        public bool IsEvaluado { get; set; }
        public bool Deleted { get; set; }
        public UserForGridView Evaluador{ get; set; }
        public EvaluacionForGridView Evaluacion { get; set; }
        public List<SelectListItem> Evaluaciones{ get; set; }
        public List<UserForGridView> UsuariosAsignados { get; set; }
        public List<CatalogModel> Usuarios { get; set; }
    }
}
