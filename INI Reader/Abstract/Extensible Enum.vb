Imports CultureInfo = System.Globalization.CultureInfo
Imports System.Reflection

Namespace Abstract
    '''<include file='EnumDocs.xml' path='//mainClass'/>
    Public MustInherit Class Enumeration
        Implements IComparable
        Private Structure TThis
            Public Value As Integer
            Public DisplayName As String
        End Structure

        Private This As TThis
        '''<include file='EnumDocs.xml' path='//methods/new'/>
        '''<include file='EnumDocs.xml' path='//parameters/value[@parentMethod="new"]/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/displayName[@parentMethod="new"]/*'/>
        Protected Sub New(ByVal Value As Integer, ByVal DisplayName As String)
            This = New TThis With {
                    .Value = Value,
                    .DisplayName = DisplayName
                }
        End Sub
        '''<include file='EnumDocs.xml' path='//properties/value/*'/>
        Public ReadOnly Property Value As Integer
            Get
                Return This.Value
            End Get
        End Property
        '''<include file='EnumDocs.xml' path='//properties/displayName/*'/>
        Public ReadOnly Property DisplayName As String
            Get
                Return This.DisplayName
            End Get
        End Property
        '''<include file='EnumDocs.xml' path='//methods/toString'/>
        Public Overrides Function ToString() As String
            Return This.DisplayName.Replace(" "c, Nothing)
        End Function
        '''<include file='EnumDocs.xml' path='//methods/equals'/>
        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing OrElse TypeOf obj IsNot Enumeration Then
                Return False
            Else
                Return This.Value.Equals(CType(obj, Enumeration).Value)
            End If
        End Function
        '''<include file='EnumDocs.xml' path='//methods/getHashCode'/>
        Public Overrides Function GetHashCode() As Integer
            Return 397 * This.Value.GetHashCode()
        End Function
        '''<include file='EnumDocs.xml' path='//methods/getAll'/>
        Public Shared Function GetAll(Of T As Enumeration)() As IEnumerable(Of T)
            Dim Fields As FieldInfo() = GetType(T).GetFields(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.DeclaredOnly)

            Return Fields.Select(Function(f) CType(f.GetValue(Nothing), T)).Cast(Of T)
        End Function
        '''<include file='EnumDocs.xml' path='//methods/absoluteDifference'/>
        Public Shared Function AbsoluteDifference(ByVal FirstValue As Enumeration, ByVal SecondValue As Enumeration) As Integer
            If FirstValue Is Nothing OrElse SecondValue Is Nothing Then
                Throw New ArgumentException(If(FirstValue Is Nothing, NameOf(FirstValue), NameOf(SecondValue)))
            Else
                Return Math.Abs(FirstValue.Value - SecondValue.Value)
            End If
        End Function
        '''<include file='EnumDocs.xml' path='//methods/fromValue'/>
        Public Shared Function FromValue(Of T As Enumeration)(ByVal Value As Integer) As T
            Dim MatchingItem As T = Parse(Of T, Integer)(Value, NameOf(Value), Function(Item) Item.Value = Value)
            Return MatchingItem
        End Function
        '''<include file='EnumDocs.xml' path='//methods/fromDisplayName'/>
        Public Shared Function FromDisplayName(Of T As Enumeration)(ByVal DisplayName As String) As T
            Dim MatchingItem As T = Parse(Of T, String)(DisplayName, NameOf(DisplayName), Function(Item) Item.DisplayName = DisplayName)
            Return MatchingItem
        End Function
        '''<include file='EnumDocs.xml' path='//methods/parse'/>
        Public Shared Function Parse(Of T As Enumeration, TValue)(ByVal Value As TValue, ByVal Description As String, ByVal Predicate As Func(Of T, Boolean)) As T
            Dim MatchingItem As T = GetAll(Of T).FirstOrDefault(Predicate)

            If MatchingItem Is Nothing Then
                Throw New ApplicationException(String.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid {1} for a {2}.", Value, Description, GetType(T)))
            End If

            Return MatchingItem
        End Function
        '''<include file='EnumDocs.xml' path='//methods/compareTo'/>
        Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
            If obj Is Nothing OrElse TypeOf obj IsNot Enumeration Then
                Throw If(obj Is Nothing, New ArgumentNullException(NameOf(obj)), New ArgumentException(String.Format(CultureInfo.InvariantCulture, "Object of type {0} cannot be compared to an Enumeration", obj.GetType.Name)))
            Else
                Return This.Value.CompareTo(CType(obj, Enumeration).Value)
            End If
        End Function
        '''<include file='EnumDocs.xml' path='//operators/lessThan'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator <(left As Enumeration, right As Enumeration) As Boolean
            Return If(left Is Nothing, right IsNot Nothing, left.CompareTo(right) < 0)
        End Operator
        '''<include file='EnumDocs.xml' path='//operators/lessThanOrEqualTo'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator <=(left As Enumeration, right As Enumeration) As Boolean
            Return left Is Nothing OrElse left.CompareTo(right) <= 0
        End Operator
        '''<include file='EnumDocs.xml' path='//operators/greaterThan'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator >(left As Enumeration, right As Enumeration) As Boolean
            Return left IsNot Nothing AndAlso left.CompareTo(right) > 0
        End Operator
        '''<include file='EnumDocs.xml' path='//operators/greaterThanOrEqualTo'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator >=(left As Enumeration, right As Enumeration) As Boolean
            Return If(left Is Nothing, right Is Nothing, left.CompareTo(right) >= 0)
        End Operator
        '''<include file='EnumDocs.xml' path='//operators/equals'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator =(left As Enumeration, right As Enumeration) As Boolean
            Return If(left Is Nothing, right Is Nothing, left.Equals(right))
        End Operator
        '''<include file='EnumDocs.xml' path='//operators/notEqual'/>
        '''<include file='EnumDocs.xml' path='//parameters/left/*'/>
        '''<include file='EnumDocs.xml' path='//parameters/right/*'/>
        Public Shared Operator <>(left As Enumeration, right As Enumeration) As Boolean
            Return Not left = right
        End Operator
    End Class
End Namespace
