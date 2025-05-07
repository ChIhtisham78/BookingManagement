using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos.PhotoSessionDtos
{
    public class PhotoSessionToCreateDto
    {
        public decimal Cost { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int? RoomId { get; set; }
    }
}
