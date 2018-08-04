using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Divvy.Core
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class DivvyPanel : MonoBehaviour
	{
		[SerializeField] private bool _expandSelf;
		
		protected bool Initialized;
		
		private float _posRef;
		private Vector2 _position;

		public DivvyVisibility Visibility { get; private set; }
		public bool IsVisible { get; private set; }
		public bool Transported { get; private set; }
		public RectTransform Rect { get; private set; }
		public Vector2 TargetPosition { get; private set; }
		
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

		public Vector2 Position
		{
			get { return Rect.anchoredPosition; }
			private set { Rect.anchoredPosition = value; }
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
			
			Rect.pivot = Rect.anchorMin = Rect.anchorMax = new Vector2(0, 1);
			Transported = false;
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
			TransportSelf(instant);
		}

		private void OnVisibilityChange(bool isVisible)
		{
			if (isVisible == IsVisible) return;
			IsVisible = isVisible;
			if (Parent == null) return;
			Parent.ChildrenPositioned = false;
		}

		public void SetTargetPosition(Vector2 position, bool instant)
		{
			if (position == TargetPosition) return;
			TargetPosition = position;
				
			if (!instant)
			{
				Transported = false;
			}
			else
			{
				// quick transport
				Position = TargetPosition;
				Transported = true;
			}
		}

		// TransportSelf

		private void TransportSelf(bool instant)
		{
			if (Transported || !Parent) return;

			if (!instant)
			{
				if (Math.Abs(Position.x - TargetPosition.x) > .00001f)
				{
					var x = Mathf.SmoothDamp(Position.x, TargetPosition.x, ref _posRef, .2f);
					Position = new Vector2(x, Position.y);
					return;
				}
			
				if (Math.Abs(Position.y - TargetPosition.y) > .00001f)
				{
					var y = Mathf.SmoothDamp(Position.y, TargetPosition.y, ref _posRef, .2f);
					Position = new Vector2(Position.x, y);
					return;
				}
			}

			FinishTransport();
		}

		public virtual void FinishTransport()
		{
			Position = TargetPosition;
			Transported = true;
		}
	}

	public enum LayoutStyle
	{
		Vertical,
		Horizontal,
		Grid,
	}
}
