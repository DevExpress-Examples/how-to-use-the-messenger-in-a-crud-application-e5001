Imports DataAnnotationAttributes.Model
Imports DevExpress.Xpf.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows.Input

Namespace DXSample
    Public Class MainViewModel
        Inherits ViewModelBase

        Protected Context As ContactContext

        Public Property Entities() As IList(Of Contact)
        Public Property SelectedEntity() As Contact
        Private privateEditCommand As ICommand
        Public Property EditCommand() As ICommand
            Get
                Return privateEditCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateEditCommand = value
            End Set
        End Property

        Protected ReadOnly Property DocumentService() As IDocumentManagerService
            Get
                Return GetService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Sub New()
            Context = New ContactContext()
            Entities = New ObservableCollection(Of Contact)(Context.Contacts)
            EditCommand = New DelegateCommand(AddressOf Edit, AddressOf CanEdit)
            Messenger.Default.Register(Of EntityChanged(Of Contact))(Me, AddressOf OnRefresh)
        End Sub
        Public Sub Edit()
            Dim document = DocumentService.Documents.Where(Function(x) Equals(x.Id, SelectedEntity.Id)).FirstOrDefault()
            If document Is Nothing Then
                document = DocumentService.CreateDocument("EditEntityDialog", SelectedEntity.Id, Me)
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