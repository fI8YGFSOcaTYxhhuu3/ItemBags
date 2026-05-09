using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using 物品包.Items;
using 物品包.配置;

namespace 物品包.界面;



public class 类型_包窗口 : UIPanel {

    private const float 槽位大小 = 38f;
    private const float 槽位间距 = 4f;
    private const float 边缘间距 = 10f; 
    private const float 滚动条宽度 = 20f;

    public 类型_物品包 所属包;

    private readonly UIGrid 物品网格 = [];
    private readonly UIScrollbar 滚动条 = new();

    private Vector2 位置 = new( Main.screenWidth / 3, Main.screenHeight / 3 );
    private Vector2? 拖拽偏移;

    public bool 待更新布局属性 = true;

    public 类型_包窗口( 类型_物品包 包 ) {
        所属包 = 包;
        所属包.更新容量();
        for ( int 槽位索引 = 0; 槽位索引 < 所属包.物品矩阵.Length; 槽位索引++ ) {
            UIElement 界面槽位 = 所属包.界面槽位( 槽位索引 );
            界面槽位.Width.Set( 槽位大小, 0f );
            界面槽位.Height.Set( 槽位大小, 0f );
            物品网格.Add( 界面槽位 );
        }

        SetPadding( 0f );
        BackgroundColor = new Color( 73, 94, 171 ) * 0.8f;

        滚动条.SetView( 100f, 2160f );
        滚动条.Height.Set( -边缘间距 * 2, 1f );
        滚动条.Top.Set( 边缘间距, 0f );
        滚动条.Left.Set( -边缘间距 - 滚动条宽度, 1f );
        Append( 滚动条 );

        物品网格.SetScrollbar( 滚动条 );
        物品网格.ListPadding = 槽位间距;
        物品网格.Top.Set( 边缘间距, 0f );
        物品网格.Left.Set( 边缘间距, 0f );
        Append( 物品网格 );
    }

    public override void Update( GameTime 游戏时间 ) {
        Update_鼠标拦截();
        Update_鼠标拖拽();
        Update_布局属性();
        base.Update( 游戏时间 );
    }

    private void Update_鼠标拦截() { if ( IsMouseHovering ) { Main.LocalPlayer.mouseInterface = true; Main.blockMouse = true; } }

    private void Update_鼠标拖拽() {
        if ( !拖拽偏移.HasValue ) return;
        位置 = Main.MouseScreen - 拖拽偏移.Value;
        if ( !Main.mouseLeft ) 拖拽偏移 = null;
        待更新布局属性 = true;
    }

    private void Update_布局属性() {
        if ( !待更新布局属性 ) return;
        待更新布局属性 = false;

        类型_配置_物品包 配置 = 所属包.配置;

        float 网格宽度 = 槽位大小 * 配置.列数 + 槽位间距 * ( 配置.列数 - 1 );
        float 窗口宽度 = 边缘间距 + 网格宽度 + 槽位间距 + 滚动条宽度 + 边缘间距;
        float 窗口高度 = 边缘间距 * 2 + 槽位大小 * 配置.行数 + 槽位间距 * ( 配置.行数 - 1 );

        Left.Set( 位置.X, 0f ); Top.Set( 位置.Y, 0f ); Width.Set( 窗口宽度, 0f ); Height.Set( 窗口高度, 0f );
        物品网格.Width.Set( 网格宽度, 0f ); 物品网格.Height.Set( 窗口高度 - 边缘间距 * 2, 0f );

        Recalculate();
    }

    public override void LeftMouseDown( UIMouseEvent 鼠标事件 ) {
        if ( 鼠标事件.Target == this ) { 拖拽偏移 = 鼠标事件.MousePosition - new Vector2( Left.Pixels, Top.Pixels ); Parent.Append( this ); }
        base.LeftMouseDown( 鼠标事件 );
    }

}