using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.UIHelpers
{
    public class TaskTemplateUIHelper
    {
        public static IEnumerable<UITaskTemplate> Convert(IEnumerable<TaskTemplate> aTemplates)
        {
            var lOutput = new List<UITaskTemplate>();
            if (aTemplates == null)
                return lOutput;

            foreach (var lTemplate in aTemplates)
                lOutput.Add(Convert(lTemplate));
            return lOutput;
        }

        public static UITaskTemplate Convert(TaskTemplate aTemplate)
        {
            if (aTemplate == null)
                return null;

            var lUI = new UITaskTemplate();
            lUI.CompanyId = aTemplate.CompanyId;
            lUI.CompanyName = aTemplate.Company.Name;
            lUI.Name = aTemplate.Name;
            lUI.FromStartDate = aTemplate.FromStartDate;
            lUI.FromEndDate = aTemplate.FromEndDate;
            lUI.Opportunity = aTemplate.Opportunity;

            return lUI;
        }
    }
}
