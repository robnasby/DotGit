Public Class Git

#Region "Properties"

    ''' <summary>
    ''' The path to the git executable.
    ''' </summary>
    ''' <remarks>
    ''' Defaults to "git", which requires the executable directory path be set
    ''' on the PATH environment variable.
    ''' </remarks>
    Public Shared Property ExecutablePath As String = "git"

#End Region

End Class
