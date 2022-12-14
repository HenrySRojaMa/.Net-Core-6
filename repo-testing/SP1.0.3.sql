USE [prueba]
GO
/****** Object:  StoredProcedure [dbo].[procedimientosClientes]    Script Date: 18/04/2022 10:14:59 ******/
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
		select '000' as Codigo, 'Cliente registrado exitosamente' as Mensaje
	END
	ELSE
	BEGIN
	select '999' as Codigo, 'Cliente ya existe' as Mensaje

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
		select '000' as Codigo, 'La informacion de este cliente ha sido actualizada existosamente' as Mensaje
	END
	ELSE
	BEGIN
		select '999' as Codigo, 'No se puede actualizar un cliente inexistente' as Mensaje
	END
END
END