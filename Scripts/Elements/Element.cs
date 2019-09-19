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
		
		public abstract Spacing Margin { get; }
		public abstract Spacing Padding { get; }
		public abstract bool Expand { get; }

		public string Name => gameObject.name;
		public string Tag => gameObject.tag;

		public virtual float Width
		{
			get => Transform.sizeDelta.x;
			set => Transform.sizeDelta = new Vector2(value, Height);
		}

		public virtual float Height
		{
			get => Transform.sizeDelta.y;
			set => Transform.sizeDelta = new Vector2(Width, value);
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

		public virtual void UpdatePosition(bool instant)
		{
			if (!Parent) Position.TransportSelf(instant);
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

		public virtual void ExpandWidth(float parentWidth)
		{
			Width = parentWidth - (Margin.Right + Margin.Left);
		}

		public virtual void ExpandHeight(float parentHeight)
		{
			Height = parentHeight - (Margin.Top + Margin.Bottom);
		}

		public void SetPivot(Vector2 pivot)
		{
			if (Transform.pivot == pivot) return;
			Transform.pivot = Transform.anchorMin = Transform.anchorMax = pivot;
		}
	}

	public enum LayoutType
	{
		Vertical,
		Horizontal,
		Grid,
	}
}
