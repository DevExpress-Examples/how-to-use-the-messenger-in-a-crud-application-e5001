Imports DataAnnotationAttributes.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Linq

Namespace DXSample
    Public Class DialogViewModel
        Implements ISupportParameter

        Protected Context As ContactContext1

        Public Overridable Property Entity() As Contact
        Public Overridable Property Parameter() As Object Implements ISupportParameter.Parameter

        Protected ReadOnly Property CurrentWindowService() As ICurrentWindowService
            Get
                Return Me.GetService(Of ICurrentWindowService)()
            End Get
        End Property

        Protected Overridable Sub OnParameterChanged()
            If Not (TypeOf Parameter Is Integer) Then
                Return
            End If
            Context = New ContactContext1()
            Entity = Context.Contacts.Where(Function(x) x.Id = DirectCast(Parameter, Integer)).FirstOrDefault()
        End Sub
        Public Sub Save()
            Context.SaveChanges()
            Messenger.Default.Send(New EntityChanged(Of Contact)(Entity.Id))
        End Sub
        Public Sub SaveAndClose()
            Save()
            CurrentWindowService.Close()
        End Sub
    End Class
End Namespace