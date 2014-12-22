Imports DotGit
Imports System.IO
Imports Xunit

Public Class RepositoryTests

#Region "Tests"

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
