using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

namespace WorkflowAnalyzerRules
{
    public class Logs
    {

        // This static class is not mandatory. It just helps organizining the code.
        internal static class OutputLogMessages
        {
            // This should be as unique as possible, and should follow the naming convention.
            private const string RuleId = "SG-LOG-001";
            internal static Rule<IActivityModel> Get()
            {
                var rule = new Rule<IActivityModel>("Log Message", RuleId, Inspect)
                {
                    RecommendationMessage = "The contents of a log message.",
                    /// Off and Verbose are not supported.
                    ErrorLevel = System.Diagnostics.TraceLevel.Info,
                };
                return rule;
            }

            // This is the function that executes for each activity in all the files. Might impact performance.
            // The rule instance is the rule provided above which also contains the user-configured data.
            private static InspectionResult Inspect(IActivityModel activity, Rule ruleInstance)
            {
                var messageList = new List<string>();
                if ((activity.ToolboxName.ToLower().Contains("logmessage")))
                {
                    messageList.Add($"{activity.DisplayName} | {activity.Arguments.First().DefinedExpression}");
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