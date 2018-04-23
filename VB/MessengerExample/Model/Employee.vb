Imports DevExpress.Mvvm
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace MessengerExample.Model
    Public Class Employee
        Inherits BindableBase

        <[ReadOnly](True), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)> _
        Public Property ID() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Sub Refresh()
            RaisePropertyChanged()
        End Sub

        Public Sub New()
        End Sub
        Public Sub New(ByVal firstName As String, ByVal lastName As String)
            Me.FirstName = firstName
            Me.LastName = lastName
        End Sub
    End Class
End Namespace