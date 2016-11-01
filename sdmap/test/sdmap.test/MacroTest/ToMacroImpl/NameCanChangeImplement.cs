﻿using sdmap.Functional;
using sdmap.Macros.Attributes;
using sdmap.Parser.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdmap.test.MacroTest.ToMacroImpl
{
    public static class NameCanChangeImpl
    {
        [MacroName("NiceName")]
        public static Result<string> HelloWorld(SdmapContext context, object[] arguments)
        {
            return Result.Ok("Hello World");
        }
    }
}
