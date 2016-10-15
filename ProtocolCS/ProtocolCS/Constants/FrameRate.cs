using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolCS.Constants
{
    public class FrameRate
    {
        public static readonly float TargetFps = 20.0f;

        public static int Interval
        {
            get
            {
                return (int)((1.0 / TargetFps) * 1000);
            }
        }
    }
}
