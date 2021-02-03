Imports SimpleSettings

<TestClass>
Public Class Cache_Tests
    Private CUT As ISettingsFile
    Private INITestFilePath As String = IO.Path.Combine(Environ("UserProfile"), "Desktop", "SettingsTest.ini")
    Private Function Environ(VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function

    <TestInitialize>
    Public Sub Setup()
        CUT = New SettingsFile(INITestFilePath)
    End Sub

    <TestCleanup>
    Public Sub GetSettingReturnsSameInstance()
        Dim SettingA As ISetting(Of String) = CUT.GetSetting("TestSection", "TestSetting", defaultValue:="DefaultValue")
        Dim SettingB As ISetting(Of String) = CUT.GetSetting("TestSection", "TestSetting", defaultValue:="ADifferentValue")

        Assert.IsTrue(SettingA Is SettingB)
    End Sub
End Class
Namespace TestingOnly
    Public Class Section
        Inherits Extensibility.UserDefinedSection
        Protected Sub New(name As String)
            MyBase.New(name)
        End Sub

        Public Shared ReadOnly General As Section = New Section(NameOf(General))
    End Class
    Public Class Setting
        Inherits Extensibility.UserDefinedSetting
        Protected Sub New(DisplayName As String)
            MyBase.New(DisplayName)
        End Sub

        Public Shared ReadOnly TestSetting As Setting = New Setting(NameOf(TestSetting))
        Public Shared ReadOnly AnotherSetting As Setting = New Setting(NameOf(AnotherSetting))
    End Class
End Namespace
