using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using 物品包.Items;

namespace 物品包.玩家;



public struct 结构_槽位指针 {
    public 接口_拾取包 包;
    public int 槽位;

    public Item 物品 => 包.物品矩阵[ 槽位 ];
}

// 特征成员
public partial interface 接口_玩家_拾取包 : 接口_玩家_缓存包<接口_拾取包> {
    List<结构_槽位指针>[] 物品统计 { get; set; }

    void 统计重构() {
        foreach ( var 列表 in 物品统计 ) 列表.Clear();
        foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) for ( int i = 0; i < 包.物品矩阵.Length; i++ ) if ( !包.物品矩阵[ i ].IsAir ) 物品统计[ 包.物品矩阵[ i ].type ].Add( new() { 包 = 包, 槽位 = i } );
    }
    void 统计移除( int 物品类型, 结构_槽位指针 指针 ) => 物品统计[ 物品类型 ].RemoveAll( 元素 => 元素.包 == 指针.包 && 元素.槽位 == 指针.槽位 );
    void 统计添加( int 物品类型, 结构_槽位指针 指针 ) => 物品统计[ 物品类型 ].Add( 指针 );

    bool 拾取( Item 物品 ) {
        int 初始数量 = 物品.stack;

        foreach ( var 指针 in CollectionsMarshal.AsSpan( 物品统计[ 物品.type ] ) ) {
            int 填充数量 = System.Math.Min( 指针.物品.maxStack - 指针.物品.stack, 物品.stack );
            指针.物品.stack += 填充数量; 物品.stack -= 填充数量;
            if ( 物品.stack == 0 ) { 物品.TurnToAir(); break; }
        }

        if ( 物品.stack > 0 ) foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) if ( 包.收纳许可( 物品 ) ) {
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
    public List<结构_槽位指针>[] 物品统计 { get; set; } = new List<结构_槽位指针>[ ItemLoader.ItemCount ];
}

// TML 重写成员
public partial class 类型_玩家_拾取包 {
    public override void Initialize() { for ( int i = 0; i < ItemLoader.ItemCount; i++ ) 物品统计[ i ] = []; }
    public override bool OnPickup( Item 物品 ) => !接口.拾取( 物品 ) && base.OnPickup( 物品 );
}