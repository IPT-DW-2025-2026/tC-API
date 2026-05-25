using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Data;
using API.Models;
using API.Models.ViewModels;

namespace API.Controllers.API {
   [Route("api/[controller]")]
   [ApiController]
   public class PhotosController:ControllerBase {
      private readonly ApplicationDbContext _context;

      public PhotosController(ApplicationDbContext context) {
         _context = context;
      }

      // GET: api/Photos
      [HttpGet]
      public async Task<ActionResult<IEnumerable<PhotosDTO>>> GetPhotos() {
         return await _context.Photos
                              .Where(ph => ph.Category.Name == "Paisagens")
                              .Select(ph => new PhotosDTO {
                                 Id = ph.Id,
                                 Title = ph.Title,
                                 Description = ph.Description,
                                 FileName = ph.File,
                                 Category = ph.Category.Name
                              }
                              )
                              .ToListAsync();
      }

      // GET: api/Photos/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Photography>> GetPhotography(int id) {
         var photography = await _context.Photos.FindAsync(id);

         if(photography == null) {
            return NotFound();
         }

         return photography;
      }

      // PUT: api/Photos/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutPhotography(int id, Photography photography) {
         if(id != photography.Id) {
            return BadRequest();
         }

         _context.Entry(photography).State = EntityState.Modified;

         try {
            await _context.SaveChangesAsync();
         }
         catch(DbUpdateConcurrencyException) {
            if(!PhotographyExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/Photos
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Photography>> PostPhotography(Photography photography) {
         _context.Photos.Add(photography);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetPhotography", new { id = photography.Id }, photography);
      }

      // DELETE: api/Photos/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeletePhotography(int id) {
         var photography = await _context.Photos.FindAsync(id);
         if(photography == null) {
            return NotFound();
         }

         _context.Photos.Remove(photography);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool PhotographyExists(int id) {
         return _context.Photos.Any(e => e.Id == id);
      }
   }
}
