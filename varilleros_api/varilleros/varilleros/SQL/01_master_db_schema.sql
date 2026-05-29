-- Crear la base de datos master de Varilleros
CREATE DATABASE IF NOT EXISTS varilleros_master 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

USE varilleros_master;

-- Tabla de módulos disponibles en el sistema
CREATE TABLE IF NOT EXISTS modules_catalog (
    id INT PRIMARY KEY AUTO_INCREMENT,
    code VARCHAR(50) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de tenants (empresas)
CREATE TABLE IF NOT EXISTS tenants (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    slug VARCHAR(100) UNIQUE NOT NULL,
    db_host VARCHAR(255) NOT NULL,
    db_port INT DEFAULT 3306,
    db_name VARCHAR(100) NOT NULL,
    db_user VARCHAR(100) NOT NULL,
    db_password VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NULL,
    is_active BOOLEAN DEFAULT TRUE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_slug (slug),
    INDEX idx_is_active (is_active)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de asignación de módulos a tenants
CREATE TABLE IF NOT EXISTS tenant_modules (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tenant_id INT NOT NULL,
    module_id INT NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    granted_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    expires_at DATETIME NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uk_tenant_module (tenant_id, module_id),
    FOREIGN KEY (tenant_id) REFERENCES tenants(id) ON DELETE CASCADE,
    FOREIGN KEY (module_id) REFERENCES modules_catalog(id) ON DELETE CASCADE,
    INDEX idx_tenant_id (tenant_id),
    INDEX idx_module_id (module_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insertar módulos de ejemplo
INSERT INTO modules_catalog (code, name, description, is_active) VALUES
    ('clientes', 'Gestión de Clientes', 'Módulo para gestionar clientes', TRUE),
    ('peritos', 'Gestión de Peritos', 'Módulo para gestionar peritos', TRUE),
    ('articulos', 'Gestión de Artículos', 'Módulo para gestionar artículos', TRUE),
    ('precios', 'Gestión de Precios', 'Módulo para gestionar tarifas de precios', TRUE),
    ('presupuestos', 'Gestión de Presupuestos', 'Módulo para crear y gestionar presupuestos', TRUE);

-- Insertar un tenant de ejemplo
INSERT INTO tenants (name, slug, db_host, db_port, db_name, db_user, db_password, is_active) VALUES
    ('Taller PDR Demo', 'demo', 'localhost', 3306, 'varilleros_demo', 'root', '1234', TRUE);
