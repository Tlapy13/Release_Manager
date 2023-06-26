using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Release_Manager
{
    public class SolutionsConfig
    {
        public string SolutionName { get; set; }
        public string SolutionPath { get; set; }

        public override string ToString()
        {
            return $"{SolutionName} : {SolutionPath}";
        }
    }
}
