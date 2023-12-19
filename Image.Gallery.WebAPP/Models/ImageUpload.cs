namespace Image.Gallery.WebAPP.Models
{
    public class ImageUpload
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public byte[] Content { get; set; }
    }
}
