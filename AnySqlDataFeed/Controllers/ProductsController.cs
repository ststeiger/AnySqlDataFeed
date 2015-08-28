using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;


using AnySqlDataFeed.Models;


// http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v3/creating-an-odata-endpoint
// http://www.odata.org/getting-started/basic-tutorial/



// http://www.odata.org/blog/how-to-use-web-api-odata-to-build-an-odata-v4-service-without-entity-framework/

// http://localhost:[portNumber]/

// Service metadata
// http://localhost:[portNumber]/$metadata

// Get People
// http://localhost:[portNumber]/People

// Queries
// http://localhost:[portNumber]/People?$filter=contains(Description,'Lorem')
// http://localhost:[portNumber]/People?$select=Name
// http://localhost:[portNumber]/People?$expand=Trips


// http://localhost:5570/ExcelDataFeed.svc/
// http://localhost:5570/ExcelDataFeed.svc/T_Admin
// http://localhost:5570/ExcelDataFeed.svc/$metadata


namespace AnySqlDataFeed.Controllers
{
    /*
    Führen Sie zum Hinzufügen einer Route für diesen Controller diese Anweisungen in der Methode "Register" der Klasse "WebApiConfig" zusammen. Beachten Sie, dass für OData-URLs zwischen Groß- und Kleinschreibung unterschieden wird.

    using System.Web.Http.OData.Builder;
    using OmgSucky.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductsController : ODataController
    {
        // private ProductServiceContext db = new ProductServiceContext();

        // GET odata/Products
        [Queryable]
        public IQueryable<Product> GetProducts()
        // public IQueryable<System.Data.DataRow> GetProducts()
        {
            List<Product> prod = new List<Product>();


            prod.AddRange(new Product[] {
                new Product() { ID = 1, Name = "Hat", Price = 15, Category = "Apparel" },
                new Product() { ID = 2, Name = "Socks", Price = 5, Category = "Apparel" },
                new Product() { ID = 3, Name = "Scarf", Price = 12, Category = "Apparel" },
                new Product() { ID = 4, Name = "Yo-yo", Price = 4.95M, Category = "Toys" },
                new Product() { ID = 5, Name = "Puzzle", Price = 8, Category = "Toys" },
            });


            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Price", typeof(System.Decimal));
            dt.Columns.Add("Category", typeof(string));


            System.Data.DataRow dr = null;

            foreach (Product myprod in prod)
            {
                dr = dt.NewRow();
                dr["Id"] = myprod.ID;
                dr["Name"] = myprod.Name;
                dr["Price"] = myprod.Price;
                dr["Category"] = myprod.Category;

                dt.Rows.Add(dr);
            }

            // return dt.AsEnumerable().AsQueryable();
            return prod.AsQueryable();
        }

        /*
        // GET odata/Products(5)
        [Queryable]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Products.Where(product => product.ID == key));
        }

        // PUT odata/Products(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != product.ID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST odata/Products
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Created(product);
        }

        // PATCH odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE odata/Products(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.Products.Count(e => e.ID == key) > 0;
        }
        */
    }
}
