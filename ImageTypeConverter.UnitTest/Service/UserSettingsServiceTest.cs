using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.Service
{
    public class UserSettingsServiceTest : ServiceTestBase
    {
        [ClassInitialize]
        public static void UserSettingsServiceClassInit(TestContext context)
        {
            ServiceTestClassInit(context);
        }

        [ClassCleanup]
        public static void UserSettingsServiceClassCleanup()
        {
            ServiceTestClassCleanup();
        }

        [TestInitialize]
        public override void ServiceTestInit()
        {
        }

        [TestCleanup]
        public override void ServiceTestCleanup()
        {
        }
    }
}
