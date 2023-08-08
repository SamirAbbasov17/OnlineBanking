namespace AdminPanel.ViewModels
{
    public class BlogVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
