using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace SailClubLibraryTest
{
    [TestClass]
    public sealed class BoatRepoUnitTest
    {
        [TestMethod]
        public void TestAddBoat()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            Boat newBoat = new Boat(3, BoatType.LASERJOLLE, "Model", "18-1234", "A new engine", 30, 20, 10, "2022");
            int beforeCount = boatRepo.Count;
            // Act
            boatRepo.AddBoat(newBoat);
            // Assert
            Assert.AreEqual(beforeCount + 1, boatRepo.Count);
            Assert.IsTrue(boatRepo.GetAllBoats().Any(b => b.SailNumber == "18-1234"));
        }

        [TestMethod]
        [ExpectedException(typeof(BoatSailnumberExistsException))]
        public void AddExistingBoat_Exception()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            Boat boat = new Boat(4, BoatType.LASERJOLLE, "Model", "16-7775", "A new engine", 30, 20, 10, "2022");
            boatRepo.AddBoat(boat);
            // Act
            boatRepo.AddBoat(boat);
            // Assert

        }

        [TestMethod]
        public void AddExistingBoat_ExceptionAlternative()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            Boat boat = new Boat(4, BoatType.LASERJOLLE, "Model", "16-7775", "A new engine", 30, 20, 10, "2022");
            boatRepo.AddBoat(boat);
            Boat boatWithSameSailNumber = new Boat(3, BoatType.FEVA, "Model 2", "16-7775", "Fast engine", 40, 30, 50, "2019");
            // Act & assert
            Assert.ThrowsException<BoatSailnumberExistsException>(() => boatRepo.AddBoat(boatWithSameSailNumber));
        }

        [TestMethod]
        public void TestRemoveBoat()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            Boat newBoat = new Boat(3, BoatType.LASERJOLLE, "Model", "18-1234", "A new engine", 30, 20, 10, "2022");
            boatRepo.AddBoat(newBoat);
            string sailNumberToRemove = newBoat.SailNumber;
            int beforeCount = boatRepo.Count;
            // Act
            boatRepo.RemoveBoat(sailNumberToRemove);
            // Assert
            Assert.AreEqual(beforeCount - 1, boatRepo.Count);
            Assert.IsFalse(boatRepo.GetAllBoats().Any(b => b.SailNumber == sailNumberToRemove));
        }

        [TestMethod]
        public void TestRemoveNonExistingBoat()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            string sailNumberToRemove = "16-7777";
            int beforeCount = boatRepo.Count;
            // Act
            boatRepo.RemoveBoat(sailNumberToRemove);
            // Assert
            Assert.AreEqual(beforeCount, boatRepo.Count);
        }

        [TestMethod]
        public void SearchExistingBoat()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            string sailNumber = "16-7775";
            Boat boatToSearchFor = new Boat(4, BoatType.LASERJOLLE, "Model", sailNumber, "A new engine", 30, 20, 10, "2022");
            boatRepo.AddBoat(boatToSearchFor);

            // Act
            Boat foundBoat = boatRepo.SearchBoat(sailNumber)!;

            // Assert
            Assert.AreEqual(boatToSearchFor, foundBoat);
        }

        [TestMethod]
        public void SearchNonExistingBoat_ReturnsNull()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            string sailNumber = "16-7775";

            // Act 
            Boat? foundBoat = boatRepo.SearchBoat(sailNumber);

            // Assert
            Assert.IsNull(foundBoat);
        }

        [TestMethod]
        public void UpdateBoat()
        {
            // Arrange
            IBoatRepository boatRepo = new BoatRepository();
            Boat boatToUpdate = boatRepo.GetAllBoats()[0];
            string existingSailNumber = boatToUpdate.SailNumber;

            BoatType newBoatType = BoatType.WAYFARER;
            string newModel = "Good model";
            string newEngineInfo = "Very very fast";
            double newDraft = 25;
            double newWidth = 15;
            double newLength = 40;
            string newYearOfConstruction = "1999";
            Boat updatedBoat = new Boat(1, newBoatType, newModel, existingSailNumber, newEngineInfo, newDraft, newWidth, newLength, newYearOfConstruction);

            // Act
            boatRepo.UpdateBoat(updatedBoat);

            // Assert
            Boat boatAfterUpdate = boatRepo.SearchBoat(existingSailNumber)!;

            Assert.AreEqual(newBoatType, boatAfterUpdate.TheBoatType);
            Assert.AreEqual(newModel, boatAfterUpdate.Model);
            Assert.AreEqual(newEngineInfo, boatAfterUpdate.EngineInfo);
            Assert.AreEqual(newDraft, boatAfterUpdate.Draft);
            Assert.AreEqual(newWidth, boatAfterUpdate.Width);
            Assert.AreEqual(newLength, boatAfterUpdate.Length);
            Assert.AreEqual(newYearOfConstruction, boatAfterUpdate.YearOfConstruction);
        }
    }
}
