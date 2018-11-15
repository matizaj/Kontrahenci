using Kontrahenci.Data;
using Kontrahenci.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrahenci.Controllers
{
    [Authorize]
    public class CustomerController:Controller
    {
        private readonly AppDbContext context;
        private int pageSize = 3;

        public CustomerController(AppDbContext ctx) => context = ctx;

        public ViewResult Index(string searchString, int page = 1)
        {
            var model = new CustomerListViewModel();
            var query = context.Customers.AsEnumerable();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString));
            }

           
            model.Customers = query.OrderBy(c => c.Id).Skip((page - 1) * pageSize).Take(pageSize);
            model.PageInfo = new PageInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = context.Customers.Count()
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create() => View();

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customer customerToAdd = new Customer
                {
                    Name = customer.Name,
                    Surname = customer.Surname,
                    City = customer.City,
                    Email = customer.Email
                };
                context.Customers.Add(customerToAdd);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var customerToDelete = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customerToDelete == null)
            {
                return Content("Klient nie istnieje");
            }
            context.Customers.Remove(customerToDelete);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var customerToEdit = context.Customers.FirstOrDefault(x=>x.Id==id);
            if (customerToEdit != null)
            {
                return View(customerToEdit);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Edit(Customer customer, int id)
        {
            var customerToEdit = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customerToEdit != null)
            {
                customerToEdit.Name = customer.Name;
                customerToEdit.Surname = customer.Surname;
                customerToEdit.Email = customer.Email;
                customerToEdit.City = customer.City;
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono klienta");
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var customerToShow = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customerToShow != null)
            {
                return View(customerToShow);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
