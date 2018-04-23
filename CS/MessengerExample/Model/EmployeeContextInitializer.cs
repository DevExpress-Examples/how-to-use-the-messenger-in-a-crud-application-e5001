using System.Collections.Generic;
using System.Data.Entity;

namespace MessengerExample.Model {
    public class EmployeeContextInitializer : DropCreateDatabaseIfModelChanges<EmployeeContext> {
        protected override void Seed(EmployeeContext context) {
            base.Seed(context);
            List<Employee> employees = new List<Employee>() {
                new Employee("John", "Smith"),
            };
            employees.ForEach(x => context.Employees.Add(x));
            context.SaveChanges();
        }
    }
}
