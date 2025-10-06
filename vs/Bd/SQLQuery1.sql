CREATE DATABASE ClinicaMedica;
GO
USE ClinicaMedica;
GO

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255)
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrase�aHash NVARCHAR(255) NOT NULL,
    RolId INT NOT NULL,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

CREATE TABLE Especialidades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

CREATE TABLE Medicos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    EspecialidadId INT NOT NULL,
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id)
);

CREATE TABLE Pacientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE,
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    Direccion NVARCHAR(255),
    Activo BIT DEFAULT 1
);

CREATE TABLE Citas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PacienteId INT NOT NULL,
    MedicoId INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'Programada',
    Notas NVARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (MedicoId) REFERENCES Medicos(Id)
);

INSERT INTO Roles (Nombre, Descripcion) VALUES 
('Administrador', 'Acceso completo al sistema'),
('M�dico', 'Puede ver sus citas y actualizar historiales'),
('Recepcionista', 'Puede agendar citas y gestionar pacientes');

INSERT INTO Usuarios (NombreUsuario, Contrase�aHash, RolId) 
VALUES ('admin', '$2a$11$r9cZJ3V2H5q7w9yB1nL3M.5p7r9s1t3v5z7x9yB1nL3M5p7r9s1t3v5x7', 1);

INSERT INTO Usuarios (NombreUsuario, Contrase�aHash, RolId) 
VALUES ('medico', '$2a$11$r9cZJ3V2H5q7w9yB1nL3M.5p7r9s1t3v5z7x9yB1nL3M5p7r9s1t3v5x7', 2);

INSERT INTO Usuarios (NombreUsuario, Contrase�aHash, RolId) 
VALUES ('recepcion', '$2a$11$r9cZJ3V2H5q7w9yB1nL3M.5p7r9s1t3v5z7x9yB1nL3M5p7r9s1t3v5x7', 3);

INSERT INTO Especialidades (Nombre, Descripcion) VALUES 
('Cardiolog�a', 'Especialidad en enfermedades del coraz�n'),
('Pediatr�a', 'Medicina para ni�os y adolescentes'),
('Dermatolog�a', 'Especialidad en enfermedades de la piel'),
('Ginecolog�a', 'Salud femenina y reproductive'),
('Traumatolog�a', 'Especialidad en huesos y articulaciones');

INSERT INTO Medicos (Nombre, Apellido, EspecialidadId, Telefono, Email) VALUES
('Carlos', 'G�mez', 1, '555-1234', 'carlos.gomez@clinica.com'),
('Ana', 'L�pez', 2, '555-5678', 'ana.lopez@clinica.com'),
('Miguel', 'Rodr�guez', 3, '555-9012', 'miguel.rodriguez@clinica.com');

INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) VALUES
('Mar�a', 'Garc�a', '1985-03-15', '555-1111', 'maria.garcia@email.com', 'Calle Principal 123'),
('Juan', 'Mart�nez', '1990-07-22', '555-2222', 'juan.martinez@email.com', 'Avenida Central 456'),
('Laura', 'Hern�ndez', '1978-11-30', '555-3333', 'laura.hernandez@email.com', 'Plaza Mayor 789');

INSERT INTO Citas (PacienteId, MedicoId, FechaHora, Estado, Notas) VALUES
(1, 1, '2024-01-15 09:00:00', 'Programada', 'Consulta de rutina'),
(2, 2, '2024-01-15 10:30:00', 'Programada', 'Control pedi�trico'),
(3, 3, '2024-01-16 11:00:00', 'Programada', 'Revisi�n dermatol�gica');

GO

PRINT 'Base de datos ClinicaMedica creada exitosamente!';