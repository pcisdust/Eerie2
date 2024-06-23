using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangerManager : MonoBehaviour
{
    public List<MaterialChanger> materialChangers;
    public void ChangeMaterial(int _type)
    {
        if (materialChangers.Count != 0)
        {
            foreach (var item in materialChangers)
            {
                if(item)
                item.ChangeMaterial(_type);
            }
        }
    }
}
