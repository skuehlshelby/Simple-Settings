# Simple-Settings
Provides a fast and easy way to manage application settings in a desktop environment. Setting values are guaranteed to be up-to-date at all times. Uses .ini files internally. Multiple files are supported.

# Example
        Dim greeting As ISetting(Of String) = INI.GetSetting("Path-To-My-File", Section.General, Setting.Greeting, "Hello World!")

        Console.WriteLine(greeting.Value)

        INI.WriteAllToDisk()
