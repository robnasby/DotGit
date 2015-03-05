Imports DotGit
Imports Xunit

Public Class IndexOperationsTests

    <Fact>
    Public Sub AddingFileToIndexSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Dim filePath As String = TestHelper.CreateFileInDirectory(repo.Path)
                repo.Index.Add(filePath)
            End Sub)

    End Sub

    <Fact>
    Public Sub AddingFilesToIndexSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Dim filePaths As IEnumerable(Of String) = TestHelper.CreateFilesInDirectory(repo.Path, 10)
                repo.Index.Add(filePaths)
            End Sub)

    End Sub

    <Fact>
    Public Sub RemovingFileFromIndexSucceeds()

        TestHelper.OnPersonalRepository(
           Sub(repo As Repository)
               Dim filePath As String = TestHelper.CreateFileInDirectory(repo.Path)
               repo.Index.Add(filePath)
               repo.Index.Remove(filePath)
           End Sub)

    End Sub

    <Fact>
    Public Sub RemovingFilesFromIndexSucceeds()

        TestHelper.OnPersonalRepository(
           Sub(repo As Repository)
               Dim filePaths As IEnumerable(Of String) = TestHelper.CreateFilesInDirectory(repo.Path, 10)
               repo.Index.Add(filePaths)
               repo.Index.Remove(filePaths)
           End Sub)

    End Sub

End Class
