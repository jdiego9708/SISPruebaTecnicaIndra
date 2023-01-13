CREATE OR ALTER PROC sp_Estudiantes_g
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'TODOS')
	BEGIN
		SELECT *
		FROM Estudiantes
	END
	ELSE IF (@Tipo_busqueda = 'ID ESTUDIANTE')
	BEGIN
		SELECT *
		FROM Estudiantes
		WHERE Id_estudiante = CONVERT(int, @Texto_busqueda)
	END
	ELSE IF (@Tipo_busqueda = 'NOMBRES')
	BEGIN
		SELECT *
		FROM Estudiantes
		WHERE Nombres_estudiante like @Texto_busqueda + '%'
	END
END