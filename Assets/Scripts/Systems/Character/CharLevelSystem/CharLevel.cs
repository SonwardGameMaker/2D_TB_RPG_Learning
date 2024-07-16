using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CharLevel : CharParameterBase, IMinValUnmod, ICurrValUnmod, IMaxValUnmod
//{
//    [SerializeField] private float _minValue;
//    [SerializeField] private float _currentValue;
//    [SerializeField] private float _maxValue;

//    public CharLevel(float minValue = 1, float currentValue = 1, float maxValue = 20)
//    {
//        _minValue = minValue;
//        _currentValue = currentValue;
//        _maxValue = maxValue;
//    }

//    public float MinValue 
//    { 
//        get => _minValue;
//        set
//        {
//            if (value > _maxValue)
//                _minValue = _maxValue;
//            else 
//                _maxValue = value;

//            if (_currentValue < _minValue)
//                _currentValue = _minValue;
//        }
//    }
//    public float CurrentValue 
//    {
//        get => _currentValue; 
//        set
//        {
//            if (_currentValue > _maxValue)
//                _currentValue = _maxValue;
//            else if (_currentValue < _minValue)
//                _currentValue = _minValue;
//            else
//                _currentValue = value;
//        }
//    }
//    public float MaxValue 
//    { 
//        get => _maxValue; 
//        set
//        {
//            if (value < _minValue)
//                _maxValue = _minValue;
//            else
//                _maxValue= value;

//            if (_currentValue > _maxValue)
//                _currentValue = _maxValue;
//        }
//    }
//}
