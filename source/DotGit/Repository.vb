Imports DotGit.Internal

''' <summary>
''' A class to represent a Git repository.
''' </summary>
Public Class Repository

#Region "Properties"

    ''' <summary>
    ''' Commit operations on a repository.
    ''' </summary>
    Public ReadOnly Property Commit As CommitOperations
        Get
            If _Commit Is Nothing Then _Commit = New CommitOperations(Me)

            Return _Commit
        End Get
    End Property
    Private _Commit As CommitOperations

    ''' <summary>
    ''' Index operation on a repository.
    ''' </summary>
    Public ReadOnly Property Index As IndexOperations
        Get
            If _Index Is Nothing Then _Index = New IndexOperations(Me)

            Return _Index
        End Get
    End Property
    Private _Index As IndexOperations

    ''' <summary>
    ''' The path where the repository is located.
    ''' </summary>
    Public Property Path As String
        Get
            Return _Path
        End Get
        Private Set(value As String)
            _Path = value
        End Set
    End Property
    Private _Path As String

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Instantiates as new instance of <see cref="Repository">Repository.</see>
    ''' </summary>
    ''' <param name="path">
    ''' The path to the repository.
    ''' </param>
    Private Sub New(path As String)

        Me.Path = path

    End Sub

#End Region

#Region "Creation Methods"

    ''' <summary>
    ''' Initializes a new Git repository.
    ''' </summary>
    ''' <param name="path">
    ''' The path where the new repository will be created.
    ''' </param>
    ''' <param name="asBare">
    ''' Indicates if the new repository should be a bare repository.
    ''' </param>
    ''' <returns>
    ''' An instance of <see cref="Repository">Repository</see> pointing at the newly created repository.
    ''' </returns>
    Public Shared Function Initialize(path As String,
                             Optional asBare As Boolean = False) _
                                      As Repository

        Dim arguments As String = String.Format("""{0}""", path)
        If asBare Then arguments = "--bare " + arguments

        Dim command As New Command("init", commandArguments:=arguments)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException(String.Format("Unable to initialize repository at '{0}'.", path))

        Return New Repository(path)

    End Function

    ''' <summary>
    ''' Opens a Git repository.
    ''' </summary>
    ''' <param name="path">
    ''' The path where the repository is located.
    ''' </param>
    ''' <returns>
    ''' An instance of <see cref="Repository">Repository</see> pointing to the repository.
    ''' </returns>
    Public Shared Function Open(path As String) _
                                As Repository

        If Not IsValidRepository(path) Then _
            Throw New ApplicationException(String.Format("'{0}' is not a valid repository.", path))

        Return New Repository(path)

    End Function

#End Region

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
