using System.Data.Entity.ModelConfiguration;
using Model.Domain.Pegunta;

namespace Persistence.DatabaseContext.Mapping
{
    public class PreguntasMapping :EntityTypeConfiguration<Pregunta>
    {
        public PreguntasMapping()
        {
            Property(m => m.IdPregunta).IsRequired();
            Property(m => m.Titulo).IsRequired().HasMaxLength(int.MaxValue);                  
        }
    }
}
