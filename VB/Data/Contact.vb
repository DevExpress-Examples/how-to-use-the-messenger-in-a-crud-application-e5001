Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Namespace DataAnnotationAttributes.Model
    Public Class Contact
        <[ReadOnly](True), Display(AutoGenerateField := False)> _
        Public Property Id() As Integer

        Public Property FirstName() As String

        Public Property LastName() As String

        Public Property Gender() As Gender

        Public Property Email() As String

        Public Property Phone() As String

        Public Property Address() As String

        Public Property City() As String

        Public Property State() As String

        Public Property Zip() As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal firstName As String, ByVal lastName As String)
            Me.FirstName = firstName
            Me.LastName = lastName
        End Sub
    End Class
End Namespace