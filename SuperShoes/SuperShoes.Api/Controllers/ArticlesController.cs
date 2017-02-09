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
using Newtonsoft.Json;
using SuperShoes.Api.Utils;
using SuperShoes.Api.Dtos.Responses;
using SuperShoes.Api.Utils.Authentication;

namespace SuperShoes.Api.Controllers
{
    [WebAPIBasicAuthentication]
    [Authorize]
    public class ArticlesController : ApiController
    {
        private SuperShoesContext db = new SuperShoesContext();

        /// <summary>
        /// Load all the articles that are in the Database.
        /// </summary>
        /// <returns></returns>
        [GeneralResponseFilter]
        async public Task<JObject> GetArticles()
        {
            try
            {
                var articles = await db.Articles.ToListAsync();
                var baseResponse = new BaseResponse<List<Article>>();
                return baseResponse.Get(articles, articles.Count(), "articles");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Load all the articles from a specific store that are in the Database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: services/articles/stores/:id
        [GeneralResponseFilter]
        [Route("services/articles/stores/{id}")]
        [ResponseType(typeof(Article))]
        async public Task<JObject> GetArticlesStore(string id)
        {
            Guid newGuid;
            if (!Guid.TryParse(id, out newGuid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            } 
            try
            {
                var articles = await db.Articles.Where(x => x.Store.Id == newGuid).ToListAsync();
                if (articles == null || articles.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var baseResponse = new BaseResponse<List<Article>>();
                return baseResponse.Get(articles, articles.Count(), "articles");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [GeneralResponseFilter]
        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public async Task<JObject> GetArticle(string id)
        {
            Guid newGuid;
            if (!Guid.TryParse(id, out newGuid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            try
            {
                var article = await db.Articles.FindAsync(newGuid);
                if (article == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                var baseResponse = new BaseResponse<Article>();
                return baseResponse.Get(article, 0, "article");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArticle(Guid id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(article);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArticleExists(article.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public async Task<IHttpActionResult> DeleteArticle(Guid id)
        {
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            await db.SaveChangesAsync();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(Guid id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}