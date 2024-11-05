using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum ELawForceType
{
    Repulsion=0,
    Gravitation=1,
}

public static class FieldUtil 
{
    public static int MaxEnergy;
    public static int MinEnergy;
    public static int col;
    public static int row;
    public static int GravitationApplyCellCount;
    public static int RepulsionApplyCellCount;

    public static float CellSize;
    public static float GravitationConstant;


    public static int AllNum;
    public static Vector2 UniverseBorder;

    public static void Init()
    {
        var _config=Manager<TableManager>.Inst.GlobalConfig;
        MinEnergy=_config.StarMinEnergy;
        MaxEnergy=_config.StarMaxEnergy;
        col=_config.FieldColCount;
        row=_config.FieldRowCount;
        AllNum=_config.FieldColCount*_config.FieldRowCount;
        CellSize=_config.GridSize;
        GravitationApplyCellCount = _config.GravitationApplyCount;
        RepulsionApplyCellCount=_config.RepulsionApplyCellCount;
        GravitationConstant=_config.GravitationalConstant;
        UniverseBorder = new Vector2(col / 2, row / 2);

    }

    #region FiledPos/UniverPos <=> Index
    public static int FieldfPosToIndex(int2 pos)
    {
        return pos.x*col + pos.y;
    }

    public static int UniversePosToIndex(UnityEngine.Vector2 pos)
    {
        pos.x += col / 2 * CellSize;
        pos.y += row / 2 * CellSize;
        int2 fieldPos;
        fieldPos.y=(int)(pos.x/CellSize);
        fieldPos.x=(int)(pos.y/CellSize);
        return FieldfPosToIndex(fieldPos) ;
    }

    public static int2 IndexToFieldPos(int index)
    {
        int2 pos;
        pos.y = index / col;
        pos.x = index % col;
        return pos;
    }

    public static float2 IndexToUniversePosF2(int index)
    {
        var fieldPos=IndexToFieldPos(index);
        fieldPos.x-=col/2;
        fieldPos.y-=row/2;
        float2 pos;
        pos.x = fieldPos.x*CellSize;
        pos.y = fieldPos.y*CellSize;
        return pos;
    }

    public static UnityEngine.Vector2 IndexToUniversePosV2(int index)
    {
        var fieldPos=IndexToFieldPos(index);
        fieldPos.x-=col/2;
        fieldPos.y-=row/2;
        UnityEngine.Vector2 pos;
        pos.x = fieldPos.x*CellSize;
        pos.y = fieldPos.y*CellSize;
        return pos;
    }
    #endregion

    #region Get Func
    public static List<int> GetNearCellIndex(int index,int bound)
    {
        var cells=new List<int>(bound*bound*4-1);
        var fieldPos = IndexToFieldPos(index);
        int2 newPos;
        for(int x=-bound;x<=bound;x++ )
        {
            for(int y=-bound;y<=bound;y++ )
            {
                newPos.x = fieldPos.x + x;
                newPos.y = fieldPos.y + y;
                if ( CheckIfFieldPosInField(newPos) && !( x == fieldPos.x && y == fieldPos.y ) )
                {
                    //UnityEngine.Debug.Log($"add index:{ FieldfPosToIndex(newPos)} fieldPos:{newPos}");
                    cells.Add(FieldfPosToIndex(newPos));
                }
                else
                {
                    //UnityEngine.Debug.Log($"unsafe index:{ FieldfPosToIndex(newPos)} fieldPos:{newPos}");
                }
            }
        }
        //UnityEngine.Debug.Log($"{cells.Count}"+JsonConvert.SerializeObject(cells));

        return cells;   
    }

    public static List<int2> GetNearCellFieldPos(int index,int bound)
    {
        var cells=new List<int2>(bound*bound*4-1);
        var fieldPos = IndexToFieldPos(index);
        int2 newPos;
        for(int x=-bound;x<=bound;x++ )
        {
            for(int y=-bound;y<=bound;y++ )
            {
                newPos.x = fieldPos.x + x;
                newPos.y = fieldPos.y + y;
                if ( CheckIfFieldPosInField(newPos) && !( x == fieldPos.x && y == fieldPos.y ) )
                {
                    //UnityEngine.Debug.Log($"add index:{ FieldfPosToIndex(newPos)} fieldPos:{newPos}");
                    cells.Add(newPos);
                }
                else
                {
                    //UnityEngine.Debug.Log($"unsafe index:{ FieldfPosToIndex(newPos)} fieldPos:{newPos}");
                }
            }
        }
        //UnityEngine.Debug.Log($"{cells.Count}"+JsonConvert.SerializeObject(cells));

        return cells;   
    }

    public static int GetChessDis(int2 fieldPos1,int2 fieldPos2)
    {
        return Mathf.Max(Mathf.Abs(fieldPos1.x - fieldPos2.x), Mathf.Abs(fieldPos1.y - fieldPos2.y));
    }
    #endregion

    #region Check Func
    public static bool CheckIfFieldPosInField(int2 fieldPos)
    {
        return fieldPos.x>=0 && fieldPos.y>=0 &&fieldPos.x<col&&fieldPos.y<row;
    }

    public static int CheckLawForceType(int2 fieldPos1,int2 fieldPos2)
    {
        var dis = GetChessDis(fieldPos1, fieldPos2);
        //Repulsion
        if ( dis <= RepulsionApplyCellCount )
        {
            return -1;
        }
        //Gravitation
        else if ( dis <= GravitationApplyCellCount )
        {
            return 1;
        }
        return 0;
    }
    #endregion

    #region Caculate Func
    public static Vector2 CaculateLawForce(int2 fieldPos1,int energy1,int2 fieldPos2,int energy2)
    {
        var dis=(fieldPos2.x - fieldPos1.x)*(fieldPos2.x - fieldPos1.x)+(fieldPos2.y - fieldPos1.y)*(fieldPos2.y - fieldPos1.y);
        var force = CheckLawForceType(fieldPos1, fieldPos2) * GravitationConstant * energy1 * energy2 / dis;
        var dir = fieldPos2 - fieldPos1;
        return force*(new Vector2(dir.x,dir.y).normalized);
    }

    public static Vector2 CaculateMotionStep(Vector2 pos,Vector2 step)
    {
        Vector2 newPos = new Vector2(pos.x + step.x, pos.y + step.y);

        newPos.x = Mathf.Clamp(newPos.x,-UniverseBorder.x , UniverseBorder.x);

        newPos.y = Mathf.Clamp(newPos.y, -UniverseBorder.y, UniverseBorder.y);

        return newPos;
    }
    #endregion
}
