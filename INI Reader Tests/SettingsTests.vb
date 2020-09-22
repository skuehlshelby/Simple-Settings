Imports Microsoft.VisualStudio.TestTools.UnitTesting
<TestClass>
Public Class SettingsTests
    Private CUT As Setting
    Private Function Environ(ByVal VariableName As String) As String
        Return Environment.GetEnvironmentVariable(VariableName)
    End Function
    <TestMethod>
    Public Sub SettingIdentifiesBooleanValue()
        CUT = New Setting(Of Boolean)("TestSection", "TestSetting", True)
        Assert.IsTrue(CUT.IsBoolean)
    End Sub
    <TestMethod>
    Public Sub SettingIdentifiesWholeNumbers()
        For Each TestCase As Setting In New List(Of Setting) From {
                                                            New Setting(Of Long)("TestSection", "TestSetting", 123456),
                                                            New Setting(Of Byte)("TestSection", "TestSetting", 12),
                                                            New Setting(Of Integer)("TestSection", "TestSetting", 0)}
            Assert.IsTrue(TestCase.IsWholeNumber)
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
        Dim Cache As ISettingsFile = New SettingsFile(IO.Path.Combine(Environ("userprofile"), "Desktop", "SettingsTest"))

        Dim SettingA As ISetting(Of Long) = Cache.GetSetting(Of Long)("TestSettings", "LongSetting", 1)
        Dim SettingB As ISetting(Of Long) = Cache.GetSetting(Of Long)("TestSettings", "LongSetting", 1)

        SettingA.Value = 25

        Assert.AreEqual(SettingA.Value, SettingB.Value)
    End Sub
    <TestMethod>
    Public Sub EventIsRaisedWhenSettingValueChanges()
        Const DefaultValue As Long = 1
        Const NewValue As Long = 56

        Dim EventsReceived As List(Of String) = New List(Of String)
        Dim SettingA As ISetting(Of Long) = Factory.GetSetting(Of Long)(IO.Path.Combine(Environ("userprofile"), "Desktop", "SettingsTest"), "TestSettings", "LongSetting", DefaultValue)

        AddHandler SettingA.SettingChanged, (Sub(Sender As Object, E As SettingChangedEventArgs(Of Long))
                                                 EventsReceived.Add(NameOf(E))
                                             End Sub)
        SettingA.Value = NewValue

        Assert.AreEqual(1, EventsReceived.Count)
        Assert.AreEqual("E", EventsReceived.First())
        Assert.AreEqual(NewValue, SettingA.Value)
    End Sub

    <TestInitialize>
    Public Sub Setup()
        If IO.Directory.GetFiles(IO.Path.Combine(Environ("userprofile"), "Desktop")).Contains("SettingsTest.ini") Then
            Kill(Environ("userprofile") & "\Desktop")
        End If
    End Sub
End Class
