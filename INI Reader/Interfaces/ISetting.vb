''' <include file='Docs.xml' path='//classes/setting'/>
Public Interface ISetting
    ''' <include file='Docs.xml' path='//properties/section'/>
    ReadOnly Property Section As String
    ''' <include file='Docs.xml' path='//properties/name'/>
    ReadOnly Property Name As String
    ''' <include file='Docs.xml' path='//methods/isWholeNumber'/>
    ReadOnly Property IsWholeNumber() As Boolean
    ''' <include file='Docs.xml' path='//methods/toLong'/>
    Function ToLong() As Long
    ''' <include file='Docs.xml' path='//methods/toString[@parentClass="setting"]'/>
    Function ToString() As String
    ''' <include file='Docs.xml' path='//methods/isBoolean'/>
    ReadOnly Property IsBoolean() As Boolean
    ''' <include file='Docs.xml' path='//methods/toBoolean'/>
    Function ToBoolean() As Boolean
    ''' <include file='Docs.xml' path='//methods/equals[@parentClass="setting"]'/>
    ''' <include file='Docs.xml' path='//parameters/obj[@parentMethod="equals"]'/>
    Function Equals(ByVal obj As Object) As Boolean
End Interface

''' <include file='Docs.xml' path='//classes/settingOfT'/>
''' <include file='Docs.xml' path='//genericParameters/T'/>
''' <inheritdoc/>
Public Interface ISetting(Of T)
    Inherits ISetting
    ''' <include file='Docs.xml' path='//events/settingChanged'/>
    Event SettingChanged(ByRef sender As Object, e As SettingChangedEventArgs(Of T))
    ''' <include file='Docs.xml' path='//properties/defaultValue'/>
    ReadOnly Property DefaultValue As T
    ''' <include file='Docs.xml' path='//properties/value/*'/>
    Property Value As T
End Interface
