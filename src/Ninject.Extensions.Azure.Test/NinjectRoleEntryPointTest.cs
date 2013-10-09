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

            this.testee.KernelCreated.Should().BeTrue();
        }

        [Fact]
        public void InjectsPropertyOnStart()
        {
            this.testee.OnStart();

            this.testee.Injection.Should().NotBeNull();
        }

        [Fact]
        public void InjectsMethod()
        {
            this.testee.OnStart();

            this.testee.MethodHasBeenCalled.Should().BeTrue();
        }

        private class TestableNinjectRoleEntryPoint : NinjectRoleEntryPoint
        {
            [Inject]
            public InjectedClass Injection { get; set; }

            [Inject]
            public void InjectionMethod(InjectedClass obj)
            {
                this.MethodHasBeenCalled = obj != null;
            }

            public bool MethodHasBeenCalled { get; private set; }

            public bool KernelCreated { get; private set; }

            public override void Run()
            {
            }

            protected override IKernel CreateKernel()
            {
                this.KernelCreated = true;

                return new StandardKernel();
            }
        }
    }

    public class InjectedClass { }
}