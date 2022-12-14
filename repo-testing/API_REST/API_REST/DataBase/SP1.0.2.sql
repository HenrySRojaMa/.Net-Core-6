USE [prueba]
GO
/****** Object:  StoredProcedure [dbo].[procedimientosClientes]    Script Date: 13/4/2022 22:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[procedimientosClientes] (@Metodo int=null,@Id INT=null,@Nombre VARCHAR(50)=null,@Apellido VARCHAR(50)=null,@Identificacion VARCHAR(50)=null,@Edad INT=null,@EstadoCivil CHAR(1)=null)
AS
BEGIN
IF Convert(INT, @Metodo)=1
BEGIN
	IF not exists (SELECT * FROM Cliente with (nolock) WHERE Identificacion=@Identificacion)
	BEGIN
		INSERT INTO Cliente(Nombre,Apellido,Identificacion,Edad,EstadoCivil)
		VALUES			  (@Nombre,@Apellido,@Identificacion,@Edad,@EstadoCivil)
		PRINT ('Cliente registrado exitosamente')
	END
	ELSE
	BEGIN
		PRINT ('Este cliente ya fue registrado previamente')
	END
END
ELSE IF @Metodo=2 BEGIN SELECT * FROM Cliente with (nolock) END
ELSE IF @Metodo=3 BEGIN SELECT * FROM Cliente with (nolock) WHERE Identificacion=@Identificacion END
ELSE IF @Metodo=4 
BEGIN
	IF exists (SELECT * FROM Cliente with (nolock) WHERE Identificacion=@Identificacion)
	BEGIN
		UPDATE Cliente 
		SET Nombre=@Nombre,Apellido=@Apellido,Edad=@Edad,EstadoCivil=@EstadoCivil
		WHERE Identificacion=@Identificacion
		PRINT ('La informacion de este cliente ha sido actualizada existosamente')
	END
	ELSE
	BEGIN
		PRINT ('No se puede actualizar un cliente inexistente')
	END
END
END

--EXEC procedimientosClientes 4,0,'Henry Stefano','Rojas Martinez','0931357305',24,'S'