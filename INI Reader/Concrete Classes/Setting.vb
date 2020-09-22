Imports System.Globalization
''' <inheritdoc/>
Friend MustInherit Class Setting
    Implements ISetting
    Private Structure TThis
        Public Name As String
        Public Section As String
    End Structure

    Private This As TThis

    Friend Sub New(ByVal Section As String, ByVal Name As String)
        This = New TThis With {
            .Name = Name,
            .Section = Section
        }
    End Sub
    Friend ReadOnly Property Name As String Implements ISetting.Name
        Get
            Return This.Name
        End Get
    End Property

    Friend ReadOnly Property Section As String Implements ISetting.Section
        Get
            Return This.Section
        End Get
    End Property

    Friend MustOverride ReadOnly Property IsWholeNumber() As Boolean Implements ISetting.IsWholeNumber

    Friend MustOverride Function ToLong() As Long Implements ISetting.ToLong

    Friend MustOverride ReadOnly Property IsBoolean() As Boolean Implements ISetting.IsBoolean

    Friend MustOverride Function ToBoolean() As Boolean Implements ISetting.ToBoolean

    Public MustOverride Overrides Function ToString() As String Implements ISetting.ToString

    Public Overrides Function Equals(obj As Object) As Boolean Implements ISetting.Equals
        If obj Is Nothing OrElse TypeOf obj IsNot Setting Then
            Return False
        Else
            With DirectCast(obj, Setting)
                Return This.Section = .Section AndAlso This.Name = .Name OrElse ReferenceEquals(Me, obj)
            End With
        End If
    End Function
    Public Overrides Function GetHashCode() As Integer
        On Error Resume Next
        Return CInt(397 ^ Name.GetHashCode() ^ Section.GetHashCode())
    End Function
End Class
Friend Class Setting(Of T)
    Inherits Setting
    Implements ISetting(Of T)
    Private Structure TThis
        Public DefaultValue As T
        Public Value As T
    End Structure
    Private This As TThis

    Public Event SettingChanged As ISetting(Of T).SettingChangedEventHandler Implements ISetting(Of T).SettingChanged

    Friend Sub New(ByVal Section As String, ByVal Name As String, ByVal DefaultValue As T)
        MyBase.New(Section, Name)
        This = New TThis With {
            .DefaultValue = DefaultValue,
            .Value = DefaultValue
        }
    End Sub
    Friend ReadOnly Property DefaultValue As T Implements ISetting(Of T).DefaultValue
        Get
            Return This.DefaultValue
        End Get
    End Property

    Friend Property Value As T Implements ISetting(Of T).Value
        Get
            Return This.Value
        End Get
        Set(NewValue As T)
            If Not This.Value.Equals(NewValue) Then
                Dim OldValue As T = This.Value
                This.Value = NewValue
                RaiseEvent SettingChanged(Me, New SettingChangedEventArgs(Of T)(OldValue, NewValue))
            End If
        End Set
    End Property

    Friend Overrides ReadOnly Property IsWholeNumber() As Boolean
        Get
            For Each Character As Char In This.Value.ToString
                If Not Char.IsDigit(Character) Then
                    Return False
                End If
            Next Character

            Return True
        End Get
    End Property

    Friend Overrides Function ToLong() As Long
        Return Long.Parse(This.Value.ToString, CultureInfo.InvariantCulture)
    End Function

    Friend Overrides ReadOnly Property IsBoolean() As Boolean
        Get
            Return Boolean.TryParse(This.Value.ToString, Nothing)
        End Get
    End Property

    Friend Overrides Function ToBoolean() As Boolean
        Return Boolean.Parse(This.Value.ToString)
    End Function

    Public Overrides Function ToString() As String
        Return This.Value.ToString
    End Function
End Class