Public Class SettingChangedEventArgs(Of T)
    Private Structure TThis
        Public OldValue As T
        Public NewValue As T
    End Structure

    Private This As TThis
    Public Sub New(OldValue As T, ByVal NewValue As T)
        This = New TThis With {
            .OldValue = OldValue,
            .NewValue = NewValue
        }
    End Sub
    Public ReadOnly Property OldValue As T
        Get
            Return This.OldValue
        End Get
    End Property

    Public ReadOnly Property NewValue As T
        Get
            Return This.NewValue
        End Get
    End Property
End Class