# .NET Version Upgrade — Notebook2Canvas Plan

## Overview

**Target**: Upgrade Notebook2Canvas solution from .NET Framework 4.7.2 to net10.0 (.NET 10 LTS).
**Scope**: Single WinForms project (Notebook2Canvas) that targets .NET Framework 4.7.2; small-to-medium codebase with a few UI forms and a JSON-to-text converter used for quiz generation.

## Tasks

### 01-sanitize-quiz-markdown: Sanitize quiz markdown and remove bracketed instruction lines

Add a small sanitizer to Notebook2Canvas/JsonToTextConverter.cs that removes or normalizes standalone bracketed instruction lines such as "[Select all that apply]" before the text is output to the text2qti pipeline.

**Done when**: The JsonToTextConverter produces output that no longer contains standalone bracketed instruction lines causing the text2qti parser error; solution builds without new warnings in modified files.

---
