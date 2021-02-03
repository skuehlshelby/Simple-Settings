Imports SimpleSettings.CRUD

<TestClass>
Public Class DynamicBufferTests
    Private CUT As DynamicBuffer
    Private Const DefaultStartingSize As Byte = 128

    <TestInitialize>
    Public Sub Setup()
        CUT = New DynamicBuffer(StartingSize:=DefaultStartingSize)
        CUT.Value.Clear()
    End Sub
    <TestMethod>
    Public Sub ValueIsCapturedWhenInputIsLessThanCapacityMinusTwo()
        Dim TestString As String = New String("X"c, DefaultStartingSize \ 2)
        CUT.Value.Append(TestString)

        Assert.IsTrue(CUT.ValueCaptured)
    End Sub

    <TestMethod>
    Public Sub ValueIsNotCapturedWhenInputIsGreaterThanCapacityMinusTwo()
        Dim TestString As String = New String("X"c, DefaultStartingSize)
        CUT.Value.Append(TestString)

        Assert.IsFalse(CUT.ValueCaptured)
    End Sub
    <TestMethod>
    Public Sub CapturedValueMatchesStartingValue()
        Dim TestString As String = New String("X"c, DefaultStartingSize \ 2)
        CUT.Value.Append(TestString)

        Assert.AreEqual(TestString, CUT.ToString)
    End Sub
    <TestMethod>
    Public Sub BufferExpandsWhenValueIsNotCaptured()
        Dim TestString As String = New String("X"c, DefaultStartingSize)
        CUT.Value.Append(TestString)

        Assert.IsFalse(CUT.ValueCaptured)
        Assert.AreEqual(DefaultStartingSize * 2, CUT.Size)
    End Sub
End Class
