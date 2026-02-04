create database MambaTeam;
use MambaTeam;

create table Equipos (
    idEquipo INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    victorias INT DEFAULT 0,
    derrotas INT DEFAULT 0
);

create table Staff (
    idStaff INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    puesto NVARCHAR(50) NOT NULL,
    equipo NVARCHAR(100) NOT NULL
);

create table JugadoresNac (
    idJugador INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    posicion NVARCHAR(50) NOT NULL,
    idEquipo INT,
    dorsal INT,
    FOREIGN KEY (idEquipo) REFERENCES Equipos(idEquipo)
);

create table EstadisticasJugadorNac (
    idJugador INT PRIMARY KEY,
    puntos float,
    valoracion float,
    rebotes float,
    asistencias float,
    porLibres float,
    por2Pts float, 
    por3Pts float,
    FOREIGN KEY (idJugador) REFERENCES JugadoresNac(idJugador)
);

create table JugadoresArag (
    idJugador INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    posicion NVARCHAR(50) NOT NULL,
    idEquipo INT,
    dorsal INT
    FOREIGN KEY (idEquipo) REFERENCES Equipos(idEquipo)
);

create table EstadisticasJugadorArag (
    idJugador INT PRIMARY KEY,
    puntos float,
    libres float,
    porLibres float,
    dosPts float,
    tresPts float,
    FOREIGN KEY (idJugador) REFERENCES JugadoresArag(idJugador)
);

/* EQUIPOS */
INSERT INTO Equipos (nombre, victorias, derrotas)
VALUES ('Nacional A1', 9, 4);
INSERT INTO Equipos (nombre, victorias, derrotas)
VALUES ('Nacional A2 Femenino', 14, 0);
INSERT INTO Equipos (nombre, victorias, derrotas)
VALUES ('Nacional A2 Masculino', 4, 7);
INSERT INTO Equipos (nombre, victorias, derrotas)
VALUES ('2 Aragonesa', 8, 5);
INSERT INTO Equipos (nombre, victorias, derrotas)
VALUES ('3 Aragonesa', 10, 1);

/* STAFF */
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('MARIO SERANO', 'PRESIDENTE', 'DIRECCION GENERAL');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('FERNANDO SERANO', 'VICEPRESIDENTE', 'AREA MEDICA');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('ISABEL ASENSIO', 'FISIOTERAPIA', 'DIRECCION GENERAL');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('DAVID VIJUESCA', 'ENTRENADOR PRINCIPAL', 'NACIONAL A1');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('MARCO ANTONIO DOMINGUEZ', 'ENTRENADOR AYUDANTE', 'NACIONAL A1');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('SERGIO FRANCES', 'ENTRENADOR PRINCIPAL', 'NACIONAL A2 FEMENINO');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('ADRIAN LLEYDA', 'ENTRENADOR PRINCIPAL', 'NACIONAL A2 MASCULINO');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('LUIS MONTAÑES', 'ENTRENADOR AYUDANTE', 'NACIONAL A2 MASCULINO');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('JAVIER GARCIA', 'ENTRENADOR PRINCIPAL', '2 ARAGONESA');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('JAVIER GARCIA', 'ENTRENADOR AYUDANTE', '2 ARAGONESA');
INSERT INTO Staff (nombre, puesto, equipo)
VALUES ('DAVID SOLANAS', 'ENTRENADOR PRINCIPAL', '3 ARAGONESA');

/* JUGADORES NACIONAL A1 12 FICHAS */
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('EDUARDO JIMENEZ', 'PIVOT', 1, 1);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('DANIEL SANZ', 'BASE', 1, 8);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('NACHO SERRANO', 'ESCOLTA', 1, 11);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('NACHO SANZ', 'ALA PIVOT', 1, 13);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('MARCOS FRANCO ', 'ESCOLTA', 1, 14);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('NATANEL JIMENEZ', 'ALA PIVOT', 1, 15);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JORGE GARCIA', 'BASE', 1, 16);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('LORIEN ALLUE', 'ALERO', 1, 18);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('YASSINE KHERRAB', 'PIVOT', 1, 22);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('ALVARO LINACERO', 'ALERO', 1, 23);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('LUIS ANDRES', 'ESCOLTA', 1, 32);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JAIME GASCON', 'PIVOT', 1, 95);

/* JUGADORES NACIONAL A2 FEMENINO 11 FICHAS */
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('PAULA GALLAN', 'ALERO', 2, 4);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('SOFIA MORENO', 'ALERO', 2, 5);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('SARA LOPEZ', 'ALA PIVOT', 2, 6);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('MARIA JABAL', 'BASE', 2, 10);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('ALBA AZNAR', 'PIVOT', 2, 12);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('PUERTO ARTAL', 'BASE', 2, 14);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('LAIA PALAGUERRI', 'ESCOLTA', 2, 15);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('VANESA ASO', 'PIVOT', 2, 16);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('ALICIA SANCHEZ', 'ALA PIVOT', 2 , 22);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JULIA PLOU', 'ESCOLTA', 2, 21);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('CARMEN URRIES', 'ALERO', 2, 23);

/* JUGADORES NACIONAL A2 MASCULINO 15 FICHAS */
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('NATHAN ABUY', 'ALERO', 3, 0);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JOEL CEA', 'BASE', 3, 1);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('LUIS MONTAÑES', 'ESCOLTA', 3, 3);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('LUIS BAQUEDANO', 'PIVOT', 3, 4);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('ALEJANDRO FISAC', 'BASE', 3, 5);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JAVIER GARCIA', 'ESCOLTA', 3, 7);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('SERGIO RUIZ', 'ALA PIVOT', 3, 9);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('MARIO MOLINA', 'ESCOLTA', 3, 10);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('SERGIO MONTAÑES', 'BASE', 3, 11);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('ISRAEL CEA', 'ALERO', 3, 14);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('PAU RODRIGUEZ', 'ESCOLTA', 3, 17);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('SERGIO LLES', 'ALERO', 3, 20);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('JORGE BELTRAN', 'PIVOT', 3 , 24);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('CARLOS CANO', 'PIVOT', 3, 34);
INSERT INTO JugadoresNac (nombre, posicion, idEquipo, dorsal)
VALUES ('NACHO AUSERE', 'ALA PIVOT', 3, 44);

/* JUGADORES 2 ARAGONESA 16 FICHAS */
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JOSE LUIS MUÑOZ', 'ALERO', 4, 1);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('LUIS MONTAÑES', 'ESCOLTA', 4, 3);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('AMIT EGIDO', 'ALA PIVOT', 4, 4);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('WILVER VELOZ', 'PIVOT', 4, 6);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('MILO GONZALEZ', 'BASE', 4, 8);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('QUIQUE GARCIA', 'ALA PIVOT', 4, 13);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('MARIO AGUILA', 'ALA PIVOT', 4, 16);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('PAU RODRIGUEZ', 'ESCOLTA', 4, 17);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('ALVARO RODRIGUEZ', 'ALERO', 4, 19);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('EZEQUIEL DE LOS SANTOS', 'BASE', 4, 21);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('IKER ESTEVE', 'PIVOT', 4, 25);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('DIEGO ARCOS', 'ESCOLTA', 4, 27);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('DANIEL COXI', 'PIVOT', 4, 35);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('IÑAKO LASA', 'ALA PIVOT', 4, 41);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JOEL LOPEZ', 'ALERO', 4, 47);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('DIEGO LEIRO', 'ALERO', 4 , 50);

/* JUGADORES 3 ARAGONESA 16 FICHAS */
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JESUS URRIZA', 'BASE', 5, 0);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JOSE LUIS MUÑOZ', 'ALA PIVOT', 5, 1);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('SAMUEL ARTIGAS', 'PIVOT', 5, 3);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('PABLO FUMANAL', 'ALA PIVOT', 5, 7);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('MARIO SERRANO', 'ESCOLTA', 5, 8);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('NACHO SERRANO', 'ESCOLTA', 5 , 11);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('ENRIQUE ESTAUN', 'PIVOT', 5, 13);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('SAMUEL FONDEVILA', 'ESCOLTA', 5, 15);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JOSE ANTONIO VALLESPIN', 'PIVOT', 5, 16);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('SERGIO FRANCES', 'BASE', 5, 17);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('PABLO GAMBAU', 'ALERO', 5, 24);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('ERIC CAMARERO', 'ALA PIVOT', 5, 30);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('JUAN ASENSIO', 'ALA PIVOT', 5, 33);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('NACHO AUSERE', 'ALA PIVOT', 5, 44);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('DIEGO SERRANO', 'ALERO', 5, 71);
INSERT INTO JugadoresArag (nombre, posicion, idEquipo, dorsal)
VALUES ('ANGEL BENITO', 'ALERO', 5, 75);


/* ESTADISTICAS JUGADORES NACIONAL */
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (1, 6.4, 5.6, 5.3, 0.4, 40, 37, 53.8);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (2, 9.3, 11.3, 2.9, 3.4, 69, 48.7, 39.6);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (3, 12.4, 11.1, 2.4, 1, 70.6, 46.2, 44.6);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (4, 12.3, 13.9, 5.9, 1.3, 67.1, 45.1, 28.6);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (5, 3.2, 2.9, 2.1, 1.2, 54.5, 34.3, 16.7);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (6, 5, 7.5, 4, 0.7, 69.2, 50, 33.3);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (7, 8.4, 7.9, 2.1, 2.3, 65, 35.5, 30.3);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (8, 3.7, 2.2, 2.5, 0.8, 53.3, 34.1, 16);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (9, 3.9, 3.9, 4.8, 0.6, 65, 31.6, 42.9);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (10, 2.5, 1.5, 1.9, 0.6, 100, 50, 10);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (11, 16.2, 17.4, 4.8, 1.5, 61.5, 61, 28.3);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (12, 5, 4.5, 3.8, 0.2, 25, 53.7, 40);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (13, 17.7, 22.1, 8, 1.9, 81, 57.9, 34.5);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (14, 8.6, 7.9, 2.9, 1.1, 77.8, 43.6, 35.4);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (15, 5.6, 10.1, 4.3, 2.4, 70.6, 54.5, 37.5);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (16, 5.8, 9.9, 4, 3.2, 72.7, 56.8, 21.4);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (17, 7.8, 10.3, 5.1, 1.6, 75, 44.6, 21.4);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (18, 1.5, 2.5, 3.1, 1.3, 25, 32, 0);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (19, 9, 7.8, 3, 1.2, 53.8, 59.2, 25.7);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (20, 10.6, 12.1, 7.3, 0.8, 64.7, 49.2, 0);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (21, 3.4, 2.5, 4.8, 0.8, 38.9, 28.6, 25);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (22, 3.9, 4, 2.8, 2.1, 66.7, 37.3, 0);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (23, 5.3, 2.8, 2.4, 0.6, 45.5, 43.6, 28.6);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (24, 2.8, 1.7, 2, 0.3, 50, 45.5, 10);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (25, 9, 3.2, 1.5, 1.1, 64.8, 41.8, 20.5);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (26, 11.7, 11.2, 4.5, 0.8, 73.3, 47.1, 29);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (27, 4.1, 5.1, 5.1, 1.2, 56.5, 33.3, 0);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (28, 4.9, 7.9, 4.3, 2.7, 56.3, 42.1, 25);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (29, 3.8, 0.5, 1.8, 0.8, 100, 22.2, 26.3);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (30, 9.2, 7.6, 4.5, 1, 55.6, 45.5, 43.6);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (31, 4, 6.1, 2.5, 1.2, 80, 81.3, 18.2);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (32, 7.5, 5.2, 3.5, 1.2, 59.1, 33.3, 45.5);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (33, 11.5, 10, 3.9, 2.4, 73.1, 48.6, 26);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (34, 3, 3.5, 1, 2.5, 50, 50, 50);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (35, 5, 2, 5, 0.7, 0, 46.2, 12.5);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (36, 0, 0, 0.5, 1.5, 0, 0, 0);
INSERT INTO EstadisticasJugadorNac (idJugador, puntos, valoracion, rebotes, asistencias, porLibres, por2Pts, por3Pts)
VALUES (38, 7.1, 8.8, 5.3, 0.9, 52.2, 43.2, 25.7);


/* ESTADISTICAS JUGADORES ARAGONESA */
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (1, 2.9, 0.2, 50, 0.82, 0.4);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (2, 19.8, 4.1, 79, 3.3, 3);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (3, 4.8, 1.1, 57.9, 1.1, 0.5);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (4, 3, 0, 0, 1.5, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (5, 2.6, 0.2, 50, 0.8, 0.3);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (6, 5.7, 1.1, 91.7, 1.7, 0.4);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (7, 0, 0, 0, 0, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (8, 7.2, 0.9, 52.9, 1.8, 0.9);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (9, 1.1, 0, 0, 0.4, 0.1);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (10, 5.2, 0.6, 23.8, 1.3, 0.7);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (11, 5, 2, 51.6, 1.5, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (12, 7.8, 0.9, 31.4, 2.6, 0.6);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (13, 6.8, 0.5, 62.5, 3, 0.1);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (14, 1.5, 0.6, 50, 0.3, 0.1);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (15, 4.4, 1.3, 50, 0.6, 0.7);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (16, 8, 0.8, 47.1, 2.7, 0.6);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (17, 0, 0, 0, 0, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (18, 10, 0.8, 57.1, 2.2, 1.6);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (19, 8.1, 1.1, 44, 3.5, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (20, 3.3, 0.4, 37.5, 0.9, 0.4);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (21, 7.4, 2.6, 70.7, 1, 0.9);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (22, 19, 2.8, 78.6, 3.3, 3.3);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (23, 8.1, 1.4, 61.1, 3.4, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (24, 5.8, 0.8, 100, 1.6, 0.6);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (25, 4.1, 1.2, 30.6, 1.4, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (26, 7.7, 0, 0, 1.3, 1.7);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (27, 7.4, 1.3, 52.9, 2, 0.7);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (28, 6, 1.3, 65, 2.2, 0.1);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (29, 2, 0.5, 66.7, 0.8, 0);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (30, 6.5, 0.2, 50, 0.7, 1.7);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (31, 3.3, 0.3, 50, 1, 0.3);
INSERT INTO EstadisticasJugadorArag (idJugador, puntos, libres, porLibres, dosPts, tresPts)
VALUES (32, 16.1, 2, 47.6, 3.3, 2.5);
