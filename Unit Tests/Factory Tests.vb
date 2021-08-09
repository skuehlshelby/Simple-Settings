Imports System.IO
Imports SimpleSettings

<TestClass>
Public Class FactoryTests
    Private ReadOnly _iniTestFilePath As String = Path.Combine(Environ("UserProfile"), "Desktop", "SettingsTest")

    Private Shared Function Environ(variableName As String) As String
        Return Environment.GetEnvironmentVariable(variableName)
    End Function

    <TestMethod>
    Public Sub GetSettingsFileReturnsASettingsFile()
        Assert.IsTrue(TypeOf INI.GetSettingsFile(Environ("userprofile"), "Desktop", "SettingsTest") Is ISettingsFile)
    End Sub

    <TestMethod>
    Public Sub NullChecks()
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.GetSetting (Of Boolean)(String.Empty, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.GetSetting (Of Boolean)(_iniTestFilePath, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.GetSetting (Of Boolean)(_iniTestFilePath, "TestSection", String.Empty, Nothing))

        Assert.ThrowsException (Of ArgumentNullException)(
            Sub() INI.GetSetting(_iniTestFilePath, Nothing, "TestName", True))
        Assert.ThrowsException (Of ArgumentNullException)(
            Sub() INI.GetSetting(_iniTestFilePath, "TestSection", Nothing, True))

        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.GetSettingsFile(String.Empty, String.Empty, String.Empty))

        Assert.ThrowsException (Of ArgumentException)(Sub() INI.RemoveSetting(String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentNullException)(Sub() INI.RemoveSetting(_iniTestFilePath, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.RemoveSetting(String.Empty, String.Empty, String.Empty))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.RemoveSetting(_iniTestFilePath, String.Empty, String.Empty))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() INI.RemoveSetting(_iniTestFilePath, "TestSection", String.Empty))
    End Sub

End Class

