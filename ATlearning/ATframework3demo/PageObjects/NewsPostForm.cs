using atFrameWork2.BaseFramework;

namespace ATframework3demo.PageObjects
{
    /// <summary>
    /// Форма добавления нового сообщения в новости
    /// </summary>
    public class NewsPostForm
    {
        public bool IsRecipientPresent(string recipientName)
        {
            //проверить наличие шильдика
            var recipientsArea = new atFrameWork2.SeleniumFramework.WebItem("//div[@id='entity-selector-oPostFormLHE_blogPostForm']//div[@class='ui-tag-selector-items']",
                "Область получателей поста");
            bool isRecipientPresent = Waiters.WaitForCondition(() => recipientsArea.AssertTextContains(recipientName, default), 2, 6,
                $"Ожидание появления строки '{recipientName}' в '{recipientsArea.Description}'");
            return isRecipientPresent;
        }
    }
}
