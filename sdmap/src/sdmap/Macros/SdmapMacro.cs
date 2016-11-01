﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace sdmap.Macros
{
    public class SdmapMacro
    {
        public string Name { get; set; }

        public SdmapTypes[] Arguments { get; set; }

        public SdmapMacroDelegate Function { get; set; }
    }
}
