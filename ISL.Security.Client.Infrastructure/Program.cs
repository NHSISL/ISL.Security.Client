// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using ISL.Security.Client.Infrastructure.Services;

namespace ISL.Security.Client.Infrastructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();

            scriptGenerationService.GenerateBuildScript(
                branchName: "main",
                projectName: "ISL.Security.Client",
                dotNetVersion: "9.0.100");

            scriptGenerationService.GeneratePrLintScript("main");
        }
    }
}
