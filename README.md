# DynamicProxySamples
A small project to explore dynamic proxy functionality in .NET Core.

### Executing code
Navigate to ../Shared.Tests and execute ```dotnet test```.

### What is it about?
This sample project demonstrates the application of dynamic proxies applied to Adapter object oriented pattern, which converts the interface of a class into another interface clients expect. Mature projects such as Castle DynamicProxy have made it possible to generate lightweight dynamic proxies on the fly at runtime.

Scenarios commonly encountered in enterprise applications are generated clients and data transfer objects without declared interface or virtual methods, either of which is usually required for Castle dynamic proxy functionality. While there may exist libraries without such restrictions, one should aim to build enterprise applications on mature projects with large user and contributor community. 

Suppose you have a database service class:
