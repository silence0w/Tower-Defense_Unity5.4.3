using UnityEngine;
using System.Collections;
/// <summary>
/// 启动脚本  用于启动UI框架 同时显示主界面
/// </summary>
public class GameRoot : MonoBehaviour {

    void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);//初始状态把主界面先启动 然后再在此基础上进行交互！
    }
}
