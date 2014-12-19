Imports DotGit
Imports System.IO
Imports Xunit

Public Class RepositoryTests

#Region "Tests"

#Region "Creation"

    <Fact>
    Public Sub InitializeBareRepositorySucceeds()

        TestHelper.OnBareRepository(
            Sub(repo As Repository)
                Assert.True(Repository.IsValidRepository(repo.Path))
            End Sub)

    End Sub

    <Fact>
    Public Sub InitializePersonalRepositorySucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Assert.True(Repository.IsValidRepository(repo.Path))
            End Sub)

    End Sub

#End Region

#Region "Validation"

    <Fact>
    Public Sub ValidationOfEmptyDirectoryFails()

        TestHelper.OnTempDirectory(
            Sub(emptyDirectoryPath As String)
                Assert.False(Repository.IsValidRepository(emptyDirectoryPath))
            End Sub)

    End Sub

#End Region

#End Region

End Class
