-- Crear la base de datos tenant de demo (varilleros_demo)
CREATE DATABASE IF NOT EXISTS varilleros_demo 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

USE varilleros_demo;

-- Tabla de clientes
CREATE TABLE IF NOT EXISTS cliente (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombrecliente VARCHAR(200) NOT NULL,
    nifcif VARCHAR(20) NOT NULL UNIQUE,
    direccion VARCHAR(255) NOT NULL,
    poblacion VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_nifcif (nifcif),
    INDEX idx_email (email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de peritos
CREATE TABLE IF NOT EXISTS peritos (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(200) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_email (email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de artículos
CREATE TABLE IF NOT EXISTS articulo (
    id INT PRIMARY KEY AUTO_INCREMENT,
    codigo VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NOT NULL,
    codigopreciopresupuesto INT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_codigo (codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de precios (tarifas)
CREATE TABLE IF NOT EXISTS precios (
    numeroabolladuras INT PRIMARY KEY,
    aletaleve INT,
    aletamedio INT,
    aletagrave INT,
    lateralleve INT,
    lateralmedio INT,
    lateralgrave INT,
    techoleve INT,
    techomedio INT,
    techograve INT,
    puertasleve INT,
    puertasmedio INT,
    puertasgrave INT,
    capotleve INT,
    capotmedio INT,
    capotgrave INT,
    pozonleve INT,
    pozonmedio INT,
    pozongrave INT,
    montantegrave INT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de presupuestos (~110 columnas agrupadas por panel)
CREATE TABLE IF NOT EXISTS presupuesto (
    id INT PRIMARY KEY AUTO_INCREMENT,
    cliente_id INT NOT NULL,
    perito_id BIGINT,
    estado VARCHAR(50) DEFAULT 'BORRADOR',
    
    -- Panel ADI (Aleta Derecha Interior)
    adileve INT,
    aditotaldanyoleve INT,
    adimedio INT,
    aditotaldanyomedio INT,
    adigrave INT,
    aditotaldanyograve INT,
    adipintura BOOLEAN,
    dialuminio BOOLEAN,
    aditotal INT,
    
    -- Panel ADD (Aleta Derecha Exterior)
    addleve INT,
    addtotaldanyoleve INT,
    addmedio INT,
    addtotaldanyomedio INT,
    addgrave INT,
    addtotaldanyograve INT,
    addpintura BOOLEAN,
    adaluminio BOOLEAN,
    addtotal INT,
    
    -- Panel ATI (Aleta Trasera Interior)
    atileve INT,
    atitotaldanyoleve INT,
    atimedio INT,
    atitotaldanyomedio INT,
    atigrave INT,
    atitotaldanyograve INT,
    atipintura BOOLEAN,
    atialuminio BOOLEAN,
    atitotal INT,
    
    -- Panel ATD (Aleta Trasera Exterior)
    atdleve INT,
    atdtotaldanyoleve INT,
    atdmedio INT,
    atdtotaldanyomedio INT,
    atdgrave INT,
    atdtotaldanyograve INT,
    atdpintura BOOLEAN,
    atdaluminio BOOLEAN,
    atdtotal INT,
    
    -- Panel PDI (Puerta Delantera Interior)
    pdileve INT,
    pditotaldanyoleve INT,
    pdimedio INT,
    pditotaldanyomedio INT,
    pdigrave INT,
    pditotaldanyograve INT,
    pdipintura BOOLEAN,
    pdialuminio BOOLEAN,
    pditotal INT,
    
    -- Panel PDD (Puerta Delantera Exterior)
    pddleve INT,
    pddtotaldanyoleve INT,
    pddmedio INT,
    pddtotaldanyomedio INT,
    pddgrave INT,
    pddtotaldanyograve INT,
    pddpintura BOOLEAN,
    pddaluminio BOOLEAN,
    pddtotal INT,
    
    -- Panel PTI (Puerta Trasera Interior)
    ptileve INT,
    ptitotaldanyoleve INT,
    ptimedio INT,
    ptitotaldanyomedio INT,
    ptigrave INT,
    ptitotaldanyograve INT,
    ptipintura BOOLEAN,
    ptialuminio BOOLEAN,
    ptitotal INT,
    
    -- Panel PTD (Puerta Trasera Exterior)
    ptdleve INT,
    ptdtotaldanyoleve INT,
    ptdmedio INT,
    ptdtotaldanyomedio INT,
    ptdgrave INT,
    ptdtotaldanyograve INT,
    ptdpintura BOOLEAN,
    ptdaluminio BOOLEAN,
    ptdtotal INT,
    
    -- Panel CAPO (Capota)
    capololeve INT,
    capoltotaldanyoleve INT,
    capolomedio INT,
    capoltotaldanyomedio INT,
    capolograve INT,
    capoltotaldanyograve INT,
    caplopintura BOOLEAN,
    caploaluminio BOOLEAN,
    capolitotal INT,
    
    -- Panel TECHO (Techo)
    techoleve INT,
    techototaldanyoleve INT,
    techomedio INT,
    techototaldanyomedio INT,
    techograve INT,
    techototaldanyograve INT,
    techopintura BOOLEAN,
    techoaluminio BOOLEAN,
    techototal INT,
    
    -- Panel PORTON (Portón)
    portonleve INT,
    portontotaldanyoleve INT,
    portonmedio INT,
    portontotaldanyomedio INT,
    portongrave INT,
    portontotaldanyograve INT,
    portonpintura BOOLEAN,
    portonaluminio BOOLEAN,
    portontotal INT,
    
    -- Panel MI (Montante Izquierdo)
    mileve INT,
    mitotaldanyoleve INT,
    mimedio INT,
    mitotaldanyomedio INT,
    migrave INT,
    mitotaldanyograve INT,
    mipintura BOOLEAN,
    mialuminio BOOLEAN,
    mitotal INT,
    
    -- Panel MD (Montante Derecho)
    mdleve INT,
    mdtotaldanyoleve INT,
    mdmedio INT,
    mdtotaldanyomedio INT,
    mdgrave INT,
    mdtotaldanyograve INT,
    mdpintura BOOLEAN,
    mdaluminio BOOLEAN,
    mdtotal INT,
    
    -- Resumen
    totalimporte DECIMAL(10, 2),
    observaciones TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (cliente_id) REFERENCES cliente(id) ON DELETE CASCADE,
    FOREIGN KEY (perito_id) REFERENCES peritos(id) ON DELETE SET NULL,
    INDEX idx_cliente_id (cliente_id),
    INDEX idx_perito_id (perito_id),
    INDEX idx_estado (estado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insertar datos de prueba
INSERT INTO cliente (nombrecliente, nifcif, direccion, poblacion, email, telefono) VALUES
    ('Cliente Test 1', '12345678A', 'Calle Principal 1', 'Madrid', 'cliente1@test.com', '911234567'),
    ('Cliente Test 2', '87654321B', 'Calle Secundaria 2', 'Barcelona', 'cliente2@test.com', '933456789');

INSERT INTO peritos (nombre, email) VALUES
    ('Perito Test 1', 'perito1@test.com'),
    ('Perito Test 2', 'perito2@test.com');

INSERT INTO articulo (codigo, descripcion, codigopreciopresupuesto) VALUES
    ('ART001', 'Artículo de prueba 1', 1),
    ('ART002', 'Artículo de prueba 2', 2);

INSERT INTO precios (numeroabolladuras, aletaleve, aletamedio, aletagrave) VALUES
    (1, 50, 75, 100),
    (2, 60, 85, 115),
    (3, 70, 95, 130);
