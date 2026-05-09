using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



public abstract class 类型_玩家_缓存包<类型_缓存包类型> : 类型_玩家_脏标记包<类型_缓存包类型> where 类型_缓存包类型 : 类型_物品包 {
    
    public List<类型_缓存包类型> 缓存列表 = [];

    public override void UpdateEquips() {
        if ( !脏标记 ) return;
        重构缓存();
        脏标记 = false;
    }

    internal void 重构缓存() { 缓存列表.Clear(); foreach ( var 玩家容器 in 玩家容器列表 ) 扫描容器( 玩家容器 ); }

    internal void 扫描容器( Item[] 容器 ) {
        foreach ( var 物品 in 容器 ) {
            if ( 物品.IsAir ) continue;
            if ( 物品.ModItem is 类型_缓存包类型 目标包 ) 缓存列表.Add( 目标包 );
            if ( 物品.ModItem is 类型_嵌套包 嵌套包 ) 扫描容器( 嵌套包.物品矩阵 );
        }
    }

    public virtual bool 存在重复( Item 查询物品 ) {
        foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表 ) ) foreach ( var 物品 in 包.物品矩阵 ) if ( 物品.type == 查询物品.type ) return true;
        return false;
    }

}