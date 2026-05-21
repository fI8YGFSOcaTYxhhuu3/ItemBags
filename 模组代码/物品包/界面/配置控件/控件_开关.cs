using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace 物品包.界面.配置控件;



public class 类型_控件_开关 : UITextPanel<string> {
    private bool 当前状态;
    private readonly Action<bool> 设置器;

    public 类型_控件_开关( bool 当前状态, Action<bool> 设置器 ) : base( "" ) {
        this.当前状态 = 当前状态;
        this.设置器 = 设置器;
        TextHAlign = 0.5f; Width.Set( 60f, 0f ); Height.Set( 30f, 0f );
    }

    public override void LeftClick( UIMouseEvent evt ) {
        base.LeftClick( evt );
        当前状态 = !当前状态;
        设置器( 当前状态 );
        SoundEngine.PlaySound( SoundID.MenuTick );
    }

    public override void Update( GameTime gameTime ) {
        base.Update( gameTime );
        SetText( 当前状态 ? "ON" : "OFF" );
        BackgroundColor = 当前状态 ? new( 73, 180, 73 ) : new( 180, 73, 73 );
    }
}