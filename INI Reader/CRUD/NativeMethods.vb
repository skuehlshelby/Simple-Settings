Imports System.Runtime.InteropServices
Imports System.Text

Namespace CRUD

    Friend MustInherit Class NativeMethods
        <DllImport("kernel32.dll", EntryPoint:="GetPrivateProfileStringA", BestFitMapping:=False, SetLastError:=True, CharSet:=CharSet.Ansi)>
        Protected Shared Function GetPrivateProfileString(lpAppName As String, lpKeyName As String, lpDefault As String, <MarshalAs(UnmanagedType.LPStr)> lpReturnedString As StringBuilder, nSize As Integer, lpFileName As String) As Integer
        End Function
        <DllImport("kernel32.dll", EntryPoint:="WritePrivateProfileStringA", BestFitMapping:=False, SetLastError:=True, CharSet:=CharSet.Ansi)>
        Protected Shared Function WritePrivateProfileString(lpAppName As String, lpKeyName As String, lpString As String, lpFileName As String) As Boolean
        End Function
    End Class
End Namespace