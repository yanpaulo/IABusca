using BuscaBidirecional.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaBidirecional.WebApp.Controllers
{
    public class BuscaController : Controller
    {
        // GET: Busca
        public ActionResult Index()
        {
            
            return View();
        }

        // POST: Busca
        [HttpPost]
        public ActionResult Index(Busca busca)
        {
            var mapa = Mapa.FromFile(Server.MapPath("~/mapa.txt"));
            var problema = new Problema
            {
                Mapa = mapa
            };
            
            if (ModelState.IsValid)
            {
                problema.Origem = mapa.Locais.FirstOrDefault(l => l.Nome.ToLower().Contains(busca.LocalOrigem.ToLower()));
                problema.Destino = mapa.Locais.FirstOrDefault(l => l.Nome.ToLower().Contains(busca.LocalDestino.ToLower()));

                if (problema.Origem == null)
                {
                    ModelState.AddModelError("LocalOrigem", "Local não encontrado.");
                }

                if (problema.Destino == null)
                {
                    ModelState.AddModelError("LocalDestino", "Local não encontrado.");
                }

                if (ModelState.IsValid)
                {
                    var algoritmo = new BuscaBidirecional(problema, busca.A1, busca.A2);
                    string saidaListas = "";

                    while (!algoritmo.AtingiuObjetivo)
                    {
                        algoritmo.Expande();
                        saidaListas += algoritmo.ImprimeListas().Replace("\n", "<br />") + "<br />";
                        saidaListas += "------------------------- <br />";
                    }

                    ViewBag.Lists = saidaListas;
                    ViewBag.Result = $"Caminho: {algoritmo.ImprimeCaminho()}";
                }
            }
            return View(busca);
        }
    }
}