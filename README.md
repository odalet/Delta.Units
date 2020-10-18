# Delta.Units

![Build](https://github.com/odalet/Delta.Units/workflows/Build/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=odalet_Delta.Units&metric=alert_status)](https://sonarcloud.io/dashboard?id=odalet_Delta.Units)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=odalet_Delta.Units&metric=coverage)](https://sonarcloud.io/dashboard?id=odalet_Delta.Units)
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=odalet/Delta.Units)](https://dependabot.com)
[![NuGet](https://img.shields.io/nuget/v/Delta.Units.svg)](https://www.nuget.org/packages/Delta.Units/)

## About

This is a library allowing to define various units and to convert values between them.
Because it is based on modelling the dimension of each unit, it is straightforward (once a unit is defined in relation with other ones) to convert between quantities. 

This library was heavily inspired by this [Units and Amounts article on codeproject](http://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts). However, it goes beyond what is provided here by supporting units that don't convert between them simply by applying a multiplication factor.

This is possible because, instead of storing a factor, the conversion (and inverse conversion) functions are stored and combined when building units.
 
## History

### [Version 0.5.0 - WIP](https://github.com/odalet/Delta.Units/releases/tag/v0.5.0)

* Targets .NET Standard 2.0
* License is now MIT
* Replaced appveyor CI with Github Actions
* Replaced coveralls.io coverage platform with sonarcloud

### [Version 0.4.0 - 2018/05*/18](https://github.com/odalet/Delta.Units/releases/tag/v0.4.0)

* Initial Version. Targets .NET 4.0 and .NET Standard 1.0
* License is Ms-RL

## License

This work is provided under the terms of the [MIT License](LICENSE).
