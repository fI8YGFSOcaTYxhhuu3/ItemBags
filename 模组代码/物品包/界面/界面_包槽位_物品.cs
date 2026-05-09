using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;
using 物品包.Items;

namespace 物品包.界面;



public class 类型_包槽位_物品( 类型_物品包 所属包, int 槽位索引 ) : UIElement {

    public int 索引 => 槽位索引;
    public Item 所属物品 => 所属包.物品矩阵[ 槽位索引 ];

    public override void Update( GameTime 游戏时间 ) {
        base.Update( 游戏时间 );
        Update_鼠标物品();
    }

    private void Update_鼠标物品() {
        if ( !IsMouseHovering || 所属物品.IsAir ) return;
        Main.HoverItem = 所属物品;
        Main.hoverItemName = Main.HoverItem.Name;
    }

    public override void LeftClick( UIMouseEvent 鼠标事件 ) {
        if ( Main.mouseItem.IsAir ) { LeftClick_交换物品(); return; }
        if ( !所属包.放入许可( Main.mouseItem ) ) { SoundEngine.PlaySound( SoundID.MenuClose ); return; }
        if ( Main.mouseItem.type == 所属物品.type ) { LeftClick_交换物品(); return; }
        if ( !所属包.玩家.条件符合( Main.mouseItem ) ) { SoundEngine.PlaySound( SoundID.MenuClose ); return; }
        LeftClick_交换物品();
    }

    internal virtual void LeftClick_交换物品() {
        (所属包.物品矩阵[ 槽位索引 ], Main.mouseItem) = (Main.mouseItem, 所属物品);
        SoundEngine.PlaySound( SoundID.Grab );
    }

    public override int CompareTo( object obj ) {
        if ( obj is 类型_包槽位_物品 其它槽位 ) return 索引.CompareTo( 其它槽位.索引 );
        return base.CompareTo( obj );
    }

    public override void Draw( SpriteBatch spriteBatch ) {
        Rectangle slotRect = GetInnerDimensions().ToRectangle();
        Texture2D backgroundTexture = TextureAssets.InventoryBack.Value;
        Color backgroundColor = Color.White * 0.7f;
        Main.spriteBatch.Draw( backgroundTexture, slotRect, backgroundColor );

        if ( 所属物品.IsAir ) return;

        Texture2D itemTexture = TextureAssets.Item[ 所属物品.type ].Value;

        Rectangle sourceRect = Main.itemAnimations[ 所属物品.type ]?.GetFrame( itemTexture ) ?? itemTexture.Frame();

        int 最大边长 = Math.Max( sourceRect.Width, sourceRect.Height );
        float scale = 最大边长 > 32 ? 32f / 最大边长 : 1f;

        Vector2 itemSize = new Vector2( sourceRect.Width * scale, sourceRect.Height * scale );
        Vector2 drawPosition = new Vector2(
            slotRect.X + ( slotRect.Width - itemSize.X ) * 0.5f,
            slotRect.Y + ( slotRect.Height - itemSize.Y ) * 0.5f
        );

        spriteBatch.Draw(
            itemTexture,
            drawPosition,
            sourceRect,
            所属物品.GetAlpha( Color.White ),
            0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f
        );

        if ( 所属物品.stack > 1 ) Utils.DrawBorderStringFourWay(
            spriteBatch,
            FontAssets.ItemStack.Value,
            所属物品.stack.ToString(),
            slotRect.X,
            slotRect.Y + slotRect.Height - 12,
            Color.White,
            Color.Black,
            Vector2.Zero,
            0.8f
        );
    }

}