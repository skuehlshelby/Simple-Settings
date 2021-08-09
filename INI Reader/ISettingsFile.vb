''' <summary>
''' A collection of named settings grouped by section.
''' </summary>
Public Interface ISettingsFile

    ''' <summary>
    ''' Get the setting in the specified <paramref name="Section"/> with the specified <paramref name="Name"/>.
    ''' </summary>
    ''' <typeparam name="T">
    ''' The type of value contained by the target setting.<br/>
    ''' A <see cref="ComponentModel.TypeConverter"/> capable of yielding type <typeparamref name="T"/> from a <c>String</c> must exist.<br/>
    ''' Most .NET types already have one.
    ''' </typeparam>
    ''' <param name="section">The section containing this setting.</param>
    ''' <param name="name">The name of this setting.</param>
    ''' <param name="defaultValue">The default value of this setting. If the requested setting is not on disk, it is created using this value as the initial value.</param>
    ''' <returns></returns>
    Function GetSetting(Of T)(section As String, name As String, defaultValue As T) As ISetting(Of T)

    ''' <summary>
    ''' Get the setting in the specified <paramref name="Section"/> with the specified <paramref name="Name"/>.
    ''' </summary>
    ''' <typeparam name="T">
    ''' The type of value contained by the target setting.<br/>
    ''' A <see cref="ComponentModel.TypeConverter"/> capable of yielding type <typeparamref name="T"/> from a <c>String</c> must exist.<br/>
    ''' Most .NET types already have one.
    ''' </typeparam>
    ''' <param name="section">The section containing this setting.</param>
    ''' <param name="name">The name of this setting.</param>
    ''' <param name="defaultValue">The default value of this setting. If the requested setting is not on disk, it is created using this value as the initial value.</param>
    ''' <returns></returns>
    Function GetSetting(Of T)(section As Extensibility.UserDefinedSection, name As Extensibility.UserDefinedSetting, defaultValue As T) As ISetting(Of T)
    
    ''' <summary>
    ''' Remove a setting from this file.
    ''' </summary>
    ''' <param name="setting">The setting to remove.</param>
    Sub RemoveSetting(setting As ISetting)

    ''' <summary>
    ''' Remove a setting from this file.
    ''' </summary>
    ''' <param name="section">The section containing this setting.</param>
    ''' <param name="name">The name of this setting.</param>
    Sub RemoveSetting(section As String, name As String)
    
    ''' <summary>
    ''' Remove a setting from this file.
    ''' </summary>
    ''' <param name="section">The section containing this setting.</param>
    ''' <param name="name">The name of this setting.</param>
    Sub RemoveSetting(section As Extensibility.UserDefinedSection, name As Extensibility.UserDefinedSetting)

    ''' <summary>
    ''' Write the contents of this fie to disk.
    ''' </summary>
    Sub WriteToDisk()

End Interface