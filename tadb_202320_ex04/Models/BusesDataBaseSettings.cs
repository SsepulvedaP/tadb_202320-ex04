namespace tadb_202320_ex04.Models
{
    public class BusesDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ColeccionAutobus { get; set; } = null!;
        public string ColeccionCargador { get; set; } = null!;
        public string ColeccionDisp_Autobus { get; set; } = null!;
        public string ColeccionDisp_Cargador { get; set; } = null!;
        public string ColeccionHorario { get; set; } = null!;


        public BusesDatabaseSettings(IConfiguration unaConfiguracion)
        {
            var configuracion = unaConfiguracion.GetSection("BusesDatabaseSettings");

            DatabaseName = configuracion.GetSection("DatabaseName").Value!;
            ColeccionAutobus = configuracion.GetSection("ColeccionAutobus").Value!;
            ColeccionCargador = configuracion.GetSection("ColeccionCargador").Value!;
            ColeccionDisp_Autobus = configuracion.GetSection("ColeccionDips_Autobus").Value!;
            ColeccionDisp_Cargador = configuracion.GetSection("ColeccionDisp_Cargador").Value!;
            ColeccionHorario = configuracion.GetSection("ColeccionHorario").Value!;

        }
    }
}