This extension adds support for Microsoft Azure Worker Roles.

Just derive from NinjectRoleEntryPoint in your WorkerRole.cs (previously RoleEntryPoint) and implement the following methods:

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

If you like to extend the shutdown behavior of your role instance just override OnRoleStopped() which is called after the kernel is disposed.
```csharp
    protected virtual void OnRoleStopped();
```
