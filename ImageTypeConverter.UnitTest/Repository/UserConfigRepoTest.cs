using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Repository
{
    [TestClass]
    public class UserConfigRepoTest  : RepositoryTestBase
    {

        [ClassInitialize]
        protected void RepositoryClassInit(TestContext context)
        {
            RepositoryTestClassInit(context);
        }

        [ClassCleanup]
        public static void AppSettingsRepoClassCleanup()
        {
            RepositoryTestClassCleanup();
        }

        [TestInitialize]
        public override void RepositoryTestInit()
        {
        }

        [TestCleanup]
        public override void RepositoryTestCleanup()
        {

        }





    }
}
