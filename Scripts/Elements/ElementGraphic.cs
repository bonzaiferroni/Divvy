using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
    public class ElementGraphic
    {
        public ElementGraphic(Graphic graphic, IGraphicStyle style)
        {
            Graphic = graphic;
            Style = style;
        }

        public Graphic Graphic { get; }
        public IGraphicStyle Style { get; }
    }
    
    public class GraphicList : List<ElementGraphic> { }
}