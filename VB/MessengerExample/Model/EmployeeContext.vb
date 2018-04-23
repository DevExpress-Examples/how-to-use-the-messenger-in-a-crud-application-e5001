Imports System.Data.Entity

Namespace MessengerExample.Model
    Public Class EmployeeContext
        Inherits DbContext

        Public Property Employees() As DbSet(Of Employee)
        Public Function IsNew(ByVal employee As Employee) As Boolean
            If employee Is Nothing Then
                Return False
            End If
            Dim state As EntityState = Entry(employee).State
            Return state = EntityState.Added
        End Function
        Public Function NeedSave(ByVal employee As Employee) As Boolean
            If employee Is Nothing Then
                Return False
            End If
            Dim state As EntityState = Entry(employee).State
            Return state = EntityState.Modified OrElse state = EntityState.Added
        End Function
    End Class
End Namespace
