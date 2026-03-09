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
      public string Name { get; set; }

      /*  ************************************** 
      *  Relationships
      *  ************************************** */

      /// <summary>
      /// List of photos that a category has
      /// </summary>
      public ICollection<Photography>ListOfPhotos { get; set; }

   }
}
