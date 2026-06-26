using System.Collections.Generic;
using System.Runtime.InteropServices;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public partial interface 接口_玩家_缓存包_网络同步<类型_网络同步包, 结构_同步数据> : 接口_玩家_缓存包<类型_网络同步包>, 接口_玩家_网络同步_缓存<List<结构_同步数据>> where 类型_网络同步包 : 接口_网络同步包<结构_同步数据> where 结构_同步数据 : struct {
    void 接口_玩家_网络同步_缓存.重构缓存_同步缓存() { 缓存列表_同步缓存.Clear(); foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) 缓存列表_同步缓存.Add( 包.同步数据() ); }
}

// 特征重写函数
public partial interface 接口_玩家_缓存包_网络同步<类型_网络同步包, 结构_同步数据> {
    void 接口_玩家_缓存包.脏标记更新() { 接口_玩家_缓存包_脏标记更新(); if ( 脏标记_同步缓存 ) 脏标记更新_同步缓存(); }
    void 接口_玩家_缓存包.脏标记更新_缓存包() { 接口_玩家_缓存包_脏标记更新_缓存包(); 脏标记_同步缓存 = true; }
}