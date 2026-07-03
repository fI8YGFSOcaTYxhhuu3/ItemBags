using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public partial interface 接口_玩家_拾取包 : 接口_玩家_缓存包<接口_拾取包> {
    bool 拾取( Item 物品 ) {
        int 初始数量 = 物品.stack;
        foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) if ( 包.收纳许可( 物品 ) ) {
            物品.stack = 包.自动存入( 物品 );
            if ( 物品.stack == 0 ) { 物品.TurnToAir(); break; }
        }

        if ( 物品.stack != 初始数量 ) Terraria.Audio.SoundEngine.PlaySound( Terraria.ID.SoundID.Grab );

        return 物品.IsAir;
    }
}

// 简单重写成员
public partial class 类型_玩家_拾取包 : 类型_玩家_缓存包<接口_拾取包>, 接口_玩家_拾取包 {
    public new 接口_玩家_拾取包 接口 => this;
    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.拾取包;
}

// TML 重写成员
public partial class 类型_玩家_拾取包 {
    public override bool OnPickup( Item 物品 ) => !接口.拾取( 物品 ) && base.OnPickup( 物品 );
}