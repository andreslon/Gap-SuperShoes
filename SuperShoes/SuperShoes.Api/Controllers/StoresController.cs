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
using System.Web.Http.Description;
using SuperShoes.Data.Context;
using SuperShoes.Data.Models;
using SuperShoes.Api.Utils.Filters;
using Newtonsoft.Json.Linq;
using SuperShoes.Api.Dtos.Responses;

namespace SuperShoes.Api.Controllers
{
    public class StoresController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        // GET: api/Stores 
        [GeneralResponseFilter]
        async public Task<JObject> GetStores()
        {
            try
            {
                var stores = await db.Stores.ToListAsync();
                var baseResponse = new BaseResponse<List<Store>>();
                return baseResponse.Get(stores, stores.Count(), "stores");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

         
        [GeneralResponseFilter]
        // GET: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<JObject> GetStore(string id)
        {
            Guid newGuid;
            if (!Guid.TryParse(id, out newGuid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            try
            {
                var store = await db.Stores.FindAsync(newGuid);
                if (store == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var baseResponse = new BaseResponse<Store>();
                return baseResponse.Get(store, 0, "store");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(Guid id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoreExists(store.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> DeleteStore(Guid id)
        {
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(Guid id)
        {
            return db.Stores.Count(e => e.Id == id) > 0;
        }
    }
}