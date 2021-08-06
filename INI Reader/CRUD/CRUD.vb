Imports System.ComponentModel
Imports System.IO

Namespace CRUD

    Friend Class CRUD
        Inherits NativeMethods

        Public Shared Function EntryRead (Of T)(filePath As String, section As String, key As String, defaultValue As T) _
            As T
            Const fileNotFound As Integer = &H2
            Dim buffer As DynamicBuffer = New DynamicBuffer()

            Do
                If _
                    GetPrivateProfileString(section, key, defaultValue.ToString, buffer.Value, buffer.Size, filePath) =
                    fileNotFound Then
                    Return defaultValue
                End If
            Loop Until buffer.ValueCaptured

            Return GetTFromString (Of T)(buffer.ToString())
        End Function

        Private Shared Function GetTFromString (Of T)(input As String) As T
            Dim converter As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
            Return DirectCast(converter.ConvertFromInvariantString(input), T)
        End Function

        Public Shared Sub EntryWrite (Of T)(filePath As String, section As String, key As String, value As T)
            WritePrivateProfileString(section, key, value.ToString, filePath)
        End Sub

        Public Shared Sub EntryDelete(filePath As String, section As String, entryName As String)
            WritePrivateProfileString(section, entryName, Nothing, filePath)
        End Sub

        Public Shared Sub ClearFile(filePath As String)
            File.Create(filePath).Close()

            FlushWindowsINICache()
        End Sub

        Public Shared Sub FlushWindowsINICache()
            WritePrivateProfileString(Nothing, Nothing, Nothing, Nothing)
        End Sub

    End Class

End Namespace