Imports System
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace MessengerExample.ViewModels
    <POCOViewModel> _
    Public Class MainViewModel
        Inherits ViewModelBase

        Protected Overridable ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub ShowDocument(ByVal documentName As String)
            Dim doc = DocumentManagerService.FindDocumentById(documentName)
            If doc Is Nothing Then
                doc = DocumentManagerService.CreateDocument(documentName, Nothing, Me)
                doc.Id = documentName
            End If
            doc.Show()
        End Sub

        Public Shared Function Create() As MainViewModel
            Dim factory = ViewModelSource.Factory(Function() New MainViewModel())
            Return factory()
        End Function
        Protected Sub New()
        End Sub
    End Class
End Namespace