# 01-sanitize-quiz-markdown: Sanitize quiz markdown and remove bracketed instruction lines

Add a small sanitizer to Notebook2Canvas/JsonToTextConverter.cs that removes or normalizes standalone bracketed instruction lines such as "[Select all that apply]" before the text is output to the text2qti pipeline.

**Done when**: The JsonToTextConverter produces output that no longer contains standalone bracketed instruction lines causing the text2qti parser error; solution builds without new warnings in modified files.
