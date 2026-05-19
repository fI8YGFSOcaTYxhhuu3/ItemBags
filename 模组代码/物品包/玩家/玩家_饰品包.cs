using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;

namespace 物品包.玩家;



// 简单重写成员
public partial class 类型_玩家_饰品包 : 类型_玩家_同步缓存包<类型_饰品包, 结构_同步数据_饰品包> {
    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.饰品包;
}

// 特征重写函数
public partial class 类型_玩家_饰品包 {
    public override void UpdateEquips() { base.UpdateEquips(); 应用缓存列表加成(); }
    public override void UpdateVisibleAccessories() { base.UpdateVisibleAccessories(); 应用缓存列表视效(); }
}

// 网络同步
public partial class 类型_玩家_饰品包 {
    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.饰品包 );
        网络数据.Write( ( byte ) Player.whoAmI );
        网络数据.Write( 缓存列表_同步缓存.Count );
        foreach ( var 数据 in 缓存列表_同步缓存 ) {
            网络数据.Write( 数据.功能配置.应用前缀效果 );
            网络数据.Write( 数据.功能配置.应用视觉效果 );
            网络数据.Write( 数据.数据列表.Count ); foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) ItemIO.Send( 饰品, 网络数据, writeStack: false, writeFavorite: false );
        }
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }
    public override void 网络接收( BinaryReader 网络流 ) {
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

// 辅助函数
public partial class 类型_玩家_饰品包 {
    private void 应用缓存列表加成() {
        foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) {
            Player.ApplyEquipFunctional( 饰品, true );
            Player.GrantArmorBenefits( 饰品 );
            if ( 数据.功能配置.应用视觉效果 ) Player.ApplyEquipVanity( 饰品 );
            if ( 数据.功能配置.应用前缀效果 ) Player.GrantPrefixBenefits( 饰品 );
            饰品.ModItem?.UpdateEquip( Player );
        }
    }
    private void 应用缓存列表视效() { foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) if ( 数据.功能配置.应用视觉效果 ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) Player.UpdateVisibleAccessory( 13, 饰品, true ); }
}