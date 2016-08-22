using FluentAssertions;
using GigHub.AccountController.Api;
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
    public class GigsControllerTests
    {
        GigsController _controller;
        Mock<IGigRepository> _mockRepository;
        string _userid;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _userid = "1";
            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser(_userid, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(rp => rp.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnAnautorized()
        {
            var gig = new Gig() { ArtistId = _userid + "_" };

            _mockRepository.Setup(rp => rp.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOkResult()
        {
            var gig = new Gig() { ArtistId = _userid };
            _mockRepository.Setup(rp => rp.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
