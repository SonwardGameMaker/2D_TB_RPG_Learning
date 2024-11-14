using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests._Scripts.PlayMode.Equpment
{
    [Serializable]
    public class EquipmentItemTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void EquipmentItemTestsSimplePasses()
        {
            var a = new EquipmentItem("name", "description", 0);
            Debug.LogError(a);
        }
    }
}
