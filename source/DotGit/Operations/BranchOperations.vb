Imports DotGit.Internal

Public Class BranchOperations

#Region "Properties"

    Private Property Repository As Repository

#End Region

#Region "Constructors"

    Friend Sub New(repository As Repository)

        Me.Repository = repository

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' List the local branches in a repository.
    ''' </summary>
    ''' <returns>
    ''' The local branches in a repository.
    ''' </returns>
    Public Function List() As IEnumerable(Of String)

        Dim command As New Command("branch", repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to retrieve branches.")

        Return command.Output _
            .Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries) _
            .Select(Function(line As String) line.TrimStart("*"c).Trim) _
            .ToList

    End Function

#End Region

End Class
