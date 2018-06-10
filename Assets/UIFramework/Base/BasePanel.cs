using UnityEngine;
using System.Collections;


/// <summary>
/// 各个面板的基类（定义各个面板共有的四个状态）：
/// </summary>
public class BasePanel : MonoBehaviour {
    /// <summary>
    /// 页面进入显示，可交互
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 页面暂停（弹出了其他页面），不可交互
    /// </summary>
    public virtual void OnPause() { }

    /// <summary>
    /// 页面继续显示（其他页面关闭），可交互
    /// </summary>
    public virtual void OnResume() { }

    /// <summary>
    /// 本页面被关闭（移除），不再显示在界面上
    /// </summary>
    public virtual void OnExit() { }

}
