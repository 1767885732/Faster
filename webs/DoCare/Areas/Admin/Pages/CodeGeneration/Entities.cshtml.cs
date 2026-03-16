using FastFrame.CodeGenerator;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoCare.Admin.Areas.CodeGeneration.Pages
{
    public class EntitiesModel : PageModel
    {
        private readonly ITypeMetadataHandler typeMetadataHandler;

        public EntitiesModel(ITypeMetadataHandler typeMetadataHandler)
        {
            this.typeMetadataHandler = typeMetadataHandler;
        }

        public TypeMetadata[] EntityTypes { get; set; }

        public void OnGet()
        {
            EntityTypes = typeMetadataHandler.GetEntityTypeMetadatas();
        }
    }
}