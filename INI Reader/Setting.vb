Imports System.Runtime.InteropServices

Friend Class Setting(Of T)
    Implements ISetting
    Implements ISetting(Of T)

    Public Event ValueChanged As ISetting(Of T).ValueChangedEventHandler Implements ISetting(Of T).ValueChanged

    Private _value As T

    Friend Sub New(section As String, name As String, value As T)
        Me.Section = section
        Me.Name = name
        _value = value
    End Sub

    Public ReadOnly Property Name As String Implements ISetting.Name

    Public ReadOnly Property Section As String Implements ISetting.Section

    Friend Property Value As T Implements ISetting(Of T).Value
        Get
            Return _value
        End Get
        Set
            If Not _value.Equals(value) Then
                Dim e As ValueChangedEventArgs(Of T) = New ValueChangedEventArgs(Of T)(_value, value)
                _value = Value
                RaiseEvent ValueChanged(Me, e)
            End If
        End Set
    End Property

    Public Function CastValue(Of TValue)() As TValue Implements ISetting.CastValue
        Return CType(CType(Value, Object), TValue)
    End Function

    Public Function TryCastValue(Of TValue)(<Out> ByRef returnValue As TValue) As Boolean Implements ISetting.TryCastValue
        returnValue = Nothing

        Try
            returnValue = CType(CType(Value, Object), TValue)
            Return True
        Catch ex As InvalidCastException
            Return False
        End Try
    End Function

    Public Overrides Function ToString() As String
        Return Value.ToString()
    End Function

End Class