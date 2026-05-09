using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;


public abstract class 类型_玩家_缓存包_额外缓存<类型_缓存包类型, 类型_额外缓存对象> : 类型_玩家_缓存包<类型_缓存包类型> where 类型_缓存包类型 : 类型_缓存包<类型_额外缓存对象> {

    public bool 脏标记_额外缓存 = true;
    public List<类型_额外缓存对象> 缓存列表_额外缓存 = [];

    public override void UpdateEquips() {
        if ( Player.whoAmI != Main.myPlayer ) return;
        if ( 脏标记_缓存包 ) 脏标记更新_缓存包();
        if ( 脏标记_额外缓存 ) 脏标记更新_额外缓存();
    }

    internal override void 脏标记更新_缓存包() { base.脏标记更新_缓存包(); 脏标记_额外缓存 = true; }

    internal virtual void 脏标记更新_额外缓存() { 脏标记_额外缓存 = false; 重构缓存_额外缓存(); }

    internal virtual void 重构缓存_额外缓存() { 缓存列表_额外缓存.Clear(); foreach ( var 缓存包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) { 缓存包.更新缓存(); 缓存列表_额外缓存.AddRange( 缓存包.缓存列表 ); } }

}