Delta.Units
=============

This is a library allowing to define various units and to convert values between them.
Because it is based on modelling the dimension of each unit, it is straightforward (once a unit is defined in relation with other ones) to convert between quantities. 

This library was heavily inspired by this [Units and Amounts article on codeplex](http://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts). However, it goes beyond what is provided here by supporting units that don't convert between them simply by applying a multiplication factor.

This is possible because, instead of storing a factor, the conversion (and inverse conversion) functions are stored and combined when building units.
 
Licensing
---------
[Ms-RL][msrl]

  [msrl]: License.md "MS-RL License"
