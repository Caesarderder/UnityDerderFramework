using UnityEngine;

public class TestDataModule : DataModule
{
    #region Fileds 亻尔 女子
    TestSavedData _data;

    public override void OnCreate()
    {
        base.OnCreate();
        _data = DataFabUtil.LocalLoad<TestSavedData>(typeof(TestSavedData).Name);
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }

    #endregion

    #region Methods
    public int GetTestSavedData()
    {
        Debug.Log($"[TestDataModule::GetTestSavedData] {_data.Num}");
        return _data.Num;
    }

    public void OnTestDataChange(int num)
    {
        _data.Num=num;
        _data.Save();
    }

    #endregion
}
