﻿Imports DotGit.Internal

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
    ''' Delete a branch from a repository.
    ''' </summary>
    ''' <param name="name">
    ''' The name of the branch to delete.
    ''' </param>
    ''' <param name="force">
    ''' Force the deletion, regardless of merge status of the branch.
    ''' </param>
    Public Sub Delete(name As String,
             Optional force As Boolean = False)

        Dim arguments As String = If(force, String.Format("-D {0}", name), String.Format("-d {0}", name))
        Dim command As New Command("branch", commandArguments:=arguments, repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException(String.Format("Unable to delete branch '{0}'.", name))

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

    ''' <summary>
    ''' Rename a branch in a repository.
    ''' </summary>
    ''' <param name="oldName">
    ''' The old name of the branch.
    ''' </param>
    ''' <param name="newName">
    ''' The new name of the branch.
    ''' </param>
    Public Sub Rename(oldName As String,
                      newName As String)

        Dim command As New Command("branch",
                                   commandArguments:=String.Format("-m {0} {1}", oldName, newName),
                                   repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException(String.Format("Unable to rename branch '{0}' to '{1}'.", oldName, newName))

    End Sub

#End Region

End Class
