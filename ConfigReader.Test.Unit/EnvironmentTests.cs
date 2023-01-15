using System.Configuration;

namespace ConfigReader.Test.Unit;

public class EnvironmentTests
{
    [Fact]
    public void GetValue_ReturnsCorrectValue()
    {
        // Arrange
        var variable = "testVariable";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(variable, expectedValue);

        // Act
        var actualValue = Environment.GetValue<string>(variable);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValue_ThrowsExceptionForNonExistentVariable()
    {
        // Arrange
        var variable = "nonExistentVariable";

        // Act & Assert
        var exception = Assert.Throws<ConfigurationErrorsException>(() => Environment.GetValue<string>(variable));
        Assert.Equal($"Configuração não existente: {variable}", exception.Message);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsCorrectValue()
    {
        // Arrange
        var variable = "testVariable";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(variable, expectedValue);
        var defaultValue = "defaultValue";

        // Act
        var actualValue = Environment.GetValueOrDefault<string>(variable, defaultValue);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsDefaultValueForNonExistentVariable()
    {
        // Arrange
        var variable = "nonExistentVariable";
        var defaultValue = "defaultValue";

        // Act
        var actualValue = Environment.GetValueOrDefault<string>(variable, defaultValue);

        // Assert
        Assert.Equal(defaultValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var variable = "testVariable";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(variable, expectedValue);

        // Act
        var result = Environment.TryGetValue<string>(variable, out var actualValue);

        // Assert
        Assert.True(result);
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsFalseAndDefaultValueForNonExistentVariable()
    {
        // Arrange
        var variable = "nonExistentVariable";

        // Act
        var result = Environment.TryGetValue<string>(variable, out var actualValue);

        // Assert
        Assert.False(result);
        Assert.Equal(default(string), actualValue);
    }
}