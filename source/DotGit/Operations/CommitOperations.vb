Imports DotGit.Internal

Public Class CommitOperations

#Region "Properties"

    Private Property Repository As Repository

#End Region

#Region "Constructors"

    Friend Sub New(repository As Repository)

        Me.Repository = repository

    End Sub

#End Region

#Region "Methods"

    Public Sub Amend()

        Dim arguments As String = "--amend --no-edit"
        Dim command As New Command("commit", commandArguments:=arguments, repositoryPath:=Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to amend the commit.")

    End Sub

    Public Sub Amend(message As String)

        Dim arguments As String = String.Format("--amend -m ""{0}""", message)
        Dim command As New Command("commit", commandArguments:=arguments, repositoryPath:=Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to amend the commit.")

    End Sub

    Public Sub Create(message As String)

        Dim arguments As String = String.Format("-m ""{0}""", message)
        Dim command As New Command("commit", commandArguments:=arguments, repositoryPath:=Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to create a commit.")

    End Sub

#End Region

End Class
