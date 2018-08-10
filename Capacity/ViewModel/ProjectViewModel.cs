using System.ComponentModel.DataAnnotations ;
using bigmojo.net.capacity.api.Model;

namespace bigmojo.net.capacity.api.ViewModel{
    public class ProjectViewModel
    {
        [Required]
        [MinLength(10)]
        public string firstName {get; set;}
        public string lastName  {get; set;}


        public static implicit operator Project(ProjectViewModel vm) => new Project
        {
            firstName = vm.firstName,
            lastName = vm.lastName,
        };

    }
}