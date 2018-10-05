using UnityEngine;

namespace DivLib.Core
{
	
	[RequireComponent(typeof(RectTransform))]
	public class Element : MonoBehaviour
	{
		[SerializeField] private bool _expandSelf;
		public Spacing Margin;
		
		public DivVisibility Visibility { get; private set; }
		public bool IsVisible { get; private set; }
		public RectTransform Rect { get; private set; }
		public DivPosition Position { get; private set; }
		
		public Div Parent { get; set; }
		
		public bool Initialized { get; private set; }

		public bool ExpandSelf
		{
			get { return _expandSelf; }
			set { _expandSelf = value; }
		}
		
		public virtual float Width
		{
			get { return Rect.sizeDelta.x; }
			set { Rect.sizeDelta = new Vector2(value, Rect.sizeDelta.y); }
		}

		public virtual float Height
		{
			get { return Rect.sizeDelta.y; }
			set { Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, value); }
		}

		internal virtual void Init()
		{
			Visibility = GetComponent<DivVisibility>();
			if (Visibility)
			{
				IsVisible = Visibility.IsVisible;
				Visibility.OnVisibilityChange += OnVisibilityChange;
				Visibility.Init();
			}
			else
			{
				IsVisible = true;
			}

			Rect = GetComponent<RectTransform>();
			Position = new DivAnimatedPosition();
			Position.Init(Rect);
			Initialized = true;
		}

		public virtual void UpdatePosition(bool instant)
		{
			if (Parent != null) Position.TransportSelf(instant);
		}

		private void OnVisibilityChange(bool isVisible)
		{
			if (isVisible == IsVisible) return;
			IsVisible = isVisible;
			if (Parent == null) return;
			Parent.ChildrenPositioned = false;
		}

		public virtual void FinishTransport()
		{
			Position.FinishTransport();
		}

		public virtual void ExpandWidth(float width)
		{
			// not sure if this is correct
			Width = width - (Margin.Right + Margin.Left);
		}

		public void SetPivot(Vector2 childPivot)
		{
			Rect.pivot = Rect.anchorMin = Rect.anchorMax = childPivot;
		}
	}

	public enum LayoutStyle
	{
		Vertical,
		Horizontal,
		Grid,
	}
}
