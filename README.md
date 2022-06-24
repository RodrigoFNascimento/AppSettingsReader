![size](https://img.shields.io/github/languages/code-size/RodrigoFNascimento/AppSettingsReader)
![files](https://img.shields.io/github/directory-file-count/RodrigoFNascimento/AppSettingsReader)
![language](https://img.shields.io/github/languages/top/RodrigoFNascimento/AppSettingsReader)
![license](https://img.shields.io/github/license/RodrigoFNascimento/AppSettingsReader)

# AppSettingsReader
A library for reading the configuration in AppSettings

## About
Using the regular `ConfigurationManager` class can be quite verbose, specially if you want to check if the setting exists and convert it's value to a type other than `string`:

```c#
string value = ConfigurationManager.AppSettings[key];

if (string.IsNullOrWhiteSpace(value))
    throw new ConfigurationErrorsException($"Setting not found: {key}");

if (!int.TryParse(value, out int foo))
    throw new ConfigurationErrorsException($"Invalid setting: {key}");

return foo;
```

This package was developed to simplify the way that settings are read from AppSettings in .Net Framework.

## Installation
This package is still in development and not yet available in nuget. Therefore, it's not ready to be used in production.

To install in it's current state, compile a .nupkg from the code.

## Usage
To get value from AppSettings as a `string`, simply use the `Get` method. If the key is found, returns its value as a `string`; otherwise, throws a `ConfigurationErrorsException`.

```c#
try
{
    string foo = AppSettingsReader.Get("foo");
}
catch (ConfigurationErrorsException ex)
{
    // Do something
}
```

To get a value from AppSettings as another type, use `Get<TValue>`. If the key is found, returns its value as a `TValue` type; otherwise, throws a `ConfigurationErrorsException`.

```c#
try
{
    int foo = AppSettingsReader.Get<int>("foo");
}
catch (ConfigurationErrorsException ex)
{
    // Do something
}
```

The `TryGet` method works similarly to the `TryParse` methods. If the key is found, returns `true` and outputs it's value in `value`; otherwise, returns `false` and outputs the `default` value of type `TValue` in `value`.

This method does not throw a `ConfigurationErrorsException` like the others.

```c#
bool converted = AppSettingsReader.TryGet<decimal>("foo", out decimal bar);
```

## Compatibility
This project was developed in [.Net Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) to support the latest versions of .Net Framework.

## License
Distributed under the MIT License. See [LICENSE.txt](./LICENSE) for more information.
