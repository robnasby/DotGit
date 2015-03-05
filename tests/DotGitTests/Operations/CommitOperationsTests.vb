Imports DotGit
Imports Xunit

Public Class CommitOperationsTests

    <Fact>
    Public Sub CreateNewCommitSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")
            End Sub)

    End Sub

End Class
