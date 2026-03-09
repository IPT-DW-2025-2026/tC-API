using Mono.TextTemplating;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {

   /// <summary>
   /// data related with the Photos that a user buys
   /// </summary>
   public class Purchase {

      /// <summary>
      /// PK
      /// </summary>
      [Key]
      public int Id { get; set; }

      /// <summary>
      /// the Date of the buy
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// the state of the buy
      /// </summary>
      public State State { get; set; }

      /*  ************************************** 
      *  Relationships
      *  ************************************** */

      /// <summary>
      /// FK to the buyer of the purchase
      /// </summary>
      [ForeignKey(nameof(Buyer))]
      public int BuyerFK { get; set; }
      /// <summary>
      /// FK to the buyer of the purchase
      /// </summary>
      public MyUser Buyer { get; set; }

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
