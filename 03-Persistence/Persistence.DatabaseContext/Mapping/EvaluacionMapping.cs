using System.Data.Entity.ModelConfiguration;
using Model.Domain.Evaluacion;


namespace Persistence.DatabaseContext.Mapping
{
   public class EvaluacionMapping : EntityTypeConfiguration<Evaluacion>
    {
        public EvaluacionMapping()
        {
            Property(m => m.IdEvaluacion).IsRequired();
            Property(m => m.Nombre).IsRequired().HasMaxLength(int.MaxValue);            
        }
    }
}
