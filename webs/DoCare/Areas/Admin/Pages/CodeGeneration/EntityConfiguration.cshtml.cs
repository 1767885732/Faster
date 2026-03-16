using FastFrame.CodeGeneration;
using FastFrame.CodeGeneration.Extensions;
using FastFrame.CodeGeneration.Schema;
using FastFrame.CodeGenerator;
using FastFrame.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace DoCare.Admin.Pages.CodeGeneration
{
    public class EntityConfigurationModel : PageModel
    {
        private readonly ITypeMetadataHandler typeMetadataHandler;
        private readonly ICodeGenerator generator;

        public EntityConfigurationModel(ITypeMetadataHandler typeMetadataHandler, ICodeGenerator generator)
        {
            this.typeMetadataHandler = typeMetadataHandler;
            this.generator = generator;
        }

        public CodeFile CodeFile { get; set; }

        public void OnGet(string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            var typeMetadatas = typeMetadataHandler.GetEntityTypeMetadatas();

            var entityMetata = typeMetadatas.ToList().FirstOrDefault(a => a.FullName == name)?.ToEntityMetadata();
            CodeFile = generator.GenerateEntityConfiguration(entityMetata);
        }
    }
}