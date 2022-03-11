using atFrameWork2.BaseFramework;
using ATframework3demo.BaseFramework;
using Microsoft.AspNetCore.Components;

namespace ATframework3demo.Pages.TestRunPage
{
    public class TestRunComponentBase : ComponentBase
    {
        CaseCollectionGenerator CaseColBuilder = new CaseCollectionGenerator();
        protected bool RunButtonDisabled { get; set; }
        protected List<TestCase> CaseCollection { get; set; }

        protected async void RunSelectedTests()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            CaseCollection = CaseColBuilder.FrameworkCaseCollection;
        }
    }
}
