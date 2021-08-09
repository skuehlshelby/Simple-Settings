Imports SimpleSettings.Extensibility

Public Class Section
    Inherits UserDefinedSection

    Public Sub New(name As String)
        MyBase.New(name)
    End Sub
    

    Public Shared ReadOnly Property General As Section = New Section(NameOf(General))
End Class