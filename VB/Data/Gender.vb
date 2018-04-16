Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace DataAnnotationAttributes.Model
    Public Enum Gender
        Female = 0
        Male = 1
    End Enum
    Public Class GenderList
        Inherits ReadOnlyCollection(Of Gender)

        Public Shared Function GetGenderValues() As IList(Of Gender)
            Dim res As IList = System.Enum.GetValues(GetType(Gender))
            res = DirectCast(res.Cast(Of Gender)(), IList)
            Return DirectCast(res, IList(Of Gender))
        End Function
        Public Sub New()
            MyBase.New(GetGenderValues())
        End Sub
    End Class
End Namespace