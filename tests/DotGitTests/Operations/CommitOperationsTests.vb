Imports DotGit
Imports Xunit

Public Class CommitOperationsTests

    <Fact>
    Public Sub AmendCommitContentsSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")

                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Amend()
            End Sub)

    End Sub

    <Fact>
    Public Sub AmendCommitMessageSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")

                repo.Commit.Amend("Better message")
            End Sub)

    End Sub

    <Fact>
    Public Sub CreateNewCommitSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")
            End Sub)

    End Sub

End Class
