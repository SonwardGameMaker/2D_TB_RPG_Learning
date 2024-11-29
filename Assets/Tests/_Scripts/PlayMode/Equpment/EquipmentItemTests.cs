using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests._Scripts.PlayMode.Equpment
{
    [Serializable]
    public class EquipmentItemTests : MonoBehaviour
    {
        [SerializeField] private EquipmentItemSO _equipmentItemSO;
        
        private EquipmentItem _equipmentItem;

        #region init
        [Test]
        private void Init()
        {
            _equipmentItem = _equipmentItemSO.CreateItem() as EquipmentItem;
            Assert.Equals(_equipmentItemSO.Name, _equipmentItem.Name);
            Assert.Equals(_equipmentItemSO.Description, _equipmentItem.Description);
            Assert.Equals(_equipmentItemSO.Price, _equipmentItem.Price);
            Assert.Equals(_equipmentItemSO.Weight, _equipmentItem.Weight);
            Assert.Equals(_equipmentItemSO.CurrentDurability, _equipmentItem.Durability.CurrentValue);
            Assert.Equals(_equipmentItemSO.MaxDurability, _equipmentItem.Durability.MaxValue);
        }
        #endregion

        // A Test behaves as an ordinary method
        [Test]
        public void EquipmentItemTestsSimplePasses()
        {
            var a = new EquipmentItem("name", "description", 0);
            Debug.LogError(a);
        }
    }
}
