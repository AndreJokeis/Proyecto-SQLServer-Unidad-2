CREATE DATABASE tallermecanico;

USE tallermecanico;

select * from tallermecanico.INFORMATION_SCHEMA.TABLES;

select * from tallermecanico.INFORMATION_SCHEMA.COLUMNS;

CREATE TABLE clientes(
    idCliente INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    RFC CHAR(13) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(200) NOT NULL,
    Telefono1 CHAR(10) NOT NULL,
    Telefono2 CHAR(10),
    Telefono3 CHAR(10),
    Correo NVARCHAR(320) NOT NULL UNIQUE,
    FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE vehiculos (    
    NumeroDeSerie INT PRIMARY KEY NOT NULL,
    idCliente INT NOT NULL,
    Placa NVARCHAR(7) NOT NULL UNIQUE,
    Marca NVARCHAR(20) NOT NULL,
    Modelo NVARCHAR(50) NOT NULL,
    Año INT NOT NULL,
    Color NVARCHAR(15) NOT NULL,
    Kilometraje INT NOT NULL,
    Tipo NVARCHAR(25),
    Antiguedad INT,
    CONSTRAINT FK_Vehiculo_Cliente FOREIGN KEY (idCliente)
        REFERENCES clientes (idCliente)
);

CREATE TABLE mecanicos(
    idMecanico INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    RFC CHAR(13) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Especialidad NVARCHAR(25),
    Telefono CHAR(10) NOT NULL,
    Salario DECIMAL(9,2) NOT NULL,
    AñosExperiencia INT NOT NULL
);

CREATE TABLE servicios(
    idServicio INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    NombreServicio NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Costo DECIMAL(7,2) NOT NULL,
    TiempoEstimado DECIMAL(3,1) NOT NULL
);

CREATE TABLE refacciones(
    idRefaccion INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Marca NVARCHAR(20) NOT NULL,
    PrecioUnitario DECIMAL(15,2) NOT NULL,
    Stock INT NOT NULL CHECK (Stock >= 0),
    StockMinimo INT NOT NULL CHECK (StockMinimo >= 0),
    Proveedor NVARCHAR(50) NOT NULL    
);
    

CREATE TABLE orden_servicios(
    Folio INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    FechaIngreso DATE NOT NULL,
    EntregaEstimada DATE NOT NULL,
    Estado NVARCHAR(15) NOT NULL DEFAULT 'Abierto' 
        CHECK (Estado IN ('Abierto', 'En proceso', 'Finalizado', 'Cancelado')),
    NumeroDeSerie INT NOT NULL,
    CostoTotal DECIMAL(8,2),
    CONSTRAINT FK_Orden_Vehiculo FOREIGN KEY (NumeroDeSerie) 
        REFERENCES vehiculos (NumeroDeSerie)
);

CREATE TABLE detalles_orden(
    idDetalle INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    idFolio INT NOT NULL,
    idServicio INT NOT NULL,
    PrecioAplicado DECIMAL(8,2),
    CONSTRAINT FK_Detalle_Orden FOREIGN KEY (idFolio) 
        REFERENCES orden_servicios (Folio),
    CONSTRAINT FK_Detalle_Servicio FOREIGN KEY (idServicio) 
        REFERENCES servicios (idServicio)
);

/*
CREATE TABLE orden_mecanicos(
    idOrden INT NOT NULL,
    Folio INT NOT NULL,
    idMecanico INT NOT NULL,
    PRIMARY KEY (idOrden, idMecanico),
    CONSTRAINT FK_OrdenMec_Folio FOREIGN KEY (Folio)
        REFERENCES orden_servicios (Folio),
    CONSTRAINT FK_OrdenMec_Mecanico FOREIGN KEY (idMecanico)
        REFERENCES mecanicos (idMecanico)
);*/

/*
CREATE TABLE orden_refacciones(
    idOrden INT NOT NULL,
    Folio INT NOT NULL,
    idRefaccion INT NOT NULL,
    PRIMARY KEY (idOrden, idRefaccion),
    CONSTRAINT FK_OrdenRef_Folio FOREIGN KEY (Folio)
        REFERENCES orden_servicios (Folio),
    CONSTRAINT FK_OrdenRef_Refaccion FOREIGN KEY (idRefaccion)
        REFERENCES refacciones (idRefaccion)
);*/

INSERT INTO clientes (RFC, Nombre, Direccion, Telefono1, Telefono2, Telefono3, Correo) VALUES
    ('GAMA800512H12', 'Andrés Martínez García', 'Calle Morelos 123, Centro, Moroleón', '4451234567', NULL, NULL, 'andres.mtz@gmail.com'),
    ('LOJS900120M34', 'Jorge López Sánchez', 'Av. del Trabajo 45, Uriangato', '4459876543', '4451112223', NULL, 'jorge.lopez@outlook.com'),
    ('GUPM851130H56', 'Max Guzmán Pérez', 'Portal Aldama 12, Moroleón', '4455556666', NULL, NULL, 'max.guzman@yahoo.com'),
    ('HEDF090315M78', 'Jesús Hernández Díaz', 'Calle 5 de Mayo 202, Uriangato', '4611203040', NULL, NULL, 'jesus.hdez@gmail.com'),
    ('RAOC880722H90', 'Carlos Ramírez Ortiz', 'Prolongación Juárez s/n, Moroleón', '4457778888', '4458889999', NULL, 'carlos.ramirez@hotmail.com'),
    ('VARC081205M11', 'Kevin Vázquez Ruiz', 'Col. La Joyita 56, Uriangato', '4454443322', NULL, NULL, 'kevin.vazquez@gmail.com'),
    ('TOLJ090210H22', 'José Torres Luna', 'Calle Hidalgo 89, Moroleón', '4459990000', NULL, NULL, 'jose.torres@live.com'),
    ('CAMM080418M33', 'Andrés Castillo Mendoza', 'Barrio de San Miguel, Uriangato', '4452223344', '4456667788', NULL, 'andres.castillo@gmail.com'),
    ('REFI090530H44', 'Iris Reyes Flores', 'Calle Mina 400, Moroleón', '4451012020', NULL, NULL, 'iris.reyes@gmail.com'),
    ('MOJE081010M55', 'Estrella Morales Jiménez', 'Av. Arbolada 12, Uriangato', '4453034040', NULL, NULL, 'estrella.morales@outlook.com');

INSERT INTO vehiculos (NumeroDeSerie, idCliente, Placa, Marca, Modelo, Año, Color, Kilometraje, Tipo, Antiguedad) VALUES
    (100200300, 1, 'GLN1234', 'Nissan', 'Sentra', 2020, 'Gris', 45000, 'Sedán', 6),
    (100200301, 2, 'GTO5678', 'Volkswagen', 'Jetta', 2018, 'Blanco', 82000, 'Sedán', 8),
    (100200302, 3, 'GTO9012', 'Toyota', 'Hilux', 2022, 'Rojo', 15000, 'Pick-up', 4),
    (100200303, 4, 'ABC3456', 'Chevrolet', 'Aveo', 2015, 'Azul', 120000, 'Sedán', 11),
    (100200304, 5, 'XYZ7890', 'Honda', 'CR-V', 2021, 'Negro', 35000, 'SUV', 5),
    (100200305, 6, 'GTO1122', 'Mazda', 'Mazda 3', 2019, 'Plata', 60000, 'Hatchback', 7),
    (100200306, 7, 'MOR3344', 'Ford', 'Ranger', 2017, 'Blanco', 95000, 'Pick-up', 9),
    (100200307, 8, 'URI5566', 'Kia', 'Rio', 2023, 'Azul Marino', 8000, 'Sedán', 3),
    (100200308, 9, 'GTO7788', 'Hyundai', 'Tucson', 2020, 'Arena', 52000, 'SUV', 6),
    (100200309, 10, 'GTO9900', 'Nissan', 'March', 2016, 'Blanco', 110000, 'Hatchback', 10);

INSERT INTO servicios (NombreServicio, Descripcion, Costo, TiempoEstimado) VALUES
    ('Cambio de Aceite Sintético', 'Incluye cambio de aceite 5W-30, filtro de aceite y revisión de niveles de líquidos (frenos, anticongelante y dirección).', 1250.00, 1.0),
    ('Afinación Mayor', 'Servicio completo que incluye cambio de bujías de iridio, filtro de aire, filtro de combustible, limpieza de cuerpo de aceleración y lavado de inyectores por laboratorio.', 2800.00, 4.0),
    ('Mantenimiento de Frenos', 'Limpieza y ajuste de frenos traseros, cambio de balatas delanteras y rectificado de discos para evitar vibraciones al frenar.', 1500.00, 2.5),
    ('Diagnóstico Eléctrico', 'Revisión técnica con escáner profesional para lectura de códigos de falla (Check Engine), revisión de batería y alternador.', 550.00, 1.5),
    ('Reparación de Suspensión', 'Sustitución de amortiguadores delanteros, rótulas y terminales de dirección para mejorar la estabilidad del vehículo.', 3200.00, 5.0),
    ('Carga de Aire Acondicionado', 'Detección de fugas con vacío y recarga de gas refrigerante R134a para optimizar el enfriamiento de la cabina.', 950.00, 1.0);

-- *************************
--      PROCEDIMIENTOS
-- *************************

-- Obtener clientes
go
CREATE PROCEDURE get_clientes
AS
BEGIN
    SELECT * FROM clientes;
END
go

-- Obtener cliente por id
go
CREATE PROCEDURE get_cliente
    @id INT
AS
BEGIN
    SELECT * FROM clientes
        WHERE idCliente = @id;
END
go

-- Obtener vehiculos por cliente
go
CREATE PROCEDURE get_vehiculos
    @idCliente INT
AS
BEGIN
    SELECT * FROM vehiculos
        WHERE idCliente = @idCliente;
END
go  

-- Obtener vehiculo por id
go
CREATE PROCEDURE get_vehiculo
    @id INT
AS
BEGIN
    SELECT * FROM vehiculos
        WHERE NumeroDeSerie = @id;
END
go

-- Obtener servicios
go
CREATE PROCEDURE get_servicios
AS
BEGIN
    SELECT * FROM servicios;
END
go

-- Obtener servicio por id
go
CREATE PROCEDURE get_servicio
    @id INT
AS
BEGIN
   SELECT * FROM servicios
        WHERE idServicio = @id;
END
go

-- Crear orden
CREATE PROCEDURE sp_InsertarOrden
    @idCliente INT,
    @numSerie VARCHAR(50)
AS
BEGIN
    INSERT INTO Orden_Servicios (idCliente, numSerie, fecha)
    VALUES (@idCliente, @numSerie, GETDATE());

    
    SELECT SCOPE_IDENTITY();
END


-- Insertar orden
GO
CREATE PROCEDURE sp_InsertarOrden
    @fechaEntrega DATE,
    @numSerie INT,
    @costoTotal DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO orden_servicios (FechaIngreso, EntregaEstimada, NumeroDeSerie, CostoTotal, Estado)
    VALUES (GETDATE(), @fechaEntrega, @numSerie, @costoTotal, 'Abierto');

    SELECT SCOPE_IDENTITY() AS FolioGenerado;
END
GO

CREATE PROCEDURE sp_InsertarDetalleOrden
    @idFolio INT,
    @idServicio INT,
    @precio DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO detalles_orden (idFolio, idServicio, PrecioAplicado)
    VALUES (@idFolio, @idServicio, @precio);
END
GO


-- ampliar Salario de Mecánicos
ALTER TABLE mecanicos 
ALTER COLUMN Salario DECIMAL(18,2) NOT NULL;

-- Ampliar Costo de Servicios
ALTER TABLE servicios 
ALTER COLUMN Costo DECIMAL(18,2) NOT NULL;

-- Ampliar PrecioUnitario de Refacciones
ALTER TABLE refacciones 
ALTER COLUMN PrecioUnitario DECIMAL(18,2) NOT NULL;

-- Ampliar CostoTotal de la Orden
ALTER TABLE orden_servicios 
ALTER COLUMN CostoTotal DECIMAL(18,2);

-- Ampliar PrecioAplicado en los detalles
ALTER TABLE detalles_orden 
ALTER COLUMN PrecioAplicado DECIMAL(18,2);