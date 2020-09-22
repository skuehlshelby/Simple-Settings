Imports SettingsStorage

<TestClass>
Public Class Cache_Tests
    Private CUT As ISettingsFile
    Private INITestFilePath As String = IO.Path.Combine(Environ("UserProfile"), "Desktop", "SettingsTest.ini")
    Private Function Environ(ByVal VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestInitialize>
    Public Sub Setup()
        CUT = New SettingsFile(INITestFilePath)
    End Sub
    <TestCleanup>
    Public Sub GetSettingReturnsSameInstance()
        Dim SettingA As ISetting(Of String) = CUT.GetSetting("TestSection", "TestSetting", DefaultValue:="DefaultValue")
        Dim SettingB As ISetting(Of String) = CUT.GetSetting("TestSection", "TestSetting", DefaultValue:="ADifferentValue")

        Assert.IsTrue(SettingA Is SettingB)
    End Sub
End Class
Namespace TestingOnly
    Public Class Section
        Inherits SettingsStorage.Abstract.UserDefinedSection
        Protected Sub New(ByVal Value As Integer, ByVal DisplayName As String)
            MyBase.New(Value, DisplayName)
        End Sub

        Public Shared ReadOnly General As Section = New Section(0, "General")
    End Class
    Public Class Setting
        Inherits Abstract.UserDefinedSetting
        Protected Sub New(ByVal Value As Integer, ByVal DisplayName As String)
            MyBase.New(Value, DisplayName)
        End Sub

        Public Shared ReadOnly TestSetting As Setting = New Setting(0, "Test Setting")
        Public Shared ReadOnly AnotherSetting As Setting = New Setting(1, "Another Setting")
    End Class
End Namespace
