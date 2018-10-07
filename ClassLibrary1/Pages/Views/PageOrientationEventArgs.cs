﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class PageOrientationEventArgs : EventArgs
    {
        public PageOrientationEventArgs(PageOrientation orientation)
        {
            Orientation = orientation;
        }

        public PageOrientation Orientation { get; }
    }

    public enum PageOrientation
    {
        Horizontal = 0,
        Vertical = 1,
    }
}
