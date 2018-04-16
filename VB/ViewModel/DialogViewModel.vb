Imports DataAnnotationAttributes.Model
Imports DevExpress.Xpf.Mvvm
Imports System
Imports System.Linq
Imports System.Windows.Input

Namespace DXSample
    Public Class DialogViewModel
        Inherits ViewModelBase

        Protected Context As ContactContext


        Private entity_Renamed As Contact
        Public Property Entity() As Contact
            Get
                Return entity_Renamed
            End Get
            Set(ByVal value As Contact)
                entity_Renamed = value
                RaisePropertyChanged(Function() Entity)
            End Set
        End Property
        Private privateSaveCommand As ICommand
        Public Property SaveCommand() As ICommand
            Get
                Return privateSaveCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateSaveCommand = value
            End Set
        End Property

        Public Sub New()
            SaveCommand = New DelegateCommand(AddressOf Save)
        End Sub

        Protected Overrides Sub OnParameterChanged(ByVal parameter As Object)
            MyBase.OnParameterChanged(parameter)
            If Not(TypeOf parameter Is Integer) Then
                Return
            End If
            Context = New ContactContext()
            Entity = Context.Contacts.Where(Function(x) x.Id = DirectCast(parameter, Integer)).FirstOrDefault()
        End Sub
        Public Sub Save()
            Context.SaveChanges()
            Messenger.Default.Send(New EntityChanged(Of Contact)(Entity.Id))
        End Sub
    End Class
End Namespace