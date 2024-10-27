using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Support_Ticketing_System_PL.HelperClasses
{
    interface ICloseWindow
    {
        Action Close { get; set; }
        bool DialogResult { get; set; }
    }
}
