using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using 物品包.Items;
using 物品包.玩家;

namespace 物品包.界面;



// 特征成员
public partial class 类型_包槽位_物品( 接口_物品包 所属包, int 槽位索引 ) : UIElement {
    public int 索引 => 槽位索引;
    public Item 所属物品 => 所属包.物品矩阵[ 槽位索引 ];

    protected virtual void LeftClick_交换物品() {
        (所属包.物品矩阵[ 槽位索引 ], Main.mouseItem) = (Main.mouseItem, 所属物品);
        SoundEngine.PlaySound( SoundID.Grab );
    }
}

// 常规 TML 成员
public partial class 类型_包槽位_物品 : UIElement {
    public override void Update( GameTime 游戏时间 ) {
        base.Update( 游戏时间 );
        if ( !IsMouseHovering || 所属物品.IsAir ) return;
        Main.HoverItem = 所属物品;
        Main.hoverItemName = Main.HoverItem.Name;
    }
    public override void LeftClick( UIMouseEvent 鼠标事件 ) {
        var 鼠标物品 = Main.mouseItem;
        if ( 鼠标物品.IsAir ) LeftClick_取出物品();
        else if ( 鼠标物品.type == 所属物品.type ) {
            if ( ItemLoader.CanStack( 所属物品, 鼠标物品 ) ) LeftClick_叠加物品();
            else if ( 所属包.置换许可( 鼠标物品 ) ) LeftClick_交换物品();
            else SoundEngine.PlaySound( SoundID.MenuClose );
        }
        else if ( !所属包.放入许可( 鼠标物品 ) ) SoundEngine.PlaySound( SoundID.MenuClose );
        else LeftClick_交换物品();
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

        Main.instance.LoadItem( 所属物品.type );
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

// 辅助函数
public partial class 类型_包槽位_物品 : UIElement {
    private void LeftClick_取出物品() { if ( !所属物品.IsAir ) LeftClick_交换物品(); }
    private void LeftClick_叠加物品() {
        int 剩余容量 = 所属物品.maxStack - 所属物品.stack;
        int 填充数量 = Math.Min( 剩余容量, Main.mouseItem.stack );
        所属物品.stack += 填充数量;
        Main.mouseItem.stack -= 填充数量;
        if ( Main.mouseItem.stack == 0 ) Main.mouseItem.TurnToAir();
        SoundEngine.PlaySound( SoundID.Grab );
    }
}