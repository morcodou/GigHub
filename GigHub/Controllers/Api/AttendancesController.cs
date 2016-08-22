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
            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId);

            var userid = User.Identity.GetUserId();
            if (attendance.AttendeeId == userid)
            {
                return BadRequest("The attendance already exists.");
            }

            var newattendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userid
            };

            _unitOfWork.Attendances.Add(newattendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var attendance = _unitOfWork.Attendances.GetAttendance(id);

            if (attendance == null)
            {
                return NotFound();
            }

            var userid = User.Identity.GetUserId();
            if (attendance.AttendeeId != userid)
            {
                return Unauthorized();
            }

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
