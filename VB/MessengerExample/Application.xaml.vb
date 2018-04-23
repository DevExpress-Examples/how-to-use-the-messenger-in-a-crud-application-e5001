Imports MessengerExample.Model
Imports System.Data.Entity
Imports System.Windows

Namespace MessengerExample
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Database.SetInitializer(Of EmployeeContext)(New EmployeeContextInitializer())
            MyBase.OnStartup(e)
        End Sub
    End Class
End Namespace
