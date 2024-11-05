using System;
using System.Collections.Generic;
public enum EGameState
{
    Login,
    Stage,
}
public struct SGameStateChangeEvent
{
    public EGameState OldState;
    public EGameState NewState;
}

public static class GContext 
{

    // �洢��������
    private static Dictionary<Type, object> dic_singletonRegistry = new Dictionary<Type, object>();

    private static Dictionary<Type, object> dic_ModuleRegistry = new Dictionary<Type, object>();

    // Register Sigleton
    public static  void RegisterSingleton<T>(T instance=null) where T : class, new()
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
    public static T ResloveSingleton<T>() where T : class, new()
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

    public static void RegisterMoudle<T>() where T : Module, new()
    {
        var type = typeof(T);

        // ����Ѵ��ڸ����͵ĵ������򷵻�
        if (dic_ModuleRegistry.ContainsKey(type))
        {
            return;
        }

        // �����µĵ������洢
        var inst=new T();
        inst.OnCreate();
        dic_ModuleRegistry[type] = inst;
    }

    public static T ResloveMoudle<T>() where T : Module, new()
    {
        var type = typeof(T);

        // ����Ѵ��ڸ����͵ĵ������򷵻�
        if (dic_ModuleRegistry.ContainsKey(type))
        {
            return dic_ModuleRegistry[type] as T;
        }
        else
        {
            RegisterMoudle<T>();
            return dic_ModuleRegistry[type] as T;
        }
    }

    public static Module ResloveMoudle(Type type)  
    {
        // ����Ѵ��ڸ����͵ĵ������򷵻�
        if (dic_ModuleRegistry.ContainsKey(type))
        {
            return dic_ModuleRegistry[type] as Module;
        }
        else
        {
            var inst = Activator.CreateInstance(type) as Module;
            inst.OnCreate();
            dic_ModuleRegistry[type] = inst;
            return dic_ModuleRegistry[type] as Module;
        }
    }


    public static void UnrigisterModule(Type type)
    {
        if(dic_ModuleRegistry.TryGetValue(type,out var obj))
        {
            dic_ModuleRegistry.Remove(type);    
            var module = obj as Module; 
            module.OnDestory();
        }
    }

}

public static class Manager<T> where T:class, new()
{
    public static T Inst=>GContext.ResloveSingleton<T>();
}

public static class Module<T> where T:Module,new()
{
    public static T Inst=>GContext.ResloveMoudle<T>();
}
