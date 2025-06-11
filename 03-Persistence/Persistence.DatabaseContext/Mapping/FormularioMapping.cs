using System.Data.Entity.ModelConfiguration;
using Model.Domain.Formulario;

namespace Persistence.DatabaseContext.Mapping
{
    public class FormularioMapping : EntityTypeConfiguration<Formulario>
    {
        public FormularioMapping()
        {
            Property(m=>m.IdFormulario).IsRequired();
            Property(m => m.Nombre).IsRequired().HasMaxLength(int.MaxValue);            
        }        
    }
}
