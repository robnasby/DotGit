Imports DotGit
Imports Xunit

Public Class BranchOperationsTests

    <Fact>
    Public Sub ListBranchesOnEmptyRepositorySucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Assert.Equal(0, repo.Branch.List.Count)
            End Sub)

    End Sub

End Class
