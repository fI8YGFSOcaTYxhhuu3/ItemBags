using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using 物品包.Items;

namespace 物品包.界面;



public class 类型_窗口_物品包配置 : 类型_窗口_通用 {
    protected override float 窗口宽度 => 300f;
    protected override float 窗口高度 => 400f;

    public 接口_物品包 包;

    public 类型_窗口_物品包配置( 接口_物品包 包 ) {
        this.包 = 包;
        界面初始化();

        UIText 标题 = new( $"配置: {((ModItem)包).Item.Name}" ) { HAlign = 0.5f };
        标题.Top.Set( 5f, 0f );
        Append( 标题 );

        UIText 占位文本 = new( "独立配置功能占位符...\n(即将实现)" ) { HAlign = 0.5f, VAlign = 0.5f };
        Append( 占位文本 );

        UITextPanel<string> 关闭按钮 = new( "关闭" ) { HAlign = 0.5f };
        关闭按钮.Width.Set( 80f, 0f ); 关闭按钮.Height.Set( 30f, 0f );
        关闭按钮.Top.Set( -40f, 1f );
        关闭按钮.OnLeftClick += ( evt, element ) => 类型_系统_界面交互.界面管理器.关闭配置窗口();
        Append( 关闭按钮 );
    }
}