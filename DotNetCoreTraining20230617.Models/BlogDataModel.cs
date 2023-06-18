using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreTraining20230617.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        //[Column("")]
        public int Blog_Id { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }
}