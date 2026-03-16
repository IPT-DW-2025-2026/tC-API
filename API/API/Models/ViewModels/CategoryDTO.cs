namespace API.Models.ViewModels {

   /// <summary>
   /// list of Categories
   /// </summary>
   public class CategoryDTO {

      /// <summary>
      /// the Id of the Category on database
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// the 'name' of the Category
      /// </summary>
      public string Name { get; set; } = ""; // <=> string.Empty;

   }
}
