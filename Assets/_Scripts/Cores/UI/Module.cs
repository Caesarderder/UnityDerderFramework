using System;
using Unity.VisualScripting;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataModuleAttribute : Attribute
{
    public EDataModuleType LifeCycleType{ get; }
    public int Scope { get; }

    public DataModuleAttribute(EDataModuleType lifecycleType,int scope=0)
    {
        LifeCycleType= lifecycleType;
        Scope = scope;
    }
}

public abstract class Module  
{
    public virtual void OnCreate()
    {


    }

    public virtual void OnDestory()
    {

    }
}

public enum EDataModuleType
{
    Persistent,
    GameState,
    Act,
}
/// <summary>
/// 根据GameState/Act来创建和销毁?
/// 存储数据，提供数据接口，处理游戏数据的逻辑
/// </summary>
public class DataModule :Module
{

    public static DataModule Resolve(Type type)=> GContext.ResloveMoudle(type) as DataModule;
    public static T Resolve<T>() where T:DataModule,new()=> GContext.ResloveMoudle<T>();

    public static void Unregister(Type type)=> GContext.UnrigisterModule(type);

    public override void OnCreate()
    {
        Debug.Log($"<color=green>DataModule:   {this.GetType().Name.ToString()}   Create</color>");
        var attribute = Attribute.GetCustomAttribute(this.GetType(), typeof(DataModuleAttribute)) as DataModuleAttribute;
        if (attribute != null) {
            switch (attribute.LifeCycleType)
            {
                case EDataModuleType.Persistent:
                    break;
                case EDataModuleType.GameState:
                    EventAggregator.Subscribe<SGameStateChangeEvent>(OnLifeCycleCheck);
                    break;
                case EDataModuleType.Act:
                    EventAggregator.Subscribe<SActUnloadedEvent>(OnLifeCycleCheck);
                    break;
                default:
                    break;
            }
        }
    }

    public override void OnDestory()
    {
        Debug.Log($"<color=yellow>DataModule:   {this.GetType().Name.ToString()}   Destory</color>");
    }

    public void OnLifeCycleCheck(SGameStateChangeEvent evt)
    {
        Debug.Log("Yes2 GameState");

        var attribute = Attribute.GetCustomAttribute(this.GetType(), typeof(DataModuleAttribute)) as DataModuleAttribute;
        if (attribute != null) {
            if(evt.OldState==(EGameState)attribute.Scope)
            {
                EventAggregator.Unsubscribe<SGameStateChangeEvent>(OnLifeCycleCheck);
                DataModule.Unregister(this.GetType());
            }
        }
    }

    public void OnLifeCycleCheck(SActUnloadedEvent evt)
    {
        Debug.Log("Yes2 Act");

        var attribute = Attribute.GetCustomAttribute(this.GetType(), typeof(DataModuleAttribute)) as DataModuleAttribute;
        if (attribute != null) {
            if(evt.ActName==(EAct)attribute.Scope)
            {
                EventAggregator.Unsubscribe<SActUnloadedEvent>(OnLifeCycleCheck);
                DataModule.Unregister(this.GetType());
            }
        }
    }
}

/// <summary>
/// 根据绑定的UI来创建和销毁
/// 存储UI的暂时数据，处理UI的逻辑
/// </summary>
public class ViewModule :Module
{
    public static ViewModule Resolve(Type type) => GContext.ResloveMoudle(type) as ViewModule;
    public static T Resolve<T>() where T:ViewModule,new()=> GContext.ResloveMoudle<T>();

    public static void Unregister(Type type)=> GContext.UnrigisterModule(type);


    public override void OnCreate()
    {
        Debug.Log($"<color=green>ViewModule:   {this.GetType().Name.ToString()}   Create</color>");
    }

    public override void OnDestory()
    {
        Debug.Log($"<color=yellow>ViewModule:   {this.GetType().Name.ToString()}   Destory</color>");
    }
}


