using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public abstract partial class 类型_玩家_同步缓存包<类型_网络同步包, 结构_同步数据> : 类型_玩家_缓存包<类型_网络同步包> where 类型_网络同步包 : 接口_缓存包, 接口_网络同步包<结构_同步数据> where 结构_同步数据 : struct {
    public bool 脏标记_同步缓存 = true;
    public List<结构_同步数据> 缓存列表_同步缓存;

    protected virtual void 脏标记更新_同步缓存() { 脏标记_同步缓存 = false; 重构缓存_同步缓存(); if ( Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient ) 网络发送( -1, Player.whoAmI ); }
    protected virtual void 重构缓存_同步缓存() { 缓存列表_同步缓存.Clear(); foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) { 包.更新缓存(); 缓存列表_同步缓存.Add( 包.同步数据 ); } }
}

// 特征重写函数
public abstract partial class 类型_玩家_同步缓存包<类型_网络同步包, 结构_同步数据> {
    public override void 脏标记更新() { base.脏标记更新(); if ( 脏标记_同步缓存 ) 脏标记更新_同步缓存(); }
    protected override void 脏标记更新_缓存包() { base.脏标记更新_缓存包(); 脏标记_同步缓存 = true; }
}