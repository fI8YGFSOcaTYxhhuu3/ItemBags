using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace 物品包.Items;



public abstract class 类型_缓存包<类型_缓存数据> : 类型_物品包 {
    public List<类型_缓存数据> 缓存列表 = [];
    public bool 脏标记 = true;

    public void 更新缓存() {
        if ( !脏标记 ) return;
        脏标记 = false;
        清空缓存();
        建立缓存();
    }
    protected virtual void 清空缓存() { 缓存列表.Clear(); }
    protected abstract void 建立缓存();

    public override ModItem Clone( Item 克隆目标 ) {
        var 克隆实例 = ( 类型_缓存包<类型_缓存数据> ) base.Clone( 克隆目标 );
        克隆实例.脏标记 = true;
        克隆实例.缓存列表 = [];
        return 克隆实例;
    }

    public override void 更新容量() { base.更新容量(); 脏标记 = true; }
    public override void LoadData( TagCompound 存档标签 ) { base.LoadData( 存档标签 ); 脏标记 = true; }
    public override void NetReceive( BinaryReader 网络流 ) { base.NetReceive( 网络流 ); 脏标记 = true; }
}