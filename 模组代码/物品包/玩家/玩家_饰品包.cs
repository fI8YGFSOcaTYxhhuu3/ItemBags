using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public partial interface 接口_玩家_饰品包 : 接口_玩家_缓存包_网络同步<接口_饰品包, 结构_同步数据_饰品包> {
    void 应用缓存列表加成() {
        foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) {
            玩家.ApplyEquipFunctional( 饰品, true );
            玩家.GrantArmorBenefits( 饰品 );
            if ( 数据.功能配置.应用视觉效果 ) 玩家.ApplyEquipVanity( 饰品 );
            if ( 数据.功能配置.应用前缀效果 ) 玩家.GrantPrefixBenefits( 饰品 );
            饰品.ModItem?.UpdateEquip( 玩家 );
        }
    }
    void 应用缓存列表视效() { foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) if ( 数据.功能配置.应用视觉效果 ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) 玩家.UpdateVisibleAccessory( 13, 饰品, true ); }
}

// 网络同步
public partial interface 接口_玩家_饰品包 {
    void 接口_玩家_网络同步.网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = 模组玩家.Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.饰品包 );
        网络数据.Write( ( byte ) 玩家.whoAmI );
        网络数据.Write( 缓存列表_同步缓存.Count );
        foreach ( var 数据 in 缓存列表_同步缓存 ) {
            网络数据.Write( 数据.功能配置.应用前缀效果 );
            网络数据.Write( 数据.功能配置.应用视觉效果 );
            网络数据.Write( 数据.数据列表.Count ); foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) ItemIO.Send( 饰品, 网络数据, writeStack: false, writeFavorite: false );
        }
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }
    void 接口_玩家_网络同步.网络接收( BinaryReader 网络流 ) {
        缓存列表_同步缓存.Clear();
        int 包数量 = 网络流.ReadInt32();
        for ( int i = 0; i < 包数量; i++ ) {
            结构_同步数据_饰品包 数据 = new() {
                功能配置 = new() { 应用前缀效果 = 网络流.ReadBoolean(), 应用视觉效果 = 网络流.ReadBoolean() },
                数据列表 = []
            };

            int 饰品数量 = 网络流.ReadInt32();
            for ( int j = 0; j < 饰品数量; j++ ) 数据.数据列表.Add( ItemIO.Receive( 网络流, readStack: false, readFavorite: false ) );
            缓存列表_同步缓存.Add( 数据 );
        }
    }
}

// 接口实现
public partial class 类型_玩家_饰品包 : 类型_玩家_缓存包<接口_饰品包>, 接口_玩家_饰品包 {
    public new 接口_玩家_饰品包 接口 => this;
    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.饰品包;
    public bool 脏标记_同步缓存 { get; set; } = true;
    public List<结构_同步数据_饰品包> 缓存列表_同步缓存 { get; set; } = [];
}

// 特征重写函数
public partial class 类型_玩家_饰品包 {
    public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) { 接口.网络发送( toWho, fromWho ); }
    public override void UpdateEquips() { base.UpdateEquips(); 接口.应用缓存列表加成(); }
    public override void UpdateVisibleAccessories() { base.UpdateVisibleAccessories(); 接口.应用缓存列表视效(); }
}