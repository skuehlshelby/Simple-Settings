
Namespace Abstract
    ''' <include file="SectionAndSettingDocs.xml" path="//section"/>
    Public MustInherit Class UserDefinedSection
        Inherits Enumeration
        Protected Sub New(ByVal Value As Integer, ByVal DisplayName As String)
            MyBase.New(Value, DisplayName)
        End Sub
    End Class

    ''' <include file="SectionAndSettingDocs.xml" path="//setting"/>
    Public MustInherit Class UserDefinedSetting
        Inherits Enumeration
        Protected Sub New(ByVal Value As Integer, ByVal DisplayName As String)
            MyBase.New(Value, DisplayName)
        End Sub
    End Class
End Namespace
