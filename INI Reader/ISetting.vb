Imports System.Runtime.InteropServices

''' <include file='Docs.xml' path='//classes/setting'/>
Public Interface ISetting

    ''' <include file='Docs.xml' path='//properties/section'/>
    ReadOnly Property Section As String

    ''' <include file='Docs.xml' path='//properties/name'/>
    ReadOnly Property Name As String

    Function CastValue(Of T)() As T

    Function TryCastValue(Of T)(<Out>ByRef returnValue As T) As Boolean

End Interface

''' <include file='Docs.xml' path='//classes/settingOfT'/>
''' <include file='Docs.xml' path='//genericParameters/T'/>
''' <inheritdoc/>
Public Interface ISetting(Of T)
    Inherits ISetting

    ''' <include file='Docs.xml' path='//events/settingChanged'/>
    Event ValueChanged(ByRef sender As Object, e As ValueChangedEventArgs(Of T))

    ''' <include file='Docs.xml' path='//properties/value/*'/>
    Property Value As T

End Interface
