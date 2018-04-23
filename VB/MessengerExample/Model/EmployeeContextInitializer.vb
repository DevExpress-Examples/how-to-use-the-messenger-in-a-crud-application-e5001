Imports System.Collections.Generic
Imports System.Data.Entity

Namespace MessengerExample.Model
    Public Class EmployeeContextInitializer
        Inherits DropCreateDatabaseIfModelChanges(Of EmployeeContext)

        Protected Overrides Sub Seed(ByVal context As EmployeeContext)
            MyBase.Seed(context)
            Dim employees As New List(Of Employee)() From {New Employee("John", "Smith")}
            employees.ForEach(Sub(x) context.Employees.Add(x))
            context.SaveChanges()
        End Sub
    End Class
End Namespace
