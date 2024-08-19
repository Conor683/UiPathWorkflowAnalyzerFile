using static WorkflowAnalyzerTST.ClassicRules;
using UiPath.Studio.Activities.Api.Analyzer;
using UiPath.Studio.Activities.Api;

namespace WorkflowAnalyzerTST
{
    public class Register : IRegisterAnalyzerConfiguration
    {
        public void Initialize(IAnalyzerConfigurationService workflowAnalyzerConfigurationService)
        {
            if (!workflowAnalyzerConfigurationService.HasFeature("ObjectRepositoryV1"))
                return;
            else
                workflowAnalyzerConfigurationService.AddCounter(NumberOfClassicActivitiesInFile.Get());
                workflowAnalyzerConfigurationService.AddRule(ClassicActivitiesRule.Get());
        }
    }
}
