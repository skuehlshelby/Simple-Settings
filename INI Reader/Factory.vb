Imports System.IO
Imports SimpleSettings.Extensibility

''' <include file='Docs.xml' path='//classes/factory' />
Public NotInheritable Class Factory
    Private Shared ReadOnly Cache As IDictionary(Of String, ISettingsFile) = New Dictionary(Of String, ISettingsFile)

    Private Sub New()
    End Sub

    ''' <exception cref="ArgumentException"></exception>
    ''' <exception cref="ArgumentNullException"></exception>
    ''' <exception cref="Security.SecurityException"></exception>
    ''' <exception cref="NotSupportedException"></exception>
    ''' <exception cref="PathTooLongException"></exception>
    Private Shared Function StandardizeFileName(ParamArray pathComponents As String()) As String
        Return Path.ChangeExtension(Path.GetFullPath(Path.Combine(pathComponents)), ".ini")
    End Function

    ''' <include file='Docs.xml' path='//methods/getSettingsFile' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    Public Shared Function GetSettingsFile(ParamArray filePath As String()) As ISettingsFile
        Contracts.RequireNonNull(filePath, NameOf(filePath), nameOf(GetSettingsFile))
        Contracts.Require(Of ArgumentException)(filePath.Any(Function(segment) Not String.IsNullOrEmpty(segment)), $"Parameter '{NameOf(filePath)}' cannot contain only empty strings.")

        Dim settingsFileInfo As FileInfo = New FileInfo(StandardizeFileName(filePath))

        If settingsFileInfo.Directory.Exists Then
            If Not Cache.ContainsKey(settingsFileInfo.FullName) Then
                Cache.Add(settingsFileInfo.FullName, New SettingsFile(settingsFileInfo.FullName))
            End If
        Else
            Throw _
                New DirectoryNotFoundException(
                    $"Folder {settingsFileInfo.Directory.Name} must be created before file {settingsFileInfo.Name} can be created.")
        End If

        Return Cache.Item(settingsFileInfo.FullName)
    End Function

    ''' <include file='Docs.xml' path='//methods/getSetting/*[@parentClass="factory"]' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*' />
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*' />
    ''' <include file='Docs.xml' path='//parameters/defaultValue' />
    ''' <include file='Docs.xml' path='//genericParameters/T' />
    Public Overloads Shared Function GetSetting (Of T)(filePath As String, section As String, name As String, defaultValue As T) As ISetting(Of T)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(section), $"Parameter '{NameOf(section)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(name), $"Parameter '{NameOf(name)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(defaultValue IsNot Nothing, $"Parameter '{NameOf(defaultValue)}' cannot be null (Nothing in Visual Basic).")

        Return GetSettingsFile(filePath).GetSetting(section, name, defaultValue)
    End Function

    ''' <include file='Docs.xml' path='//methods/getSetting/*[@parentClass="factory"]' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*' />
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*' />
    ''' <include file='Docs.xml' path='//parameters/defaultValue' />
    ''' <include file='Docs.xml' path='//genericParameters/T' />
    Public Overloads Shared Function GetSetting (Of T)(filePath As String, section As UserDefinedSection, name As UserDefinedSetting, defaultValue As T) As ISetting(Of T)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(defaultValue IsNot Nothing, $"Parameter '{NameOf(defaultValue)}' cannot be null (Nothing in Visual Basic).")

        Return GetSettingsFile(filePath).GetSetting(section, name, defaultValue)
    End Function

    ''' <include file='Docs.xml' path='//methods/removeSetting' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*' />
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*' />
    Public Overloads Shared Sub RemoveSetting(filePath As String, section As String, name As String)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(section), $"Parameter '{NameOf(section)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(name), $"Parameter '{NameOf(name)}' cannot be empty.")

        Cache.Item(StandardizeFileName(filePath)).RemoveSetting(section, name)
    End Sub

    ''' <include file='Docs.xml' path='//methods/removeSetting' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/setting' />
    Public Overloads Shared Sub RemoveSetting(filePath As String, setting As ISetting)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(setting IsNot Nothing, $"Parameter '{NameOf(setting)}' cannot be null (Nothing in Visual Basic).")

        RemoveSetting(filePath, setting.Section, setting.Name)
    End Sub

    ''' <include file='Docs.xml' path='//methods/removeSetting' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*' />
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*' />
    Public Overloads Shared Sub RemoveSetting(filePath As String, section As UserDefinedSection, name As UserDefinedSetting)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")

        RemoveSetting(filePath, section.ToString, name.ToString)
    End Sub

    ''' <include file='Docs.xml' path='//methods/removeSettingsFile' />
    ''' <include file='Docs.xml' path='//parameters/filePath' />
    ''' <include file='Docs.xml' path='//parameters/killFile' />
    Public Shared Sub RemoveSettingsFile(filePath As String, Optional killFile As Boolean = False)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")

        Dim standardizedFileName As String = StandardizeFileName(filePath)

        If Cache.ContainsKey(standardizedFileName) Then
            DirectCast(Cache.Item(standardizedFileName), SettingsFile).Dispose()
            Cache.Remove(standardizedFileName)
        End If

        If killFile Then
            If File.Exists(standardizedFileName) Then
                Kill(standardizedFileName)
            End If
        End If
    End Sub

End Class