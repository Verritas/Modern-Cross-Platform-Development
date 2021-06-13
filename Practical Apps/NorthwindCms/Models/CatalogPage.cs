using Piranha.AttributeBuilder;
using Piranha.Models;

namespace NorthwindCms.Models {
    [PageType(Title = "Catalog page", UseBlocks = false)]
    //[PageTypeRouteAttribute (Title = "Default", Route = "/catalog")]
    public class CatalogPage : Page<CatalogPage> {

    }
}