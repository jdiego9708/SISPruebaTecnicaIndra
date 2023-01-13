CREATE OR ALTER PROC sp_Calificaciones_g
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'ID ESTUDIANTE')
	BEGIN
		SELECT *
		FROM Calificaciones ca
		INNER JOIN Estudiantes es ON ca.Id_estudiante = es.Id_estudiante
		WHERE ca.Id_estudiante = CONVERT(int, @Texto_busqueda)
	END
	ELSE IF (@Tipo_busqueda = 'PERIODO')
	BEGIN
		SELECT *
		FROM Calificaciones ca
		INNER JOIN Estudiantes es ON ca.Id_estudiante = es.Id_estudiante
		WHERE ca.Periodo = @Texto_busqueda
	END
	ELSE IF (@Tipo_busqueda = 'MATERIA')
	BEGIN
		SELECT *
		FROM Calificaciones ca
		INNER JOIN Estudiantes es ON ca.Id_estudiante = es.Id_estudiante
		WHERE ca.Materia = @Texto_busqueda
	END
END