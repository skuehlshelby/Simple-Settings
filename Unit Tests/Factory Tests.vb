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
        Assert.IsTrue(TypeOf Factory.GetSettingsFile(Environ("userprofile"), "Desktop", "SettingsTest") Is ISettingsFile)
    End Sub

    <TestMethod>
    Public Sub NullChecks()
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.GetSetting (Of Boolean)(String.Empty, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.GetSetting (Of Boolean)(_iniTestFilePath, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.GetSetting (Of Boolean)(_iniTestFilePath, "TestSection", String.Empty, Nothing))

        Assert.ThrowsException (Of ArgumentNullException)(
            Sub() Factory.GetSetting(_iniTestFilePath, Nothing, "TestName", True))
        Assert.ThrowsException (Of ArgumentNullException)(
            Sub() Factory.GetSetting(_iniTestFilePath, "TestSection", Nothing, True))

        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.GetSettingsFile(String.Empty, String.Empty, String.Empty))

        Assert.ThrowsException (Of ArgumentException)(Sub() Factory.RemoveSetting(String.Empty, Nothing))
        Assert.ThrowsException (Of ArgumentNullException)(Sub() Factory.RemoveSetting(_iniTestFilePath, Nothing))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.RemoveSetting(String.Empty, String.Empty, String.Empty))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.RemoveSetting(_iniTestFilePath, String.Empty, String.Empty))
        Assert.ThrowsException (Of ArgumentException)(
            Sub() Factory.RemoveSetting(_iniTestFilePath, "TestSection", String.Empty))
    End Sub

End Class

