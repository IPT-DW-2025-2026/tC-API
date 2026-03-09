namespace API.Models {

   /// <summary>
   /// Web shop user's data
   /// </summary>
   public class MyUser {

      /// <summary>
      /// PK
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// name of user
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// address of users
      /// </summary>
      public string Address { get; set; }

      /// <summary>
      /// postal code of users' address
      /// </summary>
      public string PostalCode { get; set; }

      /// <summary>
      /// Country of users
      /// </summary>
      public  string Country { get; set; }

      /// <summary>
      /// tax number of users
      /// </summary>
      public string TaxNumber { get; set; }

      /// <summary>
      /// The cell phone number that an user has
      /// </summary>
      public string CellPhone { get; set; }



   }
}
