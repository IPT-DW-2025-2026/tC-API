namespace API.Models.ViewModels {

   /// <summary>
   /// Represents a purchase data transfer object containing identifiers, 
   /// state, date, buyer name, and associated photo information.  
   /// </summary>
   /// <remarks>Intended for transferring purchase data between application 
   /// layers or services; 
   /// properties are simple data holders and do not perform validation or 
   /// business logic.</remarks>
   public class PurchasesDTO {

      public int PurchaseId { get; set; }
      public string PurchaseState { get; set; } = "";
      public DateTime PurchaseDate { get; set; }
      public string BuyerName { get; set; } = "";

      public List<PhotosPurchaseDTO> Photos { get; set; } = [];

   }

   /// <summary>
   /// Represents a photo included in a purchase request,
   /// containing the photo identifier and the file name.
   /// </summary>
   /// <remarks>Used as a data transfer object for transmitting photo purchase 
   /// information between application and service layers.</remarks>
   public class PhotosPurchaseDTO {

      public int PhotoId { get; set; }
      public string PhotoFile { get; set; } = "";
   }


}

/*
id compra
estado da compra
data da compra
nome do comprador
fotos
   - id
   - ficheiro da foto
*/
