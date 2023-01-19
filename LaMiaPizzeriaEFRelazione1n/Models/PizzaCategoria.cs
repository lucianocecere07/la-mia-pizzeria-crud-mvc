using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaMiaPizzeriaEFRelazione1n.Models
{
    public class PizzaCategoria
    {
        public Pizza Pizza { get; set; }
        public List<Categoria>? Categoria { get; set; }

        public List<SelectListItem>? Ingredienti { get; set; }

        public List<string>? IngredientiSelect { get; set; }
    }
}
