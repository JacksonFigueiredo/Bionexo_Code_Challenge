using System;
using System.Collections.Generic;
using System.Text;

namespace DispatchingModule.Model.TO
{
   //Transfer Object made to be possible to save the transformed entity to the Json
    class CaptureTO
    {
        public string ComputerName { get; set; }
        public DateTime CaptureDate { get; set; }
    }
}
