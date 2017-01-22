using FurnitureStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStore.Abstract
{
    public interface IFurnitureRepo
    {
        IEnumerable<Furniture> Furnitures { get; }
        IEnumerable<Category> Categories { get; }
   

        Furniture Details(int id);
        Category BrowseResult(string genre);
        void SaveCategory(Category category , bool updateImage = false);
        void SaveFurniture(Furniture furniture , bool updateImage = false);
        Category DeleteCategory(int categoryId);
        Furniture DeleteFurniture(int furnitureId);

        Furniture FindById(int? id);
    }
}
