CREATE DATABASE Clinica_Prueba
GO
USE Clinica_Prueba
GO

-- TABLA DE ROLES
CREATE TABLE Roles (
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) NOT NULL,
    DescripcionRol NVARCHAR(255)
);

-- TABLA DE USUARIOS
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    ContraseñaHash NVARCHAR(255) NOT NULL,
    IdRol INT NOT NULL,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol) ON DELETE CASCADE
);

-- TABLA DE ESPECIALIDADES MÉDICAS
CREATE TABLE Especialidades (
    IdEspecialidad INT PRIMARY KEY IDENTITY(1,1),
    NombreEspecialidad NVARCHAR(100) NOT NULL,
    DescripcionEspecialidad NVARCHAR(255)
);

-- TABLA DE MÉDICOS
CREATE TABLE Medicos (
    IdMedico INT PRIMARY KEY IDENTITY(1,1),
    NombreMedico NVARCHAR(100) NOT NULL,
    ApellidoMedico NVARCHAR(100) NOT NULL,
    IdEspecialidad INT NOT NULL,
    TelefonoMedico NVARCHAR(20),
    EmailMedico NVARCHAR(100),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdEspecialidad) REFERENCES Especialidades(IdEspecialidad) ON DELETE CASCADE
);

-- TABLA DE PACIENTES
CREATE TABLE Pacientes (
    IdPaciente INT PRIMARY KEY IDENTITY(1,1),
    NombrePaciente NVARCHAR(100) NOT NULL,
    ApellidoPaciente NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE,
    TelefonoPaciente NVARCHAR(20),
    EmailPaciente NVARCHAR(100),
    DireccionPaciente NVARCHAR(255),
    Activo BIT DEFAULT 1
);

-- TABLA DE CITAS
CREATE TABLE Citas (
    IdCita INT PRIMARY KEY IDENTITY(1,1),
    IdPaciente INT NOT NULL,
    IdMedico INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'Programada',
    Notas NVARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente) ON DELETE CASCADE,
    FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico) ON DELETE CASCADE
);

-- Insertar Roles
INSERT INTO Roles (NombreRol, DescripcionRol) VALUES 
('Administrador', 'Acceso completo al sistema'),
('Médico', 'Puede ver sus citas y actualizar historiales'),
('Recepcionista', 'Puede agendar citas y gestionar pacientes');

-- Insertar Usuarios
INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, IdRol) VALUES
('admin', '$2a$11$r9cZ...', 1),
('medico', '$2a$11$r9cZ...', 2),
('recepcion', '$2a$11$r9cZ...', 3);

-- Insertar Especialidades
INSERT INTO Especialidades (NombreEspecialidad, DescripcionEspecialidad) VALUES 
('Cardiología', 'Especialidad en enfermedades del corazón'),
('Pediatría', 'Medicina para niños y adolescentes'),
('Dermatología', 'Especialidad en enfermedades de la piel'),
('Ginecología', 'Salud femenina y reproductiva'),
('Traumatología', 'Especialidad en huesos y articulaciones');

-- Insertar Médicos
INSERT INTO Medicos (NombreMedico, ApellidoMedico, IdEspecialidad, TelefonoMedico, EmailMedico) VALUES
('Carlos', 'Gómez', 1, '555-1234', 'carlos.gomez@clinica.com'),
('Ana', 'López', 2, '555-5678', 'ana.lopez@clinica.com'),
('Miguel', 'Rodríguez', 3, '555-9012', 'miguel.rodriguez@clinica.com');

-- Insertar Pacientes
INSERT INTO Pacientes (NombrePaciente, ApellidoPaciente, FechaNacimiento, TelefonoPaciente, EmailPaciente, DireccionPaciente) VALUES
('María', 'García', '1985-03-15', '555-1111', 'maria.garcia@email.com', 'Calle Principal 123'),
('Juan', 'Martínez', '1990-07-22', '555-2222', 'juan.martinez@email.com', 'Avenida Central 456'),
('Laura', 'Hernández', '1978-11-30', '555-3333', 'laura.hernandez@email.com', 'Plaza Mayor 789');

-- Insertar Citas
INSERT INTO Citas (IdPaciente, IdMedico, FechaHora, Estado, Notas) VALUES
(1, 1, '2024-01-15 09:00:00', 'Programada', 'Consulta de rutina'),
(2, 2, '2024-01-15 10:30:00', 'Programada', 'Control pediátrico'),
(3, 3, '2024-01-16 11:00:00', 'Programada', 'Revisión dermatológica');
GO

-- Vista de Usuarios con Roles
CREATE VIEW Vista_UsuariosConRoles AS
SELECT 
    U.IdUsuario,
    U.NombreUsuario,
    R.NombreRol,
    R.DescripcionRol,
    U.Activo,
    U.FechaCreacion
FROM Usuarios U
INNER JOIN Roles R ON U.IdRol = R.IdRol;

-- Vista de Médicos con Especialidades
CREATE VIEW Vista_MedicosConEspecialidades AS
SELECT 
    M.IdMedico,
    M.NombreMedico,
    M.ApellidoMedico,
    E.NombreEspecialidad,
    M.TelefonoMedico,
    M.EmailMedico,
    M.Activo
FROM Medicos M
INNER JOIN Especialidades E ON M.IdEspecialidad = E.IdEspecialidad;

-- Vista de Citas detallada
CREATE VIEW Vista_CitasDetalladas AS
SELECT 
    C.IdCita,
    P.NombrePaciente + ' ' + P.ApellidoPaciente AS NombrePaciente,
    M.NombreMedico + ' ' + M.ApellidoMedico AS NombreMedico,
    E.NombreEspecialidad,
    C.FechaHora,
    C.Estado,
    C.Notas,
    C.FechaCreacion
FROM Citas C
INNER JOIN Pacientes P ON C.IdPaciente = P.IdPaciente
INNER JOIN Medicos M ON C.IdMedico = M.IdMedico
INNER JOIN Especialidades E ON M.IdEspecialidad = E.IdEspecialidad;

-- Consultas a tablas
SELECT * FROM Roles;
SELECT * FROM Usuarios;
SELECT * FROM Especialidades;
SELECT * FROM Medicos;
SELECT * FROM Pacientes;
SELECT * FROM Citas;

-- Consultas a vistas
SELECT * FROM Vista_UsuariosConRoles;
SELECT * FROM Vista_MedicosConEspecialidades;
SELECT * FROM Vista_CitasDetalladas;