
# NotebookLM to Canvas


## Overview 
This is a simple desktop application that converts the JSON output from the NotebookLM Exportkit app here: 

https://notebooklmexportkit.netlify.app/ 

Using Google NotebookLM is great for students and teachers. https://notebooklm.google.com/

It can really help with study and learning. Its quizzes are really cool. Your own ChatGPT with your own study material.

I liked the idea of being able to import quizzes from NotebookLM - but of course that doesnt work.  It cant be easily exported.

ExportKit app does exactly what you need - apart from the fact it does not currently support Canvas QTI import format. You also need to pay a sub but its well worth it.

So this tool converts to a Canvas friendly markdown format that can be imported as a quiz assignment.

## Importing to Canvas!

I played around with this import from other tools and converters and found the only one I could get working was this one:

https://pypi.org/project/text2qti/ 

which is a nice python app - both gui and command line.

## Workflow

So my workflow is now:

1. Do a quiz on NotebookLM 
2. Use the ExportKit to export to a JSON file
3. Use my tool here to convert that to an markdown file
4. Use the text2qti GUI tool to create a QTI zip file
5. import to Canvas and then use build to tidy it all up.


30 questions in say 10 minutes - perfect for regular assignments to check course/module source materials are being read and understood.

## How to use 

### Export Kit Extension - install

Install the ExportKit Extension - if you dont want to pay you have some free credits 

### Now create the quiz in NotebookLM

<img width="683" height="359" alt="image" src="https://github.com/user-attachments/assets/53b50349-614c-4813-adc7-4edfa903c6d0" />

### Generate JSON with extension

<img width="851" height="464" alt="image" src="https://github.com/user-attachments/assets/d85d7a2c-76f2-494a-a9f7-62e78240a358" />

### Use this tool to upload the JSON file and create the quiz markdown compatable file 

You can also edit the MD file if you want to add or change anything before create the MD file.

<img width="874" height="398" alt="image" src="https://github.com/user-attachments/assets/e2564ef3-4d4b-4a92-ac96-046249120514" />

### Now use the Text2QTI tool GUI

This is an exe file in this release but the original is also here:

https://github.com/gpoore/text2qti


<img width="1054" height="730" alt="image" src="https://github.com/user-attachments/assets/cbe70e9a-ee3a-4263-9856-ff68b85bc982" />

### Finally upload to Canvas 

<img width="1178" height="700" alt="image" src="https://github.com/user-attachments/assets/4cd2625e-9fff-444d-bc35-167caa3f9d0a" />

Easy to do and easy to create quality quizzes.


## Installation

I created an installer package - its in the release folder and is called publish.zip.  You should be able to download that, unzip it and then use the setup.exe - since its a .NET app. If that doesnt work you can always install Visual Studio 2026 and install and run it yourself.












## Acknowledgements

 - [NotebookLM ExportKit](https://notebooklmexportkit.netlify.app/)
 - [Text2QTI](https://github.com/gpoore/text2qti)
 



