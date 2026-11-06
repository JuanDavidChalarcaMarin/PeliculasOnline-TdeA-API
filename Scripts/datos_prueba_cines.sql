-- Datos de prueba para Cines.
-- Ejecútalo DESPUÉS de aplicar la migración (dotnet ef database update), sobre la base PeliculasTaller.
-- Es seguro ejecutarlo varias veces: solo inserta los registros que falten.
USE PeliculasTaller;
GO

IF NOT EXISTS (SELECT 1 FROM Cines)
BEGIN
    INSERT INTO Cines (Nombre, Latitud, Longitud)
    VALUES
        (N'Viva Envigado', 6.1760, -75.5920),
        (N'Premier Las Américas', 6.2380, -75.5970),
        (N'Parque Fabricato', 6.3330, -75.5560);
END
ELSE
BEGIN
    INSERT INTO Cines (Nombre, Latitud, Longitud)
    SELECT n, lat, lon
    FROM (VALUES
        (N'Viva Envigado', 6.1760, -75.5920),
        (N'Premier Las Américas', 6.2380, -75.5970),
        (N'Parque Fabricato', 6.3330, -75.5560)
    ) AS t(n, lat, lon)
    WHERE NOT EXISTS (
        SELECT 1
        FROM Cines c
        WHERE c.Nombre = t.n
    );
END
GO

