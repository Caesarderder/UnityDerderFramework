using System.Collections.Generic;
using System;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

public class EventAggregator
{
    private static readonly Dictionary<Type, List<Action<object>>> dic_eventHandlers =
        new Dictionary<Type, List<Action<object>>>();

    public static void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (!dic_eventHandlers.ContainsKey(type))
        {
            dic_eventHandlers[type] = new List<Action<object>>();
        }
        dic_eventHandlers[type].Add(obj => handler((T)obj));
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (dic_eventHandlers.ContainsKey(type))
        {
            foreach (var h in dic_eventHandlers[type])
            {
                if(h.Target != handler.Target || h.Method != handler.Method)
                {
                    dic_eventHandlers[type].Remove(h);
                    break;
                }
            }
        }
    }

    public static void Publish<T>(T eventToPublish)
    {
        var type = typeof(T);
        if (dic_eventHandlers.ContainsKey(type))
        {
            var handlers = dic_eventHandlers[type].ToArray();
            foreach (var handler in handlers)
            {
                handler(eventToPublish);
            }
        }
    }
//    public struct SEvent
//    {
//        public int num;
//    }
//    public class CTest
//    {
//        public void Observer(SEvent sEvent)
//        {
//            Debug.Log(sEvent.num);
//        }
//    }
//
//    [MenuItem("Test/EventAggregator")]
//    public static void Test()
//    {
//        CTest test = new CTest();
//        EventAggregator.Subscribe<SEvent>(test.Observer);
//        EventAggregator.Publish(new SEvent { num=2});
//        EventAggregator.Unsubscribe<SEvent>(test.Observer);
//        EventAggregator.Publish(new SEvent { num=3});
//    }
//
}
