using System.Configuration;

namespace ConfigReader.Test.Unit;

public class EnvironmentTests
{
    [Fact]
    public void GetValue_ReturnsCorrectValue_GivenExistentKey()
    {
        // Arrange
        var key = "testVariable";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(key, expectedValue);

        // Act
        var actualValue = Environment.GetValue<string>(key);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValue_ThrowsArgumentException_WhenValueCannotBeConverted()
    {
        // Arrange
        var key = "testKey";
        var value = "abc";
        System.Environment.SetEnvironmentVariable(key, value);

        // Act
        void act() => Environment.GetValue<int>(key);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void GetValue_ThrowsConfigurationErrorsException_GivenNonExistentKey()
    {
        // Arrange
        var key = "nonExistentKey";

        // Act & Assert
        var exception = Assert.Throws<ConfigurationErrorsException>(() => Environment.GetValue<string>(key));
        Assert.Equal($"Configuration not found: {key}", exception.Message);
    }

    [Fact]
    public void GetValue_ReturnsCorrectValueWithTypeConversion_GivenExistentKey()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = 123;
        System.Environment.SetEnvironmentVariable(key, expectedValue.ToString());

        // Act
        var actualValue = Environment.GetValue<int>(key);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsCorrectValue()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(key, expectedValue);
        var defaultValue = "defaultValue";

        // Act
        var actualValue = Environment.GetValueOrDefault(key, defaultValue);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsDefaultValue_GivenNonExistentVariable()
    {
        // Arrange
        var key = "nonExistentKey";
        var defaultValue = "defaultValue";

        // Act
        var actualValue = Environment.GetValueOrDefault(key, defaultValue);

        // Assert
        Assert.Equal(defaultValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = "testValue";
        System.Environment.SetEnvironmentVariable(key, expectedValue);

        // Act
        var result = Environment.TryGetValue<string>(key, out var actualValue);

        // Assert
        Assert.True(result);
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsFalseAndDefaultValue_GivenNonExistentVariable()
    {
        // Arrange
        var key = "nonExistentKey";

        // Act
        var result = Environment.TryGetValue<string>(key, out var actualValue);

        // Assert
        Assert.False(result);
        Assert.Equal(default, actualValue);
    }
}