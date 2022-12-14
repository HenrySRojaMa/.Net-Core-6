USE [master]
GO
/****** Object:  Database [Back]    Script Date: 28/10/2022 18:59:52 ******/
CREATE DATABASE [Back]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Back', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Back.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Back_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Back_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Back] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Back].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Back] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Back] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Back] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Back] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Back] SET ARITHABORT OFF 
GO
ALTER DATABASE [Back] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Back] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Back] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Back] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Back] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Back] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Back] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Back] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Back] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Back] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Back] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Back] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Back] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Back] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Back] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Back] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Back] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Back] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Back] SET  MULTI_USER 
GO
ALTER DATABASE [Back] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Back] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Back] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Back] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Back] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Back] SET QUERY_STORE = OFF
GO
USE [Back]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 28/10/2022 18:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[Cedula] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Correo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 28/10/2022 18:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarCliente] (
			@Cedula varchar(50),
            @Nombre varchar(50),
            @Apellido varchar(50),
            @Telefono varchar(10),
            @Direccion varchar(50),
            @Correo varchar(50))
AS
BEGIN
	IF exists(SELECT * FROM Persona WHERE Cedula=@Cedula )
			BEGIN
				UPDATE Persona SET Nombre=@Nombre,Apellido=@Apellido,Telefono=@Telefono,Direccion=@Direccion,Correo=@Correo
				WHERE Cedula=@Cedula
				SELECT '000' AS Codigo,'Persona actualizada correctamente' AS Mensaje
			END
		ELSE
			BEGIN
				SELECT '999' AS Codigo,'Dicha persona no existe' AS Mensaje
			END
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarCliente]    Script Date: 28/10/2022 18:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarCliente] (
			@Cedula varchar(50)
			)
AS
BEGIN
	IF exists(SELECT * FROM Persona WHERE Cedula=@Cedula )
			BEGIN
				SELECT * FROM Persona WHERE Cedula=@Cedula
			END
		ELSE
			BEGIN
				SELECT '999' AS Codigo,'Dicha persona no existe' AS Mensaje
			END
END
GO
/****** Object:  StoredProcedure [dbo].[IngresarCliente]    Script Date: 28/10/2022 18:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IngresarCliente] (
			@Cedula varchar(50),
            @Nombre varchar(50),
            @Apellido varchar(50),
            @Telefono varchar(10),
            @Direccion varchar(50),
            @Correo varchar(50))
AS
BEGIN
	IF not exists(SELECT * FROM Persona WHERE Cedula=@Cedula )
			BEGIN
				INSERT INTO Persona(Cedula,Nombre,Apellido,Telefono,Direccion,Correo)
				VALUES			(@Cedula,@Nombre,@Apellido,@Telefono,@Direccion,@Correo)
				SELECT '000' AS Codigo,'Persona registrada correctamente' AS Mensaje
			END
		ELSE
			BEGIN
				SELECT '999' AS Codigo,'Persona registrada previamente' AS Mensaje
			END
END
GO
USE [master]
GO
ALTER DATABASE [Back] SET  READ_WRITE 
GO
