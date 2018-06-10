using UnityEngine;
using System.Collections;

/// <summary>
/// 任务面板
/// </summary>
public class TaskPanel : BasePanel{
    private CanvasGroup canvasGroup;//对该页面CanvasGroup组件的引用

    void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }
    //重写父类的OnEnter方法
    public override void OnEnter()
    {


        this.canvasGroup.alpha = 1;
        this.canvasGroup.blocksRaycasts = true;
        //canvasGroup.DOFade(1, 0.6f);//使该页面渐渐显示,1是Alpha值
    }
    //重写父类的OnExit方法
    public override void OnExit()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.blocksRaycasts = false;
        //canvasGroup.DOFade(0, 0.6f);//使该页面渐渐隐藏，0是Alpha值
    }

    //点击关闭按钮事件
    public void OnClose()
    {
        UIManager.Instance.PopPanel();
    }
}
