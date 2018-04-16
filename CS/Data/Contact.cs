using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DataAnnotationAttributes.Model {
    public class Contact {
        [ReadOnly(true)]
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public Contact() { }

        public Contact(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}