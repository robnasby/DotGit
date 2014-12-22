Imports DotGit.Internal

''' <summary>
''' A class to represent a Git repository.
''' </summary>
Public Class Repository

#Region "Validation Methods"

    ''' <summary>
    ''' Verifies that the specified path is a valid Git repository.
    ''' </summary>
    ''' <param name="path">
    ''' The path to a suspected Git repository.
    ''' </param>
    ''' <returns>True if the path is a valid Git repository or False otherwise."</returns>
    Public Shared Function IsValidRepository(path As String) _
                                             As Boolean

        Dim command As New Command("rev-parse", commandArguments:="--git-dir", repositoryPath:=path)
        command.Execute()

        Return command.Status = CommandStatusOption.SUCCEEDED

    End Function

#End Region

End Class
