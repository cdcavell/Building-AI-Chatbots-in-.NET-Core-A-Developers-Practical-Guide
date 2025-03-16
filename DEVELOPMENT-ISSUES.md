# 🛠️ Issues Encountered During Development
### This file covers issues that were encountered during the development and deployment of the .NET Core MVC application. The issues are categorized based on the development environment, application configuration, and deployment settings.

<hr />

## ⚙️ Visual Studio Debugger Shows Framework Exceptions on Startup

When running the **AI.Chatbot** MVC application in Visual Studio, you may notice that exceptions like `System.IO.IOException` or other internal .NET/ASP.NET Core exceptions are displayed in the debugger during application startup.
These exceptions are **handled internally by the .NET framework** and **do not indicate actual issues with the application**.

✅ Cause

This behavior occurs when **"Just My Code"** is **disabled** in Visual Studio, causing the debugger to break or display exceptions for internal framework operations.

🧰 Solution

To avoid seeing these handled exceptions:
1. Open **Visual Studio**.
2. Go to `Tools` → `Options` → `Debugging` → `General`.
3. **Enable** the checkbox labeled **"Enable Just My Code"**.
4. Click **OK** to save the settings.

> 📖 Refer to official Microsoft documentation for more details:
>
>[Just My Code - Visual Studio](https://learn.microsoft.com/en-us/visualstudio/debugger/just-my-code?view=vs-2022)

Once enabled, the debugger will **only break on exceptions in your own code**, making the debugging experience much clearer.

🤖 Note

_Resolution of this issue was assisted using **[GitHub Copilot in Visual Studio](https://visualstudio.microsoft.com/github-copilot/)**, which provided contextual insights and recommendations during troubleshooting._

<hr />