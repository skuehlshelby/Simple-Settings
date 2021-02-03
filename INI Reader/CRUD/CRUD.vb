Imports System.ComponentModel
Imports System.IO

Namespace CRUD
    Friend Class CRUD
        Inherits NativeMethods
        Public Shared Sub FlushWindowsINICache()
            WritePrivateProfileString(Nothing, Nothing, Nothing, Nothing)
        End Sub
        Public Shared Function EntryRead(Of T)(filePath As String, section As String, key As String, defaultValue As T) As T
            Const FILE_NOT_FOUND As Integer = &H2
            Dim buffer As DynamicBuffer = New DynamicBuffer()

            Do
                If GetPrivateProfileString(section, key, defaultValue.ToString, buffer.Value, buffer.Size, filePath) = FILE_NOT_FOUND Then
                    Return defaultValue
                End If
            Loop Until buffer.ValueCaptured

            Return GetTFromString(Of T)(buffer.ToString())
        End Function
        Private Shared Function GetTFromString(Of T)(input As String) As T
            Dim converter As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
            Return CType(converter.ConvertFromInvariantString(input), T)
        End Function
        Public Shared Sub EntryWrite(Of T)(filePath As String, section As String, key As String, value As T)
            WritePrivateProfileString(section, key, value.ToString, filePath)
        End Sub
        Public Shared Sub EntryDelete(filePath As String, section As String, entryName As String)
            WritePrivateProfileString(section, entryName, Nothing, filePath)
        End Sub
        Public Shared Sub ClearFile(filePath As String)
            Using iniFile As IO.StreamWriter = File.CreateText(filePath)
            End Using

            FlushWindowsINICache()
        End Sub
    End Class
End Namespace