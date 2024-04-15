USE [VeterinariaDB]
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
           ('Activa')
GO

INSERT INTO [dbo].[EstadosMascotas]
           ([Descripcion])
     VALUES
           ('Inactiva')
GO