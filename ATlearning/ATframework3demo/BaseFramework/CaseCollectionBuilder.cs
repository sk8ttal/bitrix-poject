using atFrameWork2.BaseFramework.LogTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace atFrameWork2.BaseFramework
{
    abstract class CaseCollectionBuilder
    {
        public List<TestCase> CaseCollection { get; } = new List<TestCase>();

        public CaseCollectionBuilder()
        {
            CaseCollection.AddRange(GetCases());
        }

        abstract protected List<TestCase> GetCases();

        public static void ActivateTestCaseProvidersInstances()
        {
            IEnumerable<Type> subclassTypes = Assembly
                .GetAssembly(typeof(CaseCollectionBuilder))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(CaseCollectionBuilder)));

            foreach (var subClassType in subclassTypes)
            {
                try
                {
                    var _ = Activator.CreateInstance(subClassType);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }
    }
}
