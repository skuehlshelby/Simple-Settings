Imports SettingsStorage
<TestClass>
Public Class Factory_Tests
    Private Function Environ(ByVal VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestMethod>
    Public Sub Test1()
        Assert.IsTrue(TypeOf Factory.GetSettingsFile(Environ("userprofile"), "Desktop", "SettingsTest") Is ISettingsFile)
    End Sub
    <TestMethod>
    Public Sub NullChecks()

    End Sub
End Class

