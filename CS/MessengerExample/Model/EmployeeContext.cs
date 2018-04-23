using System.Data.Entity;

namespace MessengerExample.Model {
    public class EmployeeContext : DbContext {
        public DbSet<Employee> Employees { get; set; }
        public bool IsNew(Employee employee) {
            if(employee == null) return false;
            EntityState state = Entry(employee).State;
            return state == EntityState.Added;
        }
        public bool NeedSave(Employee employee) {
            if(employee == null) return false;
            EntityState state = Entry(employee).State;
            return state == EntityState.Modified || state == EntityState.Added;
        }
    }
}
