
Namespace Extensibility

    Public MustInherit Class UserDefinedSetting
        Implements IEquatable(Of UserDefinedSetting)

        Protected Sub New(name As String)
            Me.Name = name
            Cache.Add(Me)
        End Sub

        Public ReadOnly Property Name As String

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Equals(TryCast(obj, UserDefinedSetting))
        End Function

        Public Overloads Function Equals(other As UserDefinedSetting) As Boolean Implements IEquatable(Of UserDefinedSetting).Equals
            Return other IsNot Nothing AndAlso other.Name.Equals(Name, StringComparison.InvariantCulture)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Name.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Private Shared ReadOnly Cache As ICollection(Of UserDefinedSetting) = New List(Of UserDefinedSetting)

        Public Shared Function Values() As IEnumerable(Of UserDefinedSetting)
            Return Cache
        End Function

    End Class

End Namespace
