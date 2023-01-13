CREATE OR ALTER PROC sp_Estudiantes_u
@Id_estudiante int,
@Nombres_estudiante varchar(100),
@Apellidos_estudiante varchar(100),
@Edad_estudiante int,
@Sexo_estudiante varchar(2),
@Curso_estudiante varchar(10),
@Estado_estudiante varchar(50)
AS
BEGIN
	UPDATE Estudiantes SET
	Nombres_estudiante = @Nombres_estudiante, 
	Apellidos_estudiante = @Apellidos_estudiante, 
	Edad_estudiante = @Edad_estudiante,
	Sexo_estudiante = @Sexo_estudiante, 
	Curso_estudiante = @Curso_estudiante, 
	Estado_estudiante = @Estado_estudiante
	WHERE Id_estudiante = @Id_estudiante
END