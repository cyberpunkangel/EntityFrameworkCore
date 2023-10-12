using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculas.Entidades
{
    //[Index(nameof(Nombre), IsUnique = true)]
    public class Genero : EntidadAuditable
    {
        public int Identificador { get; set; }
        [ConcurrencyCheck]
        public string Nombre { get; set; }
        public bool EstaBorrado { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
        //public string Ejemplo { get; set; }
        //public string Ejemplo2 { get; set; }
    }
}
