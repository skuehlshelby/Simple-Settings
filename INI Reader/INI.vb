Imports System.IO
Imports SimpleSettings.Extensibility

''' <summary>
''' The entry-point for this library. Use this class to create <c>ISetting</c> and <c>ISettingsFile</c> objects.<br/>
''' Settings can be accessed either through this class, or through an <c>ISettingsFile</c>. The underlying<br/>
''' values will be the same.<br/>
'''
''' The <c>WriteAllToDisk()</c> function should be called during program shutdown to save all settings.<br/>
''' It is not necessary to call this method at any other time.
''' </summary>
Public NotInheritable Class INI
    Private Shared ReadOnly Cache As IDictionary(Of String, ISettingsFile) = New Dictionary(Of String, ISettingsFile)

    Private Sub New()
    End Sub

    Private Shared Function StandardizeFileName(ParamArray pathComponents As String()) As String
        Return Path.ChangeExtension(Path.GetFullPath(Path.Combine(pathComponents)), ".ini")
    End Function

''' <summary>
''' Get a wrapper representing the settings file at the specified <paramref name="FilePath"/>.<br/>
''' If the file does not exist, it will be created. If the parent folder does not exist, a<br/>
''' <c>DirectoryNotFoundException</c> will be thrown.
''' </summary>
''' <param name="filePath">The full path to the target file. '.INI' suffix is optional.</param>
''' <exception cref="DirectoryNotFoundException"></exception>
''' <returns>An <c>ISettingsFile</c> object whose settings can be queried and updated.</returns>
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

''' <summary>
''' Get the setting with the specified <paramref name="FilePath"/>, <paramref name="Section"/>, and <paramref name="Name"/>.
''' </summary>
''' <typeparam name="T">
''' The type of value contained by the target setting.<br/>
''' A <see cref="ComponentModel.TypeConverter"/> capable of yielding type <typeparamref name="T"/> from a <c>String</c> must exist.<br/>
''' Most .NET types already have one.
''' </typeparam>
''' <param name="filePath">The full path to the target file. File suffix is optional.</param>
''' <param name="section">The section containing this setting.</param>
''' <param name="name">The name of this setting.</param>
''' <param name="defaultValue">The default value of this setting.</param>
''' <returns></returns>
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

''' <summary>
''' Get the setting with the specified <paramref name="FilePath"/>, <paramref name="Section"/>, and <paramref name="Name"/>.
''' </summary>
''' <typeparam name="T">
''' The type of value contained by the target setting.<br/>
''' A <see cref="ComponentModel.TypeConverter"/> capable of yielding type <typeparamref name="T"/> from a <c>String</c> must exist.<br/>
''' Most .NET types already have one.
''' </typeparam>
''' <param name="filePath">The full path to the target file. File suffix is optional.</param>
''' <param name="section">The section containing this setting.</param>
''' <param name="name">The name of this setting.</param>
''' <param name="defaultValue">The default value of this setting.</param>
''' <returns></returns>
    Public Overloads Shared Function GetSetting (Of T)(filePath As String, section As UserDefinedSection, name As UserDefinedSetting, defaultValue As T) As ISetting(Of T)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(defaultValue IsNot Nothing, $"Parameter '{NameOf(defaultValue)}' cannot be null (Nothing in Visual Basic).")

        Return GetSettingsFile(filePath).GetSetting(section, name, defaultValue)
    End Function


    Public Overloads Shared Sub RemoveSetting(filePath As String, section As String, name As String)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(section), $"Parameter '{NameOf(section)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(name), $"Parameter '{NameOf(name)}' cannot be empty.")

        Cache.Item(StandardizeFileName(filePath)).RemoveSetting(section, name)
    End Sub


    Public Overloads Shared Sub RemoveSetting(filePath As String, setting As ISetting)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(setting IsNot Nothing, $"Parameter '{NameOf(setting)}' cannot be null (Nothing in Visual Basic).")

        RemoveSetting(filePath, setting.Section, setting.Name)
    End Sub

    Public Overloads Shared Sub RemoveSetting(filePath As String, section As UserDefinedSection, name As UserDefinedSetting)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")
        Contracts.Require(Of ArgumentNullException)(section IsNot Nothing, $"Parameter '{NameOf(section)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentNullException)(name IsNot Nothing, $"Parameter '{NameOf(name)}' cannot be null (Nothing in Visual Basic).")

        RemoveSetting(filePath, section.ToString, name.ToString)
    End Sub

    Public Shared Sub RemoveSettingsFile(filePath As String, Optional deleteFile As Boolean = False)
        Contracts.Require(Of ArgumentNullException)(filePath IsNot Nothing, $"Parameter '{NameOf(filePath)}' cannot be null (Nothing in Visual Basic).")
        Contracts.Require(Of ArgumentException)(Not String.IsNullOrEmpty(filePath), $"Parameter '{NameOf(filePath)}' cannot be empty.")

        Dim standardizedFileName As String = StandardizeFileName(filePath)

        If Cache.ContainsKey(standardizedFileName) Then
            Cache.Remove(standardizedFileName)
        End If

        If deleteFile Then
            If File.Exists(standardizedFileName) Then
                Kill(standardizedFileName)
            End If
        End If
    End Sub

    Public Shared Sub WriteAllToDisk()
        For Each file As ISettingsFile In Cache.Values
            file.WriteToDisk()
        Next
    End Sub

End Class