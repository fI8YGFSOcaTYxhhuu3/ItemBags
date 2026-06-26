using System.IO;
using Terraria;

namespace 物品包.玩家;



public interface 接口_玩家_网络同步 : 接口_玩家 {
    void 网络发送( int 接收玩家ID, int 发送玩家ID );
    void 网络接收( BinaryReader 网络流 );
}

public interface 接口_玩家_网络同步_缓存 : 接口_玩家_网络同步 {
    bool 脏标记_同步缓存 { get; set; }

    void 脏标记更新_同步缓存();
    void 重构缓存_同步缓存();
}

public interface 接口_玩家_网络同步_缓存<类型_同步缓存> : 接口_玩家_网络同步_缓存 {
    类型_同步缓存 缓存列表_同步缓存 { get; set; }

    void 接口_玩家_网络同步_缓存.脏标记更新_同步缓存() => 接口_玩家_网络同步_缓存_类型_同步缓存_脏标记更新_同步缓存();
    void 接口_玩家_网络同步_缓存_类型_同步缓存_脏标记更新_同步缓存() { 脏标记_同步缓存 = false; 重构缓存_同步缓存(); if ( Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient ) 网络发送( -1, 玩家.whoAmI ); }
}