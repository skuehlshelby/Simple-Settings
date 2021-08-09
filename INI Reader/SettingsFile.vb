Imports System.IO
Imports SimpleSettings.Extensibility

'''<inheritdoc/>
Friend Class SettingsFile
    Implements ISettingsFile
    Implements IDisposable

    Private _fileLocation As FileInfo
    Private _settings As IDictionary(Of String, ISetting)

    Public Sub New(filePath As String)
        _fileLocation = New FileInfo(filePath)
        _settings = New Dictionary(Of String, ISetting)
    End Sub

    Private Shared Function CreateKey(section As String, name As String) As String
        Return String.Join(String.Empty, section, name)
    End Function

    Public Overloads Function GetSetting(Of T)(section As String, name As String, defaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        Dim key As String = CreateKey(section, name)

        If Not _settings.ContainsKey(key) Then
            Dim initialValue As T = CRUD.CRUD.EntryRead(_fileLocation.FullName, section, name, defaultValue)
            _settings.Add(key, New Setting(Of T)(section, name, initialValue))
        End If

        Return DirectCast(_settings.Item(key), ISetting(Of T))
    End Function

    Public Overloads Function GetSetting(Of T)(section As UserDefinedSection, name As UserDefinedSetting, defaultValue As T) As ISetting(Of T) Implements ISettingsFile.GetSetting
        Return GetSetting(section.ToString, name.ToString, defaultValue)
    End Function

    Public Overloads Sub RemoveSetting(section As String, name As String) Implements ISettingsFile.RemoveSetting
        If _settings.ContainsKey(CreateKey(section, name)) Then
            _settings.Remove(CreateKey(section, name))
        End If
        
        CRUD.CRUD.EntryDelete(_fileLocation.FullName, section, name)
    End Sub

    Public Overloads Sub RemoveSetting(setting As ISetting) Implements ISettingsFile.RemoveSetting
        RemoveSetting(setting.Section, setting.Name)
    End Sub

    Public Overloads Sub RemoveSetting(section As UserDefinedSection, name As UserDefinedSetting) Implements ISettingsFile.RemoveSetting
        RemoveSetting(section.ToString, name.ToString)
    End Sub

    Public Sub WriteToDisk() Implements ISettingsFile.WriteToDisk
        For Each setting As ISetting In _settings.Values
            CRUD.CRUD.EntryWrite(_fileLocation.FullName, setting.Section, setting.Name, setting.ToString)
        Next setting
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        WriteToDisk()

        _fileLocation = Nothing
        _settings = Nothing
    End Sub

End Class