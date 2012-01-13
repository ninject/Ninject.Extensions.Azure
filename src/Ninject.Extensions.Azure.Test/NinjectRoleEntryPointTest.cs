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

        private class TestableNinjectRoleEntryPoint : NinjectRoleEntryPoint
        {
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
}