using ImageConverterLib.Helpers;
using ImageConverterLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageTypeConverter.UnitTest.DataAnnotationTest
{
    [TestClass]
    public class AppSettingsModelTest
    {
        private static TestContext _context;

        [ClassInitialize]
        public static void TestInit(TestContext context)
        {
            _context = context;
        }

        [TestMethod]
        public void JpegImageQualityTestCorrect1()
        {

            const int ImageQuality = 50;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            Assert.IsTrue(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        [TestMethod]
        public void JpegImageQualityTestCorrect2()
        {
            const int ImageQuality = 100;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            Assert.IsTrue(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        [TestMethod]
        public void JpegImageQualityTestCorrect3()
        {
            const int ImageQuality = 75;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            Assert.IsTrue(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        [TestMethod]
        public void JpegImageQualityTestAbove()
        {
            const int ImageQuality = 101;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            Assert.IsFalse(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        [TestMethod]
        public void JpegImageQualityTestBelow()
        {
            const int ImageQuality = 49;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            Assert.IsFalse(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        [TestMethod]
        public void JpegImageQualityTestValidationResult()
        {
            const int ImageQuality = -50;
            ApplicationSettingsModel model = CreateModel(ImageQuality);
            ModelValidator validator = new ModelValidator(model);

            bool valid = validator.ValidateModel();
            var validationResults = validator.ValidationResults;

            Assert.IsTrue(validationResults.Count == 1);

            _context.WriteLine("ValidationResults:");
            foreach (var result in validationResults)
            {
                _context.WriteLine(result.ErrorMessage);
            }

            Assert.IsFalse(valid, " validation of " + nameof(model.JpegImageQuality) + " failed!");
        }

        private static ApplicationSettingsModel CreateModel(int ImageQuality)
        {
            var model = new ApplicationSettingsModel { JpegImageQuality = ImageQuality };
            return model;
        }
    }
}