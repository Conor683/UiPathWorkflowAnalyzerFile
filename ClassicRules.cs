using System.Diagnostics;
using UiPath.Studio.Activities.Api;
using UiPath.Studio.Activities.Api.Analyzer;
using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

namespace WorkflowAnalyzerTST
{
    public class ClassicRules : IRegisterAnalyzerConfiguration
    {
        public void Initialize(IAnalyzerConfigurationService workflowAnalyzerConfigurationService)
        {
            if (!workflowAnalyzerConfigurationService.HasFeature("ObjectRepositoryV1"))
                return;

            workflowAnalyzerConfigurationService.AddCounter(NumberOfClassicActivitiesInFile.Get());
            workflowAnalyzerConfigurationService.AddRule(ClassicActivitiesRule.Get());
        }

        // This static class is not mandatory. It just helps organizining the code.
        internal static class ClassicActivitiesRule
        {
            // This should be as unique as possible, and should follow the naming convention.
            private const string RuleId = "SG-CLA-001";
            internal static Rule<IActivityModel> Get()
            {
                var rule = new Rule<IActivityModel>("Classic Activities Disallowed", RuleId, Inspect)
                {
                    RecommendationMessage = "Replace with a modern design experience counterpart",
                    /// Off and Verbose are not supported.
                    ErrorLevel = System.Diagnostics.TraceLevel.Error
                };
                return rule;
            }

            // This is the function that executes for each activity in all the files. Might impact performance.
            // The rule instance is the rule provided above which also contains the user-configured data.
            private static InspectionResult Inspect(IActivityModel activity, Rule ruleInstance)
            {
                var messageList = new List<string>();
                if ((activity.Type.ToLower().Contains("uiautomation")) && (!activity.SupportsObjectReferences))
                {
                    messageList.Add($"The activity {activity.DisplayName} has been flagged as potentially classic");
                }
                if (messageList.Count > 0)
                {
                    return new InspectionResult()
                    {
                        ErrorLevel = ruleInstance.ErrorLevel,
                        HasErrors = true,
                        RecommendationMessage = ruleInstance.RecommendationMessage,
                        // When inspecting a model, a rule can generate more than one message.
                        Messages = messageList
                    };
                }
                else
                {
                    return new InspectionResult() { HasErrors = false };
                }
            }
        }


        internal static class NumberOfClassicActivitiesInFile
        {
            private const string RuleId = "SG-CLA-002";

            internal static Counter<IActivityModel> Get()
            {
                return new Counter<IActivityModel>("COUNTER NAME", RuleId, Inspect);
            }

            // A Counter<T> receives the entire collection of T objects in the parent structure. e.g. activities in workflow, workflows in project.
            private static InspectionResult Inspect(IReadOnlyCollection<IActivityModel> activities, Counter ruleInstance)
            {
                var ClassicActivities = 0;

                foreach (var activity in activities)
                {
                    if ((activity.Type.ToLower().Contains("uiautomation"))&&(!activity.SupportsObjectReferences))
                    {
                        ClassicActivities++;
                    }
                    
                }

                return new InspectionResult()
                {
                    // For a counter, the error level is always info, even if not set here.
                    ErrorLevel = System.Diagnostics.TraceLevel.Info,
                    // For a counter, the Has Errors field is always ignored.
                    HasErrors = false,
                    Messages = new List<string>() { string.Format("Possible Classic Activities Detected: {0}", ClassicActivities) }
                };
            }
        }

    }
}