using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects.Forms;

namespace ATframework3demo.PageObjects
{
    public class FormsMainPage
    {
        public CreateFormFrame OpenCreateFormSlider()
        {
            new WebItem("//button[@class='ui-btn ui-btn-success']", "������ '�������'")
                .Click();
            new WebItem("//iframe[@class='side-panel-iframe']", "����� �������� �����")
                .SwitchToFrame();

            return new CreateFormFrame();
        }

        public FormsMainPage IsFormPresent(string Title)
        {
            WebItem NextButton = new WebItem("//a[text()='���������']", "������ '���������'");
            WebItem Form = new WebItem($"//span[text()='{Title}']", "��������� �����");

            if (Form.WaitElementDisplayed())
                {
                    return this;
                }

            while (NextButton.WaitElementDisplayed())
            {
                NextButton.Click();
                if (Form.WaitElementDisplayed())
                {
                    return this;
                }
            }

            throw new Exception("��������� ����� �� ���������� � ������� ����");
        }

        public OpenedFormFrame OpenForm(string Title)
        {
            new WebItem($"//span[text()='{Title}']", "��������� �����")
                .DoubleClick();

            new WebItem("//iframe[@class='side-panel-iframe']", $"����� ������������� {Title}")
                .SwitchToFrame();
                
            return new OpenedFormFrame();
        }

        public FormsMainPage DeleteForm(string Title)
        {
            new WebItem($"//span[text()='{Title}']/parent::div/parent::td/parent::tr//a", $"���������� ���� ����� {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='�������']", "����� '�������'")
                .Click();

            return this; 
        }
    }
}