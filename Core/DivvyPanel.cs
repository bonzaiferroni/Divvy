using UnityEngine;

namespace Divvy.Core
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class DivvyPanel : MonoBehaviour
	{
		[SerializeField] private bool _expandSelf;
		public Spacing Margin;
		
		protected bool Initialized;

		public DivvyVisibility Visibility { get; private set; }
		public bool IsVisible { get; private set; }
		public RectTransform Rect { get; private set; }
		public DivvyPosition Position { get; private set; }
		
		public DivvyParent Parent { get; set; }

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

		private void Start()
		{
			if (!Initialized) Init();
		}

		public virtual void Init()
		{
			
			Visibility = GetComponent<DivvyVisibility>();
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
			Position = new DivvyAnimatedPosition();
			Position.Init(Rect);
			
			Rect.pivot = Rect.anchorMin = Rect.anchorMax = new Vector2(0, 1);
			Initialized = true;
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				Init();
			}
#endif
			if (Parent == null) UpdatePosition(!Application.isPlaying);
		}

		public virtual void UpdatePosition(bool instant)
		{
			Position.TransportSelf(instant);
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
	}

	public enum LayoutStyle
	{
		Vertical,
		Horizontal,
		Grid,
	}
}
