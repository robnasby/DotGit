Imports DotGit
Imports Xunit

Public Class BranchOperationsTests

    <Fact>
    Public Sub CreateBranchSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit")

                Dim branchName As String = "TEST"
                repo.Branch.Create(branchName, "HEAD")
                Assert.Contains(Of String)(branchName, repo.Branch.List)
            End Sub)

    End Sub

    <Fact>
    Public Sub ListBranchesOnEmptyRepositorySucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Assert.Equal(0, repo.Branch.List.Count)
            End Sub)

    End Sub

End Class
