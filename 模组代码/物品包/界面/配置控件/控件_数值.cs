using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace 物品包.界面.配置控件;



public class 类型_控件_数值 : UIElement {
    private const float 按钮大小 = 40f;
    private const float 控件长度 = 200f;
    private const float 滑块长度 = 控件长度 - 按钮大小 - 按钮大小;

    private readonly Action<int> 设置器;
    private int 当前数值;
    private readonly int 最小值;
    private readonly int 最大值;

    private readonly UIElement 滑块区域;
    private bool 拖拽状态 = false;

    public 类型_控件_数值( Action<int> 设置器, int 初始数值, int 最小值, int 最大值 ) {
        this.设置器 = 设置器;
        this.当前数值 = 初始数值;
        this.最小值 = 最小值;
        this.最大值 = 最大值;

        Width.Set( 控件长度, 0f ); Height.Set( 30f, 0f );
        SetPadding( 0 );

        UITextPanel<string> 减号按钮 = new( "-" ) { TextHAlign = 0.5f, VAlign = 0.5f, BackgroundColor = new( 73, 94, 171 ) };
        减号按钮.Left.Set( 0f, 0f ); 减号按钮.Width.Set( 按钮大小, 0f ); 减号按钮.Height.Set( 按钮大小, 0f );
        减号按钮.OnLeftClick += ( evt, el ) => { 设置器( int.Max( --当前数值, 最小值 ) ); SoundEngine.PlaySound( SoundID.MenuTick ); };
        Append( 减号按钮 );

        UITextPanel<string> 加号按钮 = new( "+" ) { TextHAlign = 0.5f, VAlign = 0.5f, BackgroundColor = new( 73, 94, 171 ) };
        加号按钮.Left.Set( 控件长度 - 按钮大小, 0f ); 加号按钮.Width.Set( 按钮大小, 0f ); 加号按钮.Height.Set( 按钮大小, 0f );
        加号按钮.OnLeftClick += ( evt, el ) => { 设置器( int.Min( ++当前数值, 最大值 ) ); SoundEngine.PlaySound( SoundID.MenuTick ); };
        Append( 加号按钮 );

        滑块区域 = new() { VAlign = 0.5f };
        滑块区域.Left.Set( 按钮大小, 0f ); 滑块区域.Width.Set( 滑块长度, 0f ); 滑块区域.Height.Set( 按钮大小, 0f );
        滑块区域.OnLeftMouseDown += ( evt, el ) => 拖拽状态 = true;
        滑块区域.OnLeftMouseUp += ( evt, el ) => 拖拽状态 = false;
        Append( 滑块区域 );
    }

    private void 更新滑块拖拽() {
        CalculatedStyle 滑块 = 滑块区域.GetInnerDimensions();
        float 比例 = Utils.Clamp( ( Main.mouseX - 滑块.X ) / 滑块.Width, 0f, 1f );
        int 新值 = ( int ) Math.Round( 最小值 + 比例 * ( 最大值 - 最小值 ) );
        if ( 新值 != 当前数值 ) {
            当前数值 = 新值;
            设置器( 新值 );
            SoundEngine.PlaySound( SoundID.MenuTick );
        }
    }

    public override void Update( GameTime gameTime ) {
        base.Update( gameTime );
        if ( 拖拽状态 ) 更新滑块拖拽();
    }

    public override void Draw( SpriteBatch spriteBatch ) {
        base.Draw( spriteBatch );

        CalculatedStyle 区域 = 滑块区域.GetInnerDimensions();
        float 比例 = ( float ) ( 当前数值 - 最小值 ) / ( 最大值 - 最小值 );

        Rectangle 轨道矩形 = new( ( int ) 区域.X, ( int ) 区域.Y + 8, ( int ) 区域.Width, 8 );
        spriteBatch.Draw( TextureAssets.MagicPixel.Value, 轨道矩形, Color.Black * 0.5f );
        Rectangle 填充矩形 = new( ( int ) 区域.X, ( int ) 区域.Y + 8, ( int ) ( 区域.Width * 比例 ), 8 );
        spriteBatch.Draw( TextureAssets.MagicPixel.Value, 填充矩形, new Color( 73, 180, 73 ) );

        Rectangle 游标矩形 = new( ( int ) ( 区域.X + 区域.Width * 比例 - 4 ), ( int ) 区域.Y + 2, 8, 20 );
        spriteBatch.Draw( TextureAssets.MagicPixel.Value, 游标矩形, Color.White );

        if ( 滑块区域.IsMouseHovering || 拖拽状态 ) Utils.DrawBorderString( spriteBatch, 当前数值.ToString(), new( Main.mouseX, Main.mouseY - 20 ), Color.White, 0.8f );
    }
}