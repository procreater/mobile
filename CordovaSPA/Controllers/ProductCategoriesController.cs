﻿using System;
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
    public class ProductCategoriesController : ApiController
    {
        private Db db = new Db();

        //// GET: api/ProductCategories
        //public IEnumerable<ProductCategory> GetAll(bool dependOnProductName, string productName = null)
        //{
        //    IQueryable<ProductCategory> finalQuery;

        //    var catListQuery = db.ProductCategories.Include(c=>c.Products);

        //    if(dependOnProductName)
        //    {
        //        finalQuery = catListQuery.Where(c => c.Products.Any(product => product.Name == productName));
        //    }
        //    else
        //    {
        //        finalQuery = catListQuery;
        //    }

        //    return finalQuery.ToList();
        //}

        // GET: api/ProductCategories
        public IEnumerable<ProductCategory> GetAll()
        {
            return db.ProductCategories;
        }

        // GET: api/ProductCategories/GetById
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductCategory category = db.ProductCategories.Include(p => p.Products).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // GET: api/ProductCategories/5
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProductCategory(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        // PUT: api/ProductCategories/Update
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(productCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/CreateProductCategories
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult CreatePostProductCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductCategories.Add(productCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productCategory.Id }, productCategory);
        }

        // DELETE: api/ProductCategories/Delete
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult Delete(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            db.ProductCategories.Remove(productCategory);
            db.SaveChanges();

            return Ok(productCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCategoryExists(int id)
        {
            return db.ProductCategories.Count(e => e.Id == id) > 0;
        }
    }
}