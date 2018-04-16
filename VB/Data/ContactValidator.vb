Imports System.ComponentModel.DataAnnotations

Namespace DataAnnotationAttributes.Model
    Public Class ContactValidator
        Public Shared Function ValidateString(ByVal value As Object) As ValidationResult
            If value Is Nothing OrElse value.ToString().Length > 25 Then
                Return New ValidationResult("Value is too long")
            End If
            Return ValidationResult.Success
        End Function
    End Class
End Namespace
