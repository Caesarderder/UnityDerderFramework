using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEditor;

public class GContext
{
    private static GContext _container;
    public static GContext Container {
        get
        {
            if(_container== null)
                _container= new GContext();
            return _container;
        }
    }
    // 存储单例对象
    private Dictionary<Type, object> dic_singletonRegistry = new Dictionary<Type, object>();

//    // 存储对象池
//    private static Dictionary<Type, Queue<object>> dic_objectPoolRegistry = new Dictionary<Type, Queue<object>>();

    // Register Sigleton
    public void RegisterSingleton<T>(T instance=null) where T : class, new()
    {
        var type = typeof(T);

        // 如果已存在该类型的单例，则返回
        if (dic_singletonRegistry.ContainsKey(type))
        {
            dic_singletonRegistry.Remove(type);
        }

        // 创建新的单例并存储
        if(instance!=null)
            dic_singletonRegistry[type] = instance;
        else
            dic_singletonRegistry[type] = new T();
            return ;
    }

    // Get Sigleton
    public T ResloveSingleton<T>() where T : class, new()
    {
        var type = typeof(T);

        // 如果已存在该类型的单例，则返回
        if (dic_singletonRegistry.ContainsKey(type))
        {
            return dic_singletonRegistry[type] as T;
        }

        //        // 创建新的单例并存储
        //        T instance = new T();
        //        dic_singletonRegistry[type] = instance;

        UnityEngine.Debug.LogError($"haven't registered signleton： {typeof(T).ToString()}");
        return null;
    }

    // 清除单例
//    public static void ClearSingleton<T>() where T : class
//    {
//        var type = typeof(T);
//
//        if (dic_singletonRegistry.ContainsKey(type))
//        {
//            dic_singletonRegistry.Remove(type);
//        }
//    }
//
//    public struct SEvent
//    {
//        public int num;
//    }
//    public class CTest
//    {
//        public void Observer(SEvent sEvent)
//        {
//            UnityEngine.Debug.Log(sEvent.num);
//        }
//    }
//
//    [MenuItem("Test/EventAggregator")]
//    public static void Test()
//    {
//        CTest test = new CTest();
//        Container.RegisterSingleton<CTest>(test);
//        Container.ResloveSingleton<CTest>().Observer(new SEvent { num=1});
//        Container.ClearSingleton<CTest>();
//        Container.ResloveSingleton<CTest>().Observer(new SEvent { num=2});
//    }

    //    // 获取对象池中的对象
    //    public static T GetFromPool<T>() where T : class, new()
    //    {
    //        var type = typeof(T);
    //
    //        // 检查对象池中是否有可用对象
    //        if (dic_objectPoolRegistry.ContainsKey(type) && dic_objectPoolRegistry[type].Count > 0)
    //        {
    //            return dic_objectPoolRegistry[type].Dequeue() as T;
    //        }
    //
    //        // 如果池中没有对象，则创建一个新的对象
    //        return new T();
    //    }
    //
    //    // 回收对象到对象池
    //    public static void ReturnToPool<T>(T obj) where T : class
    //    {
    //        var type = typeof(T);
    //
    //        // 如果没有对象池，为该类型创建一个池
    //        if (!dic_objectPoolRegistry.ContainsKey(type))
    //        {
    //            dic_objectPoolRegistry[type] = new Queue<object>();
    //        }
    //
    //        // 将对象放回池中
    //        dic_objectPoolRegistry[type].Enqueue(obj);
    //    }
    //
    //
    //    // 清除所有单例和对象池
    //    public static void ClearAll()
    //    {
    //        dic_singletonRegistry.Clear();
    //        dic_objectPoolRegistry.Clear();
    //    }
}