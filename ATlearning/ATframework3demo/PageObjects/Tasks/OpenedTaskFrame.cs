using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.PageObjects.Forms;

namespace aTframework3demo.PageObjects.Tasks
{
    public class OpenedTaskFrame
    {
        public FormPage OpenForm()
        {
            string Href = new WebItem("//a[text()='Форма доступна по ссылке']", "")
                .GetAttribute("href");

            Uri uri = new Uri(Href);
            WebDriverActions.OpenUri(uri);

            return new FormPage();
        }
    }
}