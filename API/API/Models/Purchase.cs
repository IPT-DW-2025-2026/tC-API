using Mono.TextTemplating;

namespace API.Models {

   /// <summary>
   /// data related with the Photos that a user buys
   /// </summary>
   public class Purchase {

      /// <summary>
      /// PK
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// the Date of the buy
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// the state of the buy
      /// </summary>
      public State State { get; set; }

   }

   /// <summary>
   /// available states related with a buy
   /// </summary>
   public enum State {
      Pending,
      Paid,
      Sent,
      Delivered,
      Closed
   }


}
