// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectRoleEntryPoint.cs" company="bbv Software Services AG">
//   2009-2011
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Azure
{
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Ninject;

    /// <summary>
    /// Abstract implementation of a ninject-capable RoleEntryPoint for azure roles.
    /// </summary>
    public abstract class NinjectRoleEntryPoint : RoleEntryPoint
    {
        private IKernel Kernel { get; set; }

        /// <summary>
        /// Called by Windows Azure to initialize the role instance.
        /// </summary>
        /// <returns>
        /// True if initialization succeeds, False if it fails. The default implementation returns True.
        /// </returns>
        public sealed override bool OnStart()
        {
            this.Kernel = this.CreateKernel();
            this.Kernel.Inject(this);
            return this.OnRoleStarted();
        }

        /// <summary>
        /// Called by Windows Azure after the role instance has been initialized. This method serves as the
        /// main thread of execution for your role.
        /// </summary>
        public abstract override void Run();

        /// <summary>
        /// Called by Windows Azure when the role instance is to be stopped.
        /// </summary>
        public sealed override void OnStop()
        {
            this.OnRoleStopping();

            if (this.Kernel != null)
            {
                this.Kernel.Dispose();
                this.Kernel = null;
            }

            this.OnRoleStopped();
        }

        /// <summary>
        /// The extension point to create the kernel and load all modules for your azure role.
        /// </summary>
        /// <returns>The kernel</returns>
        protected abstract IKernel CreateKernel();

        /// <summary>
        /// The extension point to perform custom startup actions for your azure role. This method is called after the kernel is created.
        /// </summary>
        /// <returns>True if startup succeeds, False if it fails. The default implementation returns True.</returns>
        protected virtual bool OnRoleStarted()
        {
            return true;
        }

        /// <summary>
        /// The extension point to perform custom shutdown actions for your azure lore. This method is called before ninject kernel is disposed.
        /// </summary>
        protected virtual void OnRoleStopping()
        {
        }

        /// <summary>
        /// The extension point to perform custom shutdown actions for your azure role. This method is called after the ninject kernel is disposed.
        /// </summary>
        protected virtual void OnRoleStopped()
        {
        }
    }
}