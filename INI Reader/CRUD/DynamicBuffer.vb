Imports System.Text

Namespace CRUD
    Friend Class DynamicBuffer
        Private Structure TThis
            Public Buffer As StringBuilder
        End Structure

        Private This As TThis
        Public Sub New(Optional StartingSize As Integer = 128)
            This = New TThis With {
                    .Buffer = New StringBuilder(StartingSize)
                }
        End Sub
        Private Sub IncreaseSize()
            This.Buffer = New StringBuilder(Math.Min(This.Buffer.Capacity * 2, Integer.MaxValue))
        End Sub
        Public ReadOnly Property Size As Integer
            Get
                Return This.Buffer.Capacity
            End Get
        End Property
        Public ReadOnly Property ValueCaptured() As Boolean
            Get
                If This.Buffer.Capacity - Len(This.Buffer.ToString) > 2 OrElse This.Buffer.Capacity = Integer.MaxValue Then
                    Return True
                Else
                    IncreaseSize()
                    Return False
                End If
            End Get
        End Property
        Public Property Value As StringBuilder
            Get
                Return This.Buffer
            End Get
            Set(value As StringBuilder)
                If This.Buffer IsNot value Then
                    This.Buffer = value
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return This.Buffer.ToString()
        End Function
    End Class
End Namespace
