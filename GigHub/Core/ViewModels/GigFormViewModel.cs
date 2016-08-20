using GigHub.Controllers;
using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", this.Date, this.Time));
        }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, ActionResult>> update = (u => u.Update(this));
                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                var actionname = (action.Body as MethodCallExpression).Method.Name;

                return actionname;
            }
        }
        public string Heading { get; set; }


    }
}