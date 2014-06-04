using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieFanatic.Web.Models
{
    public class MovieDetailViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Overview { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}