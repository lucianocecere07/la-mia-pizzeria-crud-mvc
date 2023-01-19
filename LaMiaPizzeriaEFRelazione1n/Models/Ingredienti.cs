namespace LaMiaPizzeriaEFRelazione1n.Models
{
    public class Ingredienti
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Pizza> Pizza { get; set; }

        public Ingredienti() 
        {
        
        }
    }
}
