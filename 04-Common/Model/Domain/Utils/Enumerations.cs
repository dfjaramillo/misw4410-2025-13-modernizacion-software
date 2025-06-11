namespace Model.Domain.Utils
{
    public enum EnumEvaluacionCatalogo
    {
        Evaluacion = 101,
        EvaluacionDetalle = 102,
        EvaluacionConfig = 103

    }
    public enum EnumFormularioCatalogo
    {
        Formulario = 201,
        FormularioDetalle = 202,
        FormularioConfig = 203
    }
    public enum EnumPreguntaCatalogo
    {
        Pregunta = 301,
        PreguntaDetalle = 302,
        PreguntaDetalleCOnfig = 303
    }
    public enum EnumRespuestaCatalogo
    {
        Respuesta = 401,
        RespuestaDetalle = 402,
        
    }
    public enum EnumEvaluacionDetalleItem
    {        
        Instrucciones =102001
    }
    public enum EnumFormularioDetalleItem
    {
        Descripcion = 202001,
        Area = 202002,       
    }
    public enum EnumPreguntaDetalleItem
    {
        Enunciado = 302001        
    }
    public enum EnumRoles
    {
        Administrador,
        Evaluador
    }
}
