Namespace Extensibility
    ''' <include file="SectionAndSettingDocs.xml" path="//section"/>
    Public MustInherit Class UserDefinedSection

#Region "Instance Members"
        Protected ReadOnly Property SectionName As String

        Protected Sub New(SectionName As String)
            Me.SectionName = SectionName
            Cache.Add(Me)
        End Sub
#End Region

#Region "Shared Members"
        Protected Shared ReadOnly Cache As List(Of UserDefinedSection) = New List(Of UserDefinedSection)

        Public Shared Function Values() As IEnumerable(Of UserDefinedSection)
            Return Cache
        End Function
#End Region

#Region "Overrides"
        Public Overrides Function Equals(obj As Object) As Boolean
            Return obj IsNot Nothing AndAlso SectionName.Equals(obj.ToString, StringComparison.InvariantCulture)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return SectionName.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return SectionName
        End Function
#End Region
    End Class
End Namespace