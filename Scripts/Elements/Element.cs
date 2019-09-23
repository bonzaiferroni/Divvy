using System;
using System.Collections.Generic;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Bonwerk.Divvy.Elements
{
	
	[RequireComponent(typeof(RectTransform))]
	public abstract class Element : MonoBehaviour, IElement
	{
		public bool IsVisible => Revealer.IsVisible;
		
		public DivElement Parent { get; set; }
		
		public RectTransform Transform { get; private set; }
		public ElementRevealer Revealer { get; private set; }
		public ElementPositioner Positioner { get; private set; }
		public ElementSizer Sizer { get; private set; }
		public bool StyleDirty { get; protected set; }

		public abstract Vector2 ContentSize { get; }
		public abstract ElementStyle ElementStyle { get; }
		
		public bool Expand => ElementStyle.Expand;
		public Spacing Margin => ElementStyle.Margin;
		public Spacing Padding => ElementStyle.Padding;

		public string Name => gameObject.name;
		public string Tag => gameObject.tag;
		public Vector2 Size => Sizer.Target + new Vector2(Margin.Left + Margin.Right, Margin.Top + Margin.Bottom);

		// called once at DivRoot.Awake() or instantiation
		public virtual void Init()
		{
			Transform = GetComponent<RectTransform>();
			Positioner = new DirectPositioner(Transform);
			Sizer = new DirectSizer(Transform);
			Revealer = CreateRevealer();

			Revealer.OnVisibilityChange += OnVisibilityChange;

			Revealer.SetVisibility(ElementStyle.IsVisibleAtStart, true);
			StyleDirty = true;
		}

		// called every frame
		public virtual void Refresh(bool instant)
		{
			if (StyleDirty) ApplyStyle(instant);
			if (IsVisible && Positioner.Transporting) Positioner.Refresh(instant);
			if (Revealer.Transitioning) Revealer.Refresh(instant);
			if (Sizer.Resizing) Sizer.Refresh(instant);
		}

		// called when StyleDirty == true
		protected virtual void ApplyStyle(bool instant)
		{
			StyleDirty = false;
			ApplyStyles.Background(this, ElementStyle);
			ApplyStyles.Font(this, ElementStyle);
			ApplyStyles.Selectable(this, ElementStyle);
		}

		private void OnVisibilityChange(bool isVisible)
		{
			if (!Parent) return;
			Parent.LayoutDirty = true;
		}

		public virtual void FinishTransport()
		{
			Positioner.FinishTransport();
		}

		// called when Parent.LayoutDirty == true
		public void SetPosition(Vector2 position, bool instant)
		{
			var pivot = Transform.pivot;
			var marginX = (1 - pivot.x) * Margin.Left + pivot.x * -Margin.Right;
			var marginY = (1 - pivot.y) * Margin.Bottom + pivot.y * -Margin.Top;
			position += new Vector2(marginX, marginY);
			Positioner.SetTargetPosition(position, instant);
		}

		// called when Parent.LayoutDirty == true
		public virtual void SetSize(bool instant)
		{
			SetSize(ContentSize + new Vector2(Padding.Left + Padding.Right, Padding.Top + Padding.Bottom), instant);
		}

		protected virtual void SetSize(Vector2 size, bool instant)
		{
			if (this is IContentElement e)
			{
				e.Content.SetPadding(Padding);
			}
			Sizer.SetTargetSize(size, instant);
		}

		public void SetPivot(Vector2 pivot)
		{
			Transform.pivot = Transform.anchorMin = Transform.anchorMax = pivot;
		}

		public virtual void ExpandSize(Vector2 size)
		{
			SetSize(size - new Vector2(Margin.Right + Margin.Left, Margin.Top + Margin.Bottom), true);
		}

		private ElementRevealer CreateRevealer()
		{
			switch (ElementStyle.RevealType)
			{
				case RevealType.Instant:
					return new InstantRevealer(Positioner);
				case RevealType.Fade:
					if (this is DivElement) throw new Exception("FadeRevealer cannot be used on DivElement"); 
					return new FadeRevealer(ElementStyle.AnimationTime, GetComponentsInChildren<Graphic>());
				case RevealType.Scale:
					return new ScaleRevealer(ElementStyle.AnimationTime, transform);
				case RevealType.Canvas:
					return new CanvasRevealer(ElementStyle.AnimationTime, GetComponent<CanvasGroup>());
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public enum LayoutType
	{
		Vertical,
		Horizontal,
	}
}
