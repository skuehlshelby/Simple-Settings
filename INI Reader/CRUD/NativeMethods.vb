Imports System.Runtime.InteropServices
Imports System.Text

Namespace CRUD
    Friend MustInherit Class NativeMethods
        <DllImport _
                ("kernel32.dll", EntryPoint := "GetPrivateProfileStringA", BestFitMapping := False, SetLastError := True,
                 CharSet := CharSet.Ansi)>
        Protected Shared Function GetPrivateProfileString(lpAppName As String, lpKeyName As String, lpDefault As String,
                                                          <MarshalAs(UnmanagedType.LPStr)> lpReturnedString As _
                                                             StringBuilder, nSize As Integer, lpFileName As String) _
            As Integer
        End Function

        <DllImport _
                ("kernel32.dll", EntryPoint := "WritePrivateProfileStringA", BestFitMapping := False,
                 SetLastError := True, CharSet := CharSet.Ansi)>
        Protected Shared Function WritePrivateProfileString(lpAppName As String, lpKeyName As String, lpString As String,
                                                            lpFileName As String) As Boolean
        End Function
    End Class
End Namespace