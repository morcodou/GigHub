using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{


    [Authorize]
    public class AttendancesController : ApiController
    {
        IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userid = User.Identity.GetUserId();

            if (_unitOfWork.Attendances.GetAttendance(dto.GigId, userid) != null)
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userid = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.GetAttendance(id, userid);

            if (attendance == null)
            {
                return NotFound();
            }

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
