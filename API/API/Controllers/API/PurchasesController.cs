using API.Data;
using API.Models;
using API.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")] // use of JWT for authentication
public class PurchasesController:ControllerBase {

   private readonly ApplicationDbContext _context;

   public PurchasesController(ApplicationDbContext context) {
      _context = context;
   }

   // GET: api/Purchase
   [HttpGet]
   public async Task<ActionResult<IEnumerable<PurchasesDTO>>> GetPurchase() {

      // who is the autentictated user?
      var currentUser = User.Identity!.Name!;


      // Include the related Buyer and ListOfPhotos entities in the query
      var purchases = await _context.Purchases
                                    .Where(p => p.Buyer.UserName == currentUser)
                                    .Include(p => p.Buyer)
                                    .Include(p => p.ListofPhotos)
                                    .Select(p => new PurchasesDTO {
                                       PurchaseId = p.Id,
                                       PurchaseState = p.State.ToString(),
                                       PurchaseDate = p.Date,
                                       BuyerName = p.Buyer.Name,
                                       Photos= p.ListofPhotos.Select(photo => new PhotosPurchaseDTO {
                                          PhotoId = photo.Id,
                                          PhotoFile = photo.File
                                       }).ToList()
                                    })
                                    .ToListAsync();

      /*
        .Select(p => new PurchasesDTO {
                           PurchaseId = p.Id,
                           PurchaseState = ((int)p.State),
                           PurchaseDate = p.Date,
                           BuyerName = p.Buyer.Name,
                           PhotoId = p.ListofPhotos.FirstOrDefault() != null ? p.ListofPhotos.FirstOrDefault()!.Id : 0, // Assuming you want the first photo's ID
                           PhotoFile = p.ListofPhotos.FirstOrDefault() != null ? p.ListofPhotos.FirstOrDefault()!.File : "" // Assuming you want the first photo's file
                        })
       * 
         .Select(p => new Purchase {
                                       Id = p.Id,
                                       Date = p.Date,
                                       State = p.State,
                                       BuyerFK = p.BuyerFK,
                                       Buyer = new MyUser {
                                          Id = p.Buyer.Id,
                                          UserName = p.Buyer.UserName
                                       },
                                       ListofPhotos = p.ListofPhotos.Select(photo => new Photography {
                                          Id = photo.Id,
                                          Title = photo.Title,
                                          Description = photo.Description,
                                          Price = photo.Price
                                       }).ToList()
                                    }) 
       */
      return purchases;
   }

   // GET: api/Purchase/5
   [HttpGet("{id}")]
   public async Task<ActionResult<Purchase>> GetPurchase(int id) {
      var purchase = await _context.Purchases.FindAsync(id);

      if(purchase == null) {
         return NotFound();
      }

      return purchase;
   }

   // PUT: api/Purchase/5
   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
   [HttpPut("{id}")]
   public async Task<IActionResult> PutPurchase(int? id, Purchase purchase) {
      if(id != purchase.Id) {
         return BadRequest();
      }

      _context.Entry(purchase).State = EntityState.Modified;

      try {
         await _context.SaveChangesAsync();
      }
      catch(DbUpdateConcurrencyException) {
         if(!PurchaseExists(id)) {
            return NotFound();
         }
         else {
            throw;
         }
      }

      return NoContent();
   }

   // POST: api/Purchase
   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
   [HttpPost]
   public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase) {
      _context.Purchases.Add(purchase);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
   }

   // DELETE: api/Purchase/5
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeletePurchase(int? id) {
      var purchase = await _context.Purchases.FindAsync(id);
      if(purchase == null) {
         return NotFound();
      }

      _context.Purchases.Remove(purchase);
      await _context.SaveChangesAsync();

      return NoContent();
   }

   private bool PurchaseExists(int? id) {
      return _context.Purchases.Any(e => e.Id == id);
   }
}
