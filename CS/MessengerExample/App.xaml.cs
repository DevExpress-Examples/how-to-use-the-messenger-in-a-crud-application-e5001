using MessengerExample.Model;
using System.Data.Entity;
using System.Windows;

namespace MessengerExample {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            Database.SetInitializer<EmployeeContext>(new EmployeeContextInitializer());
            base.OnStartup(e);
        }
    }
}
