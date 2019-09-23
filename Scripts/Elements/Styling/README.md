# Styles
[Home](../../README.md), 
[Elements](../Elements/README.md) 

## Static
|Class|Methods|
|---|---|
|[ApplyStyles](ApplyStyles.cs)|`void Background(Element e, ElementStyle s)`<br>`void Font(Element e, ElementStyle s)`<br>`void Selectable(Element e, ElementStyle s)`

## Concrete
|Class|Extends|Interfaces|
|---|---|---|
|[DivStyle](DivStyle.cs)|BackgroundStyle||
|[ImageStyle](ImageStyle.cs)|BackgroundStyle||
|[InputStyle](InputStyle.cs)|BackgroundStyle|ISelectableStyle|
|[SpriteButtonStyle](SpriteButtonStyle.cs)|ButtonStyle||
|[TextButtonStyle](TextButtonStyle.cs)|ButtonStyle|IFontStyle|
|[TextStyle](TextStyle.cs)|FontStyle||

## Abstract
|Class|Extends|Interfaces|
|---|---|---|
|[ElementStyle](ElementStyle.cs)|ScriptableObject||
|[BackgroundStyle](BackgroundStyle.cs)|ElementStyle|IBackgroundStyle|
|[ButtonStyle](ButtonStyle.cs)|BackgroundStyle|ISelectableStyle|
|[FontStyle](FontStyle.cs)|BackgroundStyle|IFontStyle|

## Interfaces
|Interface|Extends|
|---|---|
|[IElementStyle](Interfaces.cs)||
|[IBackgroundStyle](Interfaces.cs)|IElementStyle|
|[IFontStyle](Interfaces.cs)|IFontStyle|
|[ISelectableStyle](Interfaces.cs)|ISelectableStyle|