using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects.Forms
{
    public class NewTaskFrame
    {
        public bool CheckTaskTitle(string Title)
        {
            WebItem Field = new WebItem($"(//input[contains(@value, '{Title}')])[last()]", "Поле названия задачи");

            if (Field.WaitElementDisplayed())
            {
                return true;
            }
            return false;
        }

        public NewTaskFrame SetContractor(string Contractor)
        {
            new WebItem("//span[text()='Ответственный']/parent::div//span[@title]", "Кнопка удлаения ответсвенного")
                .Click();
            new WebItem($"(//div[text()='{Contractor}'])[last()]", $"Сотрудник {Contractor} из списка сотрудников")
                .Click();
            
            return this;
        }

        public NewTaskFrame SetDirector(string Director)
        {
            new WebItem("//span[@data-target='originator']", "Кнопка добавления постановщика")
                .Click();
            new WebItem("//span[text()='Постановщик']/parent::div//span[@title]", "Кнопка удлаения постановщика")
                .Click();
            new WebItem($"(//div[text()='{Director}'])[last()]", $"Сотрудник {Director} из списка сотрудников")
                .Click();
            new WebItem("//span[text()='Крайний срок']", "Подпись поля 'Крайний срок'")
                .Click();
            
            return this;
        }

        public NewTaskFrame SetWatcher(string Watcher)
        {
            new WebItem("//span[@data-target='auditor']", "Кнопка добавления наблюдателя")
                .Click();
            new WebItem("(//span[text()='Наблюдатели']/parent::div//a)[last()]", "Кнопка удлаения наблюдателя")
                .Click();
            new WebItem($"(//div[text()='{Watcher}'])[last()]", $"Сотрудник {Watcher} из списка сотрудников")
                .Click();
            new WebItem("//span[text()='Крайний срок']", "Подпись поля 'Крайний срок'")
                .Click();
            
            return this;
        }

        public FormsMainPage CreateTast()
        {
            new WebItem("//span[text()='Ctrl']", "Кнопка создания задачи")
                .Click();

            return new FormsMainPage();
        }

    }
}