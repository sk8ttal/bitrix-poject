using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;

namespace aTframework3demo.PageObjects.Forms
{
    public class FormPage
    {
        public bool IsFormNameCorrect(string FormName)
        {
            WebItem Title = new WebItem($"//h1[text()='{FormName}']", "Название формы");
            
            if (Title.WaitElementDisplayed())
            {
                return true;
            }
            return false;
        }
    }
}