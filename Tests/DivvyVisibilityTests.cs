using System.Collections;
using Divvy.Core;
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
			var visibility = data.RootObject.GetComponentInChildren<DivvyVisibility>();
			visibility.SetVisibility(false);
			Assert.IsFalse(visibility.IsVisible);
		}
		
		[Test]
		public void SetVisibility_True_SetsIsVisibleToTrue()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<DivvyVisibility>();
			visibility.SetVisibility(false);
			visibility.SetVisibility(true);
			Assert.IsTrue(visibility.IsVisible);
		}
		
		[Test]
		public void SetVisibility_False_SetsTargetVisibilityTo0()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<DivvyAnimatedVisibility>();
			visibility.SetVisibility(false);
			Assert.AreEqual(0, visibility.TargetVisibility);
		}
		
		[Test]
		public void SetVisibility_True_SetsTargetVisibilityTo1()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<DivvyAnimatedVisibility>();
			visibility.SetVisibility(false);
			visibility.SetVisibility(true);
			Assert.AreEqual(1, visibility.TargetVisibility);
		}
		
		[Test]
		public void SetVisibility_False_True_SetsCurrentVisibilityTo0()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<DivvyAnimatedVisibility>();
			visibility.SetVisibility(false, true);
			Assert.AreEqual(0, visibility.CurrentVisibility);
		}
		
		[Test]
		public void SetVisibility_True_True_SetsTargetVisibilityTo1()
		{
			var data = new DivvyData();
			var visibility = data.RootObject.GetComponentInChildren<DivvyAnimatedVisibility>();
			visibility.SetVisibility(false);
			visibility.SetVisibility(true, true);
			Assert.AreEqual(1, visibility.CurrentVisibility);
		}
	}
}
