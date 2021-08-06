''' <summary>A collection of named settings grouped by section.</summary>
Public Interface ISettingsFile
    ''' <include file='Docs.xml' path='//methods/getSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section'/>
    ''' <include file='Docs.xml' path='//parameters/name'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Overloads Function GetSetting(Of T)(section As String, name As String, defaultValue As T) As ISetting(Of T)
    ''' <include file='Docs.xml' path='//methods/getSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Overloads Function GetSetting(Of T)(section As Extensibility.UserDefinedSection, name As Extensibility.UserDefinedSetting, defaultValue As T) As ISetting(Of T)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/setting'/>
    Overloads Sub RemoveSetting(setting As ISetting)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*'/>
    Overloads Sub RemoveSetting(section As String, name As String)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    Overloads Sub RemoveSetting(section As Extensibility.UserDefinedSection, name As Extensibility.UserDefinedSetting)

    Sub WriteToDisk()

End Interface