CREATE OR ALTER PROC sp_Calificaciones_u
@Id_calificacion int,
@Id_estudiante int,
@Materia varchar(50),
@Periodo varchar(50),
@Valor_nota decimal(19,2)
AS
BEGIN
	UPDATE Calificaciones SET
	Id_estudiante = @Id_estudiante, 
	Materia = @Materia, 
	Periodo = @Periodo, 
	Valor_nota = @Valor_nota
	WHERE Id_calificacion = @Id_calificacion
END