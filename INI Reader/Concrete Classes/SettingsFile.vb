Imports SettingsStorage.Abstract
Imports FileInfo = System.IO.FileInfo
'''<inheritdoc/>
Friend Class SettingsFile
    Implements ISettingsFile
    Implements IDisposable

    Private Structure TThis
        Public FileLocation As FileInfo
        Public Settings As Dictionary(Of String, ISetting)
    End Structure

    Private This As TThis

    Friend Sub New(ByVal FilePath As String)
        This = New TThis With {
            .FileLocation = New FileInfo(FilePath),
            .Settings = New Dictionary(Of String, ISetting)
        }
    End Sub

    Private Shared Function CreateKey(Section As String, Name As String) As String
        Return Join({Section, Name})
    End Function

    Public Overloads Function GetSetting(Of T)(Section As String, Name As String, DefaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        NullGuard.ThrowIfNullOrEmpty(Section, NameOf(Section), NameOf(GetSetting))
        NullGuard.ThrowIfNullOrEmpty(Name, NameOf(Name), NameOf(GetSetting))

        Dim Key As String = CreateKey(Section, Name)

        Try
            Return DirectCast(This.Settings.Item(Key), ISetting(Of T))

        Catch Ex As KeyNotFoundException
            This.Settings.Add(Key, New Setting(Of T)(Section, Name, DefaultValue) With {.Value = INI.CRUD.EntryRead(This.FileLocation.FullName, Section, Name, DefaultValue)})
            Return DirectCast(This.Settings.Item(Key), ISetting(Of T))

        Catch Ex As InvalidCastException
            Throw New InvalidCastException("The type of '" & Name & "' is different from when it was initially declared.")
        End Try
    End Function

    Public Overloads Function GetSetting(Of T)(Section As UserDefinedSection, Name As UserDefinedSetting, DefaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        NullGuard.ThrowIfNull(Section, NameOf(Section), NameOf(GetSetting))
        NullGuard.ThrowIfNull(Name, NameOf(Name), NameOf(GetSetting))

        Return GetSetting(Section.ToString, Name.ToString, DefaultValue)
    End Function

    Public Overloads Sub RemoveSetting(ByVal Section As String, ByVal Name As String) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNullOrEmpty(Section, NameOf(Section), NameOf(RemoveSetting))
        NullGuard.ThrowIfNullOrEmpty(Name, NameOf(Name), NameOf(RemoveSetting))

        If This.Settings.Remove(CreateKey(Section, Name)) Then
            INI.CRUD.EntryDelete(This.FileLocation.FullName, Section, Name)
        End If
    End Sub

    Public Overloads Sub RemoveSetting(Setting As ISetting) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNull(Setting, NameOf(Setting), NameOf(RemoveSetting))

        RemoveSetting(Setting.Section, Setting.Name)
    End Sub

    Public Overloads Sub RemoveSetting(Section As UserDefinedSection, Name As UserDefinedSetting) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNull(Section, NameOf(Section), NameOf(RemoveSetting))
        NullGuard.ThrowIfNull(Name, NameOf(Name), NameOf(RemoveSetting))

        RemoveSetting(Section.ToString, Name.ToString)
    End Sub

    Friend Sub Dispose() Implements IDisposable.Dispose
        Static Disposed As Boolean = False

        If Not Disposed Then
            For Each Setting As ISetting In This.Settings.Values
                INI.CRUD.EntryWrite(This.FileLocation.FullName, Setting.Section, Setting.Name, Setting.ToString)
            Next Setting

            This.Settings.Clear()

            Disposed = True
            GC.SuppressFinalize(Me)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
        MyBase.Finalize()
    End Sub
End Class