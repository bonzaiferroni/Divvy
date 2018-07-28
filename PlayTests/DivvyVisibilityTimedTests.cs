using System.Collections;
using Divvy.Core;
using Divvy.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Divvy.PlayTests
{
    public class DivvyVisibilityTimedTests 
    {
        [UnityTest]
        public IEnumerator SetVisibility_True_AnimatesFrom0to1()
        {
            var data = new DivvyData();
            var visibility = data.RootObject.GetComponentInChildren<DivvyAnimatedVisibility>();
            visibility.SetVisibility(false, true);
            visibility.SetVisibility(true);
            Assert.AreEqual(0, visibility.CurrentVisibility);
            yield return new WaitForSeconds(DivvyConstants.Speed / 2);
            Assert.IsTrue(visibility.CurrentVisibility > 0 && visibility.CurrentVisibility < 1);
            yield return new WaitForSeconds(DivvyConstants.Speed * 8);
            Assert.AreEqual(1, visibility.CurrentVisibility);
        }
    }
}
