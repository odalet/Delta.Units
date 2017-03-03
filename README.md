[![Build status](https://ci.appveyor.com/api/projects/status/h4dqt75twhf93fbv?svg=true)](https://ci.appveyor.com/project/odalet/delta-units)
[![Test status](http://teststatusbadge.azurewebsites.net/api/status/odalet/delta-units)](https://ci.appveyor.com/project/odalet/delta-units)
[![Coverage Status](https://coveralls.io/repos/github/odalet/Delta.Units/badge.svg?branch=master)](https://coveralls.io/github/odalet/Delta.Units)
[![NuGet](https://img.shields.io/nuget/v/Delta.Units.svg)](https://www.nuget.org/packages/Delta.Units/)

Delta.Units
=============

This is a library allowing to define various units and to convert values between them.
Because it is based on modelling the dimension of each unit, it is straightforward (once a unit is defined in relation with other ones) to convert between quantities. 

This library was heavily inspired by this [Units and Amounts article on codeproject](http://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts). However, it goes beyond what is provided here by supporting units that don't convert between them simply by applying a multiplication factor.

This is possible because, instead of storing a factor, the conversion (and inverse conversion) functions are stored and combined when building units.
 
Licensing
---------
[Ms-RL][msrl]

  [msrl]: License.md "MS-RL License"
