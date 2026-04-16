using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibraryTest
{
    [TestClass]
    public sealed class MemberRepoUnitTest
    {
        [TestMethod]
        public void TestAddMember()
        {
            // Arrange
            IMemberRepositoryAsync memberRepo = new MemberRepository();
            string memberPhoneNumber = "43214521";

            Member newMember = new Member(1, "Poul", "Poulsen", memberPhoneNumber, "Main Street 1", "Copenhagen", "poul@gmail.com", MemberType.Adult, MemberRole.Member);
            newMember.MemberImage = "default.jpg";
            int initialCount = memberRepo.Count().Result;
            Task.WaitAll();
            // Act

            memberRepo.AddMember(newMember);
            int finalCount = memberRepo.Count().Result;
            Task.WaitAll();

            // Assert
            int addedId = memberRepo.GetAllMembers().Result.Find(m => m.PhoneNumber == memberPhoneNumber).Id;
            newMember.Id = addedId;
            memberRepo.RemoveMember(addedId);
            Task.WaitAll();
            Assert.AreEqual(initialCount + 1, finalCount);



        }

        [TestMethod]
        public void TestRemoveMember()
        {
            // Arrange
            IMemberRepositoryAsync memberRepo = new MemberRepository();
            string memberPhoneNumber = "43214521";
            Member newMember = new Member(1, "Poul", "Poulsen", memberPhoneNumber, "Main Street 1", "Copenhagen", "poul@mail.dk", MemberType.Adult, MemberRole.Member);
            newMember.MemberImage = "default.jpg";
            memberRepo.AddMember(newMember);
            Task.WaitAll();
            int initialCount = memberRepo.Count().Result;
            Task.WaitAll();

            // Act
            int idToRemove = memberRepo.GetAllMembers().Result.Find(m => m.PhoneNumber == memberPhoneNumber).Id;
            Task.WaitAll();
            newMember.Id = idToRemove;
            
            memberRepo.RemoveMember(idToRemove);
            Task.WaitAll();
            int finalCount = memberRepo.Count().Result;

            // Assert
            Task.WaitAll();
            Assert.AreEqual(initialCount - 1, finalCount);


        }
    }
}
