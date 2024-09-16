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
    // �洢��������
    private Dictionary<Type, object> dic_singletonRegistry = new Dictionary<Type, object>();

//    // �洢�����
//    private static Dictionary<Type, Queue<object>> dic_objectPoolRegistry = new Dictionary<Type, Queue<object>>();

    // Register Sigleton
    public void RegisterSingleton<T>(T instance=null) where T : class, new()
    {
        var type = typeof(T);

        // ����Ѵ��ڸ����͵ĵ������򷵻�
        if (dic_singletonRegistry.ContainsKey(type))
        {
            dic_singletonRegistry.Remove(type);
        }

        // �����µĵ������洢
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

        // ����Ѵ��ڸ����͵ĵ������򷵻�
        if (dic_singletonRegistry.ContainsKey(type))
        {
            return dic_singletonRegistry[type] as T;
        }

        //        // �����µĵ������洢
        //        T instance = new T();
        //        dic_singletonRegistry[type] = instance;

        UnityEngine.Debug.LogError($"haven't registered signleton�� {typeof(T).ToString()}");
        return null;
    }

    // �������
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

    //    // ��ȡ������еĶ���
    //    public static T GetFromPool<T>() where T : class, new()
    //    {
    //        var type = typeof(T);
    //
    //        // ����������Ƿ��п��ö���
    //        if (dic_objectPoolRegistry.ContainsKey(type) && dic_objectPoolRegistry[type].Count > 0)
    //        {
    //            return dic_objectPoolRegistry[type].Dequeue() as T;
    //        }
    //
    //        // �������û�ж����򴴽�һ���µĶ���
    //        return new T();
    //    }
    //
    //    // ���ն��󵽶����
    //    public static void ReturnToPool<T>(T obj) where T : class
    //    {
    //        var type = typeof(T);
    //
    //        // ���û�ж���أ�Ϊ�����ʹ���һ����
    //        if (!dic_objectPoolRegistry.ContainsKey(type))
    //        {
    //            dic_objectPoolRegistry[type] = new Queue<object>();
    //        }
    //
    //        // ������Żس���
    //        dic_objectPoolRegistry[type].Enqueue(obj);
    //    }
    //
    //
    //    // ������е����Ͷ����
    //    public static void ClearAll()
    //    {
    //        dic_singletonRegistry.Clear();
    //        dic_objectPoolRegistry.Clear();
    //    }
}