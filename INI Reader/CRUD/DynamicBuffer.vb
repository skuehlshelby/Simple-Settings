Imports System.Text

Namespace CRUD
    Friend Class DynamicBuffer

        Private _buffer As StringBuilder

        Public Sub New(Optional startingSize As Integer = 128)
            _buffer = New StringBuilder(startingSize)
        End Sub

        Private Sub IncreaseSize()
            _buffer = New StringBuilder(Math.Min(_buffer.Capacity * 2, Integer.MaxValue))
        End Sub

        Public ReadOnly Property Size As Integer
            Get
                Return _buffer.Capacity
            End Get
        End Property

        Public ReadOnly Property ValueCaptured As Boolean
            Get
                If _buffer.Capacity - _buffer.Length  > 2 OrElse _buffer.Capacity = Integer.MaxValue _
                    Then
                    Return True
                Else
                    IncreaseSize()
                    Return False
                End If
            End Get
        End Property

        Public Property Value As StringBuilder
            Get
                Return _buffer
            End Get
            Set
                If _buffer IsNot value Then
                    _buffer = value
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return _buffer.ToString()
        End Function

    End Class

End Namespace
