Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports MessengerExample.Common
Imports MessengerExample.Model
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Windows

Namespace MessengerExample.ViewModels
    <POCOViewModel> _
    Public Class EmployeeCollectionViewModel
        Implements IDocumentViewModel

        Public ReadOnly Property Title() As Object Implements IDocumentViewModel.Title
            Get
                Return "Employees"
            End Get
        End Property
        Private privateEmployees As IList(Of Employee)
        Public Overridable Property Employees() As IList(Of Employee)
            Get
                Return privateEmployees
            End Get
            Protected Set(ByVal value As IList(Of Employee))
                privateEmployees = value
            End Set
        End Property
        <ServiceProperty(SearchMode := ServiceSearchMode.PreferParents)> _
        Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property
        <ServiceProperty(SearchMode := ServiceSearchMode.PreferParents)> _
        Protected Overridable ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Nothing
            End Get
        End Property
        Private EmployeeContext As EmployeeContext

        Public Sub Add()
            Dim document As IDocument = DocumentManagerService.CreateDocument("EmployeeView", New EmployeeViewModelInfo(EmployeeViewModelType.Add), Me)
            document.Show()
        End Sub
        Public Sub Edit(ByVal row As Employee)
            Dim rowId As Integer = row.ID
            Dim document As IDocument = FindDocument(rowId)
            If document Is Nothing Then
                document = DocumentManagerService.CreateDocument("EmployeeView", New EmployeeViewModelInfo(rowId), Me)
                document.Id = rowId
            End If
            document.Show()
        End Sub
        Public Function CanEdit(ByVal row As Employee) As Boolean
            Return row IsNot Nothing
        End Function
        Public Sub Delete(ByVal row As Employee)
            If MessageBoxService.Show("Delete Employee?", "Confirmation", MessageBoxButton.YesNo) <> MessageBoxResult.Yes Then
                Return
            End If
            CloseDocument(row.ID)
            EmployeeContext.Employees.Remove(row)
            EmployeeContext.SaveChanges()
        End Sub
        Public Function CanDelete(ByVal row As Employee) As Boolean
            Return row IsNot Nothing
        End Function

        Public Shared Function Create() As EmployeeCollectionViewModel
            Dim factory = ViewModelSource.Factory(Function() New EmployeeCollectionViewModel())
            Return factory()
        End Function
        Protected Sub New()
            Messenger.Default.Register(Of EntityMessage(Of Employee))(Me, AddressOf OnMessage)
            If Not ViewModelBase.IsInDesignMode Then
                Initialize()
            End If
        End Sub
        Private Sub Initialize()
            EmployeeContext = New EmployeeContext()
            EmployeeContext.Employees.Load()
            Employees = EmployeeContext.Employees.Local
        End Sub
        Private Sub CloseDocument(ByVal id As Integer)
            Dim doc As IDocument = FindDocument(id)
            If doc IsNot Nothing Then
                doc.Close()
            End If
        End Sub
        Private Function FindDocument(ByVal rowId As Integer) As IDocument
            For Each doc In DocumentManagerService.Documents
                If rowId.Equals(doc.Id) Then
                    Return doc
                End If
            Next doc
            Return Nothing
        End Function
        Private Sub OnMessage(ByVal message As EntityMessage(Of Employee))
            Dim employee As Employee = Nothing
            Select Case message.MessageType
                Case EntityMessageType.Added
                    EmployeeContext.Employees.Find(message.Entity.ID)
                Case EntityMessageType.Changed
                    employee = EmployeeContext.Employees.Find(message.Entity.ID)
                    EmployeeContext.Entry(Of Employee)(employee).Reload()
                    employee.Refresh()
                Case EntityMessageType.Deleted
                    employee = EmployeeContext.Employees.Find(message.Entity.ID)
                    EmployeeContext.Entry(Of Employee)(employee).Reload()
                    CloseDocument(message.Entity.ID)
                Case Else
                    Throw New NotImplementedException()
            End Select
        End Sub
        Private Function IDocumentViewModel_Close() As Boolean Implements IDocumentViewModel.Close
            Return False
        End Function
    End Class
End Namespace
