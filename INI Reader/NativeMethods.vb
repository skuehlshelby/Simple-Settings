Imports System.Runtime.InteropServices
Imports StringBuilder = System.Text.StringBuilder

Friend MustInherit Class NativeMethods
    <DllImport("kernel32.dll", EntryPoint:="GetPrivateProfileStringA", BestFitMapping:=False, SetLastError:=True, CharSet:=CharSet.Ansi)>
    Protected Shared Function GetPrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    End Function
    <DllImport("kernel32.dll", EntryPoint:="WritePrivateProfileStringA", BestFitMapping:=False, SetLastError:=True, CharSet:=CharSet.Ansi)>
    Protected Shared Function WritePrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Boolean
    End Function
End Class

