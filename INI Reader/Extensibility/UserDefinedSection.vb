
Namespace Extensibility
    Public MustInherit Class UserDefinedSection
        Implements IEquatable(Of UserDefinedSection)

        Protected Sub New(name As String)
            Me.Name = name
            Cache.Add(Me)
        End Sub

        Protected ReadOnly Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Name.GetHashCode()
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return Equals(TryCast(obj, UserDefinedSection))
        End Function

        Public Overloads Function Equals(other As UserDefinedSection) As Boolean Implements IEquatable(Of UserDefinedSection).Equals
            Return other IsNot Nothing AndAlso other.Name.Equals(Name, StringComparison.InvariantCulture)
        End Function

        Private Shared ReadOnly Cache As ICollection(Of UserDefinedSection) = New List(Of UserDefinedSection)

        Public Shared Function Values() As IEnumerable(Of UserDefinedSection)
            Return Cache
        End Function

    End Class
End Namespace