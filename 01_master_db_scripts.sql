-- ============================================================
--  MASTER DATABASE — varilleros_master
--  Generado desde MasterDbContext.cs
--  Ejecutar con usuario con privilegios suficientes
-- ============================================================

DROP DATABASE IF EXISTS varilleros_master;
CREATE DATABASE varilleros_master
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE varilleros_master;

-- ── tenants ──────────────────────────────────────────────────
CREATE TABLE tenants (
    id             INT          NOT NULL AUTO_INCREMENT,
    name           VARCHAR(200) NOT NULL,
    slug           VARCHAR(100) NOT NULL,
    db_host        VARCHAR(255) NOT NULL,
    db_port        INT          NOT NULL DEFAULT 3306,
    db_name        VARCHAR(100) NOT NULL,
    db_user        VARCHAR(100) NOT NULL,
    db_password    VARCHAR(500) NOT NULL,
    is_active      TINYINT(1)   NOT NULL DEFAULT 1,
    password_hash  VARCHAR(500)     NULL,
    created_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    UNIQUE KEY uq_tenants_slug (slug)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ── modules_catalog ──────────────────────────────────────────
CREATE TABLE modules_catalog (
    id          INT          NOT NULL AUTO_INCREMENT,
    code        VARCHAR(50)  NOT NULL,
    name        VARCHAR(200) NOT NULL,
    description VARCHAR(500) NOT NULL,
    is_active   TINYINT(1)   NOT NULL DEFAULT 1,
    created_at  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    UNIQUE KEY uq_modules_code (code)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ── tenant_modules ───────────────────────────────────────────
CREATE TABLE tenant_modules (
    id         INT       NOT NULL AUTO_INCREMENT,
    tenant_id  INT       NOT NULL,
    module_id  INT       NOT NULL,
    is_active  TINYINT(1) NOT NULL DEFAULT 1,
    granted_at DATETIME      NULL,
    expires_at DATETIME      NULL,
    created_at DATETIME  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME  NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY fk_tm_tenant (tenant_id),
    KEY fk_tm_module (module_id),
    CONSTRAINT fk_tm_tenant FOREIGN KEY (tenant_id) REFERENCES tenants(id) ON DELETE CASCADE,
    CONSTRAINT fk_tm_module FOREIGN KEY (module_id) REFERENCES modules_catalog(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ── Datos de ejemplo ─────────────────────────────────────────
INSERT INTO modules_catalog (code, name, description) VALUES
    ('CLIENTES',      'Clientes',      'Gestión de clientes'),
    ('PERITOS',       'Peritos',       'Gestión de peritos de seguros'),
    ('ARTICULOS',     'Artículos',     'Catálogo de artículos y repuestos'),
    ('PRECIOS',       'Precios',       'Tablas de precios por número de abolladuras'),
    ('PRESUPUESTOS',  'Presupuestos',  'Gestión completa de presupuestos PDR');

INSERT INTO tenants (name, slug, db_host, db_port, db_name, db_user, db_password, password_hash) VALUES
    ('Tenant Demo', 'demo', 'localhost', 3306, 'varilleros_demo', 'root', '',
     '$2a$11$GLB8QS/YxbHLmJf81UWl.O7Jphp0tgOqgAyVz9xIFYbMKgo2wjDxy');

-- Asignar todos los módulos al tenant demo
INSERT INTO tenant_modules (tenant_id, module_id, is_active, granted_at)
SELECT 1, id, 1, NOW() FROM modules_catalog;
