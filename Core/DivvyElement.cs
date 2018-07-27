using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Divvy.Core
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class DivvyElement : MonoBehaviour
	{
		protected bool Initialized;
		
		private DivvyVisibility _visibility;
		private Vector2 _targetPosition;
		private float _posRef;
		private Vector2 _position;

		public bool IsVisible { get; private set; }
		public bool Transported { get; set; }
		public RectTransform Rect { get; private set; }
		
		public DivvyParent Parent { get; set; }
		
		public virtual float Width
		{
			get { return Rect.sizeDelta.x; }
			protected set { Rect.sizeDelta = new Vector2(value, Rect.sizeDelta.y); }
		}

		public virtual float Height
		{
			get { return Rect.sizeDelta.y; }
			protected set { Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, value); }
		}
		
		public Vector2 TargetPosition
		{
			get { return _targetPosition; }
			set
			{
				if (value == _targetPosition) return;
				_targetPosition = value;
				
				// quick transport
				if (Application.isPlaying && (!_visibility || _visibility.CurrentVisibility > .2f))
				{
					Transported = false;
				}
				else
				{
					Position = TargetPosition;
					Transported = true;
				}
			}
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
			_visibility = GetComponent<DivvyVisibility>();
			if (_visibility)
			{
				IsVisible = _visibility.IsVisible;
				_visibility.OnVisibilityChange += OnVisibilityChange;
				_visibility.Init();
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
		
		protected virtual void Update()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				Init();
			}
#endif
			TransportSelf();
		}

		private void OnVisibilityChange(bool isVisible)
		{
			if (isVisible == IsVisible) return;
			IsVisible = isVisible;
			if (Parent == null) return;
			Parent.ChildrenPositioned = false;
		}

		// TransportSelf

		private void TransportSelf()
		{
			if (Transported) return;
			
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

			FinishTransport();
		}

		public void FinishTransport()
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
