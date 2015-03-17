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

End Class
