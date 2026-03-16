using FastFrame.CodeGeneration;
using FastFrame.CodeGeneration.Extensions;
using FastFrame.CodeGeneration.Schema;
using FastFrame.CodeGenerator;
using FastFrame.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.Areas.Admin.Controllers
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    [Description("代码生成器")]
    public class CodeGenerationController : AdminApiController
    {
        private readonly ModuleMetadata _module;
        private readonly EntityMetadata _entity;
        private readonly ICodeGenerator _generator;
        private readonly ITypeMetadataHandler _typeMetadataHandler;


        public CodeGenerationController(ITypeMetadataHandler typeMetadataHandler)
        {
            _typeMetadataHandler = typeMetadataHandler;
            _generator = new RazorCodeGenerator();
            ProjectMetadata project = new ProjectMetadata()
            {
                Name = "专科数据中心",
                Company = "麦迪科技",
                Copyright = "Copyright (c) 2014-2019 FastFrame. All rights reserved.",
                Creator = "陈明明",
                NamespacePrefix = "DoCare.Core.EntityConfiguration"
            };
            _module = new ModuleMetadata()
            {
                Name = "Patient",
                Display = "患者信息",
                Project = project
            };
        }

        /// <summary>
        /// 实体配置生成器
        /// </summary>
        /// <returns>菜单信息</returns>
        [HttpGet]
        public async Task<CodeFile> EntityConfiguration(string entityFullName)
        {
            Check.NotNullOrEmpty(entityFullName, nameof(entityFullName));
            var typeMetadatas = _typeMetadataHandler.GetEntityTypeMetadatas();

            var entityMetata = typeMetadatas.ToList().FirstOrDefault(a => a.FullName == entityFullName)?.ToEntityMetadata();
            CodeFile code = _generator.GenerateEntityConfiguration(entityMetata);
            return code;
        }
    }
}