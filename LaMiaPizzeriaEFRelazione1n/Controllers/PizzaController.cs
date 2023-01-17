using LaMiaPizzeriaEFRelazione1n.DataBase;
using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Aggiungi(Pizza NuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiungi", NuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizza.Add(NuovaPizza);
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
                    return View(pizzaScelta);
                }
            }
            return NotFound("Questa pizza non è stata trovata");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifica(Pizza NuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Aggiungi", NuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaScelta = db.Pizza
                      .Where(pizza => pizza.Id == NuovaPizza.Id)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    pizzaScelta.Name = NuovaPizza.Name;
                    pizzaScelta.Description = NuovaPizza.Description;
                    pizzaScelta.Image = NuovaPizza.Image;
                    pizzaScelta.Price = NuovaPizza.Price;

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
