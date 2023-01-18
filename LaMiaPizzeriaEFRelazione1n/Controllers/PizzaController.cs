using LaMiaPizzeriaEFRelazione1n.DataBase;
using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;


namespace LaMiaPizzeriaEFRelazione1n.Controllers
{
    public class PizzaController : Controller
    {

        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> listaPizza = db.Pizza.ToList<Pizza>();

                return View("Index", listaPizza);
            }
        }

        public IActionResult Dettagli(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                    .Where(pizza => pizza.Id == id)
                    .Include(pizza => pizza.Categoria)
                    .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    return View(pizzaScelta);
                }

                return NotFound("Questa pizza non esiste");
            }
        }


        //AGGIUNGI
        [HttpGet]
        public IActionResult Aggiungi()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Categoria> categoriaInDB = db.Categoria.ToList<Categoria>();

                PizzaCategoria modelloDellaView = new PizzaCategoria();

                modelloDellaView.Pizza = new Pizza();
                modelloDellaView.Categoria = categoriaInDB;

                return View("Aggiungi", modelloDellaView);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiungi(PizzaCategoria NuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Categoria> categoriaInDB = db.Categoria.ToList();

                    NuovaPizza.Categoria = categoriaInDB;
                }

                return View("Aggiungi", NuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizza.Add(NuovaPizza.Pizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        //MODIFICA
        [HttpGet]
        public IActionResult Modifica(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                      .Where(pizza => pizza.Id == id)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    List<Categoria> categoriaInDB = db.Categoria.ToList();

                    PizzaCategoria modelloDellaView = new PizzaCategoria();
                    modelloDellaView.Pizza = pizzaScelta;
                    modelloDellaView.Categoria = categoriaInDB;

                    return View("Modifica", modelloDellaView);
                }
            }
            return NotFound("Questa pizza non è stata trovata");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifica(PizzaCategoria PizzaModificata)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Categoria> categoriaInDB = db.Categoria.ToList();

                    PizzaModificata.Categoria = categoriaInDB;
                }

                return View("Modifica", PizzaModificata);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                      .Where(pizza => pizza.Id == PizzaModificata.Pizza.Id)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    pizzaScelta.Name = PizzaModificata.Pizza.Name;
                    pizzaScelta.Description = PizzaModificata.Pizza.Description;
                    pizzaScelta.Image = PizzaModificata.Pizza.Image;
                    pizzaScelta.Price = PizzaModificata.Pizza.Price;
                    pizzaScelta.CategoriaId = PizzaModificata.Pizza.CategoriaId; 

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return NotFound("Questa pizza non è stata trovata");
            }
        }


        //ELIMINARE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Elimina(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                    .Where(pizza => pizza.Id == id)
                    .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    db.Pizza.Remove(pizzaScelta);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return NotFound("Questa pizza non è stata trovata");

            }
        }

    }
}
