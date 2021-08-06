Imports SimpleSettings.CRUD

<TestClass>
Public Class INIReadWriteTests
    Private INITestFilePath As String = IO.Path.Combine(Environ("UserProfile"), "Desktop", "ReadWriteTest")
    Private Function Environ(VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestInitialize>
    Public Sub Setup()
        If IO.File.Exists(INITestFilePath) Then
            Kill(INITestFilePath)
        End If
    End Sub
    <TestMethod>
    Public Sub ReadReturnsDefaultIfFileDoesNotExist()
        Const DefaultValue As Byte = 128

        Assert.IsFalse(IO.File.Exists(INITestFilePath))
        Assert.AreEqual(DefaultValue, CRUD.EntryRead(Of Byte)(INITestFilePath, "TestSection", "TestSetting", DefaultValue))
    End Sub
    <TestMethod>
    Public Sub ReadReturnsActualValueIfFileExists()
        Const DefaultValue As Byte = 128
        Const NewValue As Byte = 15

        CRUD.EntryWrite(INITestFilePath, "TestSection", "TestSetting", NewValue)

        If IO.File.Exists(INITestFilePath) Then
            Assert.AreEqual(NewValue, CRUD.EntryRead(INITestFilePath, "TestSection", "TestSetting", NewValue))
        Else
            Assert.AreEqual(NewValue, CRUD.EntryRead(INITestFilePath, "TestSection", "TestSetting", DefaultValue))
        End If
    End Sub
    <TestMethod>
    Public Sub WriteCanCreateFile()
        CRUD.EntryWrite(INITestFilePath, "TestSection", "TestSetting", 123456)
        Assert.IsTrue(IO.File.Exists(INITestFilePath))
    End Sub
    <TestMethod>
    Public Sub WriteWorks()
        Const TestValue As Long = 123456
        CRUD.EntryWrite(INITestFilePath, "TestSection", "TestSetting", TestValue)
        Assert.AreEqual(TestValue, CRUD.EntryRead(Of Long)(INITestFilePath, "TestSection", "TestSetting", 12))
    End Sub
    <TestMethod>
    Public Sub ClearValueWorks()
        Const TestValue As Long = 123456
        CRUD.EntryWrite(INITestFilePath, "TestSection", "TestSetting", TestValue)
        CRUD.EntryDelete(INITestFilePath, "TestSection", "TestSetting")

        Dim RemainingText As String = IO.File.ReadAllText(INITestFilePath).Trim(Environment.NewLine.ToCharArray)

        Assert.AreEqual("[TestSection]", RemainingText)
    End Sub

    <TestCleanup>
    Public Sub Teardown()
        If IO.File.Exists(INITestFilePath) Then
            Kill(INITestFilePath)
        End If
    End Sub
End Class
