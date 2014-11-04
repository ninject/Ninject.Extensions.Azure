# Ninject.Extensions.Azure [![NuGet Version](http://img.shields.io/nuget/v/Ninject.Extensions.Azure.svg?style=flat)](https://www.nuget.org/packages/Ninject.Extensions.Azure/) [![NuGet Downloads](http://img.shields.io/nuget/dt/Ninject.Extensions.Azure.svg?style=flat)](https://www.nuget.org/packages/Ninject.Extensions.Azure/)
This extension adds support for Microsoft Azure Worker Roles.

## Getting started

Just derive from NinjectRoleEntryPoint in your WorkerRole.cs (previously RoleEntryPoint) and override the following methods:

The Run method is the main thread of execution for your role instance.
```csharp
public abstract override void Run();
```

Implement the CreateKernel method to create and configure your kernel and load the modules required by your role instance.
```csharp
protected abstract IKernel CreateKernel();
```

If you like to extend the startup behavior of your role instance just override OnRoleStarted() which is called after CreateKernel.
```csharp
protected virtual bool OnRoleStarted();
```

If you like to extend the shutdown behavior of your role instance you have two choices. You can override OnRoleStopping() which is called before the kernel is disposed.
```csharp
protected virtual void OnRoleStopping();
```

Addition to that you can also override OnRoleStopped() which is called after the kernel is disposed.
```csharp
protected virtual void OnRoleStopped();
```

## CI build status
[![Build Status](https://teamcity.bbv.ch/app/rest/builds/buildType:(id:bt44)/statusIcon)](http://teamcity.bbv.ch/viewType.html?buildTypeId=bt44&guest=1)