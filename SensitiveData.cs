using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

namespace WorkflowAnalyzerRules
{
    internal class SensitiveDataRules
    {
        internal static class ScreenshotActivitiesRule
        {
            // This should be as unique as possible, and should follow the naming convention.
            private const string RuleId = "CM-SCS-001";
            internal static Rule<IActivityModel> Get()
            {
                var rule = new Rule<IActivityModel>("Screenshotting Activities Should be Removed", RuleId, Inspect)
                {
                    RecommendationMessage = "Remove activity to avoid capturing sensitive data.",
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
                if ((activity.Type.ToLower().Contains("uiautomation")) && ((activity.ToolboxName.ToLower().Contains("screenshot")) | (activity.ToolboxName.ToLower().Contains("saveimage"))))
                {
                    messageList.Add($"The activity ''{activity.DisplayName}'' has been flagged as potentially saving PII.");
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
