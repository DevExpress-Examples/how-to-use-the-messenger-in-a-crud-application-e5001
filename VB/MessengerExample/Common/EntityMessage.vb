Imports DevExpress.Mvvm

Namespace MessengerExample.Common
    Public Enum EntityMessageType
        Added
        Deleted
        Changed
    End Enum
    Public Class EntityMessage(Of TEntity)
        Private privateEntity As TEntity
        Public Property Entity() As TEntity
            Get
                Return privateEntity
            End Get
            Private Set(ByVal value As TEntity)
                privateEntity = value
            End Set
        End Property
        Private privateMessageType As EntityMessageType
        Public Property MessageType() As EntityMessageType
            Get
                Return privateMessageType
            End Get
            Private Set(ByVal value As EntityMessageType)
                privateMessageType = value
            End Set
        End Property
        Public Sub New(ByVal entity As TEntity, ByVal messageType As EntityMessageType)
            Me.Entity = entity
            Me.MessageType = messageType
        End Sub
    End Class
End Namespace
