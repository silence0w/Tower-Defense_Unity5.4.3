using UnityEngine;
using System.Collections;

/// <summary>
/// 背包面板
/// </summary>
public class KnapsackPanel : BasePanel {
    private CanvasGroup canvasGroup;

    void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        this.canvasGroup.alpha = 1;
        this.canvasGroup.blocksRaycasts = true;
       //Vector3 temp = this.transform.localPosition;
       // temp.x = 600;
       // this.transform.localPosition = temp;
        //this.transform.DOLocalMoveX(0, 0.6f);
    }
    
    public override void OnExit()
    {

        //this.transform.DOLocalMoveX(-600, 0.6f).OnComplete(() => canvasGroup.alpha = 0);
        //这里有隐藏bug：当关闭背包面板 开始播放隐藏引动动画，动画未结束时进行点击操作打开同一个背包界面会导致堆栈管理面板的代码失效造成误判
        this.canvasGroup.alpha = 0;
        this.canvasGroup.blocksRaycasts = false;
    }
 
    public override void OnPause()
    {
        this.canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        
        this.canvasGroup.blocksRaycasts = true;
    }
    //点击关闭按钮
    public void OnClose()
    {
        
        UIManager.Instance.PopPanel();
    }

    //点击装备，显示装备信息
    public void OnItemButtonDown()
    {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }

}
