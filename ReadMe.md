# Divvy Library

## Elements

### Concrete
|Class|Extends|Interfaces|
|---|---|---|
|[DivElement](Scripts/Elements/DivElement.cs)|Element|IBackgroundElement|
|[TextElement](Scripts/Elements/TextElement.cs)|BackgroundElement|IBackgroundElement, IContentElement, IFontElement|
|[ImageElement](Scripts/Elements/ImageElement.cs)|BackgroundElement|IBackgroundElement, IContentElement|
|[InputElement](Scripts/Elements/InputElement.cs)|BackgroundElement|IBackgroundElement, IContentElement, ISelectableElement|
|[SpriteButton](Scripts/Elements/SpriteButton.cs)|ButtonElement|IBackgroundElement, IContentElement, ISelectableElement|
|[TextButton](Scripts/Elements/TextButton.cs)|ButtonElement|IBackgroundElement, IContentElement, ISelectableElement, IFontElement|

### Abstract
|Class|Extends|Interfaces|
|---|---|---|
|[Element](Scripts/Elements/Element.cs)||IElement|
|[BackgroundElement](Scripts/Elements/BackgroundElement.cs)|Element|IBackgroundElement|
|[ButtonElement](Scripts/Elements/ButtonElement.cs)|BackgroundElement|IBackgroundElement|

### Interfaces
|Interface|Extends|
|---|---|
|[IElement](Scripts/Elements/IElement.cs)||
|[IBackgroundElement](Scripts/Elements/Interfaces.cs)|IElement|
|[IFontElement](Scripts/Elements/Interfaces.cs)|IElement|
|[ISelectableElement](Scripts/Elements/Interfaces.cs)|IElement|
|[IContentElement](Scripts/Elements/Interfaces.cs)|IElement|

![Diagram](http://yuml.me/7274ec0d.png)
[Edit](http://yuml.me/edit/7274ec0d)
