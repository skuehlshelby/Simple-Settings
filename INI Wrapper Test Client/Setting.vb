Imports SimpleSettings.Extensibility

Public Class Setting
    Inherits UserDefinedSetting
    
    Private Sub New(name As String)
        MyBase.New(name)
    End Sub

    Public Shared ReadOnly Property Title As Setting = New Setting(NameOf(Title))

    Public Shared ReadOnly Property Greeting As Setting = New Setting(NameOf(Greeting))

    Public Shared ReadOnly Property BackGroundColor As Setting = New Setting(NameOf(BackGroundColor))
End Class