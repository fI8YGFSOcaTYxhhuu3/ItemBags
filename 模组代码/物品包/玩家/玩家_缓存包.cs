using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



public abstract class 类型_玩家_缓存包<类型_缓存包类型> : 类型_玩家_物品包 where 类型_缓存包类型 : 类型_物品包 {
    private Item 上一个鼠标物品 = new();

    public bool 脏标记_缓存包 = true;
    public List<类型_缓存包类型> 缓存列表_缓存包 = [];

    public override void UpdateEquips() {
        if ( Player.whoAmI != Main.myPlayer ) return;
        if ( 脏标记_缓存包 ) 脏标记更新_缓存包();
    }

    internal virtual void 脏标记更新_缓存包() { 脏标记_缓存包 = false; 重构缓存_物品包(); }

    internal void 重构缓存_缓存包() { 缓存列表_缓存包.Clear(); foreach ( var 玩家容器 in 玩家容器列表 ) 扫描容器( 玩家容器 ); }

    private void 扫描容器( Item[] 容器 ) {
        foreach ( var 物品 in 容器 ) {
            if ( 物品.IsAir ) continue;
            if ( 物品.ModItem is 类型_缓存包类型 目标包 ) 缓存列表_缓存包.Add( 目标包 );
            if ( 物品.ModItem is 类型_嵌套包 嵌套包 ) 扫描容器( 嵌套包.物品矩阵 );
        }
    }

    public virtual bool 存在重复( Item 查询物品 ) {
        foreach ( var 包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) foreach ( var 物品 in 包.物品矩阵 ) if ( 物品.type == 查询物品.type ) return true;
        return false;
    }

    public override void OnEnterWorld() { 脏标记_缓存包 = true; }

    public override bool OnPickup( Item 拾取物品 ) {
        if ( 拾取物品.ModItem is 类型_缓存包类型 ) 脏标记_缓存包 = true;
        return base.OnPickup( 拾取物品 );
    }

    public override bool ShiftClickSlot( Item[] 容器, int 容器索引, int 物品索引 ) {
        if ( 容器[ 物品索引 ].ModItem is 类型_缓存包类型 ) 脏标记_缓存包 = true;
        return false;
    }

    public override void PostUpdate() {
        if ( Player.whoAmI != Main.myPlayer ) return;

        bool 当前是包 = Main.mouseItem.ModItem is 类型_缓存包类型;
        bool 上次是包 = 上一个鼠标物品.ModItem is 类型_缓存包类型;
        if ( ( 当前是包 || 上次是包 ) && Main.mouseItem != 上一个鼠标物品 ) 脏标记_缓存包 = true;
        上一个鼠标物品 = Main.mouseItem;
    }

}