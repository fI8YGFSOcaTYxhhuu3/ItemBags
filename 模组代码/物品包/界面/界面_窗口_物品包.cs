using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using 物品包.Items;

namespace 物品包.界面;



// 特征成员
public partial class 类型_窗口_物品包 : 类型_窗口_通用 {
    private const float 槽位大小 = 38f;
    private const float 槽位间距 = 4f;
    private const float 边缘间距 = 10f; 
    private const float 滚动条宽度 = 20f;
    private const float 工具栏高度 = 30f;

    protected float 网格宽度 => 槽位大小 * 包.配置.列数 + 槽位间距 * ( 包.配置.列数 - 1 );
    protected float 网格高度 => 槽位大小 * 包.配置.行数 + 槽位间距 * ( 包.配置.行数 - 1 );

    private readonly UITextPanel<string> 配置按钮 = new( Language.GetTextValue( "Mods.物品包.UI.按钮.配置" ) );
    private readonly UITextPanel<string> 关闭按钮 = new( Language.GetTextValue( "Mods.物品包.UI.按钮.关闭" ) );
    private readonly UIElement 工具栏 = new();
    private readonly UIGrid 物品网格 = [];
    private readonly UIScrollbar 滚动条 = new();

    public 接口_物品包 包;

    public 类型_窗口_物品包( 接口_物品包 包 ) { 包.更新容量(); this.包 = 包; 界面初始化(); }
}

// 特征重写函数
public partial class 类型_窗口_物品包 {
    protected override float 窗口宽度 => 边缘间距 + 网格宽度 + 槽位间距 + 滚动条宽度 + 边缘间距;
    protected override float 窗口高度 => 边缘间距 + 网格高度 + 槽位间距 + 工具栏高度 + 边缘间距;

    protected override void 界面初始化() {
        base.界面初始化();
        SetPadding( 0 );
        工具栏_初始化();
        物品网格_初始化();
    }
    protected override void Update_布局属性() {
        base.Update_布局属性();
        工具栏_更新(); 物品网格_更新();
    }
    protected override bool 拖拽判断( UIMouseEvent 鼠标事件 ) => 鼠标事件.Target == this || 鼠标事件.Target == 工具栏;
}

// 子部件
public partial class 类型_窗口_物品包 {
    private void 配置按钮_初始化() {
        配置按钮.VAlign = 0.5f;
        配置按钮.Width.Set( 60f, 0f ); 配置按钮.Height.Set( 28f, 0f );
        配置按钮.OnLeftClick += ( evt, element ) => 类型_系统_界面交互.界面管理器.切换配置窗口( 包 );
        工具栏.Append( 配置按钮 );
    }
    private void 关闭按钮_初始化() {
        关闭按钮.VAlign = 0.5f; 关闭按钮.HAlign = 1f;
        关闭按钮.Width.Set( 60f, 0f ); 关闭按钮.Height.Set( 28f, 0f );
        关闭按钮.BackgroundColor = new( 180, 73, 73 );
        关闭按钮.OnLeftClick += ( evt, element ) => 类型_系统_界面交互.界面管理器.切换窗口( 包 );
        工具栏.Append( 关闭按钮 );
    }
    private void 工具栏_初始化() {
        工具栏.Top.Set( 边缘间距, 0f ); 工具栏.Left.Set( 边缘间距, 0f ); 工具栏.Height.Set( 工具栏高度, 0f );
        配置按钮_初始化();
        关闭按钮_初始化();
        Append( 工具栏 );
    }
    private void 工具栏_更新() { 工具栏.Width.Set( 窗口宽度 - 边缘间距 * 2, 0f ); }
    private void 物品网格_初始化() {
        for ( int 槽位索引 = 0; 槽位索引 < 包.物品矩阵.Length; 槽位索引++ ) {
            var 界面槽位 = 包.界面槽位( 槽位索引 );
            界面槽位.Width.Set( 槽位大小, 0f );
            界面槽位.Height.Set( 槽位大小, 0f );
            物品网格.Add( 界面槽位 );
        }
        物品网格.ListPadding = 槽位间距;
        物品网格.Top.Set( 边缘间距 + 工具栏高度 + 槽位间距, 0f ); 物品网格.Left.Set( 边缘间距, 0f );
        滚动条_初始化();
        Append( 物品网格 );
    }
    private void 物品网格_更新() { 物品网格.Width.Set( 网格宽度, 0f ); 物品网格.Height.Set( 网格高度, 0f ); 滚动条_更新(); }
    private void 滚动条_初始化() {
        滚动条.SetView( 100f, 2160f );
        滚动条.Top.Set( 边缘间距 + 工具栏高度 + 槽位间距, 0f ); 滚动条.Left.Set( -边缘间距 - 滚动条宽度, 1f );
        物品网格.SetScrollbar( 滚动条 );
        Append( 滚动条 );
    }
    private void 滚动条_更新() { 滚动条.Height.Set( 网格高度, 0f ); }
}