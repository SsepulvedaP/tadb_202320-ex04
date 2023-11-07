using tadb_202320_ex04.Models;
using MongoDB.Driver;

namespace tadb_202320_ex04.DbContexts
{
    public class MongoDbContext
    {
        private readonly string cadenaConexion;
        private readonly BusesDatabaseSettings Autobuses_BDDatabaseSettings;
        public MongoDbContext(IConfiguration unaConfiguracion)
        {
            cadenaConexion = unaConfiguracion.GetConnectionString("Mongo")!;
            Autobuses_BDDatabaseSettings = new BusesDatabaseSettings(unaConfiguracion);
        }

        public IMongoDatabase CreateConnection()
        {
            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase(Autobuses_BDDatabaseSettings.DatabaseName);

            return miDB;
        }

        public BusesDatabaseSettings configuracionColecciones
        {
            get { return Autobuses_BDDatabaseSettings; }
        }
    }
}