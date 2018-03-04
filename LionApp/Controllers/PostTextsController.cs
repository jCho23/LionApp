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
using LionApp.Models;

namespace LionApp.Controllers
{
    public class PostTextsController : ApiController
    {
        private LionAppContext db = new LionAppContext();

        // GET: api/PostTexts
        public IQueryable<PostText> GetPostTexts()
        {
            return db.PostTexts;
        }

        // GET: api/PostTexts/5
        [ResponseType(typeof(PostText))]
        public async Task<IHttpActionResult> GetPostText(int id)
        {
            PostText postText = await db.PostTexts.FindAsync(id);
            if (postText == null)
            {
                return NotFound();
            }

            return Ok(postText);
        }

        // PUT: api/PostTexts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPostText(int id, PostText postText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postText.Id)
            {
                return BadRequest();
            }

            db.Entry(postText).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostTextExists(id))
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

        // POST: api/PostTexts
        [ResponseType(typeof(PostText))]
        public async Task<IHttpActionResult> PostPostText(PostText postText)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostTexts.Add(postText);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = postText.Id }, postText);
        }

        // DELETE: api/PostTexts/5
        [ResponseType(typeof(PostText))]
        public async Task<IHttpActionResult> DeletePostText(int id)
        {
            PostText postText = await db.PostTexts.FindAsync(id);
            if (postText == null)
            {
                return NotFound();
            }

            db.PostTexts.Remove(postText);
            await db.SaveChangesAsync();

            return Ok(postText);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostTextExists(int id)
        {
            return db.PostTexts.Count(e => e.Id == id) > 0;
        }
    }
}