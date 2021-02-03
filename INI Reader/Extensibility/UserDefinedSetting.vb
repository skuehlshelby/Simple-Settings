
Namespace Extensibility
    ''' <include file="SectionAndSettingDocs.xml" path="//setting"/>
    Public MustInherit Class UserDefinedSetting
#Region "Instance Members"
        Public ReadOnly Property SettingName As String
        Protected Sub New(settingName As String)
            Me.SettingName = settingName
            Cache.Add(Me)
        End Sub
#End Region

#Region "Shared Members"
        Protected Shared ReadOnly Cache As List(Of UserDefinedSetting) = New List(Of UserDefinedSetting)

        Public Shared Function Values() As IEnumerable(Of UserDefinedSetting)
            Return Cache
        End Function
#End Region

#Region "Overrides"
        Public Overrides Function Equals(obj As Object) As Boolean
            Return obj IsNot Nothing AndAlso SettingName.Equals(obj.ToString, StringComparison.InvariantCulture)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return SettingName.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return SettingName
        End Function
#End Region
    End Class
End Namespace
