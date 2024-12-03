using System;
using System.Reflection;

namespace DotNetBatch14HZYK.RestApiDotNetFramework.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}