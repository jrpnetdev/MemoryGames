using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Helpers
{
    public interface INumbersHelper
    {
        int GetRandomNumberFromRange(int? level);
    }
}
