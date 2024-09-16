using FSM;
using System;

public interface ICombatStrategy
{
    public Combat Combat { get; set; }
    public string CombatID { get; }
    public Action ChannelEndCombat { get; set; }

    public void StartCombat();
    public void ApplyCombat();
    public void BreakCombat();
    public void Charging();
}

