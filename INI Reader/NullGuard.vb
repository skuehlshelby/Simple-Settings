Friend NotInheritable Class NullGuard
    Public Shared Sub ThrowIfNull(argument As Object, argumentName As String, methodName As String)
        If argument Is Nothing Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Public Shared Sub ThrowIfNull(argument As String, argumentName As String, methodName As String)
        If String.IsNullOrEmpty(argument) Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Public Shared Sub ThrowIfNullOrEmpty(argument As String, argumentName As String, methodName As String)
        If String.IsNullOrEmpty(argument) Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Public Shared Sub ThrowIfAllNullOrEmpty(args As String(), argumentName As String, methodName As String)
        If args.All(Function(arg) String.IsNullOrEmpty(arg)) Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Private Shared Sub ThrowException(argumentName As String, methodName As String)
        Throw New ArgumentNullException($"Argument '{argumentName}' supplied to method '{methodName}' cannot be null/nothing.")
    End Sub
End Class
