using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CordovaSPA.Dal;

namespace CordovaSPA.Controllers
{
    public class ProductsController : ApiController
    {
        private Db db = new Db();

        // GET: api/Products/GetAll
        public IEnumerable<Product> GetAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Products.Include(p => p.ProductCategory);
        }

        // GET: api/products/GetByCategory/5
        public IEnumerable<Product> GetByCategory(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var queryForAllProducts = db.Products.Include(p => p.ProductCategory);
            var queryForDefinedCategory = queryForAllProducts.Where(product => product.CategoryId == id);
            var result = queryForDefinedCategory.ToList();
            return result;
        }

        // GET: api/products/getById/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Product product = db.Products.Include(p => p.ProductCategory).FirstOrDefault(p=>p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Products/Update/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products/Create
        [ResponseType(typeof(Product))]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/Delete/5
        [HttpPost]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        
        private bool ProductExists(int? id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}