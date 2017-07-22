using System.Collections.Generic;

namespace MemoryGame.Helpers
{
    public class IconPostHelper : IIconPostHelper
    {
        public List<string> FormatIconText(List<string> values)
        {
            List <string> results = new List<string>();

            foreach (var value in values)
            {
                var sections = value.Split(' ');
                results.Add(sections[1]);
            }

            return results;
        }
    }
}
