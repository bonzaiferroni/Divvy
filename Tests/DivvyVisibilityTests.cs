using System.Collections;
using Bonwerk.Divvy.Elements;
using DivLib.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Divvy.Tests
{
	public class DivvyVisibilityTests
	{
		
		[Test]
		public void SetVisibility_False_SetsIsVisibleToFalse()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<ElementRevealer>();
			visibility.SetVisibility(false);
			Assert.IsFalse(visibility.IsVisible);
		}
		
		[Test]
		public void SetVisibility_True_SetsIsVisibleToTrue()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<ElementRevealer>();
			visibility.SetVisibility(false);
			visibility.SetVisibility(true);
			Assert.IsTrue(visibility.IsVisible);
		}
	}
}
