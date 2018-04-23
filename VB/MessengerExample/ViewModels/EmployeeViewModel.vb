Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports MessengerExample.Common
Imports MessengerExample.Model
Imports System
Imports System.Windows

Namespace MessengerExample.ViewModels
    Public Enum EmployeeViewModelType
        Add
        Modify
    End Enum
    Public Class EmployeeViewModelInfo
        Public Property Type() As EmployeeViewModelType
        Public Property RecordID() As Integer
        Public Sub New(ByVal type As EmployeeViewModelType)
            Me.New(type, -1)
        End Sub
        Public Sub New(ByVal recordId As Integer)
            Me.New(EmployeeViewModelType.Modify, recordId)
        End Sub
        Private Sub New(ByVal type As EmployeeViewModelType, ByVal recordID As Integer)
            Me.Type = type
            Me.RecordID = recordID
        End Sub
    End Class
    <POCOViewModel> _
    Public Class EmployeeViewModel
        Implements ISupportParameter, IDocumentViewModel

        Private privateTitle As Object
        Public Overridable Property Title() As Object Implements IDocumentViewModel.Title
            Get
                Return privateTitle
            End Get
            Protected Set(ByVal value As Object)
                privateTitle = value
            End Set
        End Property
        Private privateEmployee As Employee
        Public Overridable Property Employee() As Employee
            Get
                Return privateEmployee
            End Get
            Protected Set(ByVal value As Employee)
                privateEmployee = value
            End Set
        End Property
        Public Overridable Property Parameter() As Object Implements ISupportParameter.Parameter
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

        Public Sub Save()
            Dim isNew As Boolean = EmployeeContext.IsNew(Employee)
            EmployeeContext.SaveChanges()
            If isNew Then
                Messenger.Default.Send(New EntityMessage(Of Employee)(Employee, EntityMessageType.Added))
            Else
                Messenger.Default.Send(New EntityMessage(Of Employee)(Employee, EntityMessageType.Changed))
            End If
            UpdateTitle()
        End Sub
        Public Function CanSave() As Boolean
            If Employee Is Nothing Then
                Return False
            End If
            Return EmployeeContext.IsNew(Employee) OrElse EmployeeContext.NeedSave(Employee)
        End Function
        Public Sub Close()
            If TryClose() AndAlso DocumentManagerService IsNot Nothing Then
                Dim document As IDocument = DocumentManagerService.FindDocument(Me)
                If document IsNot Nothing Then
                    document.Close()
                End If
            End If
        End Sub
        Public Sub Delete()
            If MessageBoxService.Show("Delete Employee?", "Confirmation", MessageBoxButton.YesNo) <> MessageBoxResult.Yes Then
                Return
            End If
            EmployeeContext.Employees.Remove(Employee)
            EmployeeContext.SaveChanges()
            Messenger.Default.Send(New EntityMessage(Of Employee)(Employee, EntityMessageType.Deleted))
            Employee = Nothing
            UpdateTitle()
        End Sub
        Public Function CanDelete() As Boolean
            If Employee Is Nothing Then
                Return False
            End If
            Return Not EmployeeContext.IsNew(Employee)
        End Function

        Public Shared Function Create() As EmployeeViewModel
            Dim factory = ViewModelSource.Factory(Function() New EmployeeViewModel())
            Return factory()
        End Function
        Protected Sub New()
            If Not ViewModelBase.IsInDesignMode Then
                Initialize()
            End If
        End Sub
        Protected Sub OnParameterChanged()
            If ViewModelBase.IsInDesignMode Then
                Return
            End If
            Dim info As EmployeeViewModelInfo = DirectCast(Parameter, EmployeeViewModelInfo)
            If info Is Nothing Then
                Throw New InvalidOperationException()
            End If
            If info.Type = EmployeeViewModelType.Add Then
                Employee = EmployeeContext.Employees.Create()
                EmployeeContext.Employees.Add(Employee)
            Else
                Employee = EmployeeContext.Employees.Find(info.RecordID)
            End If
            UpdateTitle()
        End Sub
        Private Sub Initialize()
            EmployeeContext = New EmployeeContext()
        End Sub
        Private Sub UpdateTitle()
            If Employee IsNot Nothing Then
                Dim entityId As String = If(EmployeeContext.IsNew(Employee), "(New)", String.Format("{0} {1}", Employee.FirstName, Employee.LastName))
                Title = entityId
            Else
                Title = Nothing
            End If
        End Sub
        Private Function TryClose() As Boolean
            If Not EmployeeContext.NeedSave(Employee) Then
                Return True
            End If
            Dim result As MessageBoxResult = MessageBoxService.Show("Do you want to save changes?", "Confirmation", MessageBoxButton.YesNoCancel)
            If result = MessageBoxResult.Yes Then
                Save()
                Return True
            End If
            If result = MessageBoxResult.No Then
                Return True
            End If
            Return False
        End Function
        Private Function IDocumentViewModel_Close() As Boolean Implements IDocumentViewModel.Close
            Return TryClose()
        End Function
    End Class
End Namespace
