Public NotInheritable Class NullGuard
    Private Const ErrorMessage As String = "Argument '{0}' supplied to method '{1}' cannot be null/nothing."
    Friend Shared Sub ThrowIfNull(ByVal Argument As Object, ByVal ArgumentName As String, ByVal MethodName As String)
        If Argument Is Nothing Then
            Throw New ArgumentNullException(String.Format(Globalization.CultureInfo.InvariantCulture, ErrorMessage, ArgumentName, MethodName))
        End If
    End Sub
    Friend Shared Sub ThrowIfNullOrEmpty(ByVal Argument As String, ByVal ArgumentName As String, MethodName As String)
        If String.IsNullOrEmpty(Argument) Then
            Throw New ArgumentNullException(String.Format(Globalization.CultureInfo.InvariantCulture, ErrorMessage, ArgumentName, MethodName))
        End If
    End Sub
    Friend Shared Sub ThrowIfAllNullOrEmpty(ByVal Argument As String(), ByVal ArgumentName As String, MethodName As String)
        For Each SubString As String In Argument
            If Not String.IsNullOrEmpty(SubString) Then
                Exit Sub
            End If
        Next SubString

        Throw New ArgumentNullException(String.Format(Globalization.CultureInfo.InvariantCulture, ErrorMessage, ArgumentName, MethodName))
    End Sub
    Friend Shared Sub ThrowIfAnyNullOrEmpty(ByVal Argument As String(), ByVal ArgumentName As String, MethodName As String)
        For Each SubString As String In Argument
            If Not String.IsNullOrEmpty(SubString) Then
                Throw New ArgumentNullException(String.Format(Globalization.CultureInfo.InvariantCulture, ErrorMessage, ArgumentName, MethodName))
            End If
        Next SubString
    End Sub
End Class
