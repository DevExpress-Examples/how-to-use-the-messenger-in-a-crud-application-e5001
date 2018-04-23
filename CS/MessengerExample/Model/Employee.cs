using DevExpress.Mvvm;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerExample.Model {
    public class Employee : BindableBase {
        [ReadOnly(true)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Refresh() {
            RaisePropertyChanged();
        }

        public Employee() {
        }
        public Employee(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}