using atFrameWork2.SeleniumFramework;
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

        public bool IsFormPresent(string Title)
        {
            WebItem NextButton = new WebItem("//a[text()='���������']", "������ '���������'");
            WebItem Form = new WebItem($"//a[text()='{Title}']", "��������� �����");

            if (Form.WaitElementDisplayed())
            {
                return true;
            }

            while (NextButton.WaitElementDisplayed())
            {
                NextButton.Click();
                if (Form.WaitElementDisplayed())
                {
                    return true;
                }
            }

            return false;
        }

        public OpenedFormFrame OpenForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"���������� ���� ����� {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='�������']", "����� '�������'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"����� �������� ����� {Title}")
                .SwitchToFrame();

            return new OpenedFormFrame();
        }

        public CreateFormFrame EditForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"���������� ���� ����� {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='�������������']", "����� '�������������'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"����� �������������� ����� {Title}")
                .SwitchToFrame();

            return new CreateFormFrame();
        }

        public FormsMainPage SelectForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//td[@class='main-grid-cell main-grid-cell-checkbox']/span",
                $"������� ����� {Title}").Click();

            return this;
        }

        public FormsMainPage DeleteSelectedForms()
        {
            new WebItem("//span[text()='�������']", "������ �������������� �������� '�������'")
                .Click();
            new WebItem("//span[text()='�����������']", "������ '����������' ������������ ����")
                .Click();

            return this;
        }

        public FormsMainPage DeleteForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"���������� ���� ����� {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='�������']", "����� '�������'")
                .Click();

            return this;
        }

        public FormsMainPage CreateForm(string Title)
        {
            CreateFormFrame Form = OpenCreateFormSlider();
            Form.ChangeFormTitle(Title);
            Form.AddQuestion();
            Form.SaveForm();

            return this;
        }
    }
}