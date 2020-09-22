Imports Path = System.IO.Path
Imports System.Globalization

#If DEBUG Then
Imports InternalsVisibleTo = System.Runtime.CompilerServices.InternalsVisibleToAttribute
<Assembly: InternalsVisibleTo("Unit Tests", AllInternalsVisible:=True)>
#End If

''' <include file='Docs.xml' path='//classes/factory'/>
Public NotInheritable Class Factory

    Private Shared Cache As Dictionary(Of String, ISettingsFile) = New Dictionary(Of String, ISettingsFile)

    Private Sub New()
    End Sub

    Private Shared Function StandardizeFileName(ParamArray PathComponents As String()) As String
        Return Path.ChangeExtension(Path.GetFullPath(Path.Combine(PathComponents)), ".ini")
    End Function

    ''' <include file='Docs.xml' path='//methods/getSettingsFile'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    Public Shared Function GetSettingsFile(ParamArray FilePath As String()) As ISettingsFile
        Dim SettingsFileInfo As IO.FileInfo = New IO.FileInfo(StandardizeFileName(FilePath))

        If SettingsFileInfo.Directory.Exists Then
            If Not Cache.ContainsKey(SettingsFileInfo.FullName) Then
                Cache.Add(SettingsFileInfo.FullName, New SettingsFile(SettingsFileInfo.FullName))
            End If
        Else
            Throw New IO.DirectoryNotFoundException(String.Format(CultureInfo.InvariantCulture, "Folder {0} must be created before file {1} can be created.", SettingsFileInfo.Directory.Name, SettingsFileInfo.Name))
        End If

        Return Cache.Item(SettingsFileInfo.FullName)
    End Function

    ''' <include file='Docs.xml' path='//methods/getSetting/*[@parentClass="factory"]'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/defaultValue'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Public Overloads Shared Function GetSetting(Of T)(ByVal FilePath As String, ByVal Section As String, ByVal Name As String, ByVal DefaultValue As T) As ISetting(Of T)
        Return GetSettingsFile(FilePath).GetSetting(Section, Name, DefaultValue)
    End Function
    ''' <include file='Docs.xml' path='//methods/getSetting/*[@parentClass="factory"]'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/defaultValue'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Public Overloads Shared Function GetSetting(Of T)(ByVal FilePath As String, Section As Abstract.UserDefinedSection, Name As Abstract.UserDefinedSetting, DefaultValue As T) As ISetting(Of T)
        Return GetSettingsFile(FilePath).GetSetting(Section, Name, DefaultValue)
    End Function

    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*'/>
    Public Overloads Shared Sub RemoveSetting(ByVal FilePath As String, ByVal Section As String, ByVal Name As String)
        Try
            Cache.Item(StandardizeFileName(FilePath)).RemoveSetting(Section, Name)
        Catch Ex As KeyNotFoundException
        Catch Ex As ArgumentNullException
        End Try
    End Sub
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/setting'/>
    Public Overloads Shared Sub RemoveSetting(ByVal FilePath As String, ByVal Setting As ISetting)
        If Setting Is Nothing Then
            Throw New ArgumentNullException(NameOf(Setting))
        Else
            RemoveSetting(FilePath, Setting.Section, Setting.Name)
        End If
    End Sub
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    Public Overloads Shared Sub RemoveSetting(ByVal FilePath As String, Section As Abstract.UserDefinedSection, Name As Abstract.UserDefinedSetting)
        If Section Is Nothing OrElse Name Is Nothing Then
            Throw New ArgumentNullException(If(Section Is Nothing, NameOf(Section), NameOf(Name)))
        Else
            RemoveSetting(FilePath, Section.ToString, Name.ToString)
        End If
    End Sub

    ''' <include file='Docs.xml' path='//methods/removeSettingsFile'/>
    ''' <include file='Docs.xml' path='//parameters/filePath'/>
    ''' <include file='Docs.xml' path='//parameters/killFile'/>
    Public Shared Sub RemoveSettingsFile(ByVal FilePath As String, Optional ByVal KillFile As Boolean = False)
        Dim StandardizedFileName As String = StandardizeFileName(FilePath)

        If Cache.ContainsKey(StandardizedFileName) Then
            DirectCast(Cache.Item(StandardizedFileName), SettingsFile).Dispose()
            Cache.Remove(StandardizedFileName)
        End If

        If KillFile Then
            If IO.File.Exists(StandardizedFileName) Then
                Kill(StandardizedFileName)
            End If
        End If
    End Sub
End Class
