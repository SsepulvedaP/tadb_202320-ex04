using tadb_202320_ex04.Models;
using MongoDB.Driver;

namespace tadb_202320_ex04.DbContexts
{
    public class MongoDbContext
    {
        private readonly string cadenaConexion;
        private readonly BusesDatabaseSettings _sistema_busesDatabaseSettings;
        public MongoDbContext(IConfiguration unaConfiguracion)
        {
            cadenaConexion = unaConfiguracion.GetConnectionString("Mongo")!;
            _sistema_busesDatabaseSettings = new BusesDatabaseSettings(unaConfiguracion);
        }

        public IMongoDatabase CreateConnection()
        {
            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase(_sistema_busesDatabaseSettings.DatabaseName);

            return miDB;
        }

        public BusesDatabaseSettings configuracionColecciones
        {
            get { return _sistema_busesDatabaseSettings; }
        }
    }
}