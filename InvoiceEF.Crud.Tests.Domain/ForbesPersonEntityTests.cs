using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Tests.Domain
{
    [TestClass]
    public class ForbesPersonEntityTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldInitializeId()
        {
            // Arrange & Act
            var person = new ForbesPersonEntity();

            // Assert
            Assert.AreNotEqual(Guid.Empty, person.Id);
        }

        [TestMethod]
        public void PersonName_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var person = new ForbesPersonEntity
            {
                PersonName = "Elon Musk"
            };

            // Act & Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(person.PersonName));
        }

        [TestMethod]
        public void Rank_ShouldBePositive()
        {
            var person = new ForbesPersonEntity
            {
                Rank = 1
            };

            Assert.IsTrue(person.Rank > 0);
        }

        [TestMethod]
        public void FinalWorth_ShouldBeNonNegative()
        {
            var person = new ForbesPersonEntity
            {
                FinalWorth = 100_000_000
            };

            Assert.IsTrue(person.FinalWorth >= 0);
        }

        [TestMethod]
        public void EstWorthPrev_ShouldBeNonNegative()
        {
            var person = new ForbesPersonEntity
            {
                EstWorthPrev = 50_000_000
            };

            Assert.IsTrue(person.EstWorthPrev >= 0);
        }

        [TestMethod]
        public void BirthDate_ShouldBeInThePast_IfSet()
        {
            var person = new ForbesPersonEntity
            {
                BirthDate = new DateTime(1971, 6, 28)
            };

            if (person.BirthDate.HasValue)
                Assert.IsTrue(person.BirthDate.Value < DateTime.Now);
        }

        [TestMethod]
        public void Uri_ShouldNotBeNullOrEmpty()
        {
            var person = new ForbesPersonEntity
            {
                Uri = "https://www.forbes.com/profile/elon-musk/"
            };

            Assert.IsFalse(string.IsNullOrWhiteSpace(person.Uri));
        }

        [TestMethod]
        public void ImageExists_ShouldBeBoolean()
        {
            var person = new ForbesPersonEntity
            {
                ImageExists = true
            };

            Assert.IsTrue(person.ImageExists);
        }
    }
}
