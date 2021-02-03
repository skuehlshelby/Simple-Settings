Imports SimpleSettings.Extensibility

Public Class Section
    Inherits UserDefinedSection

    Protected Sub New(SectionName As String)
        MyBase.New(SectionName)
    End Sub

    Public Shared ReadOnly Property General As Section = New Section(NameOf(General))

    Public Shared ReadOnly Property NamingConvention As Section = New Section("Naming Convention")
End Class
