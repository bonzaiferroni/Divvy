using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Positioning;
using Bonwerk.Divvy.Styling;
using Bonwerk.Divvy.Visibility;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
	
	[RequireComponent(typeof(RectTransform))]
	public abstract class Element : MonoBehaviour, IElement
	{
		public bool IsVisible => !Visibility || Visibility.IsVisible;
		
		public Div Parent { get; set; }
		
		public DivVisibility Visibility { get; private set; }
		public RectTransform Transform { get; private set; }
		public DivPosition Position { get; private set; }

		public abstract Vector2 ContentSize { get; }
		public abstract ElementStyle ElementStyle { get; }
		public bool Expand => ElementStyle.Expand;
		public Spacing Margin => ElementStyle.Margin;
		public Spacing Padding => ElementStyle.Padding;

		public string Name => gameObject.name;
		public string Tag => gameObject.tag;
		public Vector2 Size => PaddedSize + new Vector2(Margin.Left + Margin.Right, Margin.Top + Margin.Bottom);
		
		public Vector2 PaddedSize
		{
			get => Transform.sizeDelta;
			protected set
			{
				if (value == Transform.sizeDelta) return;
				Transform.sizeDelta = value;
			}
		}

		public virtual void Init()
		{
			Visibility = GetComponent<DivVisibility>();
			Transform = GetComponent<RectTransform>();
			Position = new DivAnimatedPosition(Transform);
			
			if (Visibility)
			{
				Visibility.OnVisibilityChange += OnVisibilityChange;
				Visibility.Init();
			}
		}

		public virtual void Refresh(bool instant)
		{
			Position.Refresh(instant);
		}

		public virtual void SetSize(bool instant)
		{
			PaddedSize = ContentSize + new Vector2(Padding.Left + Padding.Right, Padding.Top + Padding.Bottom);
		}

		private void OnVisibilityChange(bool isVisible)
		{
			if (Parent == null) return;
			Parent.IsDirty = true;
		}

		public virtual void FinishTransport()
		{
			Position.FinishTransport();
		}

		public void SetPosition(Vector2 position, bool instant)
		{
			var pivot = Transform.pivot;
			var marginX = (1 - pivot.x) * Margin.Left + pivot.x * -Margin.Right;
			var marginY = (1 - pivot.y) * Margin.Bottom + pivot.y * -Margin.Top;
			position += new Vector2(marginX, marginY);
			Position.SetTargetPosition(position, instant);
		}

		public void SetPivot(Vector2 pivot)
		{
			Transform.pivot = Transform.anchorMin = Transform.anchorMax = pivot;
		}

		public virtual void ExpandSize(Vector2 size)
		{
			PaddedSize = size - new Vector2(Margin.Right + Margin.Left, Margin.Top + Margin.Bottom);
		}
	}

	public enum LayoutType
	{
		Vertical,
		Horizontal,
	}
}
