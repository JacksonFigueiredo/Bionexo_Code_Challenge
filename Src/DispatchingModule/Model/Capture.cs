using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatchingModule.Model
{
    // Main Model used on data capture and manipulation
    public class Capture
    {
        public int? Id { get; set; }
        public string ComputerName { get; set; }
        public DateTime CaptureDate { get; set; }
    }
}
