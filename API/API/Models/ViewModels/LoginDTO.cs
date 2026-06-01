namespace API.Models.ViewModels {

   /// <summary>
   /// data transfer object used to receive the login data from the client
   /// </summary>
   public class LoginDTO {

      public string Username { get; set; } = "";
      public string Password { get; set; }= "";
   }
}
