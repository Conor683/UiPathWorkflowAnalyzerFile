A custom workflow analyzer file I made based on UiPath templates. The individual rules are defined in many different cs files that are called in the rulesregister.cs file to be added to the UiPath workflow analyzer at runtime.

When compiled the  project should output a dll file that can be placed in any folder to be picked up by a UiPath Studio instance. To point studio to the folder configure it in the application's settings>Locations>Custom Workflow Analyzer rules location. This will only need to point at the folder and not the specific file.
