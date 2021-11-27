using Luban.Job.Cfg.Defs;
using Luban.Job.Common.Generate;
using Luban.Job.Common.Utils;
using System.Collections.Generic;

namespace Luban.Job.Cfg.Generate
{
    [Render("code_python_json")]
    [Render("code_python3_json")]
    class Python3CodeJsonRender : TemplateCodeRenderBase
    {
        protected override string CommonRenderTemplateDir => "python";

        protected override string RenderTemplateDir => "python_json";

        public override void Render(GenContext ctx)
        {
            ctx.Render = this;
            ctx.Lan = Common.ELanguage.PYTHON;
            DefAssembly.LocalAssebmly.CurrentLanguage = ctx.Lan;

            var lines = new List<string>(10000);
            static void PreContent(List<string> fileContent)
            {
                //fileContent.Add(PythonStringTemplates.ImportTython3Enum);
                //fileContent.Add(PythonStringTemplates.PythonVectorTypes);
                fileContent.Add(StringTemplateUtil.GetTemplateString("config/python_json/include"));
            }

            GenerateCodeMonolithic(ctx, RenderFileUtil.GetFileOrDefault(ctx.GenArgs.OutputCodeMonolithicFile, "Types.py"), lines, PreContent, null);
        }
    }
}
