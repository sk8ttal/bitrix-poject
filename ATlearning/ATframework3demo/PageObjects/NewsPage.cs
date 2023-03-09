namespace ATframework3demo.PageObjects
{
    public class NewsPage
    {
        public NewsPostForm AddPost()
        {
            //Клик в Написать сообщение
            var btnPostCreate = new atFrameWork2.SeleniumFramework.WebItem("//div[@id='microoPostFormLHE_blogPostForm_inner']", "Область в новостях 'Написать сообщение'");
            btnPostCreate.Click();
            return new NewsPostForm();
        }
    }
}
