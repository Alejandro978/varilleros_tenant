-- Base de datos tenant de demo
-- Sincronizado con el modelo C# (entidades Precio, Presupuesto, Cliente, Perito, Articulo)
CREATE DATABASE IF NOT EXISTS varilleros_demo
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE varilleros_demo;

-- ── CLIENTE ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS cliente (
    id            INT          NOT NULL AUTO_INCREMENT,
    nombrecliente VARCHAR(200) NOT NULL,
    nifcif        VARCHAR(20)  NOT NULL,
    direccion     VARCHAR(300) NOT NULL,
    poblacion     VARCHAR(150) NOT NULL,
    email         VARCHAR(150) NOT NULL,
    telefono      VARCHAR(20)  NOT NULL,
    created_at    DATETIME     NOT NULL DEFAULT NOW(),
    updated_at    DATETIME     NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT cliente_pkey PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── PERITOS ───────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS peritos (
    id         BIGINT       NOT NULL AUTO_INCREMENT,
    nombre     VARCHAR(200) NOT NULL,
    email      VARCHAR(150),
    created_at DATETIME     NOT NULL DEFAULT NOW(),
    updated_at DATETIME     NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT peritos_pkey PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── ARTICULO ──────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS articulo (
    id                      INT          NOT NULL AUTO_INCREMENT,
    codigo                  VARCHAR(100) NOT NULL,
    descripcion             VARCHAR(500) NOT NULL,
    codigopreciopresupuesto VARCHAR(100),
    created_at              DATETIME     NOT NULL DEFAULT NOW(),
    updated_at              DATETIME     NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT articulo_pkey PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── PRECIOS ───────────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS precios (
    numeroabolladuras INT  NOT NULL,
    aletaleve         INT, aletamedio  INT, aletagrave  INT,
    puertaleve        INT, puertamedio INT, puertagrave INT,
    techoleve         INT, techomedio  INT, techograve  INT,
    capoleve          INT, capomedio   INT, capograve   INT,
    portonleve        INT, portonmedio INT, portongrave INT,
    montanteleve      INT, montantemedio INT, montantegrave INT,
    updated_at        DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT precios_pkey PRIMARY KEY (numeroabolladuras)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── PRESUPUESTO ───────────────────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS presupuesto (
    id          INT          NOT NULL AUTO_INCREMENT,
    reparador   VARCHAR(200),
    marca       VARCHAR(100),
    modelo      VARCHAR(100),
    matricula   VARCHAR(20),
    preciototal DECIMAL(10,2),

    ADIleve DECIMAL(10,2), ADItotaldanyoleve DECIMAL(10,2), ADImedio DECIMAL(10,2),
    ADItotaldanyomedio DECIMAL(10,2), ADIgrave DECIMAL(10,2), ADItotaldanyograve DECIMAL(10,2),
    ADIpintura TINYINT(1), ADIaluminio TINYINT(1), ADItotal DECIMAL(10,2),

    ADDleve DECIMAL(10,2), ADDtotaldanyoleve DECIMAL(10,2), ADDmedio DECIMAL(10,2),
    ADDtotaldanyomedio DECIMAL(10,2), ADDgrave DECIMAL(10,2), ADDtotaldanyograve DECIMAL(10,2),
    ADDpintura TINYINT(1), ADDaluminio TINYINT(1), ADDtotal DECIMAL(10,2),

    ATIleve DECIMAL(10,2), ATItotaldanyoleve DECIMAL(10,2), ATImedio DECIMAL(10,2),
    ATItotaldanyomedio DECIMAL(10,2), ATIgrave DECIMAL(10,2), ATItotaldanyograve DECIMAL(10,2),
    ATIpintura TINYINT(1), ATIaluminio TINYINT(1), ATItotal DECIMAL(10,2),

    ATDleve DECIMAL(10,2), ATDtotaldanyoleve DECIMAL(10,2), ATDmedio DECIMAL(10,2),
    ATDtotaldanyomedio DECIMAL(10,2), ATDgrave DECIMAL(10,2), ATDtotaldanyograve DECIMAL(10,2),
    ATDpintura TINYINT(1), ATDaluminio TINYINT(1), ATDtotal DECIMAL(10,2),

    PDIleve DECIMAL(10,2), PDItotaldanyoleve DECIMAL(10,2), PDImedio DECIMAL(10,2),
    PDItotaldanyomedio DECIMAL(10,2), PDIgrave DECIMAL(10,2), PDItotaldanyograve DECIMAL(10,2),
    PDIpintura TINYINT(1), PDIaluminio TINYINT(1), PDItotal DECIMAL(10,2),

    PDDleve DECIMAL(10,2), PDDtotaldanyoleve DECIMAL(10,2), PDDmedio DECIMAL(10,2),
    PDDtotaldanyomedio DECIMAL(10,2), PDDgrave DECIMAL(10,2), PDDtotaldanyograve DECIMAL(10,2),
    PDDpintura TINYINT(1), PDDaluminio TINYINT(1), PDDtotal DECIMAL(10,2),

    PTDleve DECIMAL(10,2), PTDtotaldanyoleve DECIMAL(10,2), PTDmedio DECIMAL(10,2),
    PTDtotaldanyomedio DECIMAL(10,2), PTDgrave DECIMAL(10,2), PTDtotaldanyograve DECIMAL(10,2),
    PTDpintura TINYINT(1), PTDaluminio TINYINT(1), PTDtotal DECIMAL(10,2),

    PTIleve DECIMAL(10,2), PTItotaldanyoleve DECIMAL(10,2), PTImedio DECIMAL(10,2),
    PTItotaldanyomedio DECIMAL(10,2), PTIgrave DECIMAL(10,2), PTItotaldanyograve DECIMAL(10,2),
    PTIpintura TINYINT(1), PTIaluminio TINYINT(1), PTItotal DECIMAL(10,2),

    CAPOleve DECIMAL(10,2), CAPOtotaldanyoleve DECIMAL(10,2), CAPOmedio DECIMAL(10,2),
    CAPOtotaldanyomedio DECIMAL(10,2), CAPOgrave DECIMAL(10,2), CAPOtotaldanyograve DECIMAL(10,2),
    CAPOpintura TINYINT(1), CAPOaluminio TINYINT(1), CAPOtotal DECIMAL(10,2),

    TECHOleve DECIMAL(10,2), TECHOtotaldanyoleve DECIMAL(10,2), TECHOmedio DECIMAL(10,2),
    TECHOtotaldanyomedio DECIMAL(10,2), TECHOgrave DECIMAL(10,2), TECHOtotaldanyograve DECIMAL(10,2),
    TECHOpintura TINYINT(1), TECHOaluminio TINYINT(1), TECHOtotal DECIMAL(10,2),

    PORTONleve DECIMAL(10,2), PORTONtotaldanyoleve DECIMAL(10,2), PORTONmedio DECIMAL(10,2),
    PORTONtotaldanyomedio DECIMAL(10,2), PORTONgrave DECIMAL(10,2), PORTONtotaldanyograve DECIMAL(10,2),
    PORTONpintura TINYINT(1), PORTONaluminio TINYINT(1), PORTONtotal DECIMAL(10,2),

    MIleve DECIMAL(10,2), MItotaldanyoleve DECIMAL(10,2), MImedio DECIMAL(10,2),
    MItotaldanyomedio DECIMAL(10,2), MIgrave DECIMAL(10,2), MItotaldanyograve DECIMAL(10,2),
    MIpintura TINYINT(1), MIaluminio TINYINT(1), MItotal DECIMAL(10,2),

    MDleve DECIMAL(10,2), MDtotaldanyoleve DECIMAL(10,2), MDmedio DECIMAL(10,2),
    MDtotaldanyomedio DECIMAL(10,2), MDgrave DECIMAL(10,2), MDtotaldanyograve DECIMAL(10,2),
    MDpintura TINYINT(1), MDaluminio TINYINT(1), MDtotal DECIMAL(10,2),

    fechaCreacion BIGINT,
    descuento     SMALLINT NOT NULL DEFAULT 0,
    observaciones TEXT,
    desmontajes   SMALLINT,
    estado        SMALLINT NOT NULL DEFAULT 1  COMMENT '1=Borrador 2=Enviado 3=Aceptado 4=Rechazado',
    nombrecliente VARCHAR(200),
    direccion     VARCHAR(300),
    poblacion     VARCHAR(150),
    nifcif        VARCHAR(20),
    email         VARCHAR(150),
    telefono      VARCHAR(20),
    aseguradora   VARCHAR(200),
    idPerito      BIGINT,

    created_at DATETIME NOT NULL DEFAULT NOW(),
    updated_at DATETIME NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT presupuesto_pkey PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ── Datos de prueba ───────────────────────────────────────────────────────────
INSERT INTO cliente (nombrecliente, nifcif, direccion, poblacion, email, telefono) VALUES
  ('Cliente Test 1', '12345678A', 'Calle Principal 1', 'Madrid',    'cliente1@test.com', '911234567'),
  ('Cliente Test 2', '87654321B', 'Calle Secundaria 2', 'Barcelona', 'cliente2@test.com', '933456789');

INSERT INTO peritos (nombre, email) VALUES
  ('Perito Test 1', 'perito1@test.com'),
  ('Perito Test 2', 'perito2@test.com');

INSERT INTO articulo (codigo, descripcion, codigopreciopresupuesto) VALUES
  ('ART001', 'Artículo de prueba 1', 'A1'),
  ('ART002', 'Artículo de prueba 2', 'A2');

INSERT INTO precios (numeroabolladuras, aletaleve, aletamedio, aletagrave, puertaleve, puertamedio, puertagrave) VALUES
  (1, 50, 75, 100, 45, 70, 95),
  (2, 60, 85, 115, 55, 80, 110),
  (3, 70, 95, 130, 65, 90, 125);
