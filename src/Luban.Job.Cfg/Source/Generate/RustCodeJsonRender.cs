﻿using Luban.Job.Cfg.Defs;
using Luban.Job.Common.Defs;
using Luban.Job.Common.Generate;
using Luban.Job.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luban.Job.Cfg.Generate
{
    [Render("code_rust_json")]
    class RustCodeJsonRender : TemplateCodeRenderBase
    {
        protected override string CommonRenderTemplateDir => "rust";

        protected override string RenderTemplateDir => "rust_json";

        public override void Render(GenContext ctx)
        {
            string genType = ctx.GenType;
            var args = ctx.GenArgs;
            ctx.Render = this;
            ctx.Lan = RenderFileUtil.GetLanguage(genType);
            DefAssembly.LocalAssebmly.CurrentLanguage = ctx.Lan;

            var lines = new List<string>();
            GenerateCodeMonolithic(ctx, RenderFileUtil.GetFileOrDefault(ctx.GenArgs.OutputCodeMonolithicFile, "mod.rs"), lines, ls =>
            {
                var template = StringTemplateUtil.GetTemplate("config/rust_json/include");
                var result = template.RenderCode(ctx.ExportTypes);
                ls.Add(result);
            }, null);
        }
    }
}
