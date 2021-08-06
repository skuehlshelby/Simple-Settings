Friend NotInheritable Class Contracts

    Public Shared Sub Require(Of T As Exception)(condition As Boolean)
        If Not condition Then
            Dim ex As T = Activator.CreateInstance(Of T)
            Throw ex
        End If
    End Sub

    Public Shared Sub Require(Of T As Exception)(condition As Boolean, message As String)
        If Not condition Then
            Dim ex As T = CType(Activator.CreateInstance(GetType(T), message), T)
            Throw ex
        End If
    End Sub

    Public Shared Sub RequireNonNull(argument As Object, argumentName As String, methodName As String)
        If argument Is Nothing Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Public Shared Sub RequireNonNull(argument As String, argumentName As String, methodName As String)
        If String.IsNullOrEmpty(argument) Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Public Shared Sub RequireNotEmpty(argument As String, argumentName As String, methodName As String)
        If String.IsNullOrEmpty(argument) Then
            ThrowException(argumentName, methodName)
        End If
    End Sub

    Private Shared Sub ThrowException(argumentName As String, methodName As String)
        Throw New ArgumentNullException($"Argument '{argumentName}' supplied to method '{methodName}' cannot be null (Nothing in Visual Basic).")
    End Sub
End Class
