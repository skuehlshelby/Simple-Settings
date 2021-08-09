Imports System.Runtime.InteropServices

Public Interface ISetting

    ReadOnly Property Section As String

    ReadOnly Property Name As String

    Function CastValue(Of T)() As T

    Function TryCastValue(Of T)(<Out>ByRef returnValue As T) As Boolean

End Interface


Public Interface ISetting(Of T)
    Inherits ISetting

    Event ValueChanged(ByRef sender As Object, e As ValueChangedEventArgs(Of T))

    Property Value As T

End Interface
