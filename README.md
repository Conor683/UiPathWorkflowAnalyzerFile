# UiPathWorkflowAnalyzerFile
A custom workflow analyzer file I made based on UiPath templates. The individual rules are defined in many different cs files that are called in the rulesregister.cs file to be added to the UiPath workflow analyzer at runtime. Customly defined workflow analyzer rules enable you to enforce your organization's uipath coding standards more effectively and can even prevent projects with organizationally unacceptable code from moving into production if the right settings are enabled in automation ops. You can also add counters but they're not as important and function similarly enough to rules that you'll be able to figure them out.

The goal of this project is that it can eventually be used as a template to understand how to effectively implement new rules and to speed up the development process.

## Getting Started
In order to develop using the UiPath studio API you will need to install the "UiPath.Activities.API" Nuget package in your IDE. To do this in Visual Sudio 2022 click on project in the top nav bar and go to manage nuget packages.

![image](https://github.com/user-attachments/assets/50facd80-9f4e-45d5-b4a2-42f13d4d030a)

Then add the uipath package source to your IDE by clicking the settings cog next to package source.

![image](https://github.com/user-attachments/assets/759545d9-7afc-47fc-ad7c-8861c8ace49d)

Add the following if it's not already present and click "OK"

![image](https://github.com/user-attachments/assets/762b5a22-95ae-4fec-ab50-d07094821bc3)

Once this is done you can change the package source to the uipath official and simply search for and install the passage.

![image](https://github.com/user-attachments/assets/66c2b8c8-d9a2-47b1-983e-ab385fa527bf)

You are now ready to begin working on your custom workflow analyzer rules file, this package is also the required package for building your own custom UiPath activities.

## Adding a rule

The first thing you need to do in order to add a rule is import the correct studio api classes, the classes you may need to import are:
  - using UiPath.Studio.Activities.Api;
  - using UiPath.Studio.Activities.Api.Analyzer;
  - using UiPath.Studio.Activities.Api.Analyzer.Rules;
  - using UiPath.Studio.Analyzer.Models;

Once you've done this you simply need to add the code for your rule to the C# code file you're working from, an example of the code for a rule that can detect classic activities being used in a project is below.

![image](https://github.com/user-attachments/assets/7d226d3e-8f9a-47a8-a7ba-1a2f9fb71e4d)

Finally the rule must be added to the register like so:

![image](https://github.com/user-attachments/assets/6dfd79be-4aba-4e75-a99f-a5aebdad06f7)

You can see which features you need by peaking at the definition of the methods and classes you've used from the studio API.

## Using the project

When compiled the project should output a dll file that can be placed in any folder to be picked up by a UiPath Studio instance. To point studio to the folder configure it in the application's settings page the navigation is as follows **Settings>Locations>Custom Workflow Analyzer rules location**.

![image](https://github.com/user-attachments/assets/14d551eb-083d-4b29-8766-258bd1a50e1c)

This will only need to be configured to point at a folder and not the specific file. The dll files in this folder will now be picked up by studio at runtime and the rules contained in them should appear in the analyzer like this.

![image](https://github.com/user-attachments/assets/c31a78a1-e124-4272-9cba-ed911a976513)


## Resources used to understand rule building
- https://docs.uipath.com/studio/standalone/2023.10/user-guide/about-workflow-analyzer
- https://docs.uipath.com/sdk/other/latest/developer-guide/building-workflow-analyzer-rules
- https://www.youtube.com/watch?v=H2jSYbhe2tE&t=2224s
- https://www.youtube.com/watch?v=POloo1gt5K8

## Who Maintains This Project
-Me
