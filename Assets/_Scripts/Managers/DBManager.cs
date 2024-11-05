using UnityEngine;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using SQLite4Unity3d;

public class DBManager 
{
    #region Fileds 亻尔 女子
    public static SQLiteConnection Connection;
    #endregion

    #region Methods
    [MenuItem("Test/CreateTable")]
	public static void TestTable()
	{
        Debug.Log(Application.persistentDataPath);
        Connection = new SQLiteConnection(Application.persistentDataPath+ "/TestDatabase.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
//        Connection.CreateTable<DialogeTable>(tableName:"HHH");//创建表
//            var p = new DialogeTable
//            {
//                Character1Id= 2,
//                dialoge= "Chinar",
//            };
        var mapping = Connection.GetMapping(typeof(DialogeTable), tableName: "HHH");
        Debug.Log(mapping.Columns);
        //        Connection.Insert(p);
        Connection.Delete<DialogeTable>(2);

//        var datas = Connection.Table<DialogeTable>().Where(_ => _.Age == 12);//获取到所有Age为12的数据
//        foreach (var v in datas)//遍历
//        {
//            Debug.Log(v.Name);
//        }
//
//        // 获取到名字为“小明”的数据
//        var data = Connection.Table<DialogeTable>().Where(_ => _.dialoge== "小明").FirstOrDefault();
//        //更改 Weight 
//        data.dialoge = "aa";
//        //更新数据
//        Connection.Update(data);
    }
    #endregion
}
public class DialogeTable
{
    [PrimaryKey] //设置主键 
    public int Character1Id { get; set; }//Id作为主键

    public string dialoge { get; set; }
    public float test { get;set; }

//    /// <summary>
//    /// 重写ToString函数，方便控制台打印
//    /// </summary>
//    /// <returns></returns>
//    public override string ToString()
//    {
//        return dialoge;
//    }
}
