Imports DotGit.Internal
Imports System.Reflection

Public Class ConfigOperations

#Region "Properties"

    Private Property Repository As Repository

#End Region

#Region "Constructors"

    Public Sub New(repository As Repository)

        Me.Repository = repository

    End Sub

#End Region

#Region "Methods"

    Public Function GetAll() As Configuration

        Dim command As New Command("config", commandArguments:="--list --local", repositoryPath:=Me.Repository.Path)
        command.Execute()

        If Not command.Status = CommandStatusOption.SUCCEEDED Then _
            Throw New ApplicationException("Unable to get the configuration.")

        Dim configStrings As IEnumerable(Of String) = command.Output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        Return BuildConfigurationFromEntries(ConvertConfigStringToEntries(configStrings))

    End Function

#Region "Helper Methods"

#Region "Configuration <--> Entry Conversion Methods"

    Private Function BuildConfigurationFromEntries(entries As IEnumerable(Of ConfigurationEntry)) _
                                                   As Configuration

        Dim config As New Configuration

        For Each entry As ConfigurationEntry In entries
            Dim groupProperty As PropertyInfo = GetGroupProperty(config, entry.Group)
            If groupProperty IsNot Nothing Then
                Dim configGroup As Object = groupProperty.GetValue(config)
                Dim valueProperty As PropertyInfo = GetValueProperty(configGroup, entry.Name)
                If valueProperty IsNot Nothing Then
                    SetConfigValue(configGroup, valueProperty, entry.Value)
                End If
            End If
        Next

        Return config

    End Function

#End Region

#Region "Entry <--> String Conversion Methods"

    Private Function ConvertConfigStringToEntries(configStrings As IEnumerable(Of String)) _
                                                  As IEnumerable(Of ConfigurationEntry)

        Dim entries As New List(Of ConfigurationEntry)

        For Each configString As String In configStrings
            entries.Add(ConfigurationEntry.FromString(configString))
        Next

        Return entries

    End Function

#End Region

#Region "Property Retrieval Methods"

    Private Function GetGroupProperty(config As Configuration,
                                      groupName As String) _
                                      As PropertyInfo

        Return GetPropertyWithAttributeType(config, groupName, GetType(ConfigurationGroupAttribute))

    End Function

    Private Function GetPropertyWithAttributeType([object] As Object,
                                                  name As String,
                                                  attributeType As Type) _
                                                  As PropertyInfo

        Dim [property] As PropertyInfo = Nothing

        Dim candidateProperty As PropertyInfo = [object].GetType.GetProperty(name, BindingFlags.Instance Or BindingFlags.Public)
        If candidateProperty IsNot Nothing Then
            If [property].GetCustomAttributes.Any(Function(a As Attribute) a.GetType.IsAssignableFrom(attributeType)) Then
                [property] = candidateProperty
            End If
        End If

        Return [property]


    End Function

    Private Function GetValueProperty(configGroup As Object,
                                      valueName As String) _
                                      As PropertyInfo

        Return GetPropertyWithAttributeType(configGroup, valueName, GetType(ConfigurationValueAttribute))

    End Function

#End Region

#Region "Value Conversion Methods"

    Private Sub SetConfigValue(configGroup As Object,
                               [property] As PropertyInfo,
                               value As String)

        Select Case True
            Case [property].PropertyType.IsAssignableFrom(GetType(Boolean))
                Dim castValue As Boolean = Boolean.Parse(value)
                [property].SetValue(configGroup, castValue)
            Case [property].PropertyType.IsAssignableFrom(GetType(String))
                [property].SetValue(configGroup, value)
        End Select

    End Sub

#End Region

#End Region

#End Region

End Class
