Imports DotGit.Internal

Public Class IndexOperations

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
    ''' Add a file to the index of a repository.
    ''' </summary>
    ''' <param name="path">
    ''' The path to the file in the repository.
    ''' </param>
    ''' <param name="force">
    ''' Force add the file, even if it would normally be ignored.
    ''' </param>
    Public Sub Add(path As String,
          Optional force As Boolean = False)

        Add({path}, force)

    End Sub

    ''' <summary>
    ''' Add files to the index of a repository.
    ''' </summary>
    ''' <param name="paths">
    ''' The paths to the files in the repository.
    ''' </param>
    ''' <param name="force">
    ''' Force add the files, even if they would normally be ignored.
    ''' </param>
    Public Sub Add(paths As IEnumerable(Of String),
          Optional force As Boolean = False)

        Dim arguments As String = String.Format("-- {0}", String.Join(" ", paths.Select(Function(p As String) String.Format("""{0}""", p))))
        If force Then arguments = "-f " + arguments
        Dim command As New Command("add", commandArguments:=arguments, repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to add files to the index." + Environment.NewLine +
                                           String.Join(Environment.NewLine, paths.Select(Function(p As String) String.Format("'{0}'", p))))

    End Sub

#End Region

End Class
