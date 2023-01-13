CREATE OR ALTER PROC sp_Estudiantes_i
@Id_estudiante int output,
@Nombres_estudiante varchar(100),
@Apellidos_estudiante varchar(100),
@Edad_estudiante int,
@Sexo_estudiante varchar(2),
@Curso_estudiante varchar(10),
@Estado_estudiante varchar(50)
AS
BEGIN
	INSERT INTO Estudiantes
	VALUES (@Nombres_estudiante, @Apellidos_estudiante, @Edad_estudiante,
	@Sexo_estudiante, @Curso_estudiante, @Estado_estudiante)

	SET @Id_estudiante = SCOPE_IDENTITY();
END