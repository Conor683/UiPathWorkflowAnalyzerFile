using static WorkflowAnalyzerRules.ClassicRules;
using static WorkflowAnalyzerRules.SensitiveDataRules;
using static WorkflowAnalyzerRules.LoopRules;
using static WorkflowAnalyzerRules.SelectorRules;
using static WorkflowAnalyzerRules.Logs;
using UiPath.Studio.Activities.Api.Analyzer;
using UiPath.Studio.Activities.Api;

namespace WorkflowAnalyzerRules
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
                workflowAnalyzerConfigurationService.AddRule(ObjectRepoUsageRule.Get());

            if (!workflowAnalyzerConfigurationService.HasFeature("WorkflowAnalyzerV6"))
                return;
            else
                workflowAnalyzerConfigurationService.AddRule(ScreenshotActivitiesRule.Get());
                workflowAnalyzerConfigurationService.AddRule(MaxIterationsRule.Get());
                workflowAnalyzerConfigurationService.AddRule(NumOfRetriesRule.Get());
                workflowAnalyzerConfigurationService.AddRule(OutputLogMessages.Get());
        }
    }
}

