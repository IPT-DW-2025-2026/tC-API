using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.ViewModels {



   /// <summary>
   /// DTO to show Photos data
   /// </summary>
   public class PhotosDTO {

      /// <summary>
      /// PK
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// name of the photo
      /// </summary>
      public string Title { get; set; } = string.Empty; // <=> "";

      /// <summary>
      /// some description of the photo
      /// </summary>
     public string? Description { get; set; }

      /// <summary>
      /// name of the file that we use to store the
      /// photo's file at disk drive
      /// </summary>
      public string FileName { get; set; } = "";

      /// <summary>
      /// name of photos' category
      /// </summary>
      public string Category { get; set; } = "";
      
   }


}
