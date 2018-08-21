using System.Collections.Generic;
using FusionLib.Core;

namespace DivLib.Core
{
    public class DisplayDivRecipes : DisplayRecipes
    {
        public override List<Fusion> Recipes
        {
            get
            {
                var list = new List<Fusion>();
                list.Add(Fusion.Create("Label", new DivRecipe().LabelParts));
                list.Add(Fusion.Create("Input", new DivRecipe().InputParts));
                list.Add(Fusion.Create("Button", new DivRecipe().ButtonParts));
                list.Add(Fusion.Create("LabelButton", new DivRecipe().LabelButtonParts));
                return list;
            }
        }
    }
}