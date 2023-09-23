using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HitDef {

    /// <summary>
    ///程度(轻:0 中:1 重:2）
    /// </summary>
    [SerializeField]
    public int Level;
    /// <summary>
    ///垂直方向力度分量(forward:0 up:1 down:2)
    /// </summary>
    [SerializeField]
    public int ForceVDir;
    /// <summary>
    ///p1打击停顿时间
    /// </summary>
    [SerializeField]
    public int P1HitPauseTime;
    /// <summary>
    ///p2打击停顿时间
    /// </summary>
    [SerializeField]
    public int P2HitPauseTime;
    /// <summary>
    ///打击后退速度(水平)
    /// </summary>
    [SerializeField]
    public int HitBackHSpeed;
    /// <summary>
    ///打击后退速度(垂直)
    /// </summary>
    [SerializeField]
    public int HitBackVSpeed;
    /// <summary>
    ///打击恢复时间
    /// </summary>
    [SerializeField]
    public int HitRecoverTime;
}
