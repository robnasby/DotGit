Imports DotGit.Internal

Public Class ArchiveOperations

#Region "Constants"

    Public Enum FormatOption
        TAR
        ZIP
    End Enum

#End Region

#Region "Properties"

    Private Property Repository As Repository

#End Region

#Region "Constructors"

    Friend Sub New(repository As Repository)

        Me.Repository = repository

    End Sub

#End Region

#Region "Methods"

    Public Sub [Get](committish As String,
                     outputFilePath As String,
                     format As ArchiveOperations.FormatOption)

        Dim arguments As String = String.Format("-o ""{0}"" --format={1} {2}", outputFilePath, format.ToString.ToLower, committish)
        Dim command As New Command("archive", commandArguments:=arguments, repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException(String.Format("Unable to save archive for '{0}' to '{1}.'", committish, outputFilePath))

    End Sub

#End Region

End Class
