using UnityEngine;
using System.Collections;

public class MainMenuPanel :BasePanel {

    private CanvasGroup canvasGroup;//获取CanvasGroup组件，用于控制该面板的交互功能

    /***********主界面调用时间较快所以获取CanvasGroup要在创建脚本时就获取到*******/
    void Awake()
    {
       canvasGroup = this.GetComponent<CanvasGroup>();
    }
   

    public override void OnEnter()
    {//重写方法：---页面显示---        激活功能
        this.canvasGroup.alpha = 1;//使该面板显示
        this.canvasGroup.interactable=true;//  面板的按钮功能启用
        this.canvasGroup.blocksRaycasts = true;//使该面板继续交互功能
        this.canvasGroup.ignoreParentGroups=false;//是否相应父级Group方法
    }
    public override void OnExit()
    {//重写方法： ---页面显示---           页面不能功能
        this.canvasGroup.alpha = 0;//使该主面板隐藏
        this.canvasGroup.blocksRaycasts = false;//使该面板失去交互功能
    }
 
    public override void OnPause()
    {//重写父类BasePanel的OnPause方法：   ---出现多层界面   当前界面层次被移动到后面 要禁用交互功能----
        this.canvasGroup.interactable = false;//  面板的按钮功能禁用
        canvasGroup.blocksRaycasts = false;//使该面板失去交互功能
    }
   
    public override void OnResume()
    { //重写父类BasePanel的OnResume方法：  ----出现多层界面   当前界面层次被移动到前面 要启用交互功能---
        this.canvasGroup.interactable = true;//  面板的按钮功能启用
        canvasGroup.blocksRaycasts = true;//使该面板继续交互功能
    }


    //点击功能模块按钮加载面板，并将其入栈
    public void OnPushPanel(string panelTypeString)
    {
        //把一个字符串转化为对应的枚举类型
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

}
