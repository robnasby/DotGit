Imports DotGit
Imports System.IO
Imports Xunit

Public Class ArchiveOperationsTests

    <Fact>
    Public Sub ArchivingPersonalPreositorySucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")

                TestHelper.OnTempDirectory(
                    Sub(archiveDirectoryPath As String)
                        Dim archiveFilePath As String = Path.Combine(archiveDirectoryPath, "archive.zip")
                        repo.Archive.Get("HEAD", archiveFilePath, ArchiveOperations.FormatOption.ZIP)
                        Assert.True(File.Exists(archiveFilePath))
                    End Sub)
            End Sub)

    End Sub

End Class
