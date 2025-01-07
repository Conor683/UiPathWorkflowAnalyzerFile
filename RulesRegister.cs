using static WorkflowAnalyzerTST.ClassicRules;
using static WorkflowAnalyzerTST.SensitiveDataRules;
using static WorkflowAnalyzerTST.LoopRules;
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

            if (!workflowAnalyzerConfigurationService.HasFeature("WorkflowAnalyzerV6"))
                return;
            else
                workflowAnalyzerConfigurationService.AddRule(ScreenshotActivitiesRule.Get());
                workflowAnalyzerConfigurationService.AddRule(MaxIterationsRule.Get());
        }
    }
}
