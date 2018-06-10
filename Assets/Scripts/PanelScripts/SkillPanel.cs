using UnityEngine;
using System.Collections;
/// <summary>
/// 技能面板
/// 
/// </summary>
public class SkillPanel : BasePanel {
    private CanvasGroup canvasGroup;

    void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        this.canvasGroup.alpha = 1;
        this.canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnClose()
    {
        UIManager.Instance.PopPanel();
    }

}
