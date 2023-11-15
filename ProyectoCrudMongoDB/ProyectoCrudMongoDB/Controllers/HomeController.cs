using ProyectoCrudMongoDB.Models;
using ProyectoCrudMongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProyectoCrudMongoDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositoryProductos repo;

        public HomeController(RepositoryProductos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.getProductos();

            if (productos.Count == 0)
            {
                ViewBag.mensaje = "No hay productos en la base de datos";
                return View();
            }
            else
            {
                return View(productos); // Pasa la lista de productos a la vista
            }
        }

        public IActionResult InsertarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarProducto(string nombre, string descripcion, int precio, string imagen)
        {
            this.repo.InsertProducto(nombre, imagen, descripcion, precio);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(string id)
        {
            Producto p = this.repo.findProducto(id);
            return View(p);
        }

        [HttpPost]
        public IActionResult Editar(string id, string nombre, string descripcion, int precio, string imagen)
        {
            Producto p = this.repo.findProducto(id);
            p.nombre = nombre;
            p.descripcion = descripcion;
            p.precio = precio;
            p.imagen = imagen;

            this.repo.UpdateProducto(p);

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(string id)
        {
            this.repo.DeleteProducto(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
