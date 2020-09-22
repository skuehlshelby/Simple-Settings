''' <summary>A collection of named settings grouped by section.</summary>
Public Interface ISettingsFile
    ''' <include file='Docs.xml' path='//methods/getSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section'/>
    ''' <include file='Docs.xml' path='//parameters/name'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Overloads Function GetSetting(Of T)(ByVal Section As String, ByVal Name As String, ByVal DefaultValue As T) As ISetting(Of T)
    ''' <include file='Docs.xml' path='//methods/getSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//genericParameters/T'/>
    Overloads Function GetSetting(Of T)(ByVal Section As Abstract.UserDefinedSection, ByVal Name As Abstract.UserDefinedSetting, ByVal DefaultValue As T) As ISetting(Of T)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/setting'/>
    Overloads Sub RemoveSetting(ByVal Setting As ISetting)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="string"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="string"]/*'/>
    Overloads Sub RemoveSetting(ByVal Section As String, ByVal Name As String)
    ''' <include file='Docs.xml' path='//methods/removeSetting'/>
    ''' <include file='Docs.xml' path='//parameters/section[@type="userDefined"]/*'/>
    ''' <include file='Docs.xml' path='//parameters/name[@type="userDefined"]/*'/>
    Overloads Sub RemoveSetting(ByVal Section As Abstract.UserDefinedSection, ByVal Name As Abstract.UserDefinedSetting)
End Interface
