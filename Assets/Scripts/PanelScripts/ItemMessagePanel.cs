using UnityEngine;
using System.Collections;

/// <summary>
/// 背包里的装备信息提示面板
/// </summary>
public class ItemMessagePanel : BasePanel
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        this.canvasGroup.alpha = 1;
        this.canvasGroup.blocksRaycasts = true;
        //this.transform.localScale = Vector3.zero;
       // transform.DOScale(1, 0.6f);//缩放动画，1代表结束时的缩放
    }
    public override void OnExit()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.blocksRaycasts = false;
       // transform.DOScale(0, 0.6f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClose()
    {
        UIManager.Instance.PopPanel();
    }

}
