Public Class ValueChangedEventArgs (Of T)

    Public Sub New(oldValue As T, newValue As T)
        Me.OldValue = oldValue
        Me.NewValue = newValue
    End Sub

    Public ReadOnly Property OldValue As T

    Public ReadOnly Property NewValue As T

End Class