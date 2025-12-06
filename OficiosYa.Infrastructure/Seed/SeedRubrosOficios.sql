BEGIN;

-- Seed Rubros
INSERT INTO public."Rubros"("Id","Nombre","Slug","Descripcion") VALUES
(1, 'Construcción y mantenimiento', 'construccion-mantenimiento', NULL),
(2, 'Vehículos y mecánica', 'vehiculos-mecanica', NULL),
(3, 'Servicios para el hogar', 'servicios-hogar', NULL),
(4, 'Tecnología y digital', 'tecnologia-digital', NULL),
(5, 'Reparaciones varias', 'reparaciones-varias', NULL),
(6, 'Gastronomía', 'gastronomia', NULL),
(7, 'Artes y oficios manuales', 'artes-oficios', NULL)
ON CONFLICT ("Id") DO NOTHING;

-- Seed Oficios (Id, RubroId, Nombre, Descripcion, RequiereLicencia, Activo)
INSERT INTO public."Oficios"("Id","RubroId","Nombre","Descripcion","RequiereLicencia","Activo") VALUES
-- Construcción y mantenimiento (RubroId = 1)
(1, 1, 'Albañil', NULL, false, true),
(2, 1, 'Plomero / Gasista (matriculado)', NULL, true, true),
(3, 1, 'Electricista', NULL, false, true),
(4, 1, 'Pintor', NULL, false, true),
(5, 1, 'Carpintero', NULL, false, true),
(6, 1, 'Herrero', NULL, false, true),
(7, 1, 'Vidriero', NULL, false, true),
(8, 1, 'Techista', NULL, false, true),
(9, 1, 'Yesero', NULL, false, true),
(10, 1, 'Colocador de durlock', NULL, false, true),
(11, 1, 'Colocador de cerámica / porcelanato', NULL, false, true),
(12, 1, 'Instalador de aire acondicionado', NULL, false, true),
(13, 1, 'Instalador de alarmas / cámaras de seguridad', NULL, false, true),
(14, 1, 'Jardinero', NULL, false, true),
(15, 1, 'Parquero', NULL, false, true),
(16, 1, 'Podador de árboles', NULL, false, true),

-- Vehículos y mecánica (RubroId = 2)
(17, 2, 'Mecánico automotor', NULL, false, true),
(18, 2, 'Chapista', NULL, false, true),
(19, 2, 'Pintor automotor', NULL, false, true),
(20, 2, 'Gomería', NULL, false, true),
(21, 2, 'Mecánico de motos', NULL, false, true),
(22, 2, 'Electricista automotor', NULL, false, true),
(23, 2, 'Lavadero de autos / Detailing', NULL, false, true),

-- Servicios para el hogar (RubroId = 3)
(24, 3, 'Limpieza general', NULL, false, true),
(25, 3, 'Limpieza profunda', NULL, false, true),
(26, 3, 'Limpieza post-obra', NULL, false, true),
(27, 3, 'Niñera', NULL, false, true),
(28, 3, 'Cuidador de adultos mayores', NULL, false, true),
(29, 3, 'Mudanzas / Fletes', NULL, false, true),
(30, 3, 'Paseador de perros', NULL, false, true),

-- Tecnología y digital (RubroId = 4)
(31, 4, 'Técnico en PC / Notebook', NULL, false, true),
(32, 4, 'Técnico en celulares', NULL, false, true),
(33, 4, 'Instalador de redes', NULL, false, true),
(34, 4, 'Técnico en impresoras', NULL, false, true),

-- Reparaciones varias (RubroId = 5)
(35, 5, 'Cerrajero', NULL, false, true),
(36, 5, 'Tapicero', NULL, false, true),
(37, 5, 'Reparación de electrodomésticos', NULL, false, true),
(38, 5, 'Servicio técnico de heladeras', NULL, false, true),
(39, 5, 'Servicio técnico de lavarropas', NULL, false, true),
(40, 5, 'Técnico en TV', NULL, false, true),

-- Gastronomía (RubroId = 6)
(41, 6, 'Panadero', NULL, false, true),
(42, 6, 'Pastelero', NULL, false, true),
(43, 6, 'Cocinero', NULL, false, true),
(44, 6, 'Parrillero', NULL, false, true),
(45, 6, 'Bartender', NULL, false, true),

-- Artes y oficios manuales (RubroId = 7)
(46, 7, 'Costurera / Modista', NULL, false, true),
(47, 7, 'Sastre', NULL, false, true),
(48, 7, 'Artesano', NULL, false, true),
(49, 7, 'Zapatero', NULL, false, true),
(50, 7, 'Joyería / Relojería', NULL, false, true)
ON CONFLICT ("Id") DO NOTHING;

COMMIT;