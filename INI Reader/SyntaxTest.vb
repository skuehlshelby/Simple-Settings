Module SyntaxTest
    Public Sub testing()
        Dim Cache As ISettingsFile = Factory.GetSettingsFile("Desktop")
        Dim Setting As ISetting(Of Boolean) = Cache.GetSetting("TEst", "Test", True)
        With Factory.GetSettingsFile(Environ$("Userprofile"), "Desktop", "SettingsTest")
            Dim SettingB As ISetting(Of String) = .GetSetting("TestSection", "TestSetting", "Value")
        End With
        Setting = Factory.GetSetting("Desktop", "Test", "Testing", True)
    End Sub
End Module
