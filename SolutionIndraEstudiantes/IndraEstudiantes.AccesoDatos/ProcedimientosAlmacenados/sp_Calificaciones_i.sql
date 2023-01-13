CREATE OR ALTER PROC sp_Calificaciones_i
@Id_calificacion int output,
@Id_estudiante int,
@Materia varchar(50),
@Periodo varchar(50),
@Valor_nota decimal(19,2)
AS
BEGIN
	INSERT INTO Calificaciones
	VALUES (@Id_estudiante, @Materia, @Periodo, @Valor_nota)

	SET @Id_calificacion = SCOPE_IDENTITY();
END