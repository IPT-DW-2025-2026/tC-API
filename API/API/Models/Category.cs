using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace API.Models {


   /// <summary>
   /// the category that a photo must have
   /// </summary>
   public class Category {

      /// <summary>
      /// PK
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// the name of the Category
      /// </summary>
      [StringLength(50)] // the max length of the text
      [Required(ErrorMessage ="o nome da categoria é obrigatório")] // error message if you do not write anything
      [Display(Name="Nome da Categoria")]  // the name to show on screen
      public string Name { get; set; } = "";

      /*  ************************************** 
      *  Relationships
      *  ************************************** */

      /// <summary>
      /// List of photos that a category has
      /// </summary>
      public ICollection<Photography> ListOfPhotos { get; set; } = [];

   }
}
