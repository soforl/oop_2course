using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private int _maxStudentAmount; 

        [SetUp]
        public void Setup()
        {
            _maxStudentAmount = 20;
            _isuService = new IsuService(_maxStudentAmount);
        }
        

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3209");
            Student student = _isuService.AddStudent(group, "Fedor");
            
            Assert.AreEqual(student.Group, group);

        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3210");
                for (int i = 1; i <= (_maxStudentAmount + 1); ++i)
                {
                    _isuService.AddStudent(group,$"Sasha{i}");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M2212");
                Group group1 = _isuService.AddGroup("P3212");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
                Group group = _isuService.AddGroup("M3107");
                Group group2 = _isuService.AddGroup("M3108");
                Student student = _isuService.AddStudent(group, "Masha");
                _isuService.ChangeStudentGroup(student, group2);
        }
    }
}