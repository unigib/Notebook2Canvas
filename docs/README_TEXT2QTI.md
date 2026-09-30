Bundling text2qti with Notebook2Canvas

This application can use a bundled text2qti executable shipped with the app. Place the converter executable in one of the following locations relative to the app directory:

1. ./tools/text2qti/text2qti.exe
2. ./tools/text2qti.exe

When the Export → QTI button is clicked the app will prefer a bundled executable if present; otherwise it falls back to calling `text2qti` on the system PATH.

Notes:
- If you use a Python-based text2qti (python -m text2qti) you can create a small wrapper executable (e.g., with PyInstaller) named `text2qti.exe` and place it in the tools folder.
- The app does not ship the converter binary. You must add the executable to the repo/package distribution yourself.
- For unattended installs, include the tools folder alongside the app and set its files to be copied next to the executable.

Suggested packaging steps:
- Create a distribution folder: dist/Notebook2Canvas/
- Copy Notebook2Canvas.exe and related DLLs
- Copy tools/text2qti/text2qti.exe into dist/tools/text2qti/
- Ship the dist folder or create an installer that places files accordingly.

If you want, I can add a preferences UI to let users point to the converter executable instead of relying on the default locations. Reply "add preferences" to proceed.