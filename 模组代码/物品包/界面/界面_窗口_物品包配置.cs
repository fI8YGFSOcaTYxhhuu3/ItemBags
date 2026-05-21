using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using 物品包.Items;
using 物品包.配置;

namespace 物品包.界面;



public partial class 类型_窗口_物品包配置 : 类型_窗口_通用 {
    protected override float 窗口宽度 => 500f;
    protected override float 窗口高度 => 500f;

    public 接口_物品包 包;
    private readonly 类型_配置_物品包 配置草稿;

    public 类型_窗口_物品包配置( 接口_物品包 包 ) {
        this.包 = 包;
        配置草稿 = 包.配置.Clone() as 类型_配置_物品包;
        界面初始化();
    }

    private void 更新配置( 类型_配置_物品包 新配置 ) {
        包.更新配置( 新配置 );
        类型_系统_界面交互.界面管理器.切换窗口( 包 );
        类型_系统_界面交互.界面管理器.切换窗口( 包 );
    }

    protected override void 界面初始化() {
        base.界面初始化();

        SetPadding( 0 );

        UIText 标题 = new( ( 包 as ModItem ).Item.Name ) { HAlign = 0.5f };
        标题.Top.Set( 10f, 0f );
        Append( 标题 );

        string 状态文本 = 包.独立配置 == null ? "[c/888888:正在使用全局配置]" : "[c/00FF00:正在使用独立配置]";
        UIText 状态标语 = new( 状态文本 ) { HAlign = 0.5f };
        状态标语.Top.Set( 35f, 0f );
        Append( 状态标语 );

        UIList 条目列表 = new() { ListPadding = 5f };
        条目列表.Top.Set( 60f, 0f ); 条目列表.Left.Set( 15f, 0f ); 条目列表.Width.Set( -50f, 1f ); 条目列表.Height.Set( -120f, 1f );
        配置草稿.填充控件( 条目列表 );
        Append( 条目列表 );

        UIScrollbar 滚动条 = new();
        滚动条.Top.Set( 60f, 0f ); 滚动条.Left.Set( -25f, 1f ); 滚动条.Height.Set( -120f, 1f );
        滚动条.SetView( 100f, 1000f );
        条目列表.SetScrollbar( 滚动条 );
        Append( 滚动条 );

        UITextPanel<string> 保存按钮 = new( "保存配置" ) { BackgroundColor = new( 73, 180, 73 ) };
        保存按钮.Top.Set( -45f, 1f ); 保存按钮.Left.Set( 15f, 0f ); 保存按钮.Width.Set( 100f, 0f ); 保存按钮.Height.Set( 30f, 0f );
        保存按钮.OnLeftClick += ( evt, el ) => 更新配置( 配置草稿.Clone() as 类型_配置_物品包 );
        Append( 保存按钮 );

        UITextPanel<string> 恢复按钮 = new( "恢复默认" );
        恢复按钮.Top.Set( -45f, 1f ); 恢复按钮.Left.Set( 115f, 0f ); 恢复按钮.Width.Set( 100f, 0f ); 恢复按钮.Height.Set( 30f, 0f );
        恢复按钮.OnLeftClick += ( evt, el ) => 更新配置( null );
        Append( 恢复按钮 );

        UITextPanel<string> 取消按钮 = new( "取消" ) { BackgroundColor = new( 180, 73, 73 ) };
        取消按钮.Top.Set( -45f, 1f ); 取消按钮.Left.Set( 215f, 0f ); 取消按钮.Width.Set( 100f, 0f ); 取消按钮.Height.Set( 30f, 0f );
        取消按钮.OnLeftClick += ( evt, el ) => 类型_系统_界面交互.界面管理器.关闭配置窗口();
        Append( 取消按钮 );
    }
}