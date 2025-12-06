using Microsoft.EntityFrameworkCore;
using OficiosYa.Domain.Entities;
using OficiosYa.Infrastructure.Persistence;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace OficiosYa.Infrastructure.Seed
{
    public static class RubroSeeder
    {
        public static async Task SeedAsync(OficiosYaDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // If rubros already exist, do nothing
            if (await context.Rubros.AnyAsync()) return;

            var rubros = new List<Rubro>
            {
                new Rubro
                {
                    Nombre = Truncate("Construcción y mantenimiento", 150),
                    Slug = Truncate(GenerateSlug("Construcción y mantenimiento"), 150),
                    Descripcion = "Trabajos de construcción y mantenimiento",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Albañil" },
                        new Oficio { Nombre = "Plomero / Gasista (matriculado)" },
                        new Oficio { Nombre = "Electricista" },
                        new Oficio { Nombre = "Pintor" },
                        new Oficio { Nombre = "Carpintero" },
                        new Oficio { Nombre = "Herrero" },
                        new Oficio { Nombre = "Vidriero" },
                        new Oficio { Nombre = "Techista" },
                        new Oficio { Nombre = "Yesero" },
                        new Oficio { Nombre = "Colocador de durlock" },
                        new Oficio { Nombre = "Colocador de cerámica / porcelanato" },
                        new Oficio { Nombre = "Instalador de aire acondicionado" },
                        new Oficio { Nombre = "Instalador de alarmas / cámaras de seguridad" },
                        new Oficio { Nombre = "Jardinero" },
                        new Oficio { Nombre = "Parquero" },
                        new Oficio { Nombre = "Podador de árboles" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Vehículos y mecánica", 150),
                    Slug = Truncate(GenerateSlug("Vehículos y mecánica"), 150),
                    Descripcion = "Servicios para vehículos y mecánica",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Mecánico automotor" },
                        new Oficio { Nombre = "Chapista" },
                        new Oficio { Nombre = "Pintor automotor" },
                        new Oficio { Nombre = "Gomería" },
                        new Oficio { Nombre = "Mecánico de motos" },
                        new Oficio { Nombre = "Electricista automotor" },
                        new Oficio { Nombre = "Lavadero de autos / Detailing" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Servicios para el hogar", 150),
                    Slug = Truncate(GenerateSlug("Servicios para el hogar"), 150),
                    Descripcion = "Servicios domésticos y para el hogar",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Limpieza general" },
                        new Oficio { Nombre = "Limpieza profunda" },
                        new Oficio { Nombre = "Limpieza post-obra" },
                        new Oficio { Nombre = "Niñera" },
                        new Oficio { Nombre = "Cuidador de adultos mayores" },
                        new Oficio { Nombre = "Mudanzas / Fletes" },
                        new Oficio { Nombre = "Paseador de perros" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Tecnología y digital", 150),
                    Slug = Truncate(GenerateSlug("Tecnología y digital"), 150),
                    Descripcion = "Servicios técnicos y digitales",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Técnico en PC / Notebook" },
                        new Oficio { Nombre = "Técnico en celulares" },
                        new Oficio { Nombre = "Instalador de redes" },
                        new Oficio { Nombre = "Técnico en impresoras" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Reparaciones varias", 150),
                    Slug = Truncate(GenerateSlug("Reparaciones varias"), 150),
                    Descripcion = "Servicios de reparación diversos",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Cerrajero" },
                        new Oficio { Nombre = "Tapicero" },
                        new Oficio { Nombre = "Reparación de electrodomésticos" },
                        new Oficio { Nombre = "Servicio técnico de heladeras" },
                        new Oficio { Nombre = "Servicio técnico de lavarropas" },
                        new Oficio { Nombre = "Técnico en TV" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Gastronomía", 150),
                    Slug = Truncate(GenerateSlug("Gastronomía"), 150),
                    Descripcion = "Profesionales de cocina y gastronomía",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Panadero" },
                        new Oficio { Nombre = "Pastelero" },
                        new Oficio { Nombre = "Cocinero" },
                        new Oficio { Nombre = "Parrillero" },
                        new Oficio { Nombre = "Bartender" }
                    }
                },

                new Rubro
                {
                    Nombre = Truncate("Artes y oficios manuales", 150),
                    Slug = Truncate(GenerateSlug("Artes y oficios manuales"), 150),
                    Descripcion = "Oficios manuales y artísticos",
                    Oficios = new List<Oficio>
                    {
                        new Oficio { Nombre = "Costurera / Modista" },
                        new Oficio { Nombre = "Sastre" },
                        new Oficio { Nombre = "Artesano" },
                        new Oficio { Nombre = "Zapatero" },
                        new Oficio { Nombre = "Joyería / Relojería" }
                    }
                }
            };

            await context.Rubros.AddRangeAsync(rubros);
            await context.SaveChangesAsync();
        }

        private static string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            text = text.ToLowerInvariant();

            var sb = new StringBuilder(text.Length);
            foreach (var c in text)
            {
                if (char.IsLetterOrDigit(c) || c == '-') sb.Append(c);
                else if (char.IsWhiteSpace(c) || c == '_' || c == '.') sb.Append('-');
                // ignore other characters
            }

            var slug = sb.ToString();
            // collapse multiple dashes
            while (slug.Contains("--")) slug = slug.Replace("--", "-");
            slug = slug.Trim('-');
            return slug;
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value ?? string.Empty;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
