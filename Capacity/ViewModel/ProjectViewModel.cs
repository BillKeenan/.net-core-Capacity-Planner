using System.ComponentModel.DataAnnotations;
using bigmojo.net.capacity.api.Model;

namespace bigmojo.net.capacity.api.ViewModel {
    public class ProjectViewModel {
        [Required]
        [MinLength (5)]
        public string name { get; set; }

        public static implicit operator Project (ProjectViewModel vm) => new Project {
            name = vm.name,
        };
        public static implicit operator ProjectViewModel (Project vm) => new ProjectViewModel {
            name = vm.name,
        };

    }
}