namespace DotNetCoreTraining20230617.Models
{
    public class BlogDataModel
    {
        public int Blog_Id { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }

    public class BlogViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }

    }
}