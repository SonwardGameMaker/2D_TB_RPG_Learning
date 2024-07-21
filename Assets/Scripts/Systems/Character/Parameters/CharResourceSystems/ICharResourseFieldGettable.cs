using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharResourceFieldType { _health, _actionPoints, _movementPoints }
public interface ICharResourseFieldGettable
{
    public CharResource GetFieldByEnum(CharResourceFieldType field);
}
