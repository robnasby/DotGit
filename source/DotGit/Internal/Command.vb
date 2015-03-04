Imports System.Text

Namespace Internal

    Friend Class Command

#Region "Properties"

        ''' <summary>
        ''' The command to be run.
        ''' </summary>
        Private Property Command As String

        ''' <summary>
        ''' The arguments for the command.
        ''' </summary>
        Private Property CommandArguments As String
            Get
                Return If(_CommandArguments IsNot Nothing, _CommandArguments, String.Empty)
            End Get
            Set(value As String)
                _CommandArguments = value
            End Set
        End Property
        Private _CommandArguments As String

        ''' <summary>
        ''' The extra arguments for git.
        ''' </summary>
        Private Property ExtraGitArguments As String
            Get
                Return If(_ExtraGitArguments IsNot Nothing, _ExtraGitArguments, String.Empty)
            End Get
            Set(value As String)
                _ExtraGitArguments = value
            End Set
        End Property
        Private _ExtraGitArguments As String

        ''' <summary>
        ''' The arguments for git.
        ''' </summary>
        Private ReadOnly Property GitArguments As String
            Get
                Dim arguments As String = If(Me.RepositoryPath IsNot Nothing, String.Format("-C ""{0}"" ", Me.RepositoryPath), String.Empty)
                arguments += Me.ExtraGitArguments

                Return arguments
            End Get
        End Property

        ''' <summary>
        ''' The output from the command.
        ''' </summary>
        Public ReadOnly Property Output As String
            Get
                Return If(Me.OutputBuffer IsNot Nothing, Me.OutputBuffer.ToString, String.Empty)
            End Get
        End Property

        ''' <summary>
        ''' The buffer to accumulate the output from the command.
        ''' </summary>
        Private Property OutputBuffer As New StringBuilder

        ''' <summary>
        ''' The path to the repository on which to execute the command.
        ''' </summary>
        Private Property RepositoryPath As String

        ''' <summary>
        ''' The status of the command.
        ''' </summary>
        Public Property Status As CommandStatusOption
            Get
                Return _Status
            End Get
            Private Set(value As CommandStatusOption)
                _Status = value
            End Set
        End Property
        Private _Status As CommandStatusOption = CommandStatusOption.NOT_STARTED

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Instantiate a new instance of <see cref="Command">Command</see>.
        ''' </summary>
        ''' <param name="command">
        ''' The command to be run.
        ''' </param>
        ''' <param name="commandArguments">
        ''' The arguments for the command.
        ''' </param>
        ''' <param name="repositoryPath">
        ''' The path to the repository on which to execute the command.
        ''' </param>
        ''' <param name="gitArguments">
        ''' The arguments for git.
        ''' </param>
        Public Sub New(command As String,
              Optional commandArguments As String = Nothing,
              Optional repositoryPath As String = Nothing,
              Optional gitArguments As String = Nothing)

            Me.Command = command
            Me.CommandArguments = commandArguments
            Me.ExtraGitArguments = gitArguments
            Me.RepositoryPath = repositoryPath

        End Sub

#End Region

#Region "Methods"

        Private Function DetermineStatusFromExitCode(exitCode As Integer) _
                                                     As CommandStatusOption

            Return If(exitCode = 0, CommandStatusOption.SUCCEEDED, CommandStatusOption.FAILED)

        End Function

        Public Sub Execute()

            Dim gitProcess As New Process()

            gitProcess.StartInfo.FileName = Git.ExecutablePath
            gitProcess.StartInfo.Arguments = String.Format("{0} {1} {2}", Me.GitArguments, Me.Command, Me.CommandArguments)

            gitProcess.StartInfo.UseShellExecute = False

            gitProcess.StartInfo.RedirectStandardOutput = True
            AddHandler gitProcess.OutputDataReceived, AddressOf OutputHandler
            gitProcess.StartInfo.RedirectStandardError = True
            AddHandler gitProcess.ErrorDataReceived, AddressOf OutputHandler

            gitProcess.Start()
            gitProcess.BeginOutputReadLine()
            gitProcess.BeginErrorReadLine()
            Me.Status = CommandStatusOption.RUNNING

            gitProcess.WaitForExit()
            Me.Status = DetermineStatusFromExitCode(gitProcess.ExitCode)

        End Sub

        Private Sub OutputHandler(sendingProcess As Object,
                                  output As DataReceivedEventArgs)

            Me.OutputBuffer.AppendLine(output.Data)

        End Sub

#End Region

    End Class

End Namespace
