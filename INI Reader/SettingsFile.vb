Imports System.IO
Imports SimpleSettings.Extensibility

'''<inheritdoc/>
Friend Class SettingsFile
    Implements ISettingsFile
    Implements IDisposable
    Private Structure TThis
        Public FileLocation As FileInfo
        Public Settings As Dictionary(Of String, ISetting)
    End Structure

    Private This As TThis

    Public Sub New(filePath As String)
        This = New TThis With {
            .FileLocation = New FileInfo(filePath),
            .Settings = New Dictionary(Of String, ISetting)
            }
    End Sub

    Private Shared Function CreateKey(section As String, name As String) As String
        Return Join({section, name})
    End Function

    Public Overloads Function GetSetting(Of T)(section As String, name As String, defaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        NullGuard.ThrowIfNullOrEmpty(section, NameOf(section), NameOf(GetSetting))
        NullGuard.ThrowIfNullOrEmpty(name, NameOf(name), NameOf(GetSetting))

        Dim key As String = CreateKey(section, name)

        Try
            Return DirectCast(This.Settings.Item(key), ISetting(Of T))

        Catch ex As KeyNotFoundException
            This.Settings.Add(key, New Setting(Of T)(section, name, defaultValue) With {.Value = CRUD.CRUD.EntryRead(This.FileLocation.FullName, section, name, defaultValue)})
            Return DirectCast(This.Settings.Item(key), ISetting(Of T))

        Catch ex As InvalidCastException
            Throw New InvalidCastException($"The type of '{name}' is different from when it was initially declared.")
        End Try
    End Function

    Public Overloads Function GetSetting(Of T)(section As UserDefinedSection, name As UserDefinedSetting, defaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        NullGuard.ThrowIfNull(section, NameOf(section), NameOf(GetSetting))
        NullGuard.ThrowIfNull(name, NameOf(name), NameOf(GetSetting))

        Return GetSetting(section.ToString, name.ToString, defaultValue)
    End Function

    Public Overloads Sub RemoveSetting(section As String, name As String) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNullOrEmpty(section, NameOf(section), NameOf(RemoveSetting))
        NullGuard.ThrowIfNullOrEmpty(name, NameOf(name), NameOf(RemoveSetting))

        If This.Settings.Remove(CreateKey(section, name)) Then
            CRUD.CRUD.EntryDelete(This.FileLocation.FullName, section, name)
        End If
    End Sub

    Public Overloads Sub RemoveSetting(setting As ISetting) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNull(setting, NameOf(setting), NameOf(RemoveSetting))

        RemoveSetting(setting.Section, setting.Name)
    End Sub

    Public Overloads Sub RemoveSetting(section As UserDefinedSection, name As UserDefinedSetting) Implements ISettingsFile.RemoveSetting
        NullGuard.ThrowIfNull(section, NameOf(section), NameOf(RemoveSetting))
        NullGuard.ThrowIfNull(name, NameOf(name), NameOf(RemoveSetting))

        RemoveSetting(section.ToString, name.ToString)
    End Sub

    Friend Sub Dispose() Implements IDisposable.Dispose
        On Error Resume Next
        Static disposed As Boolean = False

        If Not disposed Then
            GC.SuppressFinalize(Me)

            For Each setting As ISetting In This.Settings.Values
                CRUD.CRUD.EntryWrite(This.FileLocation.FullName, setting.Section, setting.Name, setting.ToString)
            Next setting

            This.Settings.Clear()
            This.Settings = Nothing
            This.FileLocation = Nothing
            This = Nothing

            disposed = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
        MyBase.Finalize()
    End Sub
End Class