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
    Public Sub DeleteBranchSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit.")

                Dim branchName As String = "TEST"
                repo.Branch.Create(branchName, "HEAD")
                Assert.Contains(Of String)(branchName, repo.Branch.List)

                repo.Branch.Delete(branchName)
                Assert.DoesNotContain(Of String)(branchName, repo.Branch.List)
            End Sub)

    End Sub

    <Fact>
    Public Sub ListBranchesOnEmptyRepositorySucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Assert.Equal(0, repo.Branch.List.Count)
            End Sub)

    End Sub

    <Fact>
    Public Sub RenameBranchSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                repo.Index.Add(TestHelper.CreateFileInDirectory(repo.Path))
                repo.Commit.Create("Test commit.")

                Dim oldBranchName As String = "FOO"
                repo.Branch.Create(oldBranchName, "HEAD")
                Assert.Contains(Of String)(oldBranchName, repo.Branch.List)

                Dim newBranchName As String = "BAR"
                repo.Branch.Rename(oldBranchName, newBranchName)
                Dim branches As IEnumerable(Of String) = repo.Branch.List
                Assert.DoesNotContain(Of String)(oldBranchName, branches)
                Assert.Contains(Of String)(newBranchName, branches)
            End Sub)

    End Sub

End Class
