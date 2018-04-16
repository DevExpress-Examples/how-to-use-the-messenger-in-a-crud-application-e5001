Imports System.Data.Entity

Namespace DataAnnotationAttributes.Model
    Public Class ContactContext
        Inherits DbContext

        Shared Sub New()
            Database.SetInitializer(Of ContactContext)(New ContactContextInitializer())
        End Sub
        Public Property Contacts() As DbSet(Of Contact)
    End Class
End Namespace