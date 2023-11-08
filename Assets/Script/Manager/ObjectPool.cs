using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<BaseEntity> Entities = new List<BaseEntity>();
    public Dictionary<Type, IList> Pools = new Dictionary<Type, IList>();
    private void Awake()
    {
        foreach (var entity in Entities)
        {
            entity.gameObject.SetActive(false);
            Type type = entity.GetType();
            var listV = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(listV);
            Pools.Add(type, list);
        }
    }
    public bool DestroyEntity(BaseEntity obj)
    {
        try
        {
            Pools[obj.GetType()].Add(obj);
            obj.gameObject.SetActive(false);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return false;
    }
    public BaseEntity GetObject(Type targetType)
    {
        try
        {
            BaseEntity ret = null;
            if (Pools[targetType].Count == 0)
            {
                var baseEntity = Entities.FirstOrDefault(entity => entity.GetType() == targetType);
                ret = Instantiate(baseEntity);
            }
            else
            {
                ret = Pools[targetType][0] as BaseEntity;
                Pools[targetType].Remove(ret);
            }
            if (ret == null)
            {
                throw new Exception("Get a Null Object");
            }
            return ret;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }
}
