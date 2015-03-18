Imports DotGit
Imports Xunit

Public Class ConfigOperationsTests

    <Fact>
    Public Sub GetAllRepositoryConfigurationSucceeds()

        TestHelper.OnPersonalRepository(
            Sub(repo As Repository)
                Dim config As Configuration = repo.Config.GetAll
            End Sub)

    End Sub

    Public Sub SetAllRepositoryConfigurationSucceeds()

        TestHelper.OnBareRepository(
            Sub(repo As Repository)
                Dim config As New Configuration
            End Sub)

    End Sub

End Class
