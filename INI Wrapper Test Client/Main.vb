Imports SimpleSettings

Module Main

    Sub Main()
        Dim testClientSettings As ISettingsFile = INI.GetSettingsFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestClientSettings")
        
        Dim title As ISetting(Of String) = testClientSettings.GetSetting(Section.General, Setting.Title, "Test Client")
        Dim greeting As ISetting(Of String) = testClientSettings.GetSetting(Section.General, Setting.Greeting, "Hello World!")
        Dim backgroundColor As ISetting(Of ConsoleColor) = testClientSettings.GetSetting(Section.General, Setting.BackGroundColor, ConsoleColor.Black)
        Dim cursorVisible As ISetting(Of Boolean) = testClientSettings.GetSetting("Another Section", "Cursor Visible", False)'Using extensibility classes is optional.

        AddHandler title.ValueChanged, AddressOf OnTitleChange
        AddHandler backgroundColor.ValueChanged, AddressOf OnBackgroundColorChange
        AddHandler cursorVisible.ValueChanged, AddressOf OnCursorVisibleChange

        Console.Title = title.Value
        Console.BackgroundColor = backgroundColor.Value
        Console.CursorVisible = cursorVisible.Value

        Console.WriteLine(greeting.Value)
        Console.WriteLine()

        Console.Write("Please type a new window title: ")
        title.Value = Console.ReadLine()
        Console.WriteLine()

        Dim newColor As ConsoleColor
        Console.Write("Please type a new background color: ")
        If [Enum].TryParse(Console.ReadLine(), newColor) Then
            backgroundColor.Value = newColor
        End If
        Console.WriteLine()

        Console.Write("Should the cursor be visible? (True/False): ")
        Dim newValue As Boolean = False
        If Boolean.TryParse(Console.ReadLine(), newValue) Then
            cursorVisible.Value = newValue
        End If
        Console.WriteLine()

        Console.Write("Please type a greeting to use next time the application runs: ")
        greeting.Value = Console.ReadLine()
        Console.WriteLine()

        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()

        INI.WriteAllToDisk()
    End Sub

    Private Sub OnCursorVisibleChange(ByRef sender As Object, e As ValueChangedEventArgs(Of Boolean))
        Console.WriteLine($"{NameOf(Console.CursorVisible)} was updated with an event. The old value was '{e.OldValue}', and the new value is '{e.NewValue}'.")
        Console.CursorVisible = e.NewValue
    End Sub

    Private Sub OnBackgroundColorChange(ByRef sender As Object, e As ValueChangedEventArgs(Of ConsoleColor))
        Console.WriteLine($"{NameOf(Console.BackgroundColor)} was updated with an event. The old value was '{e.OldValue}', and the new value is '{e.NewValue}'.")
        Console.BackgroundColor = e.NewValue
    End Sub

    Private Sub OnTitleChange(ByRef sender As Object, e As ValueChangedEventArgs(Of String))
        Console.WriteLine($"{NameOf(Console.Title)} was updated with an event. The old value was '{e.OldValue}', and the new value is '{e.NewValue}'.")
        Console.Title = e.NewValue
    End Sub
End Module
