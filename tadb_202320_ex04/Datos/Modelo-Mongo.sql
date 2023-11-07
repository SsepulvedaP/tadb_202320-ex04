
-- Proyecto: Sistema de Autobuses
-- Motor de Base de datos: MongoDB - 7.x

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen -- https://hub.docker.com/_/mongo
docker pull mongo:latest

-- Crear el contenedor
docker run --name Autobuses_BD -e “MONGO_INITDB_ROOT_USERNAME=admin” -e MONGO_INITDB_ROOT_PASSWORD=unaClav3 -p 27017:27017 -d mongo:latest

-- crear la base de datos
use Autobuses_BD;

-- Creamos las colecciones ... usando un json schema para validación

--Autobuses
db.createCollection("Autobuses", {
   validator: {
  $jsonSchema: {
    bsonType: 'object',
    title: 'Autobuses que operaran en el sistema',
    required: [
      'Marca'
    ],
    properties: {
      Marca: {
        bsonType: 'string',
        description: "'Marca' Debe ser una cadena de caracteres y no puede ser nulo"
      }
    }
  }
}
} );

-- Horarios_operacion
db.createCollection("Horarios_operacion", {
   validator: {
  $jsonSchema: {
    bsonType: 'object',
    title: 'Horarios_operacion',
    required: [
      'hora',
      'pico'
    ],
    properties: {
      hora: {
        bsonType: 'number',
        description: "'id_hora' Es el identificador único por hora y no puede ser nulo"
      },
      pico: {
        bsonType: 'bool',
        description: "'pico' Debe ser un dato tipo bool y no puede ser nulo"
      }
    }
  }
}
} );

--Programacion_autobuses
db.createCollection("Programacion_autobuses", {
   validator: {
  $jsonSchema: {
    bsonType: 'object',
    title: 'Hora a la que un bus está en operación',
    required: [
      'id_hora',
      'id_autobus'
    ],
    properties: {
      id_hora: {
        bsonType: 'number',
        description: "'id_hora' Es uno de los identificadores de Programacion_autobuses y no puede ser nulo"
      },
      id_autobus: {
        bsonType: 'string',
        description: "'id_autobus' Es uno de los identificadores de Programacion_autobuses y no puede ser nulo"
      }
    }
  }
}
} );

--Programacion_cargadores
db.createCollection("Programacion_cargadores", {
   validator: {
  $jsonSchema: {
    bsonType: 'object',
    title: 'Hora a la que un bus usa un cargador',
    required: [
      'id_hora',
      'id_autobus',
      'id_cargador'
    ],
    properties: {
      id_hora: {
        bsonType: 'number',
        description: "'id_hora' Es uno de los identificadores de Programacion_cargadores y no puede ser nulo"
      },
      id_autobus: {
        bsonType: 'string',
        description: "'id_autobus' Es uno de los identificadores de Programacion_cargadores y no puede ser nulo"
      },
      id_cargador: {
        bsonType: 'string',
        description: "'id_cargador' Es uno de los identificadores de Programacion_cargadores y no puede ser nulo"
      }
    }
  }
}
} );

--Cargadores
db.createCollection("Cargadores", {
   validator: {
  $jsonSchema: {
    bsonType: 'object',
    title: 'Cargadores del sistema',
    required: [
      'Voltaje'
    ],
    properties: {
      Voltaje: {
        bsonType: 'string',
        description: "'Voltaje' Es una cadena  y no puede ser nulo"
      }
    }
  }
}
} );
