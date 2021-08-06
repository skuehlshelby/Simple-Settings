Imports SimpleSettings.CRUD

<TestClass>
Public Class DynamicBufferTests
    Private _cut As DynamicBuffer
    Private Const DefaultStartingSize As Byte = 128

    <TestInitialize>
    Public Sub Setup()
        _cut = New DynamicBuffer(startingSize := DefaultStartingSize)
        _cut.Value.Clear()
    End Sub

    <TestMethod>
    Public Sub ValueIsCapturedWhenInputIsLessThanCapacityMinusTwo()
        Dim testString As String = New String("X"c, DefaultStartingSize\2)
        _cut.Value.Append(testString)

        Assert.IsTrue(_cut.ValueCaptured)
    End Sub

    <TestMethod>
    Public Sub ValueIsNotCapturedWhenInputIsGreaterThanCapacityMinusTwo()
        Dim testString As String = New String("X"c, DefaultStartingSize)
        _cut.Value.Append(testString)

        Assert.IsFalse(_cut.ValueCaptured)
    End Sub

    <TestMethod>
    Public Sub CapturedValueMatchesStartingValue()
        Dim testString As String = New String("X"c, DefaultStartingSize\2)
        _cut.Value.Append(testString)

        Assert.AreEqual(testString, _cut.ToString)
    End Sub

    <TestMethod>
    Public Sub BufferExpandsWhenValueIsNotCaptured()
        Dim testString As String = New String("X"c, DefaultStartingSize)
        _cut.Value.Append(testString)

        Assert.IsFalse(_cut.ValueCaptured)
        Assert.AreEqual(DefaultStartingSize*2, _cut.Size)
    End Sub
End Class
