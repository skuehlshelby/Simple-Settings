Imports System.ComponentModel
Imports File = System.IO.File

Namespace INI
    Friend Class CRUD
        Inherits NativeMethods
        Public Shared Sub FlushWindowsINICache()
            WritePrivateProfileString(Nothing, Nothing, Nothing, Nothing)
        End Sub
        Public Shared Function EntryRead(Of T)(ByVal FilePath As String, ByVal Section As String, ByVal Key As String, ByVal DefaultValue As T) As T
            Const FILE_NOT_FOUND As Integer = &H2
            Dim BUFFER As DynamicBuffer = New DynamicBuffer()

            Do
                If GetPrivateProfileString(Section, Key, DefaultValue.ToString, BUFFER.Value, BUFFER.Size, FilePath) = FILE_NOT_FOUND Then
                    Return DefaultValue
                End If
            Loop Until BUFFER.ValueCaptured

            Return GetTFromString(Of T)(BUFFER.ToString)
        End Function
        Private Shared Function GetTFromString(Of T)(ByVal Input As String) As T
            Dim Converter As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
            Return CType(Converter.ConvertFromInvariantString(Input), T)
        End Function
        Public Shared Sub EntryWrite(Of T)(ByVal FilePath As String, ByVal Section As String, ByVal Key As String, ByVal Value As T)
            WritePrivateProfileString(Section, Key, Value.ToString, FilePath)
        End Sub
        Public Shared Sub EntryDelete(ByVal FilePath As String, ByVal Section As String, ByVal EntryName As String)
            WritePrivateProfileString(Section, EntryName, Nothing, FilePath)
        End Sub
        Public Shared Sub ClearFile(ByVal FilePath As String)
            Using IniFile As IO.StreamWriter = File.CreateText(FilePath)
            End Using

            FlushWindowsINICache()
        End Sub
    End Class
End Namespace