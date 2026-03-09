namespace API.Models {

   /// <summary>
   /// Photos' data
   /// </summary>
   public class Photography {


      /// <summary>
      /// PK
      /// </summary>
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


   }
}
