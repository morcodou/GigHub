using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        AttendancesController _controller;
        Mock<IAttendanceRepository> _mockRepository;
        string _userid;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _userid = "1";
            _controller = new AttendancesController(mockUoW.Object);
            _controller.MockCurrentUser(_userid, "user1@domain.com");
        }

        [TestMethod]
        public void DeleteAttendance_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(1);
            result.Should().BeOfType<NotFoundResult>();
        }


        [TestMethod]
        public void DeleteAttendance_UserDeletingAttendanceAnotherUserAttendance_ShouldReturnAnautorized()
        {
            var attendance = new Attendance() { AttendeeId = _userid + "_" };

            _mockRepository.Setup(rp => rp.GetAttendance(1)).Returns(attendance);
            var result = _controller.DeleteAttendance(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOkResult()
        {
            var attendance = new Attendance() { AttendeeId = _userid  };
            _mockRepository.Setup(rp => rp.GetAttendance(1)).Returns(attendance);
            var result = _controller.DeleteAttendance(1);
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }
    }
}
