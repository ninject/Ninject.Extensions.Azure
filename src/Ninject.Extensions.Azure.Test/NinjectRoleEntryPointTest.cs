// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectRoleEntryPointTest.cs" company="bbv Software Services AG">
//   2009-2011
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Ninject.Extensions.Azure
{
    using FluentAssertions;
    using Xunit;

    public class NinjectRoleEntryPointTest
    {
        private readonly TestableNinjectRoleEntryPoint testee;

        public NinjectRoleEntryPointTest()
        {
            this.testee = new TestableNinjectRoleEntryPoint();
        }

        [Fact]
        public void CreatesKernelOnStart()
        {
            this.testee.OnStart();

            this.testee.Kernel.Should().NotBeNull();
            this.testee.Kernel.IsDisposed.Should().BeFalse();
        }

        [Fact]
        public void InjectsPropertyOnStart()
        {
            this.testee.OnStart();

            this.testee.Injection.Should().NotBeNull();
        }

        [Fact]
        public void InjectsMethodOnStart()
        {
            this.testee.OnStart();

            this.testee.MethodInjection.Should().NotBeNull();
        }

        [Fact]
        public void CallsOnStarted()
        {
            this.testee.OnStart();
            this.testee.IsStarted.Should().BeTrue();
            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void CallsRun()
        {
            this.testee.Run();

            this.testee.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void CallsOnStopping()
        {
            this.testee.OnStart();
            this.testee.OnStop();

            this.testee.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void CallsOnStopped()
        {
            this.testee.OnStart();
            this.testee.OnStop();

            this.testee.IsStarted.Should().BeFalse();
            this.testee.Kernel.IsDisposed.Should().BeTrue();
        }

        private class TestableNinjectRoleEntryPoint : NinjectRoleEntryPoint
        {
            [Inject]
            public InjectedClass Injection { get; set; }

            [Inject]
            public void InjectionMethod(InjectedClass obj)
            {
                this.MethodInjection = obj;
            }

            public InjectedClass MethodInjection { get; private set; }

            public IKernel Kernel { get; private set; }

            public bool IsStarted { get; private set; }
            public bool IsRunning { get; private set; }

            public override void Run()
            {
                IsRunning = true;
            }

            protected override IKernel CreateKernel()
            {
                this.Kernel = new StandardKernel();

                return this.Kernel;
            }

            protected override bool OnRoleStarted()
            {
                IsStarted = true;
                return IsStarted;
            }

            protected override void OnRoleStopping()
            {
                IsRunning = false;
            }

            protected override void OnRoleStopped()
            {
                IsStarted = false;
            }
        }
    }

    public class InjectedClass { }
}