﻿namespace BankSystem.Web.ViewModels
{
    public class JobApplicationVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Linkedin { get; set; }
        public IFormFile Cv { get; set; }
    }
}
