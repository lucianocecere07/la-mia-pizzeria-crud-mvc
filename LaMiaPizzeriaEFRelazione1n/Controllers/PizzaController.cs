using Azure;
using LaMiaPizzeriaEFRelazione1n.DataBase;
using LaMiaPizzeriaEFRelazione1n.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
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
                    .Include(pizza => pizza.Ingredienti)
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

                List<Ingrediente> ingredientiInDB = db.Ingredienti.ToList();

                List<SelectListItem> listaPerSelectMultipla = new List<SelectListItem>();

                foreach (Ingrediente ingrediente in ingredientiInDB)
                {
                    SelectListItem opzioneSelectListItem = new SelectListItem() { Text = ingrediente.Name, Value = ingrediente.Id.ToString() };

                    listaPerSelectMultipla.Add(opzioneSelectListItem);
                }

                PizzaCategoria modelloDellaView = new PizzaCategoria();

                modelloDellaView.Pizza = new Pizza();
                modelloDellaView.Categoria = categoriaInDB;
                modelloDellaView.Ingredienti = listaPerSelectMultipla;

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

                    List<Ingrediente> ingredientiInDB = db.Ingredienti.ToList();

                    List<SelectListItem> listaPerSelectMultipla = new List<SelectListItem>();

                    foreach (Ingrediente ingrediente in ingredientiInDB)
                    {
                        SelectListItem ingredienteInSelectList = new SelectListItem() { Text = ingrediente.Name, Value = ingrediente.Id.ToString() };

                        listaPerSelectMultipla.Add(ingredienteInSelectList);
                    }

                    NuovaPizza.Categoria = categoriaInDB;
                    NuovaPizza.Ingredienti = listaPerSelectMultipla;
                }

                return View("Aggiungi", NuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                if (NuovaPizza.IngredientiSelect != null)
                {
                    NuovaPizza.Pizza.Ingredienti = new List<Ingrediente>();

                    foreach (string ingredienteId in NuovaPizza.IngredientiSelect)
                    {
                        int IngredientiIdInt = int.Parse(ingredienteId);

                        Ingrediente ingrediente = db.Ingredienti
                            .Where(ingredienteInDb => ingredienteInDb.Id == IngredientiIdInt)
                            .FirstOrDefault();

                        NuovaPizza.Pizza.Ingredienti.Add(ingrediente);
                    }
                }
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
                      .Include(pizza => pizza.Ingredienti)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    List<Categoria> categoriaInDB = db.Categoria.ToList();

                    PizzaCategoria modelloDellaView = new PizzaCategoria();
                    modelloDellaView.Pizza = pizzaScelta;
                    modelloDellaView.Categoria = categoriaInDB;


                    List<Ingrediente> ingredientiInDB = db.Ingredienti.ToList();
                    List<SelectListItem> listaPerSelectMultipla = new List<SelectListItem>();

                    foreach (Ingrediente ingrediente in ingredientiInDB)
                    {
                        bool GiaPresente = pizzaScelta.Ingredienti.Any(IngredientiScelti => IngredientiScelti.Id == ingrediente.Id);

                        SelectListItem ingredienteInSelectList = new SelectListItem() { Text = ingrediente.Name, Value = ingrediente.Id.ToString(), Selected = GiaPresente };
                        listaPerSelectMultipla.Add(ingredienteInSelectList);
                    }
                    
                    modelloDellaView.Ingredienti = listaPerSelectMultipla;

                    return View("Modifica", modelloDellaView);
                }
                return NotFound("Questa pizza non è stata trovata");
            }
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
                      .Include(pizza => pizza.Ingredienti)
                      .FirstOrDefault();

                if (pizzaScelta != null)
                {
                    pizzaScelta.Name = PizzaModificata.Pizza.Name;
                    pizzaScelta.Description = PizzaModificata.Pizza.Description;
                    pizzaScelta.Image = PizzaModificata.Pizza.Image;
                    pizzaScelta.Price = PizzaModificata.Pizza.Price;
                    pizzaScelta.CategoriaId = PizzaModificata.Pizza.CategoriaId;


                    pizzaScelta.Ingredienti.Clear();

                    if (PizzaModificata.IngredientiSelect != null)
                    {

                        foreach (string ingredienteId in PizzaModificata.IngredientiSelect)
                        {
                            int IngredientiIdInt = int.Parse(ingredienteId);

                            Ingrediente ingrediente = db.Ingredienti
                            .Where(ingredienteInDb => ingredienteInDb.Id == IngredientiIdInt)
                            .FirstOrDefault();

                            pizzaScelta.Ingredienti.Add(ingrediente);
                        }
                    }

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
