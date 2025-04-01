using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

namespace WorkflowAnalyzerRules
{
    public class SelectorRules
    {

        // This static class is not mandatory. It just helps organizining the code.
        internal static class ObjectRepoUsageRule
        {
            // This should be as unique as possible, and should follow the naming convention.
            private const string RuleId = "CM-SEL-001";
            internal static Rule<IActivityModel> Get()
            {
                var rule = new Rule<IActivityModel>("Object References Required", RuleId, Inspect)
                {
                    RecommendationMessage = "Put selector into relevant application's object repository.",
                    /// Off and Verbose are not supported.
                    ErrorLevel = System.Diagnostics.TraceLevel.Warning
                };
                return rule;
            }

            // This is the function that executes for each activity in all the files. Might impact performance.
            // The rule instance is the rule provided above which also contains the user-configured data.
            private static InspectionResult Inspect(IActivityModel activity, Rule ruleInstance)
            {
                var messageList = new List<string>();
                if ((activity.Type.ToLower().Contains("uiautomation")) && (activity.SupportsObjectReferences) && !activity.ObjectReferences.Any())
                {
                    messageList.Add($"The activity ''{activity.DisplayName}'' has been flagged as not using object references.");
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
    }
}