
Namespace Extensibility

    Public MustInherit Class UserDefinedSetting

        Protected Sub New(name As String)
            Me.Name = name
            Cache.Add(Me)
        End Sub

        Public ReadOnly Property Name As String

        Public Overrides Function Equals(obj As Object) As Boolean
            Return obj IsNot Nothing AndAlso Name.Equals(obj.ToString, StringComparison.InvariantCulture)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Name.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Protected Shared ReadOnly Cache As ICollection(Of UserDefinedSetting) = New List(Of UserDefinedSetting)

        Public Shared Function Values() As IEnumerable(Of UserDefinedSetting)
            Return Cache
        End Function

    End Class
End Namespace
