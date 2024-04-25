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
            WebDriverActions.Refresh();
            WebItem NextButton = new WebItem("//a[text()='���������']", "������ '���������'");
            WebItem Form = new WebItem($"//span[text()='{Title}']", "��������� �����");

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
                
            return new OpenedFormFrame();
        }


        // public FormsMainPage CreateNewForm(Form testForm)
        // {
        //     //������ �� ������ ������� �����
        //     var createFormButton = new WebItem("//button[@class='ui-btn ui-btn-success']", "������ '�������� �����' � ������� ������");
        //     createFormButton.Click();

        //     //������������� �� ����� �������� �����
        //     new WebItem("//iframe[@class='side-panel-iframe']", "����� �������� �����").SwitchToFrame();

        //     //������ �� ����� �����
        //     var formHeader = new WebItem("//h1[text()='����� �����']", "����� �������� ����� � ���� �������� ������");
        //     formHeader.Click();

        //     //��������� �������� �����
        //     var formNameInput = new WebItem("//input[@type='text' and @value='����� �����']", "���� ����� ����� �����");
        //     formNameInput.SendKeys(testForm.Title);

        //     //��������� ��������� �����
        //     var saveWholeFormButton = new WebItem("//button[@class='btn btn-primary']", "������ '���������' ��� ���� �������� �����");
        //     saveWholeFormButton.Click();

        //     saveWholeFormButton.Click();

        //     //��������� �� ����������� �����
        //     WebDriverActions.SwitchToDefaultContent();
        //     return this;
        // }

        // public bool IsFormPresent(Form testForm)
        // {
        //     //�������� ��������� ����� � ������ ����
        //     var taskTitleInGrid = new WebItem($"//span[@class='main-grid-cell-content' and text()='{testForm.Title}����� �����']", "��������� ����� � ����� ������ ����");
        //     bool isFormPresent = Waiters.WaitForCondition(() => taskTitleInGrid.WaitElementDisplayed(), 2, 6, $"�������� ��������� ����� '{testForm.Title}����� �����' � ������ ����");

        //     return isFormPresent;
        // }

    }
}