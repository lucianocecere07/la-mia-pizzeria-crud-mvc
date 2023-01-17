using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LaMiaPizzeriaEFRelazione1n.Validation;

namespace LaMiaPizzeriaEFRelazione1n.Models
{
    public class Pizza
    {

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "Il nome non può essere più di 100 caratteri")]
        [Required(ErrorMessage = "Il campo del nome è obbligatorio")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(1000)")]
        [StringLength(1000, ErrorMessage = "La descrizione non può essere più di 1000 caratteri")]
        [Required(ErrorMessage = "Il campo della descrizione è obbligatorio")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(500)")]
        [StringLength(500, ErrorMessage = "Il percorso dell'immagine non può essere più di 500 caratteri")]
        [ImmagineValidationAttribute]
        [Required(ErrorMessage = "Il campo del percorso dell'immagine è obbligatorio")]
        public string Image { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        [Required(ErrorMessage = "Il campo del prezzo è obbligatorio")]
        public double Price { get; set; }


        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }


        public Pizza()
        {

        }

        public Pizza(string name, string description, string image, double price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }

    }
}
