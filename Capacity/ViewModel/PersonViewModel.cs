using System.ComponentModel.DataAnnotations;
using bigmojo.net.capacity.api.Model;

namespace bigmojo.net.capacity.api.ViewModel {
    public class PersonViewModel {
        [Required]
        [MinLength (2)]
        public string firstName { get; set; }
        [Required]
        [MinLength (2)]public string lastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string email { get; set; }

        public static implicit operator Person (PersonViewModel vm) => new Person {
            firstName = vm.firstName,
            lastName = vm.lastName,
            email = vm.email,
        };
        public static implicit operator PersonViewModel (Person vm) => new PersonViewModel {
            firstName = vm.firstName,
            lastName = vm.lastName,
            email = vm.email,
        };

    }
}