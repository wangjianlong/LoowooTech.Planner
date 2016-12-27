using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.Common
{
    public static class PlannerPathHelp
    {
        public static string GetFilePath(string filePath)
        {
            string path = string.Empty;

            if (filePath.IndexOf(":") > -1)
            {
                path = filePath;
            }
            else
            {
                path = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, filePath);
            }
            return path;
        }
    }
}
