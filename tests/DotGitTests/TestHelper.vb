Imports DotGit
Imports System.IO

Friend Class TestHelper

#Region "Properties"

    Private Shared ReadOnly Property BaseTestDirectoryPath As String
        Get
            Return Path.Combine(Path.GetTempPath, Reflection.Assembly.GetCallingAssembly.GetName.Name)
        End Get
    End Property

#End Region

#Region "Methods"

    Private Shared Function CreateRandomAlphanumericString(length As Integer) _
                                                          As String

        Static characters As IEnumerable(Of Char) = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Static random As New Random

        Return New String(
            Enumerable.Repeat(Of IEnumerable(Of Char))(characters, length) _
                .Select(Of Char)(Function(s As IEnumerable(Of Char)) s(random.Next(characters.Count))) _
                .ToArray())

    End Function

    Private Shared Sub ForceDeleteDirectory(directoryPath As String)

        ForceDeleteDirectory(New DirectoryInfo(directoryPath))

    End Sub

    Public Shared Sub ForceDeleteDirectory(directory As DirectoryInfo)

        RemoveReadOnlyFromFiles(directory)
        directory.Delete(True)

    End Sub

    Private Shared Sub RemoveReadOnlyFromFiles(directory As DirectoryInfo)

        Dim readonlyFiles As IEnumerable(Of FileInfo) =
            From file As FileInfo In directory.GetFiles
            Where file.IsReadOnly

        For Each file As FileInfo In readonlyFiles
            file.IsReadOnly = False
        Next

        For Each subDirectory As DirectoryInfo In directory.GetDirectories
            RemoveReadOnlyFromFiles(subDirectory)
        Next

    End Sub

    Public Shared Sub OnBareRepository(action As Action(Of Repository))

        OnTempDirectory(
            Sub(repoPath As String)
                Dim repo As Repository = Repository.Initialize(repoPath, True)

                action(repo)
            End Sub)

    End Sub

    Public Shared Sub OnPersonalRepository(action As Action(Of Repository))

        OnTempDirectory(
            Sub(repoPath As String)
                Dim repo As Repository = Repository.Initialize(repoPath)

                action(repo)
            End Sub)

    End Sub

    Public Shared Sub OnTempDirectory(action As Action(Of String))

        Dim tempDirectoryPath As String = Path.Combine(BaseTestDirectoryPath, CreateRandomAlphanumericString(12))
        Directory.CreateDirectory(tempDirectoryPath)

        action(tempDirectoryPath)

        ForceDeleteDirectory(tempDirectoryPath)

    End Sub

#End Region

End Class
