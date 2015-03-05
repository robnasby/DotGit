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
    ''' Create a new branch on a repository.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the new branch.
    ''' </param>
    ''' <param name="committish">
    ''' The committish at which to create the branch.
    ''' </param>
    ''' <param name="checkout">
    ''' True if the new branch should be checked out; otherwise, False.
    ''' </param>
    Public Sub Create(name As String,
                      committish As String,
             Optional checkout As Boolean = False)

        Dim commandName As String
        Dim arguments As String
        If checkout Then
            commandName = "checkout"
            arguments = String.Format("-b {0} {1}", name, committish)
        Else
            commandName = "branch"
            arguments = String.Format("{0} {1}", name, committish)
        End If
        Dim command As New Command(commandName, commandArguments:=arguments, repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException(String.Format("Unable to create new branch '{0}' at committish '{1}'.", name, committish))

    End Sub

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
