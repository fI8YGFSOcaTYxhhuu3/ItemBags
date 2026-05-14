using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public abstract partial class 类型_玩家_缓存包_额外缓存<类型_缓存包类型, 类型_额外缓存对象> : 类型_玩家_缓存包<类型_缓存包类型> where 类型_缓存包类型 : 类型_缓存包<类型_额外缓存对象> {
    public bool 脏标记_额外缓存 = true;
    public List<类型_额外缓存对象> 缓存列表_额外缓存 = [];

    protected virtual void 脏标记更新_额外缓存() { 脏标记_额外缓存 = false; 重构缓存_额外缓存(); if ( Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient ) 网络发送( -1, Player.whoAmI ); }
    protected virtual void 重构缓存_额外缓存() { 缓存列表_额外缓存.Clear(); foreach ( var 缓存包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) { 缓存包.更新缓存(); 缓存列表_额外缓存.AddRange( 缓存包.缓存列表 ); } }
}

// 特征重写函数
public abstract partial class 类型_玩家_缓存包_额外缓存<类型_缓存包类型, 类型_额外缓存对象> : 类型_玩家_缓存包<类型_缓存包类型> where 类型_缓存包类型 : 类型_缓存包<类型_额外缓存对象> {
    public override void 脏标记更新() { base.脏标记更新(); if ( 脏标记_额外缓存 ) 脏标记更新_额外缓存(); }
    protected override void 脏标记更新_缓存包() { base.脏标记更新_缓存包(); 脏标记_额外缓存 = true; }
}