﻿USE [VeterinariaDB]
GO

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName])
     VALUES
           (NEWID()
           ,'Cliente'
           ,'Cliente')
GO

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName])
     VALUES
           (NEWID()
           ,'Veterinario'
           ,'Veterinario')
GO

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName])
     VALUES
           (NEWID()
           ,'Administrador'
           ,'Administrador')
GO

INSERT INTO [dbo].[EstadosMascotas]
           ([Descripcion])
     VALUES
           ('Activo')
GO

INSERT INTO [dbo].[EstadosMascotas]
           ([Descripcion])
     VALUES
           ('Inactivo')
GO

INSERT INTO [dbo].[EstadosUsuario]
           ([Descripcion])
     VALUES
           ('Activo')
GO

INSERT INTO [dbo].[EstadosUsuario]
           ([Descripcion])
     VALUES
           ('Inactivo')
GO

INSERT INTO [dbo].[EstadosCita]
           ([DescripcionCita])
     VALUES
           ('Agendada')
GO

INSERT INTO [dbo].[EstadosCita]
           ([DescripcionCita])
     VALUES
           ('Cancelada')
GO

INSERT INTO [dbo].[EstadosCita]
           ([DescripcionCita])
     VALUES
           ('En Curso')
GO

INSERT INTO [dbo].[EstadosCita]
           ([DescripcionCita])
     VALUES
           ('Finalizada')
GO