using System;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace 物品包.界面.配置控件;



public partial class 类型_控件_选项 : UITextPanel<string> {
    private readonly Action<int> 设置器;
    private readonly string[] 选项列表;
    private int 当前索引;

    public 类型_控件_选项( Action<int> 设置器, string[] 选项列表, int 当前索引 ) : base( "" ) {
        this.设置器 = 设置器;
        this.选项列表 = 选项列表;
        this.当前索引 = 当前索引;

        TextHAlign = 0.5f;
        BackgroundColor = new( 73, 94, 171 );
        Width.Set( 90f, 0f ); Height.Set( 30f, 0f );
        SetText( 选项列表[ 当前索引 ] );
    }

    public override void LeftClick( UIMouseEvent evt ) { base.LeftClick( evt ); 选项右移(); }
    public override void RightClick( UIMouseEvent evt ) { base.RightClick( evt ); 选项左移(); }
}

// 辅助函数
public partial class 类型_控件_选项 {
    private void 选项右移() { if ( ++当前索引 >= 选项列表.Length ) 当前索引 = 0; 更新(); }
    private void 选项左移() { if ( --当前索引 < 0 ) 当前索引 = 选项列表.Length - 1; 更新(); }
    private void 更新() {
        设置器( 当前索引 % 选项列表.Length );
        SetText( 选项列表[ 当前索引 ] );
        SoundEngine.PlaySound( SoundID.MenuTick );
    }
}