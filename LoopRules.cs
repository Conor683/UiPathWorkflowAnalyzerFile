using System.Diagnostics;
using UiPath.Studio.Activities.Api;
using UiPath.Studio.Activities.Api.Analyzer;
using UiPath.Studio.Activities.Api.Analyzer.Rules;
using UiPath.Studio.Analyzer.Models;

namespace WorkflowAnalyzerTST
{
    public class LoopRules 
    { 

        // This static class is not mandatory. It just helps organizining the code.
        internal static class MaxIterationsRule
        {
            // This should be as unique as possible, and should follow the naming convention.
            private const string RuleId = "SG-LOO-001";
            internal static Rule<IActivityModel> Get()
            {
                var rule = new Rule<IActivityModel>("Max Iterations Empty", RuleId, Inspect)
                {
                    RecommendationMessage = "Populate ''Max Iterations'' properties to avoid infinite loops.",
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
                if (activity.ToolboxName.ToLower().Contains("while"))
                {
                    foreach (IArgumentModel property in activity.Arguments)
                    {
                        if (property.DisplayName == "Max Iterations" && property.DefinedExpression == null)
                            messageList.Add($"The activity ''{activity.DisplayName}'' has been flagged as having an empty max iterations property.");
                    }                    
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