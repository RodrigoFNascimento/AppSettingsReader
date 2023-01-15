﻿using AppSetttingsReader;
using System.Configuration;

namespace ConfigReader.Test.Unit;
public class AppSettingsTests
{
    [Fact]
    public void GetValue_ReturnsCorrectValue_GivenExistentKey()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = "testValue";
        ConfigurationManager.AppSettings[key] = expectedValue;

        // Act
        var actualValue = AppSettings.GetValue(key);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValue_ThrowsException_GivenNonExistentKey()
    {
        // Arrange
        var key = "nonExistentKey";

        // Act & Assert
        var exception = Assert.Throws<ConfigurationErrorsException>(() => AppSettings.GetValue(key));
        Assert.Equal($"Configuração não existente: {key}", exception.Message);
    }

    [Fact]
    public void GetValue_ReturnsCorrectValueWithTypeConversion_GivenExistentKey()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = 123;
        ConfigurationManager.AppSettings[key] = expectedValue.ToString();

        // Act
        var actualValue = AppSettings.GetValue<int>(key);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsCorrectValue_GivenExistentKey()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = 123;
        ConfigurationManager.AppSettings[key] = expectedValue.ToString();
        var defaultValue = 456;

        // Act
        var actualValue = AppSettings.GetValueOrDefault(key, defaultValue);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetValueOrDefault_ReturnsDefaultValue_GivenNonExistentKey()
    {
        // Arrange
        var key = "nonExistentKey";
        var defaultValue = 456;

        // Act
        var actualValue = AppSettings.GetValueOrDefault(key, defaultValue);

        // Assert
        Assert.Equal(defaultValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsTrueAndCorrectValue_GivenExistentKey()
    {
        // Arrange
        var key = "testKey";
        var expectedValue = 123;
        ConfigurationManager.AppSettings[key] = expectedValue.ToString();

        // Act
        var result = AppSettings.TryGetValue<int>(key, out var actualValue);

        // Assert
        Assert.True(result);
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TryGetValue_ReturnsFalseAndDefaultValue_GivenNonExistentKey()
    {
        // Arrange
        var key = "nonExistentKey";

        // Act
        var result = AppSettings.TryGetValue<int>(key, out var actualValue);

        // Assert
        Assert.False(result);
        Assert.Equal(default, actualValue);
    }
}
