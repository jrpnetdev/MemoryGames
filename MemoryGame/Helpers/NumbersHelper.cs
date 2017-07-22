using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Helpers
{
    public class NumbersHelper : INumbersHelper
    {
        public int GetRandomNumberFromRange(int? level)
        {

            int result = 0;

            Random random = new Random();

            switch (level)
            {
                case 1:
                    result = random.Next(10000, 99999);
                    break;
                case 2:
                    result = random.Next(100000, 999999);
                    break;
                case 3:
                    result = random.Next(1000000, 9999999);
                    break;
                case 4:
                    result = random.Next(100000000, 999999999);
                    break;

            }

            return result;
        }
    }
}
