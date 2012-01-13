This extension adds support for azure roles.

Just derive your RoleEntryPoint from NinjectRoleEntryPoint and implement the following methods:

The Run method is the main thread of execution for your role instance.
public abstract override void Run();

Implement the CreateKernel method to create and configure your kernel and load the modules required by your role instance.
protected abstract IKernel CreateKernel();

If you like to extend the startup behavior of your role instance just override OnRoleStarted() which is called after CreateKernel.
protected virtual bool OnRoleStarted();

If you like to extend the shutdown behavior of your role instance just override OnRoleStopped() which is called after the kernel is disposed.
protected virtual void OnRoleStopped();
