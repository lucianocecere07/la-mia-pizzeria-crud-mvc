using LaMiaPizzeriaEFRelazione1n.DataBase;
using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


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

        public IActionResult Dettagli(int idScelto)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                    .Where(pizza => pizza.Id == idScelto)
                    .Include(pizza => pizza.Categoria)
                    .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    return View(pizzaScelta);
                }
            }
            return NotFound("Questa pizza non esiste");
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

                return View(modelloDellaView);
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
        public IActionResult Modifica(int idScelto)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                      .Where(pizza => pizza.Id == idScelto)
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
        public IActionResult Modifica(PizzaCategoria NuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Categoria> categoriaInDB = db.Categoria.ToList();

                    NuovaPizza.Categoria = categoriaInDB;
                }

                return View("Modifica", NuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                      .Where(pizza => pizza.Id == NuovaPizza.Pizza.Id)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    pizzaScelta.Name = NuovaPizza.Pizza.Name;
                    pizzaScelta.Description = NuovaPizza.Pizza.Description;
                    pizzaScelta.Image = NuovaPizza.Pizza.Image;
                    pizzaScelta.Price = NuovaPizza.Pizza.Price;

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
