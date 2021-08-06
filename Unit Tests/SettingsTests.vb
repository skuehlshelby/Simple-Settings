Imports SimpleSettings

<TestClass>
Public Class SettingsTests
    Private ReadOnly INITestFilePath As String = IO.Path.Combine(Environ("UserProfile"), "Desktop", "SettingsTest")
    Private Function Environ(VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestMethod>
    Public Sub SettingIdentifiesBooleanValue()
        Assert.IsTrue(New Setting(Of Boolean)("TestSection", "TestSetting", True).TryCastValue(Of Boolean)(Nothing))
    End Sub
    <TestMethod>
    Public Sub SettingIdentifiesWholeNumbers()
        For Each testCase As ISetting In New List(Of ISetting) From {
                                                            New Setting(Of Long)("TestSection", "TestSetting", 123456),
                                                            New Setting(Of Byte)("TestSection", "TestSetting", 12),
                                                            New Setting(Of Integer)("TestSection", "TestSetting", 0)}
            Assert.IsTrue(TestCase.TryCastValue(Of Long)(Nothing))
        Next TestCase
    End Sub

    <TestMethod>
    Public Sub StringConversionRetainsOriginalValue()
        Const LongValue As Long = 1234567
        Const IntValue As Integer = 12345
        Const ByteValue As Byte = 12
        Const StringValue As String = "ghfjkda"
        Const BooleanValue As Boolean = True

        Assert.AreEqual(LongValue.ToString, New Setting(Of Long)("TestSection", "TestSetting", LongValue).ToString)
        Assert.AreEqual(IntValue.ToString, New Setting(Of Integer)("TestSection", "TestSetting", IntValue).ToString)
        Assert.AreEqual(ByteValue.ToString, New Setting(Of Byte)("TestSection", "TestSetting", ByteValue).ToString)
        Assert.AreEqual(StringValue, New Setting(Of String)("TestSection", "TestSetting", StringValue).ToString)
        Assert.AreEqual(BooleanValue.ToString, New Setting(Of Boolean)("TestSection", "TestSetting", BooleanValue).ToString)
    End Sub
    <TestMethod>
    Public Sub ValuesAreSynchronizedAcrossInstances()
        Dim Cache As ISettingsFile = Factory.GetSettingsFile(INITestFilePath)

        Dim SettingA As ISetting(Of Long) = Cache.GetSetting(Of Long)("TestSettings", "LongSetting", 1)
        Dim SettingB As ISetting(Of Long) = Cache.GetSetting(Of Long)("TestSettings", "LongSetting", 1)

        SettingA.Value = 25

        Assert.AreEqual(SettingA.Value, SettingB.Value)

        Factory.RemoveSettingsFile(INITestFilePath, True)
    End Sub
    <TestMethod>
    Public Sub EventIsRaisedWhenSettingValueChanges()
        Const DefaultValue As Long = 1
        Const NewValue As Long = 56

        Dim EventsReceived As List(Of String) = New List(Of String)
        Dim SettingA As ISetting(Of Long) = Factory.GetSetting(INITestFilePath, "TestSettings", "LongSetting", DefaultValue)

        AddHandler SettingA.ValueChanged, (Sub(ByRef Sender As Object, E As ValueChangedEventArgs(Of Long))
                                                 EventsReceived.Add(NameOf(E))
                                             End Sub)
        SettingA.Value = NewValue

        Assert.AreEqual(1, EventsReceived.Count)
        Assert.AreEqual("E", EventsReceived.First())
        Assert.AreEqual(NewValue, SettingA.Value)
    End Sub

    <TestInitialize>
    Public Sub Setup()
        If IO.File.Exists(INITestFilePath) Then
            Kill(INITestFilePath)
        End If
    End Sub
    <TestCleanup>
    Public Sub Cleanup()
        If IO.File.Exists(INITestFilePath) Then
            Kill(INITestFilePath)
        End If
    End Sub
End Class
