using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos.BirthdayDtos
{
    public class BirthdayToCreateDto
    {
        public string UserName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int NumberOfGuests { get; set; }

        // Fields for Cake information
        public List<CakeDto> Cakes { get; set; } = new List<CakeDto>();

        // Fields for Decoration information
        public List<DecorationDto> Decorations { get; set; } = new List<DecorationDto>();

        public int RoomId { get; set; }
    }
}
