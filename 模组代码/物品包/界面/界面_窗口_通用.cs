using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace 物品包.界面;



public abstract class 类型_窗口_通用 : UIPanel {
    protected abstract float 窗口宽度 { get;}
    protected abstract float 窗口高度 { get; }

    protected Vector2 默认位置 => new( ( Main.screenWidth - 窗口宽度 ) / 2f, ( Main.screenHeight - 窗口高度 ) / 2f );
    protected Vector2? 位置;
    protected Vector2? 拖拽偏移;
    protected bool 脏标记_界面布局 = true;

    protected virtual void 初始化() {
        Width.Set( 窗口宽度, 0f ); Height.Set( 窗口高度, 0f );
        Left.Set( ( Main.screenWidth - 窗口宽度 ) / 2f, 0f ); Top.Set( ( Main.screenHeight - 窗口高度 ) / 2f, 0f );
        BackgroundColor = new Color( 73, 94, 171 ) * 0.7f;
    }

    public override void Update( GameTime 游戏时间 ) {
        Update_鼠标拦截();
        Update_鼠标拖拽();
        if ( 脏标记_界面布局 ) { Update_布局属性(); Recalculate(); 脏标记_界面布局 = false; }
        base.Update( 游戏时间 );
    }

    protected void Update_鼠标拦截() { if ( IsMouseHovering ) { Main.LocalPlayer.mouseInterface = true; Main.blockMouse = true; } }

    protected void Update_鼠标拖拽() {
        if ( !拖拽偏移.HasValue ) return;
        位置 = Main.MouseScreen - 拖拽偏移.Value;
        if ( !Main.mouseLeft ) 拖拽偏移 = null;
        脏标记_界面布局 = true;
    }

    protected virtual void Update_布局属性() {
        位置 ??= 默认位置;
        Left.Set( 位置.Value.X, 0f ); Top.Set( 位置.Value.Y, 0f ); Width.Set( 窗口宽度, 0f ); Height.Set( 窗口高度, 0f );
    }

    public override void LeftMouseDown( UIMouseEvent 鼠标事件 ) {
        if ( 拖拽判断( 鼠标事件 ) ) {
            拖拽偏移 = 鼠标事件.MousePosition - new Vector2( Left.Pixels, Top.Pixels );
            Parent.Append( this );
        }
        base.LeftMouseDown( 鼠标事件 );
    }

    protected virtual bool 拖拽判断( UIMouseEvent 鼠标事件 ) => 鼠标事件.Target == this;
}