using Bonwerk.Divvy.Core;
using Bonwerk.Divvy.Data;
using Bonwerk.Divvy.Positioning;
using Bonwerk.Divvy.Visibility;
using UnityEngine;

namespace Bonwerk.Divvy.Elements
{
	
	[RequireComponent(typeof(RectTransform))]
	public class Element : MonoBehaviour, IElement
	{
		[Header("Element")]
		[SerializeField] private bool _expandSelf;
		[SerializeField] private Spacing _margin;

		public bool IsVisible => !Visibility || Visibility.IsVisible;
		
		public Div Parent { get; set; }
		public DivVisibility Visibility { get; private set; }
		public RectTransform Transform { get; private set; }
		public DivPosition Position { get; private set; }

		public string Name => gameObject.name;
		
		public Spacing Margin
		{
			get => _margin;
			set => _margin = value;
		}
		
		public bool Expand
		{
			get => _expandSelf;
			set => _expandSelf = value;
		}
		
		public virtual float Width
		{
			get => Transform.sizeDelta.x;
			set => Transform.sizeDelta = new Vector2(value, Transform.sizeDelta.y);
		}

		public virtual float Height
		{
			get => Transform.sizeDelta.y;
			set => Transform.sizeDelta = new Vector2(Transform.sizeDelta.x, value);
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

		public void SetPivot(Vector2 childPivot)
		{
			Transform.pivot = Transform.anchorMin = Transform.anchorMax = childPivot;
		}
	}

	public enum LayoutStyle
	{
		Vertical,
		Horizontal,
		Grid,
	}
}
