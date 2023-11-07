namespace tadb_202320_ex04.Models
{
    public class BusesDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ColeccionBuses { get; set; } = null!;
        public string ColeccionCargadores { get; set; } = null!;
        public string ColeccionHoras { get; set; } = null!;
        public string ColeccionProgramacion_autobuses { get; set; } = null!;
        public string ColeccionProgramacion_cargadores { get; set; } = null!;

        public BusesDatabaseSettings(IConfiguration unaConfiguracion)
        {
            var configuracion = unaConfiguracion.GetSection("BusesDatabaseSettings");

            DatabaseName = configuracion.GetSection("DatabaseName").Value!;
            ColeccionBuses = configuracion.GetSection("ColeccionBuses").Value!;
            ColeccionCargadores = configuracion.GetSection("ColeccionCargadores").Value!;
            ColeccionHoras = configuracion.GetSection("ColeccionHoras").Value!;
            ColeccionProgramacion_autobuses = configuracion.GetSection("ColeccionProgramacion_autobuses").Value!;
            ColeccionProgramacion_cargadores = configuracion.GetSection("ColeccionProgramacion_cargadores").Value!;
        }
    }
}
