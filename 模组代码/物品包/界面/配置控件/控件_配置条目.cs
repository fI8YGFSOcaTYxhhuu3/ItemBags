using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace 物品包.界面.配置控件;



public class 控件_配置条目 : UIElement {
    private readonly string 提示文本;
    private readonly UIText 标签;

    public 控件_配置条目( string 标签文本, string 提示文本, UIElement 交互控件 ) {
        this.提示文本 = 提示文本;

        Width.Set( 0f, 1f ); Height.Set( 40f, 0f );

        标签 = new( 标签文本 ) { VAlign = 0.5f };
        标签.Left.Set( 10f, 0f );
        Append( 标签 );

        交互控件.VAlign = 0.5f; 交互控件.HAlign = 1f;
        交互控件.Left.Set( -10f, 0f );
        Append( 交互控件 );
    }

    public override void Draw( SpriteBatch spriteBatch ) {
        base.Draw( spriteBatch );
        if ( 标签.IsMouseHovering && !string.IsNullOrEmpty( 提示文本 ) ) Main.instance.MouseText( 提示文本 );
    }
}