Imports DataAnnotationAttributes.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Input

Namespace DXSample
    Public Class MainViewModel
        Protected Context As ContactContext1

        Public Overridable Property Entities() As IList(Of Contact)
        Public Overridable Property SelectedEntity() As Contact

        Protected ReadOnly Property DocumentService() As IDocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Sub New()
            Context = New ContactContext1()
            Entities = New ObservableCollection(Of Contact)(Context.Contacts)
            Messenger.Default.Register(Of EntityChanged(Of Contact))(Me, AddressOf OnRefresh)
        End Sub
        Public Sub Edit()
            Dim document = DocumentService.Documents.Where(Function(x) Equals(x.Id, SelectedEntity.Id)).FirstOrDefault()
            If document Is Nothing Then
                document = DocumentService.CreateDocument(Nothing, SelectedEntity.Id, Nothing)
                document.Id = SelectedEntity.Id
            End If
            document.Show()
        End Sub
        Public Function CanEdit() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Protected Sub OnRefresh(ByVal message As EntityChanged(Of Contact))
            Dim entity = Entities.First(Function(x) x.Id = DirectCast(message.EntityKey, Integer))
            Context.Entry(entity).Reload()
            Entities(Entities.IndexOf(entity)) = entity
        End Sub
    End Class
    Public Class EntityChanged(Of T)
        Public Sub New(ByVal entityKey As Object)
            Me.EntityKey = entityKey
        End Sub
        Private privateEntityKey As Object
        Public Property EntityKey() As Object
            Get
                Return privateEntityKey
            End Get
            Private Set(ByVal value As Object)
                privateEntityKey = value
            End Set
        End Property
    End Class
End Namespace