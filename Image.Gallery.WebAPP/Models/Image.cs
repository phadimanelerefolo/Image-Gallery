namespace Image.Gallery.WebAPP.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
