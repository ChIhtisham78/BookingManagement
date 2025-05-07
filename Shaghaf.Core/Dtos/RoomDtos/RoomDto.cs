using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos.RoomDtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public string Plan { get; set; }

        public decimal Rate { get; set; }

        public string Name { get; set; } = null!;

        public int Seat { get; set; }

        public string Description { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public ICollection<int> MembershipIds { get; set; } = new List<int>(); // List of Membership IDs


    }
}
