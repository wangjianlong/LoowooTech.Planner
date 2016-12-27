using LoowooTech.Planner.Common;
using LoowooTech.Planner.WorkBench.Commands;
using LoowooTech.Planner.WorkBench.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Planner.WorkBench.UI
{
    class CommandShowSketchForm:BaseTLWCommand
    {
        public override void OnClick()
        {
            try
            {
                SketchForm sketchForm = WorkBench.FindDocument<SketchForm>();
                if (sketchForm == null)
                {
                    sketchForm = WorkBench.ShowDocument<SketchForm>();
                    if (PubishFunction.LoadDataState)
                    {
                        sketchForm.LoadMapData();
                    }
                    
                }
                else
                {
                    sketchForm.Activate();
                }


            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
