using Microsoft.CodeAnalysis.Operations;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {

   /// <summary>
   /// Photos' data
   /// </summary>
   public class Photography {


      /// <summary>
      /// PK
      /// </summary>
      [Key]
      public int Id { get; set; }

      /// <summary>
      /// name of the photo
      /// </summary>
      public string Title { get; set; }

      /// <summary>
      /// some description of the photo
      /// </summary>
      public string Description { get; set; }

      /// <summary>
      /// name of the file that we use to store the
      /// photo's file at disk drive
      /// </summary>
      public string File { get; set; }

      /// <summary>
      /// the date when the photo was taken
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// the price that a user must pay for the photo
      /// </summary>
      public decimal Price { get; set; }

      /*  ************************************** 
       *  Relationships
       *  1-N
       *  ************************************** */

      /// <summary>
      /// FK to Category of Photo
      /// </summary>
      [ForeignKey(nameof(Category))]
      public int CategoryFK { get; set; }
      /// <summary>
      /// FK to Category of Photo
      /// </summary>
      public Category Category { get; set; }


      /*  ************************************** 
      *  Relationship
      *  M-N
      *  ************************************** */
      /// <summary>
      /// List of purchases where the photo was purchased
      /// </summary>
      public ICollection<Purchase> ListofPurchases { get; set; }

   }
}
