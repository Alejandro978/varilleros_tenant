-- =============================================================
-- MASTER DATABASE - varilleros_master
-- Motor: MySQL / MariaDB
-- Ejecutar conectado a la base de datos varilleros_master
--   USE varilleros_master;
-- =============================================================

-- -------------------------------------------------------------
-- 1. MODULES_CATALOG
--    Catálogo de módulos disponibles en el sistema
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS varilleros_master.modules_catalog (
    id          INT             NOT NULL AUTO_INCREMENT,
    code        VARCHAR(50)     NOT NULL   COMMENT 'Identificador único de módulo (ej: PRESUPUESTOS)',
    name        VARCHAR(200)    NOT NULL   COMMENT 'Nombre legible del módulo',
    description TEXT                       COMMENT 'Descripción opcional',
    is_active   TINYINT(1)      NOT NULL DEFAULT 1,
    created_at  DATETIME        NOT NULL DEFAULT NOW(),
    updated_at  DATETIME        NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT  modules_catalog_pkey PRIMARY KEY (id),
    CONSTRAINT  modules_catalog_code_key UNIQUE (code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Catálogo de módulos disponibles en el sistema multi-tenant';

-- -------------------------------------------------------------
-- 2. TENANTS
--    Registro de cada empresa/cliente del SaaS
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS varilleros_master.tenants (
    id          INT             NOT NULL AUTO_INCREMENT,
    name        VARCHAR(200)    NOT NULL               COMMENT 'Nombre comercial',
    slug        VARCHAR(100)    NOT NULL               COMMENT 'Identificador URL-safe, se usa como cabecera X-Tenant-Id',
    db_host     VARCHAR(255)    NOT NULL               COMMENT 'Host de su BBDD',
    db_port     SMALLINT        NOT NULL DEFAULT 3306  COMMENT 'Puerto de su BBDD',
    db_name     VARCHAR(100)    NOT NULL               COMMENT 'Nombre de su BBDD',
    db_user     VARCHAR(100)    NOT NULL               COMMENT 'Usuario de su BBDD',
    db_password VARCHAR(500)    NOT NULL               COMMENT 'Contraseña cifrada con AES o almacenada en vault',
    is_active   TINYINT(1)      NOT NULL DEFAULT 1,
    created_at  DATETIME        NOT NULL DEFAULT NOW(),
    updated_at  DATETIME        NOT NULL DEFAULT NOW() ON UPDATE NOW(),
    CONSTRAINT tenants_pkey PRIMARY KEY (id),
    CONSTRAINT tenants_slug_key UNIQUE (slug)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Registro de tenants (empresas) del sistema';

-- -------------------------------------------------------------
-- 3. TENANT_MODULES
--    Qué módulos tiene activos cada tenant
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS varilleros_master.tenant_modules (
    id          INT             NOT NULL AUTO_INCREMENT,
    tenant_id   INT             NOT NULL,
    module_id   INT             NOT NULL,
    is_active   TINYINT(1)      NOT NULL DEFAULT 1,
    granted_at  DATETIME        NOT NULL DEFAULT NOW(),
    expires_at  DATETIME                               COMMENT 'NULL = licencia indefinida',
    CONSTRAINT  tenant_modules_pkey    PRIMARY KEY (id),
    CONSTRAINT  tenant_modules_unique  UNIQUE (tenant_id, module_id),
    CONSTRAINT  tenant_modules_tenant_fk FOREIGN KEY (tenant_id)
                    REFERENCES varilleros_master.tenants (id) ON DELETE CASCADE,
    CONSTRAINT  tenant_modules_module_fk FOREIGN KEY (module_id)
                    REFERENCES varilleros_master.modules_catalog (id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
  COMMENT='Módulos habilitados por tenant';

-- -------------------------------------------------------------
-- 4. DATOS SEMILLA - modules_catalog
-- -------------------------------------------------------------
INSERT INTO varilleros_master.modules_catalog (code, name, description) VALUES
    ('CLIENTES',     'Clientes',     'Gestión de clientes'),
    ('PERITOS',      'Peritos',      'Gestión de peritos'),
    ('PRECIOS',      'Precios',      'Tabla de precios por abolladuras'),
    ('PRESUPUESTOS', 'Presupuestos', 'Creación y gestión de presupuestos de carrocería'),
    ('ARTICULOS',    'Artículos',    'Catálogo de artículos')
ON DUPLICATE KEY UPDATE name = VALUES(name);
