using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;

namespace aTframework3demo.PageObjects.Forms
{
    public class FormResultPage
    {
        public bool IsResultRowAsExpected(Dictionary<int, string> ChosenOptions)
        {
            string xPath = "//tr[@class='main-grid-row main-grid-row-body']//td/following-sibling::td";

            foreach (var optionId in ChosenOptions)
            {
                xPath += "/following-sibling::td";
                var encounteredCellInRow = new WebItem(xPath, "Очередная ячейка в строке результата");
                string encounteredAnswer = encounteredCellInRow.InnerText();

                if (encounteredAnswer != optionId.Value)
                {
                    Log.Error($"Очередное полученное значение '{encounteredAnswer}' не совпадает с ожидаемым'{optionId.Value}'");
                    return false;
                }
            }

            return true;
        }
    }
}