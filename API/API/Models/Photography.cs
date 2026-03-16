using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

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
      [StringLength(50)]
      [Required(ErrorMessage = "o {0} é obrigatório")]
      [Display(Name = "Título")]
      public string Title { get; set; } = string.Empty; // <=> "";

      /// <summary>
      /// some description of the photo
      /// </summary>
      [StringLength(200)]
      [Display(Name = "Descrição")]
      public string? Description { get; set; }

      /// <summary>
      /// name of the file that we use to store the
      /// photo's file at disk drive
      /// </summary>
      [StringLength(40)]
      public string File { get; set; } = "";

      /// <summary>
      /// the date when the photo was taken
      /// </summary>
      [Display(Name = "Data")]
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
         ApplyFormatInEditMode = true)]
      public DateTime Date { get; set; }

      /// <summary>
      /// the price that a user must pay for the photo
      /// </summary>
      [Display(Name = "Preço")]
      [Precision(10,2)]
      public decimal Price { get; set; }

      /*  ************************************** 
       *  Relationships
       *  1-N
       *  ************************************** */

      /// <summary>
      /// FK to Category of Photo
      /// </summary>
      [ForeignKey(nameof(Category))]
      [Display(Name = "Categoria")]
      public int CategoryFK { get; set; }
      /// <summary>
      /// FK to Category of Photo
      /// </summary>
      public Category Category { get; set; } = null!;


      /*  ************************************** 
      *  Relationship
      *  M-N
      *  ************************************** */
      /// <summary>
      /// List of purchases where the photo was purchased
      /// </summary>
      public ICollection<Purchase> ListofPurchases { get; set; } = [];

   }
}
