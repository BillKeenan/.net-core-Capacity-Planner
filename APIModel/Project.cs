using System.ComponentModel.DataAnnotations ;

namespace bigmojo.net.capacity.api.model{
    public class Project
    {
        [Required]
        [MinLength(10)]
        public string firstName {get; set;}
        public string lastName  {get; set;}
    }
}