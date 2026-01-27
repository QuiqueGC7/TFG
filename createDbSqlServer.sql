CREATE DATABASE Mamba;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'Mamba';

USE Mamba;

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    DNI NVARCHAR(10) NOT NULL,
    Afiliado BIT
);

CREATE TABLE JugadoresNac (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Dorsal INT NOT NULL, 
    Posicion NVARCHAR(100) NOT NULL,
    Equipo NVARCHAR(100) NOT NULL
);

CREATE TABLE EstadisticasNac (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Puntos INT NOT NULL,
    Libres INT NOT NULL,
    EsAlcoholica BIT
);

CREATE TABLE Combo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PlatoPrincipal INT NOT NULL,
    Bebida INT NOT NULL,
    Postre INT NOT NULL,
    Descuento DECIMAL(10, 2) NOT NULL CHECK (Descuento >= 0)
);

INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

INSERT INTO Postre (Nombre, Precio, Calorias)
VALUES 
('Postre dulce', 5.00, 300),
('Postre dulzón', 8.00, 600);

INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
VALUES 
('Bebida mojada', 4.40, 1),
('Bebida húmeda', 5.70, 0);

INSERT INTO Combo (PlatoPrincipal, Bebida, Postre, Descuento)
VALUES 
(1, 1, 2, 0.20);

SELECT * FROM PlatoPrincipal;

SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

SELECT DISTINCT Nombre, Precio FROM PlatoPrincipal;


