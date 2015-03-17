Namespace Internal

    Public Class ConfigurationEntry

#Region "Properties"

        Public Property Group As String

        Public Property Name As String

        Public Property Value As String

#End Region

#Region "Constructors"

        Public Sub New(group As String,
                       name As String,
                       value As String)

            Me.Group = group
            Me.Name = name
            Me.Value = value

        End Sub

#End Region

#Region "Methods"

        Public Shared Function FromString([string] As String) _
                                          As ConfigurationEntry

            Dim parts As IEnumerable(Of String) = [string].Split({"="c, "."c})

            Dim group As String = parts(0)
            Dim name As String = parts(1)
            Dim value As String = parts(2)

            Return New ConfigurationEntry(group, name, value)

        End Function

#End Region

    End Class

End Namespace
