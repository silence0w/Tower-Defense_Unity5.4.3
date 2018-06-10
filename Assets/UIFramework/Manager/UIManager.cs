using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 单例模式的核心：
/// 1.只在该类内定义一个静态的对象，该对象在外界访问，在内部构造
/// 2.构造函数私有化
/// </summary>
public class UIManager  {

    //此类作为一个单例模式，即只有一个实例的模式
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 构造函数私有化  （单例模式）
    /// </summary>
    private UIManager()
    {

        ParseUIPanelTypeJson();//构造该类时会解析Json
    }


    private Dictionary<UIPanelType, string> panelPathDict;//存储所有Prefab面板的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//借助BasePanel脚本保存所有实例化出来的面板物体（因为BasePanel脚本被所有面板预设物体的自己的脚本所继承，所以需要的时候可以根据BasePanel脚本来实例化每一个面板对象）
    private Stack<BasePanel> panelStack;//这是一个栈，用来保存实例化出来（显示出来）的面板

    private Transform canvasTransform;//用来使实例化的面板归为它的子物体
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    //页面入栈，即把页面显示在界面上
    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)//如果栈不存在，就实例化一个空栈
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();//取出栈顶元素保存起来，但是不移除
            topPanel.OnPause();//使该页面暂停，不可交互
        }
        BasePanel panelTemp = GetPanel(panelType);
        panelTemp.OnEnter();//页面进入显示，可交互
        panelStack.Push(panelTemp);
    }
    //页面出栈，即把页面从界面上移除
    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0) return;
        //关闭栈顶页面的显示
        BasePanel topPanel1 = panelStack.Pop();
        topPanel1.OnExit();
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
      
        topPanel2.OnResume();//使第二个栈里的页面显示出来，并且可交互 //先关闭上一个界面  把动画也关闭了再打开下一个需要设置等待时间再开启第二个栈里面的页面显示  ---判断面板动画状态  当为动画消失时在执行其他操作

    }

    [System.Serializable]
    class UIPanelTypeJson //内部类里面就一个链表容器,用来配合解析
    {
        public List<UIPanelInfo> infoList;
    }

    //根据面板类型UIPanelType得到实例化的面板  用于生成面板
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)//如果panelDict字典为空，就实例化一个空字典
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//不为空就根据类型得到Basepanel
        BasePanel panel = panelDict.TayGet(panelType);//我们扩展的Dictionary的方法，代码作用同上两行
        if (panel == null)//如果得到的panel为空，那就去panelPathDict字典里面根据路径path找到，然后加载，接着实例化
        {
            string path = panelPathDict.TayGet(panelType);//我们扩展的Dictionary的方法
            //panelPathDict.TryGetValue(panelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;//根据路径加载并实例化面板
            instPanel.transform.SetParent(this.CanvasTransform, false);//设置为Canvas的子物体,false表示实例化的子物体坐标以Canvas为准
            //TODO
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }

    //解析UIPanelType.json的信息
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();//实例化一个字典对象
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType"); //获取UIPanelType.json文件的文本信息
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//把UIPanel.json文本信息转化为一个内部类的对象，对象里面的链表里面对应的是每个Json信息对应的类
        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);//把每一个进过json文件转化过来的类存入字典里面(键值对的形式)
        }
    }
}





