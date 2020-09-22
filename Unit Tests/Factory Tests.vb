Imports SettingsStorage
<TestClass>
Public Class Factory_Tests
    Private ReadOnly INITestFilePath As String = IO.Path.Combine(Environ("UserProfile"), "Desktop", "SettingsTest")
    Private Function Environ(ByVal VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestMethod>
    Public Sub GetSettingsFileReturnsASettingsFile()
        Assert.IsTrue(TypeOf Factory.GetSettingsFile(Environ("userprofile"), "Desktop", "SettingsTest") Is ISettingsFile)
    End Sub
    <TestMethod>
    Public Sub NullChecks()
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.GetSetting(Of Boolean)(String.Empty, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.GetSetting(Of Boolean)(INITestFilePath, String.Empty, String.Empty, Nothing))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.GetSetting(Of Boolean)(INITestFilePath, "TestSection", String.Empty, Nothing))

        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.GetSettingsFile(String.Empty, String.Empty, String.Empty))

        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.RemoveSetting(String.Empty, Nothing))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.RemoveSetting(INITestFilePath, Nothing))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.RemoveSetting(String.Empty, String.Empty, String.Empty))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.RemoveSetting(INITestFilePath, String.Empty, String.Empty))
        Assert.ThrowsException(Of ArgumentNullException)(Sub() Factory.RemoveSetting(INITestFilePath, "TestSection", String.Empty))
    End Sub
End Class

